using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class HraciPole
    {
        public const int STANDARD_DIMENSION = 9;
        public const string DEFAULT_SYMBOLS = "1, 2, 3, 4, 5, 6, 7, 8, 9";

        public string[,] RawMatrix;
        public readonly int Dimension;
        private List<ISudokuStrategy> Strategies;
        public IndexContainer IndexContainer;
        public readonly IEnumerable<string> Symbols;

        private string[][] mColumns = null;
        private string[][] mRows;

        public HraciPole(string game, int dim = STANDARD_DIMENSION, string symbols = null)
        {
            Dimension = dim;
            RawMatrix = new string[Dimension, Dimension];
            Strategies = new List<ISudokuStrategy>();
            IndexContainer = new IndexContainer(this);

            if (String.IsNullOrWhiteSpace(symbols))
            {
                symbols = DEFAULT_SYMBOLS;
            }

            Symbols = symbols.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            LoadGame(game);
        }


        public void LoadGame(string game)
        {
            var cells = game.Split(new[] { ',', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            if (cells.Length != Dimension * Dimension)
            {
                throw new Exception("Number of cells doesn't correspond to the desk dimmensions.");
            }

            for (int i = 0; i < cells.Length; i++)
            {
                int column = i % Dimension;
                int row = i / Dimension;

                RawMatrix[row, column] = cells[i];
            }


            IndexContainer.BuildIndexes();

            Console.WriteLine("The game was loaded:");
            ShowGame();
        }


        public void StartGame()
        {
            RegisterStrategies();

            if (!Strategies.Any())
            {
                Console.WriteLine("No strategies were found, the game will exit.");
                return;
            }

            var strategyList = Strategies.ToArray();

            int counter = 0;
            int innerCounter = 0;


            while ((innerCounter < strategyList.Length) && (counter < 10))
            {
                strategyList[innerCounter].Apply(this);

                if (innerCounter >= strategyList.Length)
                {
                    // Reset inner counter
                    innerCounter = 0;
                }

                innerCounter++;
                counter++;
            }
        }


        private void RegisterStrategies()
        {
            var strategyTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ISudokuStrategy).IsAssignableFrom(t) && t.IsClass)
                .Select(t => Activator.CreateInstance(t) as ISudokuStrategy);


            Strategies.AddRange(strategyTypes);
        }


        private void ShowGame()
        {
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Console.Write(RawMatrix[i, j]);
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }


        public string[][] Columns
        {
            get
            {
                if (mColumns == null)
                {
                    var helper = new string[Dimension][];
                    
                    for (int i = 0; i < Dimension; i++)
                    {
                        var col = new string[Dimension];

                        for (int j = 0; j < Dimension; j++)
                        {
                            col[j] = RawMatrix[j, i];
                        }

                        helper[i] = col;
                    }

                    mColumns = helper;
                }

                return mColumns;
            }
        }

        public string[][] Rows
        {
            get
            {
                if (mRows == null)
                {
                    var helper = new string[Dimension][];

                    for (int i = 0; i < Dimension; i++)
                    {
                        var row = new string[Dimension];

                        for (int j = 0; j < Dimension; j++)
                        {
                            row[j] = RawMatrix[i, j];
                        }

                        helper[i] = row;
                    }

                    mRows = helper;
                }

                return mRows;
            }
        }

    }
}
