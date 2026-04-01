using SudokuSolverProject.Display;
using SudokuSolverProject.Algorithms;
using SudokuSolverProject.Core;

const int ROWS = 9, COLUMNS = 9;
int[,] blankgrid = new int[ROWS,COLUMNS];
int[,] challenge = {
    { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
    { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
    { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
    { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
    { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
    { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
    { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
    { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
    { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
};



SudokuGenerator generator = new SudokuGenerator();
SudokuSolver solver = new SudokuSolver();

SudokuBoard board = generator.Generate(30); //Easy mode
//SudokuBoard board = generator.Generate(60,100); //Hard mode


Console.WriteLine("--- UNSOLVED ---");
BoardRenderer.Draw(board);

if (solver.Solve(board))
{
    Console.WriteLine("\n--- SOLVED ---");
    BoardRenderer.Draw(board);
    Console.WriteLine("Backtracks: " + solver.BacktrackCounter);
}
else
{
    Console.WriteLine("\nNo solution exists for this puzzle.");
}