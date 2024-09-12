namespace TriangleAngleAnalysis
{
    using System;
    using TriangleAngleAnalysis.Entities;

    public static class TriangleAngleAnalyser
    {
        private static readonly double _sqrRootOFMax = Math.Sqrt(double.MaxValue);

        /// <summary>
        /// Определяет, является треугольник остро-, прямо- или тупоугольным по заданным сторонам.
        /// </summary>
        /// <param name="firstSide">Первая сторона угла.</param>
        /// <param name="secondSide">Вторая сторона угла.</param>
        /// <param name="thirdSide">Третья сторона угла.</param>
        /// <returns>Информацию о типе треугольника, либо об ошибке в переданных данных.</returns>
        public static OperationResult DefineTriangleType(double firstSide, double secondSide, double thirdSide, double tolerance = 1e-10)
        {
            var validationResult = ValidateTriangle(firstSide, secondSide, thirdSide);
            if (validationResult != TriangleError.None)
            {
                return new OperationResult(TriangleType.Undefind, validationResult);
            }

            if (!ValidateTolerance(tolerance))
            {
                return new OperationResult(TriangleType.Undefind, TriangleError.InvalidTolerance);
            }

            return DefineTriangleTypeAfterInputValidation(firstSide, secondSide, thirdSide, tolerance);
        }
        private static TriangleError ValidateTriangle(double sideA, double sideB, double sideC)
        {
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                return TriangleError.SideLessOrEqualToZero;
            }

            double halfMax = double.MaxValue / 2;

            // This condition checks to prevent potential overflow when using the Pythagorean theorem further.
            if (sideA > _sqrRootOFMax || sideB > _sqrRootOFMax || sideC > _sqrRootOFMax ||
                sideA * sideA / 2 + sideB * sideB / 2 > halfMax ||
                sideA * sideA / 2 + sideC * sideC / 2 > halfMax ||
                sideB * sideB / 2 + sideC * sideC / 2 > halfMax)
            {
                return TriangleError.PotentialOverflow;
            }

            if (sideA + sideB > sideC && sideA + sideC > sideB && sideB + sideC > sideA)
            {
                return TriangleError.None;
            }

            return TriangleError.TriangleInequalityViolation;
        }
        private static bool ValidateTolerance(double tolerance)
        {
            return (tolerance >= 0) && (tolerance <= 1);
        }
        private static OperationResult DefineTriangleTypeAfterInputValidation(double sideA, double sideB, double sideC, double tolerance = 1e-10)
        {
            double[] sides = new double[] { sideA, sideB, sideC };
            Array.Sort(sides);
            double a = sides[0];
            double b = sides[1];
            double c = sides[2];

            double sumOfSquares = a * a + b * b;
            double squareOfLongestSide = c * c;

            TriangleType triangleType;

            if (Math.Abs(sumOfSquares - squareOfLongestSide) < tolerance)
            {
                triangleType = TriangleType.Right;
            }
            else if (sumOfSquares < squareOfLongestSide)
            {
                triangleType = TriangleType.Obtuse;
            }
            else
            {
                triangleType = TriangleType.Acute;
            }

            return new OperationResult(triangleType, TriangleError.None);
        }
    }
}
