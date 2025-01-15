using Checkers.Core;

class Program
{
    private static Game game;
    private static ComputerPlayer computerPlayer;
    private static bool isComputerGame;
    private const int BoardSize = 8;

    static void Main(string[] args)
    {
        SelectGameMode();
        
        while (!game.IsGameOver)
        {
            DrawBoard();
            
            if (isComputerGame && game.CurrentPlayer == Player.Black)
            {
                Console.WriteLine("\nComputer is thinking...");
                Thread.Sleep(1000); 
                MakeComputerMove();
            }
            else
            {
                MakePlayerMove();
            }
        }

        DrawBoard();
        Console.WriteLine($"Game over! {(game.Winner == Player.White ? "White" : "Black")} won!");
    }

    static void SelectGameMode()
    {
        Console.WriteLine("Select game mode:");
        Console.WriteLine("1. Player vs Player");
        Console.WriteLine("2. Player vs Computer");
        
        while (true)
        {
            var key = Console.ReadKey(true).KeyChar;
            if (key == '1')
            {
                isComputerGame = false;
                break;
            }
            else if (key == '2')
            {
                isComputerGame = true;
                computerPlayer = new ComputerPlayer();
                break;
            }
        }
        
        game = new Game();
        Console.Clear();
    }

    static void MakeComputerMove()
    {
        if (game.IsGameOver) return;
        
        var move = computerPlayer.GetMove(game);
        if (move != null)
        {
            game.SelectPosition(move.From);
            game.MakeMove(move);

            if (game.IsCapturing && game.CurrentPlayer == Player.Black && game.SelectedPosition != null)
            {
                Thread.Sleep(500);
                MakeComputerMove();
            }
        }
    }

    static void MakePlayerMove()
    {
        if (game.SelectedPosition == null)
        {
            Console.Write("\nSelect piece (e.g. E3): ");
            var input = Console.ReadLine();
            var pos = ParsePosition(input);
            if (pos != null)
            {
                game.SelectPosition(pos);
            }
        }
        else
        {
            Console.Write("\nSelect destination (e.g. F4) or press Enter to cancel: ");
            var input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input))
            {
                game.ClearSelection();
                return;
            }
            
            var toPos = ParsePosition(input);
            if (toPos != null && game.PossibleMoves.Contains(toPos))
            {
                var move = new Move(game.SelectedPosition, toPos);
                game.MakeMove(move);
                if (!game.IsCapturing)
                {
                    game.ClearSelection();
                }
            }
        }
    }

    static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("    A B C D E F G H");
        Console.WriteLine("   ─────────────────");
        
        for (int row = 0; row < BoardSize; row++)
        {
            Console.Write($" {BoardSize - row} │");
            
            for (int col = 0; col < BoardSize; col++)
            {
                var piece = game.Board.GetPiece(row, col);
                char symbol = ' ';
                
                if (piece != null)
                {
                    if (piece.Owner == Player.White)
                        symbol = piece.IsKing ? '♔' : '○';
                    else
                        symbol = piece.IsKing ? '♚' : '●';
                }
                
                if (game.SelectedPosition?.Row == row && game.SelectedPosition?.Col == col)
                    Console.BackgroundColor = ConsoleColor.Green;
                else if (game.PossibleMoves.Contains(new Position(row, col)))
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                else
                    Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.White : ConsoleColor.DarkYellow;
                
                Console.ForegroundColor = piece?.Owner == Player.White ? ConsoleColor.White : ConsoleColor.Black;
                Console.Write($"{symbol} ");
                Console.ResetColor();
            }
            
            Console.WriteLine($"│ {BoardSize - row}");
        }
        
        Console.WriteLine("   ─────────────────");
        Console.WriteLine("    A B C D E F G H");
        Console.WriteLine($"\nIt's {(game.CurrentPlayer == Player.White ? "White's" : "Black's")} turn");
    }

    static Position? ParsePosition(string? input)
    {
        if (string.IsNullOrEmpty(input) || input.Length != 2)
            return null;

        input = input.ToUpper();
        int col = input[0] - 'A';
        int row = BoardSize - (input[1] - '0');

        if (col >= 0 && col < BoardSize && row >= 0 && row < BoardSize)
            return new Position(row, col);

        return null;
    }
}