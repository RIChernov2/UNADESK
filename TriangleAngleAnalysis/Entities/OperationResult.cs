namespace TriangleAngleAnalysis.Entities
{
    /// <summary>
    /// Класс для представления результата выполнения операции или проверки.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Результат выполнения операции/проверки.
        /// </summary>
        public TriangleType Result { get; }

        /// <summary>
        /// Тип ошибки, если возникла.
        /// </summary>
        public TriangleError Error { get; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="result">Результат выполнения операции / проверки.</param>
        /// <param name="error">Тип ошибки, если возникла. По умолчанию <see cref="TriangleError.None"/>.</param>
        public OperationResult(TriangleType result, TriangleError error = TriangleError.None)
        {
            Result = result;
            Error = error;
        }
    }
}


