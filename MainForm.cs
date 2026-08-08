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
        statusLabel.Text = result.FailureCount == 0
            ? $"{result.SuccessCount} {FileLabel(result.SuccessCount)} renamed"
            : $"{result.SuccessCount} {FileLabel(result.SuccessCount)} renamed, {result.FailureCount} {FileLabel(result.FailureCount)} failed";

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
        int successCount = 0;
        int failureCount = 0;
        List<string> conflictingFileNames = [];

        foreach (string filePath in filePaths)
        {
            if (!string.Equals(Path.GetExtension(filePath), ".jfif", StringComparison.OrdinalIgnoreCase))
            {
                failureCount++;
                continue;
            }

            string targetPath = Path.ChangeExtension(filePath, ".jpg");

            if (File.Exists(targetPath))
            {
                failureCount++;
                conflictingFileNames.Add(Path.GetFileName(targetPath));
                continue;
            }

            try
            {
                File.Move(filePath, targetPath);
                successCount++;
            }
            catch (IOException)
            {
                failureCount++;

                if (File.Exists(targetPath))
                {
                    conflictingFileNames.Add(Path.GetFileName(targetPath));
                }
            }
            catch (UnauthorizedAccessException)
            {
                failureCount++;
            }
        }

        return new RenameResult(successCount, failureCount, conflictingFileNames);
    }

    private static string FileLabel(int count) => count == 1 ? "file" : "files";

    private sealed record RenameResult(
        int SuccessCount,
        int FailureCount,
        IReadOnlyList<string> ConflictingFileNames);
}
