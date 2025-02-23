using RobotChampionData.Entities;
using RobotChampionData.Entities.Enums;

namespace RobotChampionData.UnitTest
{
    public class RobotEntityMoveRightActionTests
    {
        [Fact]
        public void MoveRobot_Right_ShouldSucceed()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 4;
            int initColumnPosition = 5;
            var initDirection = DirectionEnum.East;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveRight();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition + 1, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingEastDirection_Right_InTheSouthBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 5;
            int initColumnPosition = 2;
            var initDirection = DirectionEnum.East;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveRight();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingWestDirection_Right_InTheNorthBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 2;
            var initDirection = DirectionEnum.West;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveRight();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingNorthDirection_Right_InTheEastBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 5;
            var initDirection = DirectionEnum.North;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveRight();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }

        [Fact]
        public void MoveRobotFacingSouthDirection_Right_InTheWestBorder_ShouldNotChangeCurrentPosition()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.South;
            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            robot.MoveRight();

            // assert
            Assert.Equal(initColumnPosition, robot.columnPosition);
            Assert.Equal(initRowPosition, robot.rowPosition);
            Assert.Equal(initDirection, robot.currentDirection);
        }
    }
}
