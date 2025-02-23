using RobotChampionData.Entities;
using RobotChampionData.Entities.Enums;

namespace RobotChampionData
{
    public partial class RobotForm : Form
    {

        private int rows;
        private int columns;
        private Button[,] buttonGrid;
        private Robot? robot;
        private Image robotImage;
        private Bitmap resizedRobotImage;
        public RobotForm()
        {
            InitializeComponent();
            CreateInputFields();
            this.CenterToScreen();
        }

        private void CreateInputFields()
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            Label lblX = new Label() { Text = ElementName.lblXText, Location = new System.Drawing.Point(10, 10) };
            TextBox txtX = new TextBox() { Name = ElementName.txtXName, Location = new System.Drawing.Point(10, 30), Width = 150 };
            Label lblY = new Label() { Text = ElementName.lblYText, Location = new System.Drawing.Point(10, 60) };
            TextBox txtY = new TextBox() { Name = ElementName.txtYName, Location = new System.Drawing.Point(10, 80), Width = 150 };
            Button btnGenerate = new Button() { Text = ElementName.btnGenerateText, Location = new System.Drawing.Point(10, 110) };
            btnGenerate.Click += BtnGenerate_Click;

            this.Controls.Add(lblX);
            this.Controls.Add(txtX);
            this.Controls.Add(lblY);
            this.Controls.Add(txtY);
            this.Controls.Add(btnGenerate);

            CreateNavigationButtons();
        }

        private void CreateNavigationButtons()
        {
            Button btnForward = new Button() { Text = ElementName.btnForwardText, Location = new System.Drawing.Point(10, 150) };
            Button btnLeft = new Button() { Text = ElementName.btnLeftText, Location = new System.Drawing.Point(110, 150) };
            Button btnRight = new Button() { Text = ElementName.btnRightText, Location = new System.Drawing.Point(210, 150) };

            Button btnInitRobot = new Button() { Text = ElementName.btnInitRobotText, Location = new System.Drawing.Point(10, 190) };
            Button btnReportRobotPosition = new Button() { Text = ElementName.btnReportPositionText, Location = new System.Drawing.Point(110, 190), AutoSize = true };
            Button btnRunRobotFromCommandFile = new Button() { Text = ElementName.btnAutoRunText, Location = new System.Drawing.Point(210, 190), AutoSize = true };

            Label lblRow = new Label() { Text = ElementName.lblRowText, Location = new System.Drawing.Point(10, 240), AutoSize = true };
            TextBox txtRow = new TextBox() { Name = ElementName.txtRowName, Location = new System.Drawing.Point(50, 240), Width = 50 };
            Label lblColumn = new Label() { Text = ElementName.lblColumnText, Location = new System.Drawing.Point(100, 240), AutoSize = true };
            TextBox txtColumn = new TextBox() { Name = ElementName.txtColumnName, Location = new System.Drawing.Point(160, 240), Width = 50 };

            Label lblDirection = new Label() { Text = ElementName.lblDirectionText, Location = new System.Drawing.Point(240, 240), AutoSize = true };
            ComboBox cmbDirection = new ComboBox() { Name = ElementName.cmbDirectionName, Location = new System.Drawing.Point(300, 240), Width = 100 };
            cmbDirection.Items.AddRange(new string[] { DirectionEnum.North.ToString(), DirectionEnum.South.ToString(), DirectionEnum.East.ToString(), DirectionEnum.West.ToString() });
            cmbDirection.SelectedIndex = 0;

            Button btnPlace = new Button() { Text = ElementName.btnPlaceText, Location = new System.Drawing.Point(420, 240) };
            btnPlace.Click += BtnPlace_Click;

            Label lblAction = new Label() { Name = ElementName.lblActionName, Location = new System.Drawing.Point(10, 280), AutoSize = true };
            Label lblReport = new Label() { Name = ElementName.lblReportPositionName, Location = new System.Drawing.Point(200, 280), AutoSize = true, ForeColor = Color.Red };

            btnInitRobot.Click += BtnInitRobot_Click;
            btnReportRobotPosition.Click += BtnReportPosition_Click;
            btnRunRobotFromCommandFile.Click += BtnRunRobotByCommand_Click;

            btnForward.Click += BtnForward_Click;
            btnLeft.Click += BtnLeft_Click;
            btnRight.Click += BtnRight_Click;

            this.Controls.Add(btnForward);
            this.Controls.Add(btnLeft);
            this.Controls.Add(btnRight);

            this.Controls.Add(btnInitRobot);
            this.Controls.Add(btnReportRobotPosition);
            this.Controls.Add(btnRunRobotFromCommandFile);

            this.Controls.Add(btnPlace);
            this.Controls.Add(lblRow);
            this.Controls.Add(txtRow);
            this.Controls.Add(lblColumn);
            this.Controls.Add(txtColumn);
            this.Controls.Add(lblDirection);
            this.Controls.Add(cmbDirection);
            this.Controls.Add(lblAction);
            this.Controls.Add(lblReport);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            TextBox txtX = (TextBox)this.Controls[ElementName.txtXName];
            TextBox txtY = (TextBox)this.Controls[ElementName.txtYName];

            if (int.TryParse(txtX.Text, out rows) && int.TryParse(txtY.Text, out columns))
            {
                GenerateCaroTable(rows, columns);
            }
            else
            {
                MessageBox.Show("Please enter valid numbers for X and Y.");
            }
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            MoveRobot(movement: RoboActionEnum.Forward);
        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            MoveRobot(movement: RoboActionEnum.Left);
        }

        private void BtnRight_Click(object sender, EventArgs e)
        {
            MoveRobot(movement: RoboActionEnum.Right);
        }


        private void BtnInitRobot_Click(object sender, EventArgs e)
        {
            InitRobot();
        }

        private void BtnReportPosition_Click(object sender, EventArgs e)
        {
            if (rows > 0 && columns > 0)
            {
                ReportPosition();
            }
        }

        private void BtnPlace_Click(object sender, EventArgs e)
        {
            TextBox txtRow = (TextBox)this.Controls[ElementName.txtRowName];
            TextBox txtColumn = (TextBox)this.Controls[ElementName.txtColumnName];
            ComboBox cmbDirection = (ComboBox)this.Controls[ElementName.cmbDirectionName];

            if (int.TryParse(txtRow.Text, out int row)
                && int.TryParse(txtColumn.Text, out int column)
                && Enum.TryParse(cmbDirection.Text, out DirectionEnum direction))
            {
                if (row > 0 && row <= rows && column > 0 && column <= columns)
                {
                    PlaceRobot(row, column, direction);
                }
                else
                {
                    MessageBox.Show("Please enter valid row and column positions.");
                }
            }
            else
            {
                MessageBox.Show("Please enter valid numbers for row and column.");
            }
        }

        private void BtnRunRobotByCommand_Click(object sender, EventArgs e)
        {
            RunRobotByCommandFile();
        }

        private void InitRobot()
        {
            if (rows > 0 && columns > 0)
            {
                ClearCurrentRobotPositionButton();
                Button southWestButton = buttonGrid[rows - 1, 0];
                setImageByDirection(DirectionEnum.East);
                // Resize the image to fit the button
                this.resizedRobotImage = new Bitmap(this.robotImage, new Size(southWestButton.Width, southWestButton.Height));
                southWestButton.Image = this.resizedRobotImage;
                southWestButton.ImageAlign = ContentAlignment.MiddleCenter;

                this.robot = Robot.Init(maxRows: this.rows, maxColumns: this.columns, rowPosition: rows, columnPosition: 1, initDirection: Entities.Enums.DirectionEnum.East);
            }
        }

        private void PlaceRobot(int row, int column, DirectionEnum direction)
        {
            ClearCurrentRobotPositionButton();

            Button targetButton = buttonGrid[row - 1, column - 1];
            setImageByDirection(direction);
            // Resize the image to fit the button
            this.resizedRobotImage = new Bitmap(this.robotImage, new Size(targetButton.Width, targetButton.Height));
            targetButton.Image = resizedRobotImage;
            targetButton.ImageAlign = ContentAlignment.MiddleCenter;

            Application.DoEvents(); // Allows UI to update
            System.Threading.Thread.Sleep(500);
            this.robot?.Place(row: row, column: column, direction: direction);
        }

        private void ReportPosition()
        {
            Label reportPositionLabel = (Label)this.Controls[ElementName.lblReportPositionName];
            var position = robot.Report();
            reportPositionLabel.Text = $"I'm in row: {position.row}, column: {position.column}, direction: {position.currentDirection}";
            Application.DoEvents(); // Allows UI to update
            System.Threading.Thread.Sleep(500);
        }

        private void RunRobotByCommandFile()
        {
            if (rows > 0 && columns > 0)
            {
                string[] lines = File.ReadAllLines("commands.txt");

                foreach (string line in lines)
                {
                    if (Enum.TryParse(line, true, out RoboActionEnum movement))
                    {
                        MoveRobot(movement, line);
                        continue;
                    }
                }
            }
        }

        private void MoveRobot(RoboActionEnum movement, string positionInfo = "")
        {
            if (rows > 0 && columns > 0 && this.robot != null)
            {
                ClearCurrentRobotPositionButton();
                switch (movement)
                {
                    case RoboActionEnum.Forward:
                        this.robot.MoveForward();
                        break;

                    case RoboActionEnum.Left:
                        this.robot.MoveLeft();
                        break;

                    case RoboActionEnum.Right:
                        this.robot.MoveRight();
                        break;

                    case RoboActionEnum.Place:
                        var value = positionInfo.ToLower().Replace("place", "").Trim();
                        string[] position = value.Split(',');
                        if (position.Length < 2)
                        {
                            break;
                        }

                        if (int.TryParse(position[0].Trim(), out int row)
                            && int.TryParse(position[1].Trim(), out int column)
                            && Enum.TryParse(position[2].Trim(), ignoreCase: true, out DirectionEnum direction))
                        {
                            PlaceRobot(row, column, direction);
                        }
                        break;

                    case RoboActionEnum.Report:
                        ReportPosition();
                        break;
                    default:
                        break;
                }

                Button afterMovementPositionButton = buttonGrid[this.robot.rowPosition - 1, this.robot.columnPosition - 1];
                afterMovementPositionButton.Image = this.resizedRobotImage;
                afterMovementPositionButton.ImageAlign = ContentAlignment.MiddleCenter;
                Label actionLabel = (Label)this.Controls[ElementName.lblActionName];
                actionLabel.Text = movement.ToString();
                Application.DoEvents(); // Allows UI to update
                System.Threading.Thread.Sleep(500);
            }
        }


        private void setImageByDirection(DirectionEnum direction)
        {
            switch (direction)
            {
                case DirectionEnum.North:
                    robotImage = Image.FromFile(Properties.arrowNorth);
                    break;
                case DirectionEnum.South:
                    robotImage = Image.FromFile(Properties.arrowSouth);
                    break;

                case DirectionEnum.East:
                    robotImage = Image.FromFile(Properties.arrowEast);
                    break;

                case DirectionEnum.West:
                    robotImage = Image.FromFile(Properties.arrowWest);
                    break;

                default:
                    robotImage = Image.FromFile(Properties.arrowNorth);
                    break;
            }
        }

        private void ClearCurrentRobotPositionButton()
        {
            if (this.robot != null)
            {
                Button currentPositionBtn = buttonGrid[this.robot.rowPosition - 1, this.robot.columnPosition - 1];
                currentPositionBtn.Image = null;
            }
        }


        private void GenerateCaroTable(int rows, int columns)
        {
            if (buttonGrid != null)
            {
                foreach (Button button in buttonGrid)
                {
                    this.Controls.Remove(button);
                }
            }

            buttonGrid = new Button[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Button btn = new Button
                    {
                        Size = new System.Drawing.Size(40, 40), // Made buttons larger
                        Location = new System.Drawing.Point(10 + j * 42, 300 + i * 42) // Brought buttons closer
                    };
                    this.Controls.Add(btn);
                    buttonGrid[i, j] = btn;
                }
            }

            // Adjust form size to fit the button grid
            this.ClientSize = new System.Drawing.Size(20 + columns * 42, 370 + rows * 42);
        }
    }
}
