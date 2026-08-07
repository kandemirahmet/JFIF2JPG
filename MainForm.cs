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
            Title = "Select JFIF Files"
        };

        if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        int selectedFileCount = openFileDialog.FileNames.Length;
        statusLabel.Text = $"{selectedFileCount} {(selectedFileCount == 1 ? "file" : "files")} selected";
    }
}
