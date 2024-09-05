namespace TriangleAngleAnalysis.Entities
{
    public enum TriangleError
    {
        /// <summary>
        /// Нет ошибок. Треугольник валиден.
        /// </summary>
        None,

        /// <summary>
        /// Одна или несколько сторон треугольника имеют длину, меньше или равную нулю.
        /// Это нарушает основное требование для сторон треугольника.
        /// </summary>
        SideLessOrEqualToZero,

        /// <summary>
        /// Нарушено правило "неравенства треугольника": 
        /// сумма длин любых двух сторон треугольника должна быть больше длины третьей стороны.
        /// </summary>
        TriangleInequalityViolation,

        /// <summary>
        /// Некорректное значение для допустимой погрешности (tolerance). Значение должно быть в диапозоне от 0 до 1.
        /// </summary>
        InvalidTolerance,

        /// <summary>
        /// Одно или несколько значений сторон слишком велики, что может привести к переполнению арифметических операций.
        /// </summary>
        PotentialOverflow
    }
}
