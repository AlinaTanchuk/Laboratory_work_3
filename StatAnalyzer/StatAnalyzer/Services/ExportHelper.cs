using System;
using System.IO;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;

namespace StatAnalyzer.Services
{
    // Вспомогательный класс для экспорта графиков OxyPlot.
    public static class ExportHelper
    {
        // Открывает диалог сохранения и экспортирует график в PNG или SVG.
        public static void ExportChart(PlotModel model, string defaultFileName)
        {
            if (model == null)
            {
                MessageBox.Show("Нет графика для экспорта.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dlg = new SaveFileDialog
            {
                Filter = "PNG изображение (*.png)|*.png|SVG векторный (*.svg)|*.svg",
                FileName = defaultFileName
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    if (dlg.FilterIndex == 1) // PNG
                    {
                        // Экспорт в PNG через OxyPlot.WindowsForms
                        var exporter = new PngExporter { Width = 1200, Height = 700 };
                        using (var stream = File.Create(dlg.FileName))
                            exporter.Export(model, stream);
                    }
                    else // SVG
                    {
                        var exporter = new OxyPlot.SvgExporter { Width = 1200, Height = 700 };
                        using (var stream = File.Create(dlg.FileName))
                        using (var writer = new StreamWriter(stream))
                            writer.Write(exporter.ExportToString(model));
                    }

                    MessageBox.Show($"График сохранён:\n{dlg.FileName}", "Экспорт завершён",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Удаляет из модели все серии, заголовок которых содержит указанную строку.
        public static void RemoveSeriesByTitle(PlotModel model, string titleContains)
        {
            for (int i = model.Series.Count - 1; i >= 0; i--)
            {
                if (model.Series[i].Title != null &&
                    model.Series[i].Title.Contains(titleContains))
                    model.Series.RemoveAt(i);
            }
        }
    }
}
