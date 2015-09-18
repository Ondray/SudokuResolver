using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Strategies
{
    public class RowUniquenessStrategy : ISudokuStrategy
    {
        public void Apply(HraciPole game)
        {
            int length = game.IndexContainer.rowIndexes.Length;

            // Ziskat symboly, ktere v danem radku chybi a pro kazde nezaplnene pole testovat, 
            // zda se da jednoznacne usoudit, ze lze na pole umistit symbol, ktery se v danem sloupci nenachazi.



            for (int i = 0; i < length; i++)
            {
                var rowIndex = game.IndexContainer.rowIndexes[i];

                // Get symbols missing in the row
                var result = game.Symbols.Except(rowIndex).ToArray();

                for (int j = 0; j < result.Length; j++)
                {
                }
            }
        }
    }
}
