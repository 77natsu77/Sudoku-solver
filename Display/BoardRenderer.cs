using SudokuSolverProject.Core;
using System.Text;

namespace SudokuSolverProject.Display;

public static class BoardRenderer
{
 
   static public void Draw(SudokuBoard board)
    {
        StringBuilder output = new StringBuilder();
        string heavyDivider = "+================+===============+===============+";
        string lightDivider = "+----------------+---------------+---------------+";

        for (int row = 0; row < 9; row++)
        {
            //Draw horizontal divider every 3 rows
            if (row % 3 == 0) output.AppendLine(heavyDivider);
            else output.AppendLine(lightDivider);

            for (int col = 0; col < 9; col++)
            {
                //Draw vertical divider every 3 columns
                if (col % 3 == 0) output.Append("|| "); // Heavy side
                else output.Append("| "); // Light side (colon looks lighter than pipe)

                int val = board.GetValue(row, col);
                bool isFixed = board.IsFixed(row, col);

                if (val == 0) 
                {
                    output.Append(" . ");
                }
                else 
                {
                    // If it's an original number, make it Cyan (Blue-ish)
                    // \u001b[36m is Cyan, \u001b[0m resets color
                    string colorCode = isFixed ? "\u001b[36;1m" : "\u001b[0m";
                    output.Append($"{colorCode} {val} \u001b[0m");
                }
            }
            output.AppendLine("||"); // Close the row
        }
        
        output.AppendLine(heavyDivider); // Final floor
        Console.WriteLine(output.ToString());
    }
}