using Checkers.Core.Services;

namespace Checkers.Core
{
    public class Game
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; set; }
        public Position? SelectedPosition { get; set; }
        public List<Position> PossibleMoves { get; set; }
        public bool IsGameOver { get; set; }
        public Player? Winner { get; set; }
        public bool IsCapturing { get; set; }
        public Position? CapturePosition { get; set; }

        public Game()
        {
            Board = new Board();
            PossibleMoves = new List<Position>();
            Reset();
        }

        public void SelectPosition(Position position)
        {
            var piece = Board.GetPiece(position.Row, position.Col);
            if (piece?.Owner == CurrentPlayer)
            {
                var allCaptures = GetAllCaptureMoves();
                
                if (allCaptures.Any() && !allCaptures.ContainsKey(position))
                {
                    return;
                }

                if (IsCapturing && position != CapturePosition)
                {
                    return;
                }

                SelectedPosition = position;
                SoundService.Instance.PlaySelect();
                PossibleMoves = GetPossibleMoves(position);
            }
        }

        public bool MakeMove(Move move)
        {
            if (IsCapturing && CapturePosition != move.From)
                return false;

            bool becameKing = Board.MovePiece(move.From, move.To, out bool isCapture);
            
            if (becameKing)
                SoundService.Instance.PlayKing();

            if (isCapture)
            {
                var nextCaptures = GetCaptureMoves(move.To);
                if (nextCaptures.Any())
                {
                    IsCapturing = true;
                    CapturePosition = move.To;
                    SelectedPosition = move.To;
                    PossibleMoves = nextCaptures;
                    SoundService.Instance.PlayCapture();
                    return true;
                }
                SoundService.Instance.PlayCapture();
            }
            else 
            {
                SoundService.Instance.PlayMove();
            }

            IsCapturing = false;
            CapturePosition = null;
            SelectedPosition = null;
            PossibleMoves.Clear();
            CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;

            CheckGameOver();
            return true;
        }

        public List<Position> GetPossibleMoves(Position position)
        {
            var moves = new List<Position>();
            var piece = Board.GetPiece(position.Row, position.Col);

            var captures = GetCaptureMoves(position);
            if (captures.Any())
                return captures;

            if (piece.IsKing)
            {
                foreach (var direction in new[] { (-1, -1), (-1, 1), (1, -1), (1, 1) })
                {
                    CheckSimpleMove(position, direction.Item1, direction.Item2, moves, true);
                }
            }
            else
            {
                int direction = piece.Owner == Player.White ? -1 : 1;
                CheckSimpleMove(position, direction, -1, moves, false);
                CheckSimpleMove(position, direction, 1, moves, false);
            }

            return moves;
        }

        private List<Position> GetCaptureMoves(Position position)
        {
            var moves = new List<Position>();
            var piece = Board.GetPiece(position.Row, position.Col);

            if (piece == null)
                return moves;

            foreach (var direction in new[] { (-1, -1), (-1, 1), (1, -1), (1, 1) })
            {
                CheckCaptures(position, direction.Item1, direction.Item2, moves, piece.IsKing);
            }

            return moves;
        }

        private void CheckSimpleMove(Position from, int rowDirection, int colDirection, List<Position> moves, bool isKing)
        {
            int currentRow = from.Row + rowDirection;
            int currentCol = from.Col + colDirection;

            while (currentRow >= 0 && currentRow < Board.BoardSize &&
                   currentCol >= 0 && currentCol < Board.BoardSize)
            {
                var targetPiece = Board.GetPiece(currentRow, currentCol);
                if (targetPiece != null)
                    break;

                moves.Add(new Position(currentRow, currentCol));
                
                if (!isKing)
                    break;
                    
                currentRow += rowDirection;
                currentCol += colDirection;
            }
        }

        private void CheckCaptures(Position from, int rowDirection, int colDirection, List<Position> moves, bool isKing)
        {
            int currentRow = from.Row + rowDirection;
            int currentCol = from.Col + colDirection;
            bool foundEnemy = false;
            Position? enemyPosition = null;

            while (currentRow >= 0 && currentRow < Board.BoardSize &&
                   currentCol >= 0 && currentCol < Board.BoardSize)
            {
                var piece = Board.GetPiece(currentRow, currentCol);

                if (piece != null)
                {
                    if (foundEnemy || piece.Owner == CurrentPlayer)
                        break;

                    foundEnemy = true;
                    enemyPosition = new Position(currentRow, currentCol);
                }
                else if (foundEnemy)
                {
                    moves.Add(new Position(currentRow, currentCol));
                }

                currentRow += rowDirection;
                currentCol += colDirection;

                if (!isKing)
                {
                    if (foundEnemy && currentRow >= 0 && currentRow < Board.BoardSize &&
                        currentCol >= 0 && currentCol < Board.BoardSize &&
                        Board.GetPiece(currentRow, currentCol) == null)
                        moves.Add(new Position(currentRow, currentCol));
                    break;
                };
            }
        }

        public void CheckGameOver()
        {
            bool hasAnyMoves = false;
            
            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    var piece = Board.GetPiece(row, col);
                    if (piece?.Owner == CurrentPlayer)
                    {
                        var position = new Position(row, col);
                        var moves = GetPossibleMoves(position);
                        if (moves.Any())
                        {
                            hasAnyMoves = true;
                            break;
                        }
                    }
                }
                if (hasAnyMoves) break;
            }

            if (!hasAnyMoves)
            {
                IsGameOver = true;
                Winner = CurrentPlayer == Player.White ? Player.Black : Player.White;
                SoundService.Instance.PlayGameOver();
            }
        }

        public void ClearSelection()
        {
            SelectedPosition = null;
            PossibleMoves.Clear();
        }

        public Dictionary<Position, List<Position>> GetAllCaptureMoves()
        {
            var allCaptures = new Dictionary<Position, List<Position>>();

            for (int row = 0; row < Board.BoardSize; row++)
            {
                for (int col = 0; col < Board.BoardSize; col++)
                {
                    var piece = Board.GetPiece(row, col);
                    if (piece?.Owner == CurrentPlayer)
                    {
                        var position = new Position(row, col);
                        var captures = GetCaptureMoves(position);
                        if (captures.Any())
                        {
                            allCaptures[position] = captures;
                        }
                    }
                }
            }

            return allCaptures;
        }

        public virtual void Reset()
        {
            Board.InitializeBoard();
            
            CurrentPlayer = Player.White;
            SelectedPosition = null;
            PossibleMoves.Clear();
            IsGameOver = false;
            Winner = null;
            IsCapturing = false;
            CapturePosition = null;
        }
    }
} 