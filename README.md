# Sudoku Solver

> A console-based Sudoku solver written in **C# (.NET 8)** that uses recursive backtracking to solve any valid 9×9 Sudoku puzzle — instantly.

---

## How it works

Sudoku is a **constraint satisfaction problem (CSP)**. This solver navigates the solution space using **recursive backtracking with forward checking**:

1. **Scan** — Find the next empty cell on the board.
2. **Try** — Attempt to place each digit 1–9 in that cell.
3. **Validate** — Check the three Sudoku constraints:
   - No duplicate in the same **row**
   - No duplicate in the same **column**
   - No duplicate in the same **3×3 box**
4. **Recurse** — If valid, move to the next empty cell and repeat.
5. **Backtrack** — If no digit works, erase the current cell and return to the previous call.

This depth-first search prunes branches the moment a constraint is violated, making it dramatically faster than naive brute force. A typical newspaper puzzle solves in milliseconds.

### Project structure

```
Sudoku-solver/
├── Core/          # Board model, cell representation, constraint validation
├── Algorithms/    # Recursive backtracking solver
├── Display/       # Console rendering / pretty-print of the board
└── Program.cs     # Entry point
```

---

## Tech stack

| Layer | Detail |
|---|---|
| Language | C# 12 |
| Runtime | .NET 8 |
| Architecture | 3-layer (Core / Algorithms / Display) |
| Algorithm | Recursive Backtracking (DFS + pruning) |

---

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Clone & run

```bash
git clone https://github.com/77natsu77/Sudoku-solver.git
cd Sudoku-solver
dotnet run
```

### Provide a puzzle

Edit the puzzle input in `Program.cs` (or the relevant `Core` file). Use `0` to represent empty cells:

```csharp
int[,] puzzle = {
    {5,3,0, 0,7,0, 0,0,0},
    {6,0,0, 1,9,5, 0,0,0},
    {0,9,8, 0,0,0, 0,6,0},

    {8,0,0, 0,6,0, 0,0,3},
    {4,0,0, 8,0,3, 0,0,1},
    {7,0,0, 0,2,0, 0,0,6},

    {0,6,0, 0,0,0, 2,8,0},
    {0,0,0, 4,1,9, 0,0,5},
    {0,0,0, 0,8,0, 0,7,9}
};
```

---

## Learning outcomes

Building this project developed hands-on mastery of:

- **Recursive backtracking** — implementing DFS with in-place mutation and state restoration
- **Constraint satisfaction** — understanding how constraint propagation prunes the search tree
- **Algorithm complexity** — analysing worst-case vs. practical performance (branching factor ~9, depth 81)
- **C# OOP** — separation of concerns across Core / Algorithms / Display layers
- **.NET 8 project structure** — solution files, `.csproj`, build pipeline with `dotnet` CLI

---

## Complexity note

In the worst case the search tree has 9^81 leaves, but constraint-based pruning reduces the practical search space by orders of magnitude. Real puzzles with unique solutions typically require only a few hundred recursive calls.

---

## License

MIT
