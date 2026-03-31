using SudokuSolverProject.Core;
namespace SudokuSolverProject.Algorithms;

public class SudokuSolver
{
    public int BacktrackCounter {get; private set;} = 0;
    public bool Solve(SudokuBoard board)
{
    // 1. BASE CASE: Find the next empty cell
    if (!board.FindEmptyCell(out int row, out int col))
    {
        return true; // No empty cells? We won! Pass the signal up.
    }

    // 2. TRY POSSIBILITIES: 1 through 9
    for (int num = 1; num <= 9; num++)
    {
        if (board.IsValid(row, col, num))
        {
            // Try this number
            board.SetValue(row, col, num);

            // 3. RECURSION: Ask the "future" if this choice works
            if (Solve(board)) 
            {
                return true; // The future said yes! Pass success back.
            }

            // 4. BACKTRACK: The future said no. Undo and try next 'num'
            board.SetValue(row, col, 0);
        }
    }

    // 5. FAILURE: Tried 1-9 and none worked. 
    // This triggers the "Undo" in the previous call.
    BacktrackCounter++;
    return false; 
}
}