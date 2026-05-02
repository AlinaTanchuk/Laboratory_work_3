using OxyPlot;
using OxyPlot.WindowsForms;

namespace StatAnalyzer
{
    partial class Variant9Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private DataGridView dgv;
        private PlotView plotView;
        private NumericUpDown nudWindow;
        private NumericUpDown nudForecastSteps;
        private Label lblStats;
        private Button btnLoad, btnForecast, btnExport;
        private Panel sidePanel;

        private void InitializeComponent()
        {
            this.Text = "Вариант 9 — Цены на первичное жильё";
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            sidePanel = new Panel
            {
                Width = 230,
                Dock = DockStyle.Right,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            int y = 15;

            btnLoad = CreateButton("📂 Загрузить JSON", y, Color.FromArgb(52, 152, 219));
            btnLoad.Click += BtnLoad_Click;
            y += 50;

            var lblWindow = new Label { Text = "Размер окна (N):", Location = new Point(15, y), AutoSize = true };
            y += 22;
            nudWindow = new NumericUpDown
            {
                Location = new Point(15, y),
                Width = 190,
                Minimum = 1,
                Maximum = 100,
                Value = 3
            };
            y += 40;

            var lblSteps = new Label { Text = "Шагов прогноза:", Location = new Point(15, y), AutoSize = true };
            y += 22;
            nudForecastSteps = new NumericUpDown
            {
                Location = new Point(15, y),
                Width = 190,
                Minimum = 1,
                Maximum = 20,
                Value = 3
            };
            y += 50;

            btnForecast = CreateButton("📈 Построить прогноз", y, Color.FromArgb(155, 89, 182));
            btnForecast.Click += BtnForecast_Click;
            y += 50;

            btnExport = CreateButton("💾 Экспорт графика", y, Color.FromArgb(52, 73, 94));
            btnExport.Click += BtnExport_Click;
            y += 70;

            lblStats = new Label
            {
                Location = new Point(15, y),
                Size = new Size(195, 220),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(60, 60, 60),
                Text = "Загрузите файл для отображения статистики."
            };

            sidePanel.Controls.Add(btnLoad);
            sidePanel.Controls.Add(lblWindow);
            sidePanel.Controls.Add(nudWindow);
            sidePanel.Controls.Add(lblSteps);
            sidePanel.Controls.Add(nudForecastSteps);
            sidePanel.Controls.Add(btnForecast);
            sidePanel.Controls.Add(btnExport);
            sidePanel.Controls.Add(lblStats);

            dgv = new DataGridView
            {
                Dock = DockStyle.Bottom,
                Height = 200,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            plotView = new PlotView { Dock = DockStyle.Fill, BackColor = Color.White };
            plotView.Model = CreateEmptyPlot();

            var mainPanel = new Panel { Dock = DockStyle.Fill };
            mainPanel.Controls.Add(plotView);
            mainPanel.Controls.Add(dgv);

            this.Controls.Add(mainPanel);
            this.Controls.Add(sidePanel);
        }

        private Button CreateButton(string text, int y, Color color)
        {
            return new Button
            {
                Text = text,
                Location = new Point(15, y),
                Size = new Size(195, 36),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
        }
    }
}