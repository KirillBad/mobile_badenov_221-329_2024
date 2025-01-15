using Checkers.Core;

public class ComputerPlayer
{
    private readonly Random random = new Random();

    public Move GetMove(Game game)
    {
        var captures = game.GetAllCaptureMoves();
        if (captures.Any())
        {
            var randomCapture = captures.ElementAt(random.Next(captures.Count));
            var randomMove = randomCapture.Value[random.Next(randomCapture.Value.Count)];
            return new Move(randomCapture.Key, randomMove);
        }

        var allMoves = new List<Move>();
        for (int row = 0; row < Board.BoardSize; row++)
        {
            for (int col = 0; col < Board.BoardSize; col++)
            {
                var piece = game.Board.GetPiece(row, col);
                if (piece?.Owner == game.CurrentPlayer)
                {
                    var from = new Position(row, col);
                    var possibleMoves = game.GetPossibleMoves(from);
                    allMoves.AddRange(possibleMoves.Select(to => new Move(from, to)));
                }
            }
        }

        return allMoves.Any() ? allMoves[random.Next(allMoves.Count)] : null;
    }
} 