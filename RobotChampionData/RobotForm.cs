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

            Label lblX = new Label() { Text = "X:", Location = new System.Drawing.Point(10, 10) };
            TextBox txtX = new TextBox() { Name = "txtX", Location = new System.Drawing.Point(10, 30), Width = 150 };
            Label lblY = new Label() { Text = "Y:", Location = new System.Drawing.Point(10, 60) };
            TextBox txtY = new TextBox() { Name = "txtY", Location = new System.Drawing.Point(10, 80), Width = 150 };
            Button btnGenerate = new Button() { Text = "Generate", Location = new System.Drawing.Point(10, 110) };
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
            Button btnForward = new Button() { Text = "Forward", Location = new System.Drawing.Point(10, 150) };
            Button btnLeft = new Button() { Text = "Left", Location = new System.Drawing.Point(110, 150) };
            Button btnRight = new Button() { Text = "Right", Location = new System.Drawing.Point(210, 150) };

            Button btnInitRobot = new Button() { Text = "Init Robot", Location = new System.Drawing.Point(10, 190) };
            Button btnReportRobotPosition = new Button() { Text = "Report position", Location = new System.Drawing.Point(110, 190), AutoSize = true };
            Button btnRunRobotFromCommandFile = new Button() { Text = "Run auto", Location = new System.Drawing.Point(210, 190), AutoSize = true };
            Label lblRow = new Label() { Text = "Row:", Location = new System.Drawing.Point(10, 240), AutoSize = true };
            TextBox txtRow = new TextBox() { Name = "txtRow", Location = new System.Drawing.Point(40, 240), Width = 50 };
            Label lblColumn = new Label() { Text = "Column:", Location = new System.Drawing.Point(90, 240), AutoSize = true };
            TextBox txtColumn = new TextBox() { Name = "txtColumn", Location = new System.Drawing.Point(140, 240), Width = 50 };

            Label lblDirection = new Label() { Text = "Direction:", Location = new System.Drawing.Point(190, 240), AutoSize = true };
            ComboBox cmbDirection = new ComboBox() { Name = "cmbDirection", Location = new System.Drawing.Point(250, 240), Width = 100 };
            cmbDirection.Items.AddRange(new string[] { DirectionEnum.North.ToString(), DirectionEnum.South.ToString(), DirectionEnum.East.ToString(), DirectionEnum.West.ToString() });
            cmbDirection.SelectedIndex = 0;

            Button btnPlace = new Button() { Text = "Place", Location = new System.Drawing.Point(360, 240) };
            btnPlace.Click += BtnPlace_Click;

            Label lblAction = new Label() { Name = "labelAction", Location = new System.Drawing.Point(10, 280), AutoSize = true };

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
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            TextBox txtX = (TextBox)this.Controls["txtX"];
            TextBox txtY = (TextBox)this.Controls["txtY"];

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
            MoveRobot(movement: RobotMovementEnum.Forward);
        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            MoveRobot(movement: RobotMovementEnum.Left);
        }

        private void BtnRight_Click(object sender, EventArgs e)
        {
            MoveRobot(movement: RobotMovementEnum.Right);
        }


        private void BtnInitRobot_Click(object sender, EventArgs e)
        {
            InitRobot();
        }

        private void BtnReportPosition_Click(object sender, EventArgs e)
        {
            if (rows > 0 && columns > 0)
            {
                MessageBox.Show($"Current robot position: row {this.robot.rowPosition}, column {this.robot.columnPosition}, facing direction is {this.robot.currentDirection}");
            }
        }

        private void BtnPlace_Click(object sender, EventArgs e)
        {
            TextBox txtRow = (TextBox)this.Controls["txtRow"];
            TextBox txtColumn = (TextBox)this.Controls["txtColumn"];
            ComboBox cmbDirection = (ComboBox)this.Controls["cmbDirection"];

            if (int.TryParse(txtRow.Text, out int row) && int.TryParse(txtColumn.Text, out int column) && Enum.TryParse<DirectionEnum>(cmbDirection.Text, out DirectionEnum direction))
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

            this.robot?.Place(row: row, column: column, direction: direction);
        }

        private void RunRobotByCommandFile()
        {
            if (rows > 0 && columns > 0)
            {
                string[] lines = File.ReadAllLines("commands.txt");

                foreach (string line in lines)
                {
                    if (Enum.TryParse(line, true, out RobotMovementEnum movement))
                    {
                        MoveRobot(movement);
                    }
                }
            }
        }

        private void MoveRobot(RobotMovementEnum movement)
        {
            if (rows > 0 && columns > 0 && this.robot != null)
            {
                ClearCurrentRobotPositionButton();
                switch (movement)
                {
                    case RobotMovementEnum.Forward:
                        this.robot.MoveForward();
                        break;

                    case RobotMovementEnum.Left:
                        this.robot.MoveLeft();
                        break;

                    case RobotMovementEnum.Right:
                        this.robot.MoveRight();
                        break;

                    default:
                        break;
                }

                Button afterMovementPositionButton = buttonGrid[this.robot.rowPosition - 1, this.robot.columnPosition - 1];
                afterMovementPositionButton.Image = this.resizedRobotImage;
                afterMovementPositionButton.ImageAlign = ContentAlignment.MiddleCenter;
                Label actionLabel = (Label)this.Controls["labelAction"];
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
