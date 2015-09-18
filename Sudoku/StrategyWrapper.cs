using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class StrategyWrapper<T>
        where T : ISudokuStrategy
    {
        IEnumerable<string> Symbols;

    }
}
