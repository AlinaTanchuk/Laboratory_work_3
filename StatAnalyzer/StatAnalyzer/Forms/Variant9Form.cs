using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using StatAnalyzer.Models;
using StatAnalyzer.Services;

namespace StatAnalyzer
{
    public partial class Variant9Form : Form
    {
        private readonly Variant9DataService _service = new Variant9DataService();
        private List<HousingRecord> _records;

        public Variant9Form()
        {
            InitializeComponent();
        }

        private PlotModel CreateEmptyPlot()
        {
            return new PlotModel { Title = "Цены на первичное жильё", Background = OxyColors.White };
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

                    int maxN = _records.Count;
                    nudWindow.Maximum = maxN;
                    if (nudWindow.Value > maxN)
                        nudWindow.Value = maxN;
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
            dgv.Columns.Add("P1", "1-комн. (руб./м²)");
            dgv.Columns.Add("P2", "2-комн. (руб./м²)");
            dgv.Columns.Add("P3", "3-комн. (руб./м²)");

            foreach (var r in _records.OrderBy(x => x.Year))
                dgv.Rows.Add(r.Year,
                    r.Price1Room.ToString("N0"),
                    r.Price2Room.ToString("N0"),
                    r.Price3Room.ToString("N0"));
        }

        private void BuildMainChart()
        {
            var model = new PlotModel
            {
                Title = "Цены на первичное жильё по типам квартир",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Год", MajorGridlineStyle = LineStyle.Dot });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Цена (руб./м²)", MajorGridlineStyle = LineStyle.Dot });

            var s1 = new LineSeries { Title = "1-комнатные", Color = OxyColor.FromRgb(52, 152, 219), MarkerType = MarkerType.Circle, MarkerSize = 4, StrokeThickness = 2 };
            var s2 = new LineSeries { Title = "2-комнатные", Color = OxyColor.FromRgb(46, 204, 113), MarkerType = MarkerType.Square, MarkerSize = 4, StrokeThickness = 2 };
            var s3 = new LineSeries { Title = "3-комнатные", Color = OxyColor.FromRgb(155, 89, 182), MarkerType = MarkerType.Triangle, MarkerSize = 4, StrokeThickness = 2 };

            foreach (var r in _records.OrderBy(x => x.Year))
            {
                s1.Points.Add(new DataPoint(r.Year, r.Price1Room));
                s2.Points.Add(new DataPoint(r.Year, r.Price2Room));
                s3.Points.Add(new DataPoint(r.Year, r.Price3Room));
            }

            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Series.Add(s3);
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

            var model = plotView.Model;
            var toRemove = model.Series
        .Where(s => s.Title != null && s.Title.Contains("Прогноз"))
        .ToList();

            foreach (var s in toRemove)
            {
                model.Series.Remove(s);
            }

            int lastYear = _records.Max(x => x.Year);

            var values1 = _records.OrderBy(x => x.Year).Select(x => x.Price1Room).ToList();
            var values2 = _records.OrderBy(x => x.Year).Select(x => x.Price2Room).ToList();
            var values3 = _records.OrderBy(x => x.Year).Select(x => x.Price3Room).ToList();

            var forecast1 = _service.CalculateMovingAverageForecast(values1, windowSize, steps);
            var forecast2 = _service.CalculateMovingAverageForecast(values2, windowSize, steps);
            var forecast3 = _service.CalculateMovingAverageForecast(values3, windowSize, steps);

            var fs1 = new LineSeries
            {
                Title = "Прогноз 1-комн.",
                Color = OxyColor.FromRgb(52, 152, 219),
                LineStyle = LineStyle.Dash,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                StrokeThickness = 2
            };
            var fs2 = new LineSeries
            {
                Title = "Прогноз 2-комн.",
                Color = OxyColor.FromRgb(46, 204, 113),
                LineStyle = LineStyle.Dash,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                StrokeThickness = 2
            };
            var fs3 = new LineSeries
            {
                Title = "Прогноз 3-комн.",
                Color = OxyColor.FromRgb(155, 89, 182),
                LineStyle = LineStyle.Dash,
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                StrokeThickness = 2
            };
            for (int i = 0; i < steps; i++)
            {
                double x = lastYear + i + 1;
                fs1.Points.Add(new DataPoint(x, forecast1[i]));
                fs2.Points.Add(new DataPoint(x, forecast2[i]));
                fs3.Points.Add(new DataPoint(x, forecast3[i]));
            }
            model.Series.Add(fs1);
            model.Series.Add(fs2);
            model.Series.Add(fs3);
            model.InvalidatePlot(true);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportHelper.ExportChart(plotView.Model, "housing_chart");
        }

        private void UpdateStats()
        {
            try
            {
                var stats = _service.CalculatePriceChangeStats(_records);
                lblStats.Text =
                    $"📊 Статистика:\n\n" +
                    $"Сильнее всего подорожали:\n  {stats.mostExpensive}  (+{stats.maxGrowthPct:0.0}%)\n\n" +
                    $"Сильнее всего подешевели:\n  {stats.mostCheap}  ({stats.maxDropPct:+0.0;-0.0}%)";
            }
            catch (Exception ex)
            {
                lblStats.Text = $"Ошибка вычислений: {ex.Message}";
            }
        }
    }
}
