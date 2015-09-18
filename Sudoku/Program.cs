using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new HraciPole(Games.Sample1);
            game.StartGame();
            Console.ReadKey();
        }
    }
}
