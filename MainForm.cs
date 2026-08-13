namespace JFIF2JPG;

public partial class MainForm : Form
{
    private readonly Color defaultContentBackColor;
    private readonly Color defaultDropLabelForeColor;
    private readonly string defaultDropLabelText;
    private readonly System.Windows.Forms.Timer dragLeaveResetTimer;
    private RenameResult? latestRenameResult;
    private bool isDragVisualStateActive;

    public MainForm()
    {
        InitializeComponent();
        defaultContentBackColor = contentLayout.BackColor;
        defaultDropLabelForeColor = dropLabel.ForeColor;
        defaultDropLabelText = dropLabel.Text;
        dragLeaveResetTimer = new System.Windows.Forms.Timer { Interval = 100 };
        dragLeaveResetTimer.Tick += DragLeaveResetTimer_Tick;

        browseButton.Click += BrowseButton_Click;
        failureDetailsButton.Click += FailureDetailsButton_Click;
        DragEnter += MainForm_DragEnter;
        DragOver += MainForm_DragOver;
        DragLeave += MainForm_DragLeave;
        DragDrop += MainForm_DragDrop;
        ConfigureDropTarget(contentLayout);
        ConfigureDropTarget(dropLabel);
        ConfigureDropTarget(orLabel);
        ConfigureDropTarget(browseButton);
        ConfigureDropTarget(resultPanel);
        ConfigureDropTarget(resultLayout);
        ConfigureDropTarget(resultSummaryLabel);
        ConfigureDropTarget(failureDetailsButton);
        ConfigureDropTarget(statusStrip);
        Disposed += (_, _) => dragLeaveResetTimer.Dispose();
    }

    private void BrowseButton_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "JFIF files (*.jfif)|*.jfif|All files (*.*)|*.*",
            Multiselect = true,
            Title = "Select JFIF Files"
        };

        if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        ProcessJfifFiles(openFileDialog.FileNames);
    }

    private void MainForm_DragEnter(object? sender, DragEventArgs e)
    {
        UpdateDragVisualState(e);
    }

    private void MainForm_DragOver(object? sender, DragEventArgs e)
    {
        UpdateDragVisualState(e);
    }

    private void MainForm_DragLeave(object? sender, EventArgs e)
    {
        dragLeaveResetTimer.Stop();
        dragLeaveResetTimer.Start();
    }

    private void MainForm_DragDrop(object? sender, DragEventArgs e)
    {
        dragLeaveResetTimer.Stop();
        SetDragVisualState(false);
        string[] droppedJfifFilePaths = GetDroppedJfifFilePaths(e.Data);

        if (droppedJfifFilePaths.Length > 0)
        {
            ProcessJfifFiles(droppedJfifFilePaths);
        }
    }

    private void ConfigureDropTarget(Control control)
    {
        control.AllowDrop = true;
        control.DragEnter += MainForm_DragEnter;
        control.DragOver += MainForm_DragOver;
        control.DragLeave += MainForm_DragLeave;
        control.DragDrop += MainForm_DragDrop;
    }

    private void UpdateDragVisualState(DragEventArgs e)
    {
        dragLeaveResetTimer.Stop();
        bool hasJfifFiles = GetDroppedJfifFilePaths(e.Data).Length > 0;
        e.Effect = hasJfifFiles
            ? DragDropEffects.Copy
            : DragDropEffects.None;
        SetDragVisualState(hasJfifFiles);
    }

    private void DragLeaveResetTimer_Tick(object? sender, EventArgs e)
    {
        dragLeaveResetTimer.Stop();
        SetDragVisualState(false);
    }

    private void SetDragVisualState(bool canDrop)
    {
        if (isDragVisualStateActive == canDrop)
        {
            return;
        }

        isDragVisualStateActive = canDrop;
        contentLayout.BackColor = canDrop
            ? Color.FromArgb(240, 247, 255)
            : defaultContentBackColor;
        dropLabel.ForeColor = canDrop
            ? Color.FromArgb(0, 84, 153)
            : defaultDropLabelForeColor;
        dropLabel.Text = canDrop
            ? "Release to rename .jfif files"
            : defaultDropLabelText;
    }

    private void ProcessJfifFiles(IEnumerable<string> filePaths)
    {
        RenameResult result = RenameJfifFiles(filePaths);
        latestRenameResult = result;

        string statusMessage = CreateStatusMessage(result);
        statusLabel.Text = statusMessage;
        resultSummaryLabel.Text = statusMessage;
        resultSummaryLabel.ForeColor = GetResultSummaryColor(result);
        failureDetailsButton.Visible = result.FailedCount > 0;

        if (result.ConflictingFileNames.Count > 0)
        {
            string conflictingFiles = string.Join(Environment.NewLine, result.ConflictingFileNames);
            MessageBox.Show(
                this,
                $"The following JPG files already exist and were not overwritten:{Environment.NewLine}{Environment.NewLine}{conflictingFiles}",
                "Filename Conflicts",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void FailureDetailsButton_Click(object? sender, EventArgs e)
    {
        if (latestRenameResult is null || latestRenameResult.FailedFileResults.Count == 0)
        {
            return;
        }

        string failureDetails = string.Join(
            $"{Environment.NewLine}{Environment.NewLine}",
            latestRenameResult.FailedFileResults.Select(FormatFailureDetails));

        MessageBox.Show(
            this,
            failureDetails,
            "Rename Failure Details",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private static string FormatFailureDetails(FileRenameResult result)
    {
        string reason = string.IsNullOrWhiteSpace(result.FailureMessage)
            ? "The file could not be renamed."
            : result.FailureMessage;

        return $"File: {result.SourcePath}{Environment.NewLine}Reason: {reason}";
    }

    private static Color GetResultSummaryColor(RenameResult result)
    {
        if (result.FailedCount > 0)
        {
            return Color.Firebrick;
        }

        return result.SkippedCount > 0
            ? Color.DarkGoldenrod
            : Color.FromArgb(0, 102, 51);
    }

    private static string CreateStatusMessage(RenameResult result)
    {
        List<string> statusParts =
        [
            $"{result.RenamedCount} {FileLabel(result.RenamedCount)} renamed successfully"
        ];

        if (result.SkippedCount > 0)
        {
            statusParts.Add($"{result.SkippedCount} {FileLabel(result.SkippedCount)} skipped");
        }

        if (result.FailedCount > 0)
        {
            statusParts.Add($"{result.FailedCount} {FileLabel(result.FailedCount)} failed");
        }

        return string.Join(", ", statusParts);
    }

    private static string[] GetDroppedJfifFilePaths(IDataObject? data)
    {
        if (data?.GetData(DataFormats.FileDrop) is not string[] filePaths)
        {
            return [];
        }

        return filePaths
            .Where(filePath => File.Exists(filePath))
            .Where(filePath => string.Equals(Path.GetExtension(filePath), ".jfif", StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }

    private static RenameResult RenameJfifFiles(IEnumerable<string> filePaths)
    {
        List<FileRenameResult> fileResults = [];

        foreach (string filePath in filePaths)
        {
            fileResults.Add(RenameJfifFile(filePath));
        }

        return new RenameResult(fileResults);
    }

    private static FileRenameResult RenameJfifFile(string filePath)
    {
        if (!string.Equals(Path.GetExtension(filePath), ".jfif", StringComparison.OrdinalIgnoreCase))
        {
            return new FileRenameResult(
                filePath,
                null,
                FileRenameOutcome.Failed,
                "Only .jfif files can be renamed.");
        }

        string targetPath = Path.ChangeExtension(filePath, ".jpg");

        if (File.Exists(targetPath))
        {
            return new FileRenameResult(filePath, targetPath, FileRenameOutcome.TargetExists);
        }

        try
        {
            File.Move(filePath, targetPath);
            return new FileRenameResult(filePath, targetPath, FileRenameOutcome.Renamed);
        }
        catch (IOException exception)
        {
            return File.Exists(targetPath)
                ? new FileRenameResult(filePath, targetPath, FileRenameOutcome.TargetExists)
                : new FileRenameResult(filePath, targetPath, FileRenameOutcome.Failed, exception.Message);
        }
        catch (UnauthorizedAccessException exception)
        {
            return new FileRenameResult(filePath, targetPath, FileRenameOutcome.Failed, exception.Message);
        }
    }

    private static string FileLabel(int count) => count == 1 ? "file" : "files";

    private enum FileRenameOutcome
    {
        Renamed,
        TargetExists,
        Failed
    }

    private sealed record FileRenameResult(
        string SourcePath,
        string? TargetPath,
        FileRenameOutcome Outcome,
        string? FailureMessage = null);

    private sealed record RenameResult(IReadOnlyList<FileRenameResult> FileResults)
    {
        public int RenamedCount => FileResults.Count(result => result.Outcome == FileRenameOutcome.Renamed);

        public int SkippedCount => FileResults.Count(result => result.Outcome == FileRenameOutcome.TargetExists);

        public int FailedCount => FileResults.Count(result => result.Outcome == FileRenameOutcome.Failed);

        public IReadOnlyList<FileRenameResult> FailedFileResults => FileResults
            .Where(result => result.Outcome == FileRenameOutcome.Failed)
            .ToArray();

        public IReadOnlyList<string> ConflictingFileNames => FileResults
            .Where(result => result.Outcome == FileRenameOutcome.TargetExists)
            .Select(result => Path.GetFileName(result.TargetPath!))
            .ToArray();
    }
}
