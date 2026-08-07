namespace JFIF2JPG;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null!;

    private TableLayoutPanel contentLayout = null!;
    private Label dropLabel = null!;
    private Label orLabel = null!;
    private Button browseButton = null!;
    private StatusStrip statusStrip = null!;
    private ToolStripStatusLabel statusLabel = null!;

    /// <summary>
    /// Cleans up resources in use by the form.
    /// </summary>
    /// <param name="disposing">true to dispose managed resources; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        contentLayout = new TableLayoutPanel();
        dropLabel = new Label();
        orLabel = new Label();
        browseButton = new Button();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        contentLayout.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // contentLayout
        // 
        contentLayout.ColumnCount = 1;
        contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        contentLayout.Controls.Add(dropLabel, 0, 1);
        contentLayout.Controls.Add(orLabel, 0, 2);
        contentLayout.Controls.Add(browseButton, 0, 3);
        contentLayout.Dock = DockStyle.Fill;
        contentLayout.Location = new Point(0, 0);
        contentLayout.Name = "contentLayout";
        contentLayout.Padding = new Padding(24);
        contentLayout.RowCount = 5;
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        contentLayout.Size = new Size(884, 512);
        contentLayout.TabIndex = 0;
        // 
        // dropLabel
        // 
        dropLabel.AutoSize = true;
        dropLabel.Dock = DockStyle.Fill;
        dropLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
        dropLabel.Location = new Point(27, 214);
        dropLabel.Name = "dropLabel";
        dropLabel.Size = new Size(830, 30);
        dropLabel.TabIndex = 0;
        dropLabel.Text = "Drag && Drop .jfif files here";
        dropLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // orLabel
        // 
        orLabel.AutoSize = true;
        orLabel.Dock = DockStyle.Fill;
        orLabel.ForeColor = SystemColors.GrayText;
        orLabel.Location = new Point(27, 244);
        orLabel.Name = "orLabel";
        orLabel.Size = new Size(830, 15);
        orLabel.TabIndex = 1;
        orLabel.Text = "or";
        orLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // browseButton
        // 
        browseButton.Anchor = AnchorStyles.Top;
        browseButton.AutoSize = true;
        browseButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        browseButton.Location = new Point(382, 267);
        browseButton.Margin = new Padding(3, 8, 3, 3);
        browseButton.MinimumSize = new Size(120, 36);
        browseButton.Name = "browseButton";
        browseButton.Padding = new Padding(12, 0, 12, 0);
        browseButton.Size = new Size(120, 36);
        browseButton.TabIndex = 2;
        browseButton.Text = "Select Files";
        browseButton.UseVisualStyleBackColor = true;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
        statusStrip.Location = new Point(0, 512);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(884, 22);
        statusStrip.TabIndex = 1;
        statusStrip.Text = "statusStrip";
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(39, 17);
        statusLabel.Text = "Ready";
        // 
        // MainForm
        // 
        AllowDrop = true;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(884, 534);
        Controls.Add(contentLayout);
        Controls.Add(statusStrip);
        MinimumSize = new Size(800, 500);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "JFIF2JPG";
        contentLayout.ResumeLayout(false);
        contentLayout.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
