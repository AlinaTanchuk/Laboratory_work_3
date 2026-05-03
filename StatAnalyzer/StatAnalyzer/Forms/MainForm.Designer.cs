namespace StatAnalyzer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            titleLabel = new Label();
            subtitleLabel = new Label();
            buttonsPanel = new TableLayoutPanel();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(34, 87, 169);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Margin = new Padding(4, 5, 4, 5);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(43, 25, 43, 25);
            headerPanel.Size = new Size(1151, 183);
            headerPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(43, 30);
            titleLabel.Margin = new Padding(4, 0, 4, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(937, 60);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "📊 Статистический анализ данных России";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Font = new Font("Segoe UI", 10F);
            subtitleLabel.ForeColor = Color.FromArgb(180, 210, 255);
            subtitleLabel.Location = new Point(46, 103);
            subtitleLabel.Margin = new Padding(4, 0, 4, 0);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(284, 28);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Свежая ежегодная статистика";
            // 
            // buttonsPanel
            // 
            buttonsPanel.ColumnCount = 2;
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonsPanel.Dock = DockStyle.Fill;
            buttonsPanel.Location = new Point(0, 183);
            buttonsPanel.Margin = new Padding(4, 5, 4, 5);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(57, 50, 57, 50);
            buttonsPanel.RowCount = 2;
            buttonsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonsPanel.Size = new Size(1151, 752);
            buttonsPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1151, 935);
            Controls.Add(buttonsPanel);
            Controls.Add(headerPanel);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1173, 796);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Статистический анализатор";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.TableLayoutPanel buttonsPanel;
    }
}