using RobotChampionData.Entities;
using RobotChampionData.Entities.Enums;

namespace RobotChampionData.UnitTest
{
    public class RobotEntityInitActionTests
    {
        [Fact]
        public void InitRobot_WithValidData_ShouldSucceed()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.East;

            // act
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // assert
            Assert.Equal(maxRows, robot.maxRows);
            Assert.Equal(maxColumns, robot.maxColumns);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void InitRobot_WithInvalidData_ShouldFailed()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 0;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.East;

            // act
            var exception = Assert.ThrowsAny<Exception>(() => Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection));

            // assert
            Assert.NotNull(exception);
        }
    }
}
