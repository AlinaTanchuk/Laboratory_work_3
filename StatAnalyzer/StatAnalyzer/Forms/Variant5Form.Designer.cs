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
            this.Text = "Вариант 5 — Численность населения России";
            this.Size = new System.Drawing.Size(1100, 700);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);

            // ─── Боковая панель управления ───────────────────────
            this.sidePanel = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblWindow = new System.Windows.Forms.Label();
            this.nudWindow = new System.Windows.Forms.NumericUpDown();
            this.lblSteps = new System.Windows.Forms.Label();
            this.nudForecastSteps = new System.Windows.Forms.NumericUpDown();
            this.btnForecast = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();

            // ─── Таблица данных ───────────────────────────────────
            this.dgv = new System.Windows.Forms.DataGridView();

            // ─── График ───────────────────────────────────────────
            this.plotView = new OxyPlot.WindowsForms.PlotView();

            // ─── Основная панель ───────────────────────────────────
            this.mainPanel = new System.Windows.Forms.Panel();

            // 
            // sidePanel
            // 
            this.sidePanel.Width = 230;
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidePanel.BackColor = System.Drawing.Color.White;
            this.sidePanel.Padding = new System.Windows.Forms.Padding(15);

            // 
            // btnLoad
            // 
            this.btnLoad.Text = "📂 Загрузить JSON";
            this.btnLoad.Location = new System.Drawing.Point(15, 15);
            this.btnLoad.Size = new System.Drawing.Size(195, 36);
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);

            // 
            // lblWindow
            // 
            this.lblWindow.Text = "Размер окна (N):";
            this.lblWindow.Location = new System.Drawing.Point(15, 65);
            this.lblWindow.AutoSize = true;

            // 
            // nudWindow
            // 
            this.nudWindow.Location = new System.Drawing.Point(15, 87);
            this.nudWindow.Width = 190;
            this.nudWindow.Minimum = 1;
            this.nudWindow.Maximum = 100;
            this.nudWindow.Value = 3;

            // 
            // lblSteps
            // 
            this.lblSteps.Text = "Шагов прогноза:";
            this.lblSteps.Location = new System.Drawing.Point(15, 127);
            this.lblSteps.AutoSize = true;

            // 
            // nudForecastSteps
            // 
            this.nudForecastSteps.Location = new System.Drawing.Point(15, 149);
            this.nudForecastSteps.Width = 190;
            this.nudForecastSteps.Minimum = 1;
            this.nudForecastSteps.Maximum = 20;
            this.nudForecastSteps.Value = 3;

            // 
            // btnForecast
            // 
            this.btnForecast.Text = "📈 Построить прогноз";
            this.btnForecast.Location = new System.Drawing.Point(15, 199);
            this.btnForecast.Size = new System.Drawing.Size(195, 36);
            this.btnForecast.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnForecast.ForeColor = System.Drawing.Color.White;
            this.btnForecast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForecast.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnForecast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnForecast.Click += new System.EventHandler(this.BtnForecast_Click);

            // 
            // btnExport
            // 
            this.btnExport.Text = "💾 Экспорт графика";
            this.btnExport.Location = new System.Drawing.Point(15, 249);
            this.btnExport.Size = new System.Drawing.Size(195, 36);
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);

            // 
            // lblStats
            // 
            this.lblStats.Location = new System.Drawing.Point(15, 319);
            this.lblStats.Size = new System.Drawing.Size(195, 200);
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblStats.Text = "Загрузите файл для отображения статистики.";

            // 
            // dgv
            // 
            this.dgv.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgv.Height = 200;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ReadOnly = true;
            this.dgv.AllowUserToAddRows = false;

            // 
            // plotView
            // 
            this.plotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotView.BackColor = System.Drawing.Color.White;
            this.plotView.Model = this.CreateEmptyPlot();

            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Controls.Add(this.plotView);
            this.mainPanel.Controls.Add(this.dgv);

            // 
            // Добавление элементов в sidePanel
            // 
            this.sidePanel.Controls.Add(this.btnLoad);
            this.sidePanel.Controls.Add(this.lblWindow);
            this.sidePanel.Controls.Add(this.nudWindow);
            this.sidePanel.Controls.Add(this.lblSteps);
            this.sidePanel.Controls.Add(this.nudForecastSteps);
            this.sidePanel.Controls.Add(this.btnForecast);
            this.sidePanel.Controls.Add(this.btnExport);
            this.sidePanel.Controls.Add(this.lblStats);

            // 
            // Добавление панелей в форму
            // 
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.sidePanel);
        }

        // Поля формы (объявлены здесь, но могут быть также в main файле)
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