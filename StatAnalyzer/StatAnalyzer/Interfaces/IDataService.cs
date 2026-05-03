using System.Collections.Generic;

namespace StatAnalyzer.Interfaces
{
    // Базовый интерфейс для сервиса работы с данными.
    // Принцип инверсии зависимостей (DIP): формы зависят от абстракции, а не от реализации.
    public interface IDataService<T>
    {
        // Загрузить данные из JSON-файла.
        List<T> LoadFromFile(string filePath);

        // Вычислить прогноз методом скользящей средней на N шагов вперёд.
        List<double> CalculateMovingAverageForecast(List<double> values, int windowSize, int forecastSteps);
    }
}
