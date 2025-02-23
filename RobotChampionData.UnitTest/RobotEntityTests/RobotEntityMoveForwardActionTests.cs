using RobotChampionData.Entities;
using RobotChampionData.Entities.Enums;

namespace RobotChampionData.UnitTest
{
    public class RobotEntityMoveForwardActionTests
    {
        [Fact]
        public void MoveRobot_Forward_ShouldSucceed()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.East;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveForward();

            // assert
            Assert.Equal(initColumnPosition + 1, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingEastDirection_Forward_InTheEastBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 5;
            var initDirection = DirectionEnum.East;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveForward();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingWestDirection_Forward_InTheWestBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.West;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveForward();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingNorthDirection_Forward_InTheNorthBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.North;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveForward();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingSouthDirection_Forward_InTheSouthBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 5;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.South;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveForward();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }
    }
}
