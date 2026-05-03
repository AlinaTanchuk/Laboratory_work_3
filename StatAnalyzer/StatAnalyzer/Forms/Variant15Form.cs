using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using StatAnalyzer.Models;
using StatAnalyzer.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StatAnalyzer.Forms
{


    public partial class Variant15Form : Form
    {
        private readonly Variant15DataService _service = new Variant15DataService();
        private List<DiseaseRecord> _records;

        public Variant15Form()
        {
            InitializeComponent();
            plotView.Model = CreateEmptyPlot();
        }

        private PlotModel CreateEmptyPlot()
        {
            return new PlotModel { Title = "Инфекционные заболевания", Background = OxyColors.White };
        }

        private void PopulateDiseaseCombo()
        {
            cmbDisease.Items.Clear();
            var diseases = _records.Select(r => r.DiseaseName).Distinct().OrderBy(x => x).ToList();
            foreach (var d in diseases)
                cmbDisease.Items.Add(d);
            if (cmbDisease.Items.Count > 0)
                cmbDisease.SelectedIndex = 0;

            // Обновляем максимум окна по минимальному числу лет среди болезней
            int minCount = _records.GroupBy(r => r.DiseaseName)
                                   .Min(g => g.Count());
            nudWindow.Maximum = minCount;
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
                    PopulateDiseaseCombo();
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
            dgv.Columns.Add("Disease", "Инфекция");
            dgv.Columns.Add("Incidence", "Заболеваемость (на 100 тыс.)");

            foreach (var r in _records.OrderBy(x => x.Year).ThenBy(x => x.DiseaseName))
                dgv.Rows.Add(r.Year, r.DiseaseName, r.Incidence.ToString("F2"));
        }

        private void BuildMainChart()
        {
            var model = new PlotModel
            {
                Title = "Динамика инфекционных заболеваний",
                Background = OxyColors.White

            };
            model.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.TopRight,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical
            });

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Год", MajorGridlineStyle = LineStyle.Dot });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Заболеваемость (на 100 тыс.)", MajorGridlineStyle = LineStyle.Dot });

            var palette = new[]
            {
                OxyColor.FromRgb(231, 76, 60),
                OxyColor.FromRgb(52, 152, 219),
                OxyColor.FromRgb(46, 204, 113),
                OxyColor.FromRgb(155, 89, 182),
                OxyColor.FromRgb(241, 196, 15)
            };

            var grouped = _records.GroupBy(r => r.DiseaseName).ToList();
            for (int idx = 0; idx < grouped.Count; idx++)
            {
                var group = grouped[idx];
                var series = new LineSeries
                {
                    Title = group.Key,
                    Color = palette[idx % palette.Length],
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 4,
                    StrokeThickness = 2
                };
                foreach (var r in group.OrderBy(x => x.Year))
                    series.Points.Add(new DataPoint(r.Year, r.Incidence));

                model.Series.Add(series);
            }

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


            //string selectedDisease = _records.GroupBy(r => r.DiseaseName).First().Key;
            if (cmbDisease.SelectedItem == null)
            {
                MessageBox.Show("Выберите болезнь для прогноза.", "Нет выбора",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDisease = cmbDisease.SelectedItem.ToString();

            int windowSize = (int)nudWindow.Value;
            int steps = (int)nudForecastSteps.Value;

            var values = _records
                .Where(r => r.DiseaseName == selectedDisease)
                .OrderBy(r => r.Year)
                .Select(r => r.Incidence)
                .ToList();

            var forecast = _service.CalculateMovingAverageForecast(values, windowSize, steps);

            var model = plotView.Model;
            var seriesToRemove = model.Series
                .Where(s => s.Title != null && s.Title.StartsWith("Прогноз"))
                .ToList();

            foreach (var s in seriesToRemove)
            {
                model.Series.Remove(s);
            }

            int lastYear = _records.Max(x => x.Year);
            var fs = new LineSeries
            {
                Title = $"Прогноз: {selectedDisease}",
                Color = OxyColor.FromRgb(231, 76, 60),
                LineStyle = LineStyle.Dash,
                MarkerType = MarkerType.Triangle,
                MarkerSize = 5,
                StrokeThickness = 2
            };

            for (int i = 0; i < forecast.Count; i++)
                fs.Points.Add(new DataPoint(lastYear + i + 1, forecast[i]));

            model.Series.Add(fs);
            model.InvalidatePlot(true);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportHelper.ExportChart(plotView.Model, "disease_chart");
        }

        private void UpdateStats()
        {
            try
            {
                var stats = _service.CalculateDiseaseChangeStats(_records);
                lblStats.Text =
                    $"📊 Статистика:\n\n" +
                    $"Снизилась больше всего:\n  {stats.mostDeclined}\n  ({stats.mostDeclinedPct:+0.0;-0.0}%)\n\n" +
                    $"Снизилась меньше всего:\n  {stats.leastDeclined}\n  ({stats.leastDeclinedPct:+0.0;-0.0}%)";
            }
            catch (Exception ex)
            {
                lblStats.Text = $"Ошибка вычислений: {ex.Message}";
            }
        }
    }
}
