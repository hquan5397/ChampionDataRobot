using RobotChampionData.Entities;
using RobotChampionData.Entities.Enums;

namespace RobotChampionData.UnitTest
{
    public class RobotEntityReportActionTests
    {
        [Fact]
        public void ReportRobotPosition_ShouldSucceed()
        {
            // arrange
            int maxRows = 5;
            int maxColumns = 5;

            int initRowPosition = 1;
            int initColumnPosition = 1;
            var initDirection = DirectionEnum.East;

            var robot = Robot.Init(maxRows, maxColumns, initRowPosition, initColumnPosition, initDirection);

            // act
            var firstReport = robot.Report();

            int newRowPosition = 5;
            int newColumnPosition = 5;
            var newDirection = DirectionEnum.North;
            robot.Place(row: newRowPosition, column: newColumnPosition, direction: newDirection);

            var secondReport = robot.Report();

            // assert
            Assert.Equal(initRowPosition, firstReport.row);
            Assert.Equal(initColumnPosition, firstReport.column);
            Assert.Equal(initDirection, firstReport.currentDirection);

            Assert.Equal(newRowPosition, secondReport.row);
            Assert.Equal(newColumnPosition, secondReport.column);
            Assert.Equal(newDirection, secondReport.currentDirection);

        }
    }
}
