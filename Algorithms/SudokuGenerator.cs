using SudokuSolverProject.Core;

namespace SudokuSolverProject.Algorithms;

public class SudokuGenerator
{

    private readonly SudokuSolver _solver = new SudokuSolver();
    private readonly Random _rng = new Random();
    

    private int CountSolutions(SudokuBoard board, int limit = 2)
    {
        int count = 0;
        InternalSolve(board, ref count, limit);
        return count;
    }

    private bool InternalSolve(SudokuBoard board, ref int count, int limit)
    {
        if (!board.FindEmptyCell(out int row, out int col))
        {
            count++;
            // We return false here EVEN IF we found a solution!
            // This forces the algorithm to backtrack and look for a SECOND one.
            return false; 
        }

        for (int num = 1; num <= 9; num++)
        {
            if (board.IsValid(row, col, num))
            {
                board.SetValue(row, col, num);
                
                InternalSolve(board, ref count, limit);

                // Optimization: If we found 2 solutions, we already know 
                // the puzzle is invalid. No need to find all 500+ solutions.
                if (count >= limit) return true; 

                board.SetValue(row, col, 0); // Backtrack
            }
        }
        return false;
    }

    public SudokuBoard Generate(int cellsToRemove)
    {
        int ConsecutiveTries = 0;
        SudokuBoard board = new SudokuBoard(new int[9, 9]);

  
        _solver.Solve(board, randomize: true);


        int removed = 0;
        while (removed < cellsToRemove && ConsecutiveTries < 100)
        {
            int r = _rng.Next(9);
            int c = _rng.Next(9);

            if (board.GetValue(r, c) != 0)
            {
                int backup = board.GetValue(r, c);
                board.SetValue(r, c, 0);

                //Check if the puzzle still has exactly ONE solution
                if (CountSolutions(board) != 1)
                {
                    // If removing it made the puzzle ambiguous, put it back!
                    board.SetValue(r, c, backup);
                    ConsecutiveTries += 1;
                }
                else
                {
                    removed++;
                    ConsecutiveTries = 0;
                }
            }
        }
        return board;
    }
}