using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using StatAnalyzer.Models;
using StatAnalyzer.Services;

namespace StatAnalyzer.Forms
{

    // Форма варианта 5: численность населения России.

    public partial class Variant5Form : Form
    {
        private readonly Variant5DataService _service = new Variant5DataService();
        private List<PopulationRecord> _records;


        public Variant5Form()
        {
            InitializeComponent();
        }


        private PlotModel CreateEmptyPlot()
        {
            return new PlotModel
            {
                Title = "Численность населения России",
                Background = OxyColors.White
            };
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "JSON файлы (*.json)|*.json" })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    _records = _service.LoadFromFile(dlg.FileName);
                    PopulateGrid();
                    BuildMainChart();
                    UpdateStats();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PopulateGrid()
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("Year", "Год");
            dgv.Columns.Add("Population", "Население (млн чел.)");

            foreach (var r in _records.OrderBy(x => x.Year))
                dgv.Rows.Add(r.Year, r.Population.ToString("F2"));
        }

        private void BuildMainChart()
        {
            var model = new PlotModel
            {
                Title = "Численность населения России",
                Background = OxyColors.White
            };

            var xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Год", MajorGridlineStyle = LineStyle.Dot };
            var yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Население (млн чел.)", MajorGridlineStyle = LineStyle.Dot };
            model.Axes.Add(xAxis);
            model.Axes.Add(yAxis);

            var series = new LineSeries
            {
                Title = "Население",
                Color = OxyColor.FromRgb(52, 152, 219),
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                StrokeThickness = 2
            };

            foreach (var r in _records.OrderBy(x => x.Year))
                series.Points.Add(new DataPoint(r.Year, r.Population));

            model.Series.Add(series);
            plotView.Model = model;
        }

        private void BtnForecast_Click(object sender, EventArgs e)
        {
            if (_records == null || _records.Count == 0)
            {
                MessageBox.Show("Сначала загрузите данные.", "Нет данных",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int windowSize = (int)nudWindow.Value;
            int steps = (int)nudForecastSteps.Value;

            var values = _records.OrderBy(x => x.Year).Select(x => x.Population).ToList();
            var forecast = _service.CalculateMovingAverageForecast(values, windowSize, steps);

            var model = plotView.Model;
            // Удаляем предыдущий прогноз, если был
            // model.Series.RemoveAll(s => s.Title == "Прогноз");

            var toRemove = model.Series.Where(s => s.Title == "Прогноз").ToList();
            foreach (var item in toRemove)
                model.Series.Remove(item);

            var forecastSeries = new LineSeries
            {
                Title = "Прогноз",
                Color = OxyColor.FromRgb(231, 76, 60),
                MarkerType = MarkerType.Triangle,
                MarkerSize = 5,
                StrokeThickness = 2,
                LineStyle = LineStyle.Dash
            };

            int lastYear = 0;
            for (int i = 0; i < forecast.Count; i++)
                forecastSeries.Points.Add(new DataPoint(lastYear + i + 1, forecast[i]));

            model.Series.Add(forecastSeries);
            model.InvalidatePlot(true);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Экспорт будет реализован в следующей версии.", "Не реализовано",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateStats()
        {
            try
            {
                var stats = _service.CalculateGrowthStats(_records);
                lblStats.Text =
                    $"📊 Статистика:\n\n" +
                    $"Макс. прирост:\n  {stats.maxGrowth:+0.00;-0.00}% ({stats.maxGrowthYear})\n\n" +
                    $"Макс. убыль:\n  {stats.maxDecline:+0.00;-0.00}% ({stats.maxDeclineYear})";
            }
            catch (Exception ex)
            {
                lblStats.Text = $"Ошибка вычислений: {ex.Message}";
            }
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
