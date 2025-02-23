using RobotChampionData.Entities;
using RobotChampionData.Entities.Enums;

namespace RobotChampionData.UnitTest
{
    public class RobotEntityPlaceActionTests
    {
        [Fact]
        public void PlaceRobot_WithValidData_ShouldSucceed()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.East;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            int newRowPosition = 5;
            int newColumnPosition = 5;
            var newDirection = DirectionEnum.North;
            robot.Place(row: newRowPosition, column: newColumnPosition, direction: newDirection);

            // assert
            Assert.Equal(newRowPosition, robot.rowPosition);
            Assert.Equal(newColumnPosition, robot.columnPosition);
            Assert.Equal(newDirection, robot.currentDirection);
        }

        [Fact]
        public void PlaceRobot_WithInValidData_ShouldFailed()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.East;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            int newRowPosition = 6;
            int newColumnPosition = 5;
            var newDirection = DirectionEnum.North;
            var exception = Assert.ThrowsAny<Exception>(() => robot.Place(row: newRowPosition, column: newColumnPosition, direction: newDirection));

            // assert
            Assert.NotNull(exception);
        }
    }
}
