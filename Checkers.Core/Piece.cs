namespace Checkers.Core
{
    public class Piece
    {
        public Player Owner { get; set; }
        public bool IsKing { get; set; }
        public Position Position { get; set; }
        
        public Piece(Player owner, Position position)
        {
            Owner = owner;
            Position = position;
            IsKing = false;
        }
    }

    public enum Player
    {
        White,
        Black
    }

    public record Position(int Row, int Col);
} 