using SudokuSolverProject.Display;
using SudokuSolverProject.Algorithms;
using SudokuSolverProject.Core;

SudokuSolver solver = new SudokuSolver();
SudokuBoard board;

Console.WriteLine("Enter a Seed (or press Enter for random)");
SudokuGenerator generator = new SudokuGenerator(Console.ReadLine());
Console.WriteLine("Enter a Difficulty (1 = Easy, 2 = Medium, 3 = Hard).");

int difficulty = 0;
int.TryParse(Console.ReadLine(), out difficulty);
switch(difficulty)
{
    case 1:
        board = generator.Generate(30);
        break;
    case 2:
        board = generator.Generate(45);
        break;
    case 3:
        board = generator.Generate(60);
        break;
    default:
        Console.WriteLine("Invalid option entered, generating easy difficult by defualt.");
        board = generator.Generate(30);
        break;
}


 //Easy mode 
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