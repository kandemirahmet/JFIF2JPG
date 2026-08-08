namespace JFIF2JPG;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        browseButton.Click += BrowseButton_Click;
    }

    private void BrowseButton_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Filter = "JFIF files (*.jfif)|*.jfif|All files (*.*)|*.*",
            Multiselect = true,
            Title = "Select JFIF Files to Convert"
        };

        if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        RenameResult result = RenameJfifFiles(openFileDialog.FileNames);
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
