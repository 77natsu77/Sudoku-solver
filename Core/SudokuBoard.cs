namespace SudokuSolverProject.Core;

public struct SudokuCell
{
    public int Value { get; set; }
    public bool IsFixed { get; set; } // True if it was part of the original puzzle

    public SudokuCell(int value, bool isFixed)
    {
        Value = value;
        IsFixed = isFixed;
    }
}
public class SudokuBoard
{
    private readonly SudokuCell[,] _grid = new SudokuCell[9, 9];

    public SudokuBoard(int[,] initialGrid)
    {
        for (int r = 0; r < 9; r++)
        {
           for (int c = 0; c < 9; c++)
            {
                int val = initialGrid[r, c];
                _grid[r, c] = new SudokuCell(val, val != 0);
            } 
        }       
    }

    public bool IsValid(int row, int col, int num)
    {
        for (int i = 0; i < 9; i++)
        {
            // 1. Check the Row (Horizontal)
            // Does any cell in this row already have 'num'?
            if (_grid[row, i].Value == num) return false;

            // 2. Check the Column (Vertical)
            // Does any cell in this column already have 'num'?
            if (_grid[i, col].Value == num) return false;
        }

        // 3. Check the 3x3 Box
        // Use the integer division trick to find the top-left of the box
        int boxStartRow = (row / 3) * 3;
        int boxStartCol = (col / 3) * 3;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_grid[boxStartRow + i, boxStartCol + j].Value == num)
                    return false;
            }
        }

        return true; // No conflicts found!
    }
    public void SetValue(int row, int col, int val) => _grid[row, col].Value = val;
    public int GetValue(int row, int col) => _grid[row, col].Value;

    public bool FindEmptyCell(out int row, out int col)
{
    for (row = 0; row < 9; row++)
    {
        for (col = 0; col < 9; col++)
        {
            if (_grid[row, col].Value == 0) return true;
        }
    }
    row = -1;
    col = -1;
    return false;
}

    public bool IsFixed(int row, int col) => _grid[row,col].IsFixed;
}