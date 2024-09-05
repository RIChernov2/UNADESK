﻿namespace TriangleAngleAnalysis
{
    using System;
    using TriangleAngleAnalysis.Entities;

    public static class TriangleAngleAnalyser
    {
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
                return new OperationResult(TriangleAngleType.Undefind, validationResult);
            }

            if(!ValidateTolerance(tolerance))
            {
                return new OperationResult(TriangleAngleType.Undefind, TriangleError.InvalidTolerance);
            }

            TriangleAngleType result = DefineTriangleTypeAfterInputValidation(firstSide, secondSide, thirdSide, tolerance);

            return new OperationResult(result, TriangleError.None);
        }
        private static TriangleError ValidateTriangle(double sideA, double sideB, double sideC)
        {
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
            {
                return TriangleError.SideLessOrEqualToZero;
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
        private static TriangleAngleType DefineTriangleTypeAfterInputValidation(double sideA, double sideB, double sideC, double tolerance = 1e-10)
        {
            double[] sides = new double[] { sideA, sideB, sideC };
            Array.Sort(sides);

            double a = sides[0];
            double b = sides[1];
            double c = sides[2];

            double sumOfSquares = a * a + b * b;
            double squareOfLongestSide = c * c;

            if (Math.Abs(sumOfSquares - squareOfLongestSide) < tolerance)
            {
                return TriangleAngleType.Right;
            }
            else if (sumOfSquares < squareOfLongestSide)
            {
                return TriangleAngleType.Obtuse;
            }
            else
            {
                return TriangleAngleType.Acute;
            }
        }
    }
}
