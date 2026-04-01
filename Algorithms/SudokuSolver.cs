using SudokuSolverProject.Core;
namespace SudokuSolverProject.Algorithms;

public class SudokuSolver
{
    public int BacktrackCounter {get; private set;} = 0;
    private readonly Random _rng;

    public SudokuSolver(Random? r = null)
    {
        _rng = r == null ? new Random() : r;
    }
    public bool Solve(SudokuBoard board, bool randomize = false)
{
    //BASE CASE: finding next empty cell
    if (!board.FindEmptyCell(out int row, out int col)) return true;

    List<int> numbers = Enumerable.Range(1, 9).ToList();

    //Shuffle them if randomization is requested
    if (randomize)
    {
        //numbers = numbers.OrderBy(x => _rng.Next()).ToList();
        //Used Fisher yates shuffle for O(n) complexity rather than LINQ
        for (int i = numbers.Count() - 1; i > 0; i--) 
        {
            
            // Pick a random index
            // from 0 to i
            int j = _rng.Next(0, i+1);
            
            // Swap arr[i] with the
            // element at random index
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }
    }

    foreach (int num in numbers)
    {
        if (board.IsValid(row, col, num))//Try out this number
        {
            board.SetValue(row, col, num);
            if (Solve(board, randomize)) return true; //Ask the future version if htis chocie works
            board.SetValue(row, col, 0); // future said no, try other value (backtrack)
            BacktrackCounter++;
        }
    }
    return false;
}
}