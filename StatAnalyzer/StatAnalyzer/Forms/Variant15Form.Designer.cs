namespace StatAnalyzer.Forms
{
    partial class Variant15Form
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
            this.sidePanel = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cmbDisease = new System.Windows.Forms.ComboBox();
            this.nudWindow = new System.Windows.Forms.NumericUpDown();
            this.nudForecastSteps = new System.Windows.Forms.NumericUpDown();
            this.btnForecast = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.plotView = new OxyPlot.WindowsForms.PlotView();

            var lblDisease = new System.Windows.Forms.Label();
            var lblWindow = new System.Windows.Forms.Label();
            var lblSteps = new System.Windows.Forms.Label();
            var mainPanel = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.nudWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudForecastSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();

            // Form
            this.Text = "Вариант 15 — Инфекционные заболевания";
            this.Size = new System.Drawing.Size(1100, 700);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            // sidePanel
            this.sidePanel.Width = 230;
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidePanel.BackColor = System.Drawing.Color.White;
            this.sidePanel.Padding = new System.Windows.Forms.Padding(15);

            // Controls
            this.btnLoad = CreateButton("📂 Загрузить JSON", 15, System.Drawing.Color.FromArgb(52, 152, 219));
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);

            lblDisease.Text = "Инфекция для прогноза:";
            lblDisease.Location = new System.Drawing.Point(15, 65);
            lblDisease.AutoSize = true;

            this.cmbDisease.Location = new System.Drawing.Point(15, 87);
            this.cmbDisease.Size = new System.Drawing.Size(190, 21);
            this.cmbDisease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            lblWindow.Text = "Размер окна (N):";
            lblWindow.Location = new System.Drawing.Point(15, 123);
            lblWindow.AutoSize = true;

            this.nudWindow.Location = new System.Drawing.Point(15, 145);
            this.nudWindow.Size = new System.Drawing.Size(190, 20);
            this.nudWindow.Minimum = 1;
            this.nudWindow.Maximum = 100;
            this.nudWindow.Value = 3;

            lblSteps.Text = "Шагов прогноза:";
            lblSteps.Location = new System.Drawing.Point(15, 185);
            lblSteps.AutoSize = true;

            this.nudForecastSteps.Location = new System.Drawing.Point(15, 207);
            this.nudForecastSteps.Size = new System.Drawing.Size(190, 20);
            this.nudForecastSteps.Minimum = 1;
            this.nudForecastSteps.Maximum = 20;
            this.nudForecastSteps.Value = 3;

            this.btnForecast = CreateButton("📈 Построить прогноз", 257, System.Drawing.Color.FromArgb(231, 76, 60));
            this.btnForecast.Click += new System.EventHandler(this.BtnForecast_Click);

            this.btnExport = CreateButton("💾 Экспорт графика", 307, System.Drawing.Color.FromArgb(52, 73, 94));
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);

            this.lblStats.Location = new System.Drawing.Point(15, 377);
            this.lblStats.Size = new System.Drawing.Size(195, 200);
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblStats.Text = "Загрузите файл для отображения статистики.";

            // sidePanel Controls
            this.sidePanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.btnLoad, lblDisease, this.cmbDisease, lblWindow, this.nudWindow,
                lblSteps, this.nudForecastSteps, this.btnForecast, this.btnExport, this.lblStats
            });

            // Grid & Plot
            this.dgv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv.Height = 200;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ReadOnly = true;
            this.dgv.AllowUserToAddRows = false;

            this.plotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView.BackColor = System.Drawing.Color.White;

            // Layout
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Controls.Add(this.plotView);
            mainPanel.Controls.Add(this.dgv);

            this.Controls.Add(mainPanel);
            this.Controls.Add(this.sidePanel);

            ((System.ComponentModel.ISupportInitialize)(this.nudWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudForecastSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button CreateButton(string text, int y, System.Drawing.Color color)
        {
            return new System.Windows.Forms.Button
            {
                Text = text,
                Location = new System.Drawing.Point(15, y),
                Size = new System.Drawing.Size(195, 36),
                BackColor = color,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold),
                Cursor = System.Windows.Forms.Cursors.Hand
            };
        }

        private System.Windows.Forms.DataGridView dgv;
        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.NumericUpDown nudWindow;
        private System.Windows.Forms.NumericUpDown nudForecastSteps;
        private System.Windows.Forms.ComboBox cmbDisease;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Button btnLoad, btnForecast, btnExport;
        private System.Windows.Forms.Panel sidePanel;
    }
}