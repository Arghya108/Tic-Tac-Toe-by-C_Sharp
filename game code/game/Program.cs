using System;

class TicTacToe
{
    static char[] board = new char[9];
    static char currentPlayer = 'X';
    static bool gameActive = true;

    static int[][] winningCombos = new int[][]
    {
        new int[]{0,1,2}, new int[]{3,4,5}, new int[]{6,7,8},
        new int[]{0,3,6}, new int[]{1,4,7}, new int[]{2,5,8},
        new int[]{0,4,8}, new int[]{2,4,6}
    };

    static void Main()
    {
        ResetGame();

        while (true)
        {
            PrintBoard();
            Console.WriteLine($"Player {currentPlayer}'s turn. Enter cell (1-9), R to reset, Q to quit:");

            string input = Console.ReadLine().ToUpper();

            if (input == "Q")
            {
                Console.WriteLine("Thanks for playing!");
                break;
            }
            else if (input == "R")
            {
                ResetGame();
                continue;
            }

            if (!int.TryParse(input, out int move) || move < 1 || move > 9)
            {
                Console.WriteLine("Invalid input! Choose a number between 1-9.");
                continue;
            }

            int idx = move - 1;
            if (board[idx] != ' ')
            {
                Console.WriteLine("Cell already taken!");
                continue;
            }

            board[idx] = currentPlayer;

            string result = CheckResult();
            if (result != null)
            {
                PrintBoard();
                Console.WriteLine(result);
                Console.WriteLine("Play again? (Y/N)");
                string replay = Console.ReadLine().ToUpper();
                if (replay == "Y")
                {
                    ResetGame();
                    continue;
                }
                else
                {
                    break;
                }
            }

            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
        }
    }

    static void PrintBoard()
    {
        Console.Clear();
        Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
        Console.WriteLine("---|---|---");
        Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
        Console.WriteLine();
    }

    static string CheckResult()
    {
        foreach (var combo in winningCombos)
        {
            if (board[combo[0]] != ' ' &&
                board[combo[0]] == board[combo[1]] &&
                board[combo[0]] == board[combo[2]])
            {
                gameActive = false;
                return $"Player {board[combo[0]]} wins!";
            }
        }

        if (Array.IndexOf(board, ' ') == -1)
        {
            gameActive = false;
            return "It's a draw!";
        }

        return null;
    }

    static void ResetGame()
    {
        for (int i = 0; i < 9; i++)
            board[i] = ' ';
        currentPlayer = 'X';
        gameActive = true;
    }
}
