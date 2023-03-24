using System;

namespace GameOfLife
{

    public class LifeSimulation
    {
        private int _heigth;
        private int _width;
        private bool[,] cells;

        /// <summary>
        /// Создаем новую игру
        /// </summary>
        /// <param name="Heigth">Высота поля.</param>
        /// <param name="Width">Ширина поля.</param>

        public LifeSimulation(int Heigth, int Width)
        {
            _heigth = Heigth;
            _width = Width;
            cells = new bool[Heigth, Width];
            GenerateField();
        }

        /// <summary>
        /// Перейти к следующему поколению и вывести результат на консоль.
        /// </summary>
        public void DrawAndGrow()
        {
            DrawGame();
            Grow();
        }

        /// <summary>
        /// Двигаем состояние на одно вперед, по установленным правилам
        /// </summary>

        private void Grow()
        {
            for (int i = 0; i < _heigth; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    int numOfAliveNeighbors = GetNeighbors(i, j);

                    if (cells[i, j])
                    {
                        {
                            cells[i, j] = false;
                        }

                        if (numOfAliveNeighbors > 4)
                        {
                            cells[i, j] = false;
                        }
                    }
                    else
                    {
                        if (numOfAliveNeighbors == 3)
                        {
                            cells[i, j] = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Смотрим сколько живых соседий вокруг клетки.
        /// </summary>
        /// <param name="x">X-координата клетки.</param>
        /// <param name="y">Y-координата клетки.</param>
        /// <returns>Число живых клеток.</returns>

        private int GetNeighbors(int x, int y)
        {
            int NumOfAliveNeighbors = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= _heigth || j >= _width)))
                    {
                        if (cells[i, j] == true) NumOfAliveNeighbors++;
                    }
                }
            }
            return NumOfAliveNeighbors;
        }

        /// <summary>
        /// Нарисовать Игру в консоле
        /// </summary>

        private void DrawGame()
        {
            Console.WriteLine("Нажмите Enter чтобы выйти, нажмите любую другую клавишу чтобы продолжить!");
            Console.WriteLine();
            for (int i = 0; i < _heigth; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Console.Write(cells[i, j] ? "0" : " ");
                    if (j == _width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }

        /// <summary>
        /// Инициализируем случайными значениями
        /// </summary>

        private void GenerateField()
        {
            Random generator = new Random();
            int number;
            for (int i = 0; i < _heigth; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    //Изменил рандом на "первое поколение"
                    number = generator.Next(4);
                    cells[i, j] = ((number == 0) ? true : false);
                }
            }
        }
    }

    internal class Program
    {

        // Ограничения игры
        private const int Heigth = 10;
        private const int Width = 30;
        private const uint MaxRuns = 100;

        private static void Main(string[] args)
        {
            int runs = 0;
            LifeSimulation sim = new LifeSimulation(Heigth, Width);
            //Добавил генерацию поколений через нажатие(зажатием) клавиши.
            ConsoleKeyInfo btn;
            do
            {
                runs++;
                btn = Console.ReadKey();
                sim.DrawAndGrow();
                btn = Console.ReadKey(true);
            }
            while (btn.Key != ConsoleKey.Enter);
            //Выделил строки для вывода общего числа поколений.
            for (int i = 0; i < Heigth+2; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine($"Количество поколений: {runs}");
            Console.ReadKey();
        }
    }
}

