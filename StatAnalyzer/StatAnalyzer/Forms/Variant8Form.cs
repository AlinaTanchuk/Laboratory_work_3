using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Legends;
using StatAnalyzer.Models;
using StatAnalyzer.Services;

namespace StatAnalyzer.Forms
{
    public partial class Variant8Form : Form
    {
        private readonly Variant8DataService _service = new Variant8DataService();
        private List<SalaryRecord> _records;

        public Variant8Form()
        {
            InitializeComponent();
            plotView.Model = CreateEmptyPlot();
        }

        private PlotModel CreateEmptyPlot()
        {
            return new PlotModel { Title = "Медианная заработная плата", Background = OxyColors.White };
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
                    nudWindow.Maximum = _records.Count;
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
            dgv.Columns.Add("Male", "Зарплата муж. (руб.)");
            dgv.Columns.Add("Female", "Зарплата жен. (руб.)");

            foreach (var r in _records.OrderBy(x => x.Year))
                dgv.Rows.Add(r.Year, r.MaleSalary.ToString("N0"), r.FemaleSalary.ToString("N0"));
        }

        private void BuildMainChart()
        {
            var model = new PlotModel
            {
                Title = "Медианная заработная плата (руб.)",
                Background = OxyColors.White
            };

            model.Legends.Add(new Legend
            {
                LegendPosition = LegendPosition.TopLeft,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Год",
                MajorGridlineStyle = LineStyle.Dot,
                MajorStep = 1,
                MinorStep = 1
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Зарплата (руб.)",
                MajorGridlineStyle = LineStyle.Dot
            });

            var maleSeries = new LineSeries
            {
                Title = "Мужчины",
                Color = OxyColor.FromRgb(52, 152, 219),
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                StrokeThickness = 2
            };
            var femaleSeries = new LineSeries
            {
                Title = "Женщины",
                Color = OxyColor.FromRgb(231, 76, 60),
                MarkerType = MarkerType.Square,
                MarkerSize = 4,
                StrokeThickness = 2
            };

            foreach (var r in _records.OrderBy(x => x.Year))
            {
                maleSeries.Points.Add(new DataPoint(r.Year, r.MaleSalary));
                femaleSeries.Points.Add(new DataPoint(r.Year, r.FemaleSalary));
            }

            model.Series.Add(maleSeries);
            model.Series.Add(femaleSeries);
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

            if (windowSize > _records.Count)
            {
                MessageBox.Show(
                    $"Размер окна N={windowSize} не может превышать число наблюдений ({_records.Count}).",
                    "Неверный параметр", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var sorted = _records.OrderBy(x => x.Year).ToList();
            int lastYear = sorted.Last().Year;

            var maleValues = sorted.Select(x => x.MaleSalary).ToList();
            var femaleValues = sorted.Select(x => x.FemaleSalary).ToList();

            var maleForecast = _service.CalculateMovingAverageForecast(maleValues, windowSize, steps);
            var femaleForecast = _service.CalculateMovingAverageForecast(femaleValues, windowSize, steps);

            var model = plotView.Model;
            ExportHelper.RemoveSeriesByTitle(model, "Прогноз");

            var maleForecastSeries = new LineSeries
            {
                Title = "Прогноз (мужчины)",
                Color = OxyColor.FromRgb(52, 152, 219),
                LineStyle = LineStyle.Dash,
                MarkerType = MarkerType.Triangle,
                StrokeThickness = 2
            };

            var femaleForecastSeries = new LineSeries
            {
                Title = "Прогноз (женщины)",
                Color = OxyColor.FromRgb(231, 76, 60),
                LineStyle = LineStyle.Dash,
                MarkerType = MarkerType.Triangle,
                StrokeThickness = 2
            };

            for (int i = 0; i < maleForecast.Count; i++)
            {
                maleForecastSeries.Points.Add(new DataPoint(lastYear + i + 1, maleForecast[i]));
                femaleForecastSeries.Points.Add(new DataPoint(lastYear + i + 1, femaleForecast[i]));
            }

            model.Series.Add(maleForecastSeries);
            model.Series.Add(femaleForecastSeries);
            model.InvalidatePlot(true);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportHelper.ExportChart(plotView.Model, "salary_chart");
        }

        private void UpdateStats()
        {
            try
            {
                var stats = _service.CalculateSalaryChangeStats(_records);
                lblStats.Text =
                    $"📊 Статистика:\n\n" +
                    $"Мужчины:\n" +
                    $"  Макс. рост: {stats.maleMax:+0.00;-0.00}%\n  ({stats.maleMaxYear})\n" +
                    $"  Мин. рост: {stats.maleMin:+0.00;-0.00}%\n  ({stats.maleMinYear})\n\n" +
                    $"Женщины:\n" +
                    $"  Макс. рост: {stats.femaleMax:+0.00;-0.00}%\n  ({stats.femaleMaxYear})\n" +
                    $"  Мин. рост: {stats.femaleMin:+0.00;-0.00}%\n  ({stats.femaleMinYear})";
            }
            catch (Exception ex)
            {
                lblStats.Text = $"Ошибка вычислений: {ex.Message}";
            }
        }
    }
}