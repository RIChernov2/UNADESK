namespace TriangleAngleAnalysis.xUnitTest
{
    using TriangleAngleAnalysis.Entities;

    public class TriangleAngleAnalyserTests
    {
        [Theory]
        [InlineData(3, 4, 5, TriangleType.Right)]      // Right
        [InlineData(5, 12, 13, TriangleType.Right)]    // Right
        [InlineData(6, 8, 10, TriangleType.Right)]     // Right
        [InlineData(2, 2, 2.5, TriangleType.Acute)]    // Acute
        [InlineData(4, 5, 6, TriangleType.Acute)]      // Acute
        [InlineData(5, 5, 7, TriangleType.Acute)]      // Acute
        [InlineData(3, 6, 7, TriangleType.Obtuse)]     // Obtuse
        [InlineData(4, 5, 8, TriangleType.Obtuse)]     // Obtuse
        [InlineData(10, 10, 15, TriangleType.Obtuse)]  // Obtuse
        public void DefineTriangleType_Test(double a, double b, double c, TriangleType expected)
        {

            // Act
            var result = TriangleAngleAnalyser.DefineTriangleType(a, b, c);

            // Assert
            Assert.Equal(expected, result.Result);
            Assert.Equal(TriangleError.None, result.Error);
        }

        [Theory]
        [InlineData(-3, 4, 5)]    // Negative side 1
        [InlineData(3, -4, 5)]    // Negative side 2
        [InlineData(3, 4, -5)]    // Negative side 3
        [InlineData(0, 4, 5)]     // Zero side 1
        [InlineData(3, 0, 5)]     // Zero side 2
        [InlineData(3, 4, 0)]     // Zero side 3
        public void DefineTriangleType_SideLessOrEqualToZero_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.DefineTriangleType(a, b, c);

            // Assert
            Assert.Equal(TriangleType.Undefind, result.Result);
            Assert.Equal(TriangleError.SideLessOrEqualToZero, result.Error);
        }

        [Theory]
        [InlineData(1, 2, 5)]    // a + b < c
        [InlineData(2, 1, 5)]    // b + c < a
        [InlineData(1, 5, 2)]    // a + c < b
        public void DefineTriangleType_TriangleInequalityViolation_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.DefineTriangleType(a, b, c);

            // Assert
            Assert.Equal(TriangleType.Undefind, result.Result);
            Assert.Equal(TriangleError.TriangleInequalityViolation, result.Error);
        }

        [Theory]
        [InlineData(0.000003, 0.000004, 0.0000051, TriangleType.Right, 1)]         // Right
        [InlineData(0.000003, 0.000004, 0.0000051, TriangleType.Obtuse, 1e-12)]    // Obtuse
        [InlineData(0.000003, 0.000004, 0.0000045, TriangleType.Right)]            // Right
        [InlineData(0.000003, 0.000004, 0.0000045, TriangleType.Acute, 1e-12)]     // Acute
        public void DefineTriangleType_CheckTolerance_Test(double a, double b, double c, TriangleType expectedType, double tolerance = 1e-10)
        {
            // Act
            var result = TriangleAngleAnalyser.DefineTriangleType(a, b, c, tolerance);

            // Assert
            Assert.Equal(expectedType, result.Result);
            Assert.Equal(TriangleError.None, result.Error);
        }

        [Fact]
        public void DefineTriangleType_CheckPotentialOverflow_Test()
        {
            // Arrange
            double a = double.MaxValue / 1e+150;
            double b = double.MaxValue / 1e+150;
            double c = Math.Sqrt(double.MaxValue);

            // Act
            var result = TriangleAngleAnalyser.DefineTriangleType(a, b, c);

            // Assert
            Assert.Equal(TriangleType.Undefind, result.Result);
            Assert.Equal(TriangleError.PotentialOverflow, result.Error);
        }
    }
}