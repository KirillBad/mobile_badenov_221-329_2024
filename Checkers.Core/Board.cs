namespace Checkers.Core
{
    public class Board
    {
        public Piece[,] pieces;
        public const int BoardSize = 8;
        
        public Board()
        {
            pieces = new Piece[BoardSize, BoardSize];
            InitializeBoard();
        }
        
        public void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if ((row + col) % 2 != 0)
                    {
                        pieces[row, col] = new Piece(Player.Black, new Position(row, col));
                    }
                }
            }

            for (int row = BoardSize - 3; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if ((row + col) % 2 != 0)
                    {
                        pieces[row, col] = new Piece(Player.White, new Position(row, col));
                    }
                }
            }
        }
        
        public Piece GetPiece(int row, int col) => pieces[row, col];
        
        public bool MovePiece(Position from, Position to, out bool isCapture)
        {
            isCapture = false;
            var piece = GetPiece(from.Row, from.Col);
            if (piece == null) return false;

            int distance = Math.Abs(from.Row - to.Row);
            
            if (distance >= 2)
            {
                if (piece.IsKing)
                {
                    int rowDirection = Math.Sign(to.Row - from.Row);
                    int colDirection = Math.Sign(to.Col - from.Col);
                    int currentRow = from.Row + rowDirection;
                    int currentCol = from.Col + colDirection;
                    
                    while (currentRow != to.Row || currentCol != to.Col)
                    {
                        if (pieces[currentRow, currentCol] != null)
                        {
                            pieces[currentRow, currentCol] = null;
                            isCapture = true;
                            break;
                        }
                        currentRow += rowDirection;
                        currentCol += colDirection;
                    }
                }
                else if (distance == 2)
                {
                    int capturedRow = (from.Row + to.Row) / 2;
                    int capturedCol = (from.Col + to.Col) / 2;
                    if (pieces[capturedRow, capturedCol] != null)
                    {
                        pieces[capturedRow, capturedCol] = null;
                        isCapture = true;
                    }
                }
            }
            pieces[to.Row, to.Col] = piece;
            pieces[from.Row, from.Col] = null;
            piece.Position = to;

            if (!piece.IsKing && ((piece.Owner == Player.White && to.Row == 0) ||
                (piece.Owner == Player.Black && to.Row == BoardSize - 1)))
            {
                piece.IsKing = true;
                return true;
            }

            return false;
        }
        
        public void RemovePiece(Position position)
        {
            pieces[position.Row, position.Col] = null;
        }
    }
} 