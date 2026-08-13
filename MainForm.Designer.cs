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
    private Panel resultPanel = null!;
    private TableLayoutPanel resultLayout = null!;
    private Label resultSummaryLabel = null!;
    private Button failureDetailsButton = null!;
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
        resultPanel = new Panel();
        resultLayout = new TableLayoutPanel();
        resultSummaryLabel = new Label();
        failureDetailsButton = new Button();
        statusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        contentLayout.SuspendLayout();
        resultPanel.SuspendLayout();
        resultLayout.SuspendLayout();
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
        contentLayout.Controls.Add(resultPanel, 0, 4);
        contentLayout.Dock = DockStyle.Fill;
        contentLayout.Location = new Point(0, 0);
        contentLayout.Name = "contentLayout";
        contentLayout.Padding = new Padding(24);
        contentLayout.RowCount = 6;
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        contentLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
        // resultPanel
        // 
        resultPanel.BackColor = SystemColors.ControlLightLight;
        resultPanel.BorderStyle = BorderStyle.FixedSingle;
        resultPanel.Controls.Add(resultLayout);
        resultPanel.Dock = DockStyle.Fill;
        resultPanel.Location = new Point(27, 322);
        resultPanel.Margin = new Padding(3, 16, 3, 3);
        resultPanel.Name = "resultPanel";
        resultPanel.Size = new Size(830, 70);
        resultPanel.TabIndex = 3;
        // 
        // resultLayout
        // 
        resultLayout.ColumnCount = 1;
        resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        resultLayout.Controls.Add(resultSummaryLabel, 0, 0);
        resultLayout.Controls.Add(failureDetailsButton, 0, 1);
        resultLayout.Dock = DockStyle.Fill;
        resultLayout.Location = new Point(0, 0);
        resultLayout.Name = "resultLayout";
        resultLayout.RowCount = 2;
        resultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        resultLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        resultLayout.Size = new Size(828, 68);
        resultLayout.TabIndex = 0;
        // 
        // resultSummaryLabel
        // 
        resultSummaryLabel.AutoSize = true;
        resultSummaryLabel.Dock = DockStyle.Fill;
        resultSummaryLabel.Location = new Point(3, 3);
        resultSummaryLabel.Name = "resultSummaryLabel";
        resultSummaryLabel.Size = new Size(822, 39);
        resultSummaryLabel.TabIndex = 0;
        resultSummaryLabel.Text = "Ready to rename .jfif files.";
        resultSummaryLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // failureDetailsButton
        // 
        failureDetailsButton.Anchor = AnchorStyles.Top;
        failureDetailsButton.AutoSize = true;
        failureDetailsButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        failureDetailsButton.Location = new Point(344, 45);
        failureDetailsButton.Name = "failureDetailsButton";
        failureDetailsButton.Padding = new Padding(8, 0, 8, 0);
        failureDetailsButton.Size = new Size(140, 23);
        failureDetailsButton.TabIndex = 1;
        failureDetailsButton.Text = "View failure details";
        failureDetailsButton.UseVisualStyleBackColor = true;
        failureDetailsButton.Visible = false;
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
        resultPanel.ResumeLayout(false);
        resultLayout.ResumeLayout(false);
        resultLayout.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
