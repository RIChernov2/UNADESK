namespace TriangleAngleAnalysis
{
    using System;
    using TriangleAngleAnalysis.Entities;

    public static class TriangleAngleAnalyser
    {
        /// <summary>
        /// Определяет, является ли угол в треугольнике прямым по заданным сторонам.
        /// </summary>
        /// <param name="firstAngleSide">Первая сторона угла.</param>
        /// <param name="secondAngleSide">Вторая сторона угла.</param>
        /// <param name="oppositeSide">Сторона треугольника, противоположная углу.</param>
        /// <returns>Результат проверки, является ли угол прямым.</returns>
        public static OperationResult CheckIfRightAngle(double firstAngleSide, double secondAngleSide, double oppositeSide)
        {
            int expectedResult = 0;
            return EvaluateAngleType(firstAngleSide, secondAngleSide, oppositeSide, expectedResult);
        }

        /// <summary>
        /// Определяет, является ли угол в треугольнике острым по заданным сторонам.
        /// </summary>
        /// <param name="firstAngleSide">Первая сторона угла.</param>
        /// <param name="secondAngleSide">Вторая сторона угла.</param>
        /// <param name="oppositeSide">Сторона треугольника, противоположная углу.</param>
        /// <returns>Результат проверки, является ли угол острым.</returns>
        public static OperationResult CheckIfAcuteAngle(double firstAngleSide, double secondAngleSide, double oppositeSide)
        {
            int expectedResult = 1;
            return EvaluateAngleType(firstAngleSide, secondAngleSide, oppositeSide, expectedResult);
        }

        /// <summary>
        /// Определяет, является ли угол в треугольнике тупым по заданным сторонам.
        /// </summary>
        /// <param name="firstAngleSide">Первая сторона угла.</param>
        /// <param name="secondAngleSide">Вторая сторона угла.</param>
        /// <param name="oppositeSide">Сторона треугольника, противоположная углу.</param>
        /// <returns>Результат проверки, является ли угол тупым.</returns>
        public static OperationResult CheckIfObtuseAngle(double firstAngleSide, double secondAngleSide, double oppositeSide)
        {
            int expectedResult = -1;
            return EvaluateAngleType(firstAngleSide, secondAngleSide, oppositeSide, expectedResult);
        }

        private static OperationResult EvaluateAngleType(double firstAngleSide, double secondAngleSide, double oppositeSide, int expectedResult)
        {
            var validationResult = ValidateTriangle(firstAngleSide, secondAngleSide, oppositeSide);
            if (validationResult.Error != TriangleError.None)
            {
                return validationResult;
            }
            
            bool result = CheckIfResultMatchesExpected(firstAngleSide, secondAngleSide, oppositeSide, expectedResult);

            return new OperationResult(result, TriangleError.None);
        }
        private static OperationResult ValidateTriangle(double sideA, double sideB, double sideC)
        {
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                return new OperationResult(false, TriangleError.SideLessOrEqualToZero);
            }

            if (sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA)
            {
                return new OperationResult(true, TriangleError.None);
            }

            return new OperationResult(false, TriangleError.TriangleInequalityViolation);
        }
        private static bool CheckIfResultMatchesExpected(double firstAngleSide, double secondAngleSide, double oppositeSide, int expectedResult)
        {
            int comparisonResult = CompareSumOfSquares(firstAngleSide, secondAngleSide, oppositeSide);
            return comparisonResult == expectedResult;

        }
        private static int CompareSumOfSquares(double firstAngleSide, double secondAngleSide, double oppositeSide)
        {
            double a = firstAngleSide;
            double b = secondAngleSide;
            double c = oppositeSide;

            double sumOfSquaresOfSides = a * a + b * b;
            double squareOfOppositeSide = c * c;

            if (Math.Abs(sumOfSquaresOfSides - squareOfOppositeSide) < 1e-10)
            {
                return 0;
            }
            else if (sumOfSquaresOfSides < squareOfOppositeSide)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
