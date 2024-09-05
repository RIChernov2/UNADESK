namespace TriangleAngleAnalysis.xUnitTest
{
    using TriangleAngleAnalysis.Entities;

    public class TriangleAngleAnalyserTests
    {
        #region Main logic

        [Theory]
        [InlineData(3, 4, 5, true)]    // Right
        [InlineData(5, 12, 13, true)]   // Right
        [InlineData(6, 8, 10, true)]    // Right
        [InlineData(2, 2, 2.5, false)]  // Acute
        [InlineData(4, 5, 6, false)]    // Acute
        [InlineData(5, 5, 7, false)]    // Acute
        [InlineData(3, 6, 7, false)]    // Obtuse
        [InlineData(4, 5, 8, false)]    // Obtuse
        [InlineData(10, 10, 15, false)] // Obtuse
        public void CheckIfRightAngle_Test(double a, double b, double c, bool expected)
        {

            // Act
            var result = TriangleAngleAnalyser.CheckIfRightAngle(a, b, c);

            // Assert
            Assert.Equal(expected, result.Result);
            Assert.Equal(TriangleError.None, result.Error);
        }

        [Theory]
        [InlineData(3, 4, 5, false)]    // Right
        [InlineData(5, 12, 13, false)]   // Right
        [InlineData(6, 8, 10, false)]    // Right
        [InlineData(2, 2, 2.5, true)]    // Acute
        [InlineData(4, 5, 6, true)]      // Acute
        [InlineData(5, 5, 7, true)]      // Acute
        [InlineData(3, 6, 7, false)]     // Obtuse
        [InlineData(4, 5, 8, false)]     // Obtuse
        [InlineData(10, 10, 15, false)]  // Obtuse
        public void CheckIfAcuteAngle_Test(double a, double b, double c, bool expected)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfAcuteAngle(a, b, c);

            // Assert
            Assert.Equal(expected, result.Result);
            Assert.Equal(TriangleError.None, result.Error);
        }

        [Theory]
        [InlineData(3, 4, 5, false)]    // Right
        [InlineData(5, 12, 13, false)]   // Right
        [InlineData(6, 8, 10, false)]    // Right
        [InlineData(2, 2, 2.5, false)]   // Acute
        [InlineData(4, 5, 6, false)]     // Acute
        [InlineData(5, 5, 7, false)]     // Acute
        [InlineData(3, 6, 7, true)]      // Obtuse
        [InlineData(4, 5, 8, true)]      // Obtuse
        [InlineData(10, 10, 15, true)]   // Obtuse
        public void CheckIfObtuseAngle_Test(double a, double b, double c, bool expected)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfObtuseAngle(a, b, c);

            // Assert
            Assert.Equal(expected, result.Result);
            Assert.Equal(TriangleError.None, result.Error);
        } 

        #endregion

        #region InvalidInput

        [Theory]
        [InlineData(-3, 4, 5)]    // Negative side 1
        [InlineData(3, -4, 5)]    // Negative side 2
        [InlineData(3, 4, -5)]    // Negative side 3
        [InlineData(0, 4, 5)]     // Zero side 1
        [InlineData(3, 0, 5)]     // Zero side 2
        [InlineData(3, 4, 0)]     // Zero side 3
        public void CheckIfRightAngle_InvalidInput_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfRightAngle(a, b, c);

            // Assert
            Assert.False(result.Result);
            Assert.Equal(TriangleError.SideLessOrEqualToZero, result.Error);
        }

        [Theory]
        [InlineData(-3, 4, 5)]    // Negative side 1
        [InlineData(3, -4, 5)]    // Negative side 2
        [InlineData(3, 4, -5)]    // Negative side 3
        [InlineData(0, 4, 5)]     // Zero side 1
        [InlineData(3, 0, 5)]     // Zero side 2
        [InlineData(3, 4, 0)]     // Zero side 3
        public void CheckIfAcuteAngle_InvalidInput_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfAcuteAngle(a, b, c);

            // Assert
            Assert.False(result.Result);
            Assert.Equal(TriangleError.SideLessOrEqualToZero, result.Error);
        }

        [Theory]
        [InlineData(-3, 4, 5)]    // Negative side 1
        [InlineData(3, -4, 5)]    // Negative side 2
        [InlineData(3, 4, -5)]    // Negative side 3
        [InlineData(0, 4, 5)]     // Zero side 1
        [InlineData(3, 0, 5)]     // Zero side 2
        [InlineData(3, 4, 0)]     // Zero side 3
        public void CheckIfObtuseAngle_InvalidInput_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfObtuseAngle(a, b, c);

            // Assert
            Assert.False(result.Result);
            Assert.Equal(TriangleError.SideLessOrEqualToZero, result.Error);
        }

        #endregion

        #region InvalidTriangle

        [Theory]
        [InlineData(1, 2, 5)]    // a + b < c
        [InlineData(2, 1, 5)]    // b + c < a
        [InlineData(1, 5, 2)]    // a + c < b
        public void CheckIfRightAngle_InvalidTriangle_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfRightAngle(a, b, c);

            // Assert
            Assert.False(result.Result);
            Assert.Equal(TriangleError.TriangleInequalityViolation, result.Error);
        }

        [Theory]
        [InlineData(1, 2, 5)]    // a + b < c
        [InlineData(2, 1, 5)]    // b + c < a
        [InlineData(1, 5, 2)]    // a + c < b
        public void CheckIfAcuteAngle_InvalidTriangle_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfAcuteAngle(a, b, c);

            // Assert
            Assert.False(result.Result);
            Assert.Equal(TriangleError.TriangleInequalityViolation, result.Error);
        }

        [Theory]
        [InlineData(1, 2, 5)]    // a + b < c
        [InlineData(2, 1, 5)]    // b + c < a
        [InlineData(1, 5, 2)]    // a + c < b
        public void CheckIfObtuseAngle_InvalidTriangle_Test(double a, double b, double c)
        {
            // Act
            var result = TriangleAngleAnalyser.CheckIfObtuseAngle(a, b, c);

            // Assert
            Assert.False(result.Result);
            Assert.Equal(TriangleError.TriangleInequalityViolation, result.Error);
        }

        #endregion

    }
}