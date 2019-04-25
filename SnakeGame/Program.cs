using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
   

    struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            Position[] directions = new Position[] 
            {
                new Position(0,1), // right
                new Position(0,-1), // left
                new Position(1,0), //down
                new Position(-1,0), //up
            };
            int sleepTime = 100;
            int direction = right;
            Random randomNumbersGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight;
            Position food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));
            

            Queue<Position> snakeElements = new Queue<Position>();

            for (int i = 0; i < 6; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }

            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.Write("=");
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();

                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                       if (direction != right) direction = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left) direction = right;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up) direction = down;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = up;
                    }
                }
                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);

                if (snakeNewHead.row < 0 ||
                    snakeNewHead.col < 0 ||
                    snakeNewHead.row >= Console.WindowHeight ||
                    snakeNewHead.col >= Console.WindowWidth ||
                    snakeElements.Contains(snakeNewHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Game over!");
                    Console.WriteLine("Your points are {0}.", (snakeElements.Count - 6) * 100);
                    return;
                }

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.WriteLine("*");
               
                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    food = new Position(randomNumbersGenerator.Next(0, Console.WindowHeight), randomNumbersGenerator.Next(0, Console.WindowWidth));
                    sleepTime--;
                    
                }
                else
                {
                    Position last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");
                }

                Console.Clear();

                foreach (Position position in snakeElements)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.Write("*");
                }
 
                Console.SetCursorPosition(food.col, food.row);
                Console.Write("@");

                Thread.Sleep(sleepTime);
            }
        }
    }
}
