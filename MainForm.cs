namespace JFIF2JPG;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        browseButton.Click += BrowseButton_Click;
        DragEnter += MainForm_DragEnter;
        DragDrop += MainForm_DragDrop;
        ConfigureDropTarget(contentLayout);
        ConfigureDropTarget(dropLabel);
        ConfigureDropTarget(orLabel);
        ConfigureDropTarget(browseButton);
        ConfigureDropTarget(statusStrip);
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
        e.Effect = GetDroppedJfifFilePaths(e.Data).Length > 0
            ? DragDropEffects.Copy
            : DragDropEffects.None;
    }

    private void MainForm_DragDrop(object? sender, DragEventArgs e)
    {
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
        control.DragDrop += MainForm_DragDrop;
    }

    private void ProcessJfifFiles(IEnumerable<string> filePaths)
    {
        RenameResult result = RenameJfifFiles(filePaths);
        statusLabel.Text = CreateStatusMessage(result);

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

    private static string CreateStatusMessage(RenameResult result)
    {
        List<string> statusParts =
        [
            $"{result.RenamedCount} {FileLabel(result.RenamedCount)} renamed"
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

        public IReadOnlyList<string> ConflictingFileNames => FileResults
            .Where(result => result.Outcome == FileRenameOutcome.TargetExists)
            .Select(result => Path.GetFileName(result.TargetPath!))
            .ToArray();
    }
}
