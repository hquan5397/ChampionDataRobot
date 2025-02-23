using RobotChampionData.Entities.Enums;

namespace RobotChampionData.Entities
{
    public class Robot
    {
        public int maxColumns { get; set; }

        public int maxRows { get; set; }

        public int rowPosition { get; set; }

        public int columnPosition { get; set; }

        public DirectionEnum currentDirection { get; set; }

        private Robot()
        {

        }

        public static Robot Init(int maxRows, int maxColumns, int rowPosition, int columnPosition, DirectionEnum initDirection)
        {
            var robot = new Robot();
            if (maxRows <= 0)
            {
                throw new Exception($"Max number of row must be larger than 0, error value: {maxRows}");
            }

            if (maxColumns <= 0)
            {
                throw new Exception($"Max number of column must be larger than 0, error value: {maxColumns}");
            }

            if (rowPosition > maxRows || rowPosition <= 0)
            {
                throw new Exception($"row must be from 1 to {maxRows}");
            }

            if (columnPosition > maxColumns || columnPosition <= 0)
            {
                throw new Exception($"column must be from 1 to {maxColumns}");
            }

            robot.maxRows = maxRows;
            robot.maxColumns = maxColumns;
            robot.rowPosition = rowPosition;
            robot.columnPosition = columnPosition;
            robot.currentDirection = initDirection;
            return robot;
        }

        public void Place(int row, int column, DirectionEnum direction)
        {
            if (row > this.maxRows || row <= 0)
            {
                throw new Exception($"row must be from 1 to {maxRows}");
            }

            if (column > this.maxColumns || column <= 0)
            {
                throw new Exception($"column must be from 1 to {maxColumns}");
            }

            this.columnPosition = row;
            this.rowPosition = column;
            this.currentDirection = direction;
        }

        public void MoveForward()
        {
            switch (this.currentDirection)
            {
                case DirectionEnum.North:
                    {
                        this.rowPosition = IsAtNorthBorder() ? this.rowPosition : this.rowPosition -= 1;
                        break;
                    }

                case DirectionEnum.South:
                    {
                        this.rowPosition = IsAtSouthBorder() ? this.rowPosition : this.rowPosition += 1;
                        break;
                    }

                case DirectionEnum.East:
                    {
                        this.columnPosition = IsAtEastBorder() ? this.columnPosition : this.columnPosition += 1;
                        break;
                    }

                case DirectionEnum.West:
                    {
                        this.columnPosition = IsAtWestBorder() ? this.columnPosition : this.columnPosition -= 1;
                        break;
                    }

                default:
                    break;
            }
        }

        public void MoveLeft()
        {
            switch (this.currentDirection)
            {
                case DirectionEnum.North:
                    {
                        this.columnPosition = IsAtWestBorder() ? this.columnPosition : this.columnPosition -= 1;
                        break;
                    }

                case DirectionEnum.South:
                    {
                        this.columnPosition = IsAtEastBorder() ? this.columnPosition : this.columnPosition += 1;
                        break;
                    }

                case DirectionEnum.East:
                    {
                        this.rowPosition = IsAtNorthBorder() ? this.rowPosition : this.rowPosition -= 1;
                        break;
                    }

                case DirectionEnum.West:
                    {
                        this.rowPosition = IsAtSouthBorder() ? this.rowPosition : this.rowPosition += 1;
                        break;
                    }

                default:
                    break;
            }
        }

        public void MoveRight()
        {
            switch (this.currentDirection)
            {
                case DirectionEnum.North:
                    {
                        this.columnPosition = IsAtEastBorder() ? this.columnPosition : this.columnPosition += 1;
                        break;
                    }

                case DirectionEnum.South:
                    {
                        this.columnPosition = IsAtWestBorder() ? this.columnPosition : this.columnPosition -= 1;
                        break;
                    }

                case DirectionEnum.East:
                    {
                        this.rowPosition = IsAtSouthBorder() ? this.rowPosition : this.rowPosition += 1;
                        break;
                    }

                case DirectionEnum.West:
                    {
                        this.rowPosition = IsAtNorthBorder() ? this.rowPosition : this.rowPosition -= 1;
                        break;
                    }

                default:
                    break;
            }
        }

        public (int row, int column, DirectionEnum currentDirection) Report()
        {
            return (this.rowPosition, this.columnPosition, this.currentDirection);
        }

        public bool IsAtNorthBorder()
        {
            return this.rowPosition == 1;
        }

        public bool IsAtSouthBorder()
        {
            return this.rowPosition == this.maxRows;
        }

        public bool IsAtEastBorder()
        {
            return this.columnPosition == this.maxColumns;
        }

        public bool IsAtWestBorder()
        {
            return this.columnPosition == 1;
        }
    }
}
