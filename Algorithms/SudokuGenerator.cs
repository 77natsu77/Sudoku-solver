using SudokuSolverProject.Core;

namespace SudokuSolverProject.Algorithms;

public class SudokuGenerator
{

    private readonly SudokuSolver _solver;
    private readonly Random _rng;
    public SudokuGenerator(string? seed = null)
    {
        int seedHash = seed?.GetHashCode() ?? DateTime.Now.Ticks.GetHashCode();
        _rng = new Random(seedHash);
        
        // Pass the rng to the solver so its shuffling is also tied to the seed
        _solver = new SudokuSolver(_rng); 
    }

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
                 board.SetValue(row, col, 0); // Backtrack
                if (count >= limit) return true; 

               
            }
        }
        return false;
    }

    public SudokuBoard Generate(int cellsToRemove, int ConsecutiveTriesLimit = 100)
    {

        SudokuBoard board = new SudokuBoard(new int[9, 9]);
        _solver.Solve(board, randomize: true);

        int ConsecutiveTries = 0;
        int removed = 0;
        var coords = Shuffle(GetAllCoordinates());
        foreach (var (r, c) in coords)
        {
            if  (!(removed < cellsToRemove) || !(ConsecutiveTries < ConsecutiveTriesLimit)) break;
            if (board.GetValue(r, c) != 0)
            {
                int backup = board.GetValue(r, c);
                board.SetValue(r, c, 0);

                //Check if the puzzle still has exactly ONE solution
                if (CountSolutions(board) != 1)
                {
                    // If removing it made the puzzle ambiguous, put it back!
                    board.SetValue(r, c, backup);
                    ConsecutiveTries++;
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

    private List<(int,int)> GetAllCoordinates()
    {
        List<(int,int)> coords = new List<(int,int)>();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                coords.Add((i,j));
            }
        }
        return coords;
    }

    private List<(int,int)> Shuffle(List<(int,int)> list)
    {
        for (int i = list.Count - 1; i > 0; i--) 
        {
            
            // Pick a random index
            // from 0 to i
            int j = _rng.Next(0, i+1);
            
            // Swap arr[i] with the
            // element at random index
            (int,int) temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
}