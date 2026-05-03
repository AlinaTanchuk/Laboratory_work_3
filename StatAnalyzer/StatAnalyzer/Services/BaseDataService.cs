using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using StatAnalyzer.Interfaces;

namespace StatAnalyzer.Services
{
    // Абстрактный базовый сервис данных.
    // Принципы ООП: наследование, инкапсуляция.
    // Содержит общую логику — скользящую среднюю и загрузку JSON.
    // Конкретные сервисы (Variant5DataService и др.) наследуют от него.

    public abstract class BaseDataService<T> : IDataService<T>
    {
        // Инкапсуляция: метод загрузки скрыт от форм, доступен через интерфейс
        public List<T> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл не найден: {filePath}");

            string json = File.ReadAllText(filePath);
            var result = JsonConvert.DeserializeObject<List<T>>(json);

            if (result == null || result.Count == 0)
                throw new InvalidDataException("Файл пуст или имеет неверный формат.");

            return result;
        }

        // Метод экстраполяции по скользящей средней.
        // values     — исходный ряд данных
        // windowSize — размер окна (n)
        // forecastSteps — количество шагов прогноза
        
        public List<double> CalculateMovingAverageForecast(
            List<double> values, int windowSize, int forecastSteps)
        {
            if (windowSize <= 0 || windowSize > values.Count)
                throw new ArgumentException("Размер окна должен быть от 1 до числа наблюдений.");

            // Копируем исходный ряд для работы
            var extended = new List<double>(values);
            var forecast = new List<double>();

            for (int step = 0; step < forecastSteps; step++)
            {
                // Берём последние windowSize значений расширенного ряда
                int startIndex = extended.Count - windowSize;
                double sum = 0;
                for (int i = startIndex; i < extended.Count; i++)
                    sum += extended[i];

                double avg = sum / windowSize;
                forecast.Add(avg);
                extended.Add(avg); // добавляем прогноз для следующего шага
            }

            return forecast;
        }
    }
}
