namespace TriangleAngleAnalysis.xUnitTest
{
    using TriangleAngleAnalysis.Entities;

    public class TriangleAngleAnalyserTests
    {
        [Theory]
        [InlineData(3, 4, 5, TriangleAngleType.Right)]      // Right
        [InlineData(5, 12, 13, TriangleAngleType.Right)]    // Right
        [InlineData(6, 8, 10, TriangleAngleType.Right)]     // Right
        [InlineData(2, 2, 2.5, TriangleAngleType.Acute)]  // Acute
        [InlineData(4, 5, 6, TriangleAngleType.Acute)]    // Acute
        [InlineData(5, 5, 7, TriangleAngleType.Acute)]    // Acute
        [InlineData(3, 6, 7, TriangleAngleType.Obtuse)]    // Obtuse
        [InlineData(4, 5, 8, TriangleAngleType.Obtuse)]    // Obtuse
        [InlineData(10, 10, 15, TriangleAngleType.Obtuse)] // Obtuse
        public void DefineTriangleType_Test(double a, double b, double c, TriangleAngleType expected)
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
            Assert.Equal(TriangleAngleType.Undefind, result.Result);
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
            Assert.Equal(TriangleAngleType.Undefind, result.Result);
            Assert.Equal(TriangleError.TriangleInequalityViolation, result.Error);
        }

    }
}