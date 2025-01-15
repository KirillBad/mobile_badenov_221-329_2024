using Checkers.Core;

namespace Checkers
{
    public partial class Form1 : Form
    {
        private const int CellSize = 60;
        private const int LabelSize = 20;
        private const int TopOffset = 40;
        private Game game;
        private Panel[,] cells;
        private Image whiteChecker;
        private Image blackChecker;
        private Image whiteKing;
        private Image blackKing;
        private Button newGameButton;
        private ComputerPlayer computerPlayer;
        private bool isComputerGame;
        private Player humanPlayer;
        private Button gameModeButton;

        public Form1()
        {
            InitializeComponent();
            LoadImages();
            game = new Game();
            
            int formWidth = (Board.BoardSize * CellSize) + (2 * LabelSize);
            int formHeight = (Board.BoardSize * CellSize) + (2 * LabelSize) + TopOffset + 10;
            this.ClientSize = new Size(formWidth, formHeight);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            
            CreateButtons();
            CreateBoard();
            CreateLabels();
            UpdateBoard();
        }

        private void LoadImages()
        {
            whiteChecker = Properties.Resources.white_checker;
            blackChecker = Properties.Resources.black_checker;
            whiteKing = Properties.Resources.white_king;
            blackKing = Properties.Resources.black_king;
            
            whiteChecker = new Bitmap(whiteChecker, new Size(CellSize - 10, CellSize - 10));
            blackChecker = new Bitmap(blackChecker, new Size(CellSize - 10, CellSize - 10));
            whiteKing = new Bitmap(whiteKing, new Size(CellSize - 10, CellSize - 10));
            blackKing = new Bitmap(blackKing, new Size(CellSize - 10, CellSize - 10));
        }

        private void CreateBoard()
        {
            cells = new Panel[Board.BoardSize, Board.BoardSize];
            
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    var cell = new Panel
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(
                            col * CellSize + LabelSize,
                            row * CellSize + LabelSize + TopOffset
                        ),
                        BackColor = (row + col) % 2 == 0 ? Color.Wheat : Color.SaddleBrown
                    };
                    
                    cell.Tag = new Position(row, col);
                    cell.Click += cell_Click;
                    
                    this.Controls.Add(cell);
                    cells[row, col] = cell;
                }
            }
        }

        private void CreateLabels()
        {
            for (int col = 0; col < Board.BoardSize; col++)
            {
                var label = new Label
                {
                    Text = ((char)('A' + col)).ToString(),
                    AutoSize = false,
                    Size = new Size(CellSize, LabelSize),
                    Location = new Point(col * CellSize + LabelSize, TopOffset),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                this.Controls.Add(label);

                var bottomLabel = new Label
                {
                    Text = ((char)('A' + col)).ToString(),
                    AutoSize = false,
                    Size = new Size(CellSize, LabelSize),
                    Location = new Point(col * CellSize + LabelSize, Board.BoardSize * CellSize + LabelSize + TopOffset),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                this.Controls.Add(bottomLabel);
            }

            for (int row = 0; row < Board.BoardSize; row++)
            {
                var label = new Label
                {
                    Text = (Board.BoardSize - row).ToString(),
                    AutoSize = false,
                    Size = new Size(LabelSize, CellSize),
                    Location = new Point(0, row * CellSize + LabelSize + TopOffset),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                this.Controls.Add(label);

                var rightLabel = new Label
                {
                    Text = (Board.BoardSize - row).ToString(),
                    AutoSize = false,
                    Size = new Size(LabelSize, CellSize),
                    Location = new Point(Board.BoardSize * CellSize + LabelSize, row * CellSize + LabelSize + TopOffset),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                this.Controls.Add(rightLabel);
            }
        }

        private void cell_Click(object sender, EventArgs e)
        {
            var cell = (Panel)sender;
            var position = (Position)cell.Tag;
            
            if (game.SelectedPosition == null)
            {
                var piece = game.Board.GetPiece(position.Row, position.Col);
                if (piece?.Owner == game.CurrentPlayer)
                {
                    game.SelectPosition(position);
                }
            }
            else
            {
                if (game.PossibleMoves.Contains(position))
                {
                    var move = new Move(game.SelectedPosition, position);
                    game.MakeMove(move);
                    
                    if (!game.IsGameOver && isComputerGame && game.CurrentPlayer == Player.Black)
                    {
                        MakeComputerMove();
                    }
                }
                else
                {
                    game.ClearSelection();
                }
            }
            
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            var allCaptures = game.GetAllCaptureMoves();
            bool hasCaptures = allCaptures.Any();

            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    var cell = cells[row, col];
                    cell.Controls.Clear();
                    cell.BackgroundImage = null;
                    cell.BackColor = (row + col) % 2 == 0 ? Color.Wheat : Color.SaddleBrown;
                    
                    var position = new Position(row, col);
                    var piece = game.Board.GetPiece(row, col);
                    
                    if (game.IsCapturing && position == game.SelectedPosition)
                    {
                        cell.BackColor = Color.Green;
                    }
                    else if (hasCaptures && allCaptures.ContainsKey(position))
                    {
                        cell.BackColor = Color.Green;
                    }
                    else if (position == game.SelectedPosition)
                    {
                        cell.BackColor = Color.LightGreen;
                    }
                    
                    if (game.PossibleMoves.Contains(position))
                    {
                        cell.BackColor = Color.LightBlue;
                    }
                    
                    if (piece != null)
                    {
                        cell.BackgroundImage = piece.IsKing ? 
                            (piece.Owner == Player.White ? whiteKing : blackKing) :
                            (piece.Owner == Player.White ? whiteChecker : blackChecker);
                        cell.BackgroundImageLayout = ImageLayout.Center;
                    }
                }
            }

            if (game.IsGameOver && game.Winner.HasValue)
            {
                string winner = game.Winner == Player.White ? "White" : "Black";
                DialogResult result = MessageBox.Show(
                    $"{winner} won! New game?",
                    "Game over",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                    
                if (result == DialogResult.Yes)
                {
                    game.Reset();
                }
            }
        }

        private void CreateButtons()
        {
            newGameButton = new Button
            {
                Text = "New game",
                Size = new Size(100, 30),
                Location = new Point(
                    (this.ClientSize.Width - 220) / 2,
                    5
                ),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            newGameButton.Click += NewGame_Click;
            this.Controls.Add(newGameButton);

            gameModeButton = new Button
            {
                Text = "vs Human",
                Size = new Size(100, 30),
                Location = new Point(
                    ((this.ClientSize.Width - 220) / 2) + 120,
                    5
                ),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            gameModeButton.Click += GameMode_Click;
            this.Controls.Add(gameModeButton);
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            game = new Game();
            game.Board.InitializeBoard();
            UpdateBoard();
        }

        private void GameMode_Click(object sender, EventArgs e)
        {
            isComputerGame = !isComputerGame;
            gameModeButton.Text = isComputerGame ? "vs Computer" : "vs Human";

            if (isComputerGame)
            {
                computerPlayer = new ComputerPlayer();
                humanPlayer = Player.White;
            }
            
            game = new Game();
            UpdateBoard();
        }

        private void MakeComputerMove()
        {
            if (game.IsGameOver) return;

            var move = computerPlayer.GetMove(game);
            if (move != null)
            {
                game.SelectPosition(move.From);
                game.MakeMove(move);
                
                UpdateBoard();

                if (game.IsCapturing && game.CurrentPlayer == Player.Black)
                {
                    MakeComputerMove();
                }
            }
        }
    }
}
