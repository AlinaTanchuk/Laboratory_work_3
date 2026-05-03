namespace StatAnalyzer.Forms
{
    partial class Variant5Form
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

        private void InitializeComponent()
        {
            sidePanel = new Panel();
            btnLoad = new Button();
            lblWindow = new Label();
            nudWindow = new NumericUpDown();
            lblSteps = new Label();
            nudForecastSteps = new NumericUpDown();
            btnForecast = new Button();
            btnExport = new Button();
            lblStats = new Label();
            dgv = new DataGridView();
            plotView = new OxyPlot.WindowsForms.PlotView();
            mainPanel = new Panel();
            sidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWindow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudForecastSteps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // sidePanel
            // 
            sidePanel.BackColor = Color.White;
            sidePanel.Controls.Add(btnLoad);
            sidePanel.Controls.Add(lblWindow);
            sidePanel.Controls.Add(nudWindow);
            sidePanel.Controls.Add(lblSteps);
            sidePanel.Controls.Add(nudForecastSteps);
            sidePanel.Controls.Add(btnForecast);
            sidePanel.Controls.Add(btnExport);
            sidePanel.Controls.Add(lblStats);
            sidePanel.Dock = DockStyle.Right;
            sidePanel.Location = new Point(848, 0);
            sidePanel.Name = "sidePanel";
            sidePanel.Padding = new Padding(15);
            sidePanel.Size = new Size(230, 644);
            sidePanel.TabIndex = 1;
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.FromArgb(52, 152, 219);
            btnLoad.Cursor = Cursors.Hand;
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoad.ForeColor = Color.White;
            btnLoad.Location = new Point(15, 15);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(195, 36);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "📂 Загрузить JSON";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += BtnLoad_Click;
            // 
            // lblWindow
            // 
            lblWindow.AutoSize = true;
            lblWindow.Location = new Point(15, 65);
            lblWindow.Name = "lblWindow";
            lblWindow.Size = new Size(148, 25);
            lblWindow.TabIndex = 1;
            lblWindow.Text = "Размер окна (N):";
            // 
            // nudWindow
            // 
            nudWindow.Location = new Point(15, 87);
            nudWindow.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudWindow.Name = "nudWindow";
            nudWindow.Size = new Size(190, 31);
            nudWindow.TabIndex = 2;
            nudWindow.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblSteps
            // 
            lblSteps.AutoSize = true;
            lblSteps.Location = new Point(15, 127);
            lblSteps.Name = "lblSteps";
            lblSteps.Size = new Size(152, 25);
            lblSteps.TabIndex = 3;
            lblSteps.Text = "Шагов прогноза:";
            // 
            // nudForecastSteps
            // 
            nudForecastSteps.Location = new Point(15, 149);
            nudForecastSteps.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudForecastSteps.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudForecastSteps.Name = "nudForecastSteps";
            nudForecastSteps.Size = new Size(190, 31);
            nudForecastSteps.TabIndex = 4;
            nudForecastSteps.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnForecast
            // 
            btnForecast.BackColor = Color.FromArgb(46, 204, 113);
            btnForecast.Cursor = Cursors.Hand;
            btnForecast.FlatStyle = FlatStyle.Flat;
            btnForecast.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnForecast.ForeColor = Color.White;
            btnForecast.Location = new Point(15, 199);
            btnForecast.Name = "btnForecast";
            btnForecast.Size = new Size(195, 36);
            btnForecast.TabIndex = 5;
            btnForecast.Text = "📈 Построить прогноз";
            btnForecast.UseVisualStyleBackColor = false;
            btnForecast.Click += BtnForecast_Click;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(155, 89, 182);
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.Location = new Point(15, 249);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(195, 36);
            btnExport.TabIndex = 6;
            btnExport.Text = "💾 Экспорт графика";
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += BtnExport_Click;
            // 
            // lblStats
            // 
            lblStats.Font = new Font("Segoe UI", 8.5F);
            lblStats.ForeColor = Color.FromArgb(60, 60, 60);
            lblStats.Location = new Point(15, 319);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(195, 289);
            lblStats.TabIndex = 7;
            lblStats.Text = "Загрузите файл для отображения статистики.";
            // 
            // dgv
            // 
            this.dgv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ReadOnly = true;
            this.dgv.AllowUserToAddRows = false;
            // 
            // plotView
            // 
            plotView.BackColor = Color.White;
            plotView.Dock = DockStyle.Fill;
            plotView.Location = new Point(0, 0);
            plotView.Name = "plotView";
            plotView.PanCursor = Cursors.Hand;
            plotView.Size = new Size(848, 440);
            plotView.TabIndex = 0;
            plotView.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(plotView);
            mainPanel.Controls.Add(dgv);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(848, 644);
            mainPanel.TabIndex = 0;
            // 
            // Variant5Form
            // 
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1078, 644);
            Controls.Add(mainPanel);
            Controls.Add(sidePanel);
            MinimumSize = new Size(900, 600);
            Name = "Variant5Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вариант 5 — Численность населения России";
            sidePanel.ResumeLayout(false);
            sidePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudWindow).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudForecastSteps).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        // Поля формы
        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblWindow;
        private System.Windows.Forms.NumericUpDown nudWindow;
        private System.Windows.Forms.Label lblSteps;
        private System.Windows.Forms.NumericUpDown nudForecastSteps;
        private System.Windows.Forms.Button btnForecast;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.DataGridView dgv;
        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.Panel mainPanel;
    }
}