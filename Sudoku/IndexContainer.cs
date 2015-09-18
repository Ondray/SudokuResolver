using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class IndexContainer
    {
        public HashSet<string>[] columnIndexes;
        public HashSet<string>[] rowIndexes;
        private int Dimension;
        private HraciPole Game;

        public IndexContainer(HraciPole game)
        {
            Dimension = game.Dimension;

            Game = game;

        }


        public void BuildIndexes()
        {
            InitIndex(out rowIndexes, Dimension);
            InitIndex(out columnIndexes, Dimension);

            // Rows, Columns, Whole squares
            for (int i = 0; i < Dimension; i++)
            {
                Array.ForEach<string>(Game.Columns[i], sym => columnIndexes[i].Add(sym));
                Array.ForEach<string>(Game.Rows[i], sym => rowIndexes[i].Add(sym));
            }
        }


        private void InitIndex(out HashSet<string>[] index, int length)
        {
            var newIndex = new HashSet<string>[length];

            for (int i = 0; i < length; i++)
            {
                newIndex[i] = new HashSet<string>();
            }

            index = newIndex;
        }


    }
}
