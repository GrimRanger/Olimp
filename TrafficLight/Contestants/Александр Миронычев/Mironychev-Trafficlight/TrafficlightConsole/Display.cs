using System;
using System.Collections.Generic;

namespace Alexander_Mironych
{
    /// <summary>
    /// Класс, отображающий на консоли состояние дисплея
    /// </summary>
    class Display
    {
        #region Constants
        private const int length = 7;
        private const char on = '@';
        private const char off = ' ';
        private const int height = 9;
        private const int width = 4;
        #endregion


        #region Public methods
        /// <summary>
        /// Выводит в консоль состояние дисплея
        /// </summary>
        public void Show(Tuple<bool[], bool[]> tuple)
        {
            Show(tuple.Item1, tuple.Item2);
        }

        /// <summary>
        /// Выводит в консоль состояние дисплея
        /// </summary>
        public void Show(bool[] first, bool[] second)
        {
            bool[,] firstView = new bool[height,width];
            bool[,] secondView = new bool[height,width];

            for (int cell = 0; cell < length; cell++)
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        if (first[cell])
                            firstView[i, j] |= parts[cell][i, j];

                        if (second[cell])
                            secondView[i, j] |= parts[cell][i, j];
                    }

            char[][] view = new char[height][];
            for (int i = 0; i < height; i++)
                view[i] = new char[width * 2 + 1];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    view[i][j] = firstView[i, j] ? on : off;
                    view[i][j + width + 1] = secondView[i, j] ? on : off;
                }

                view[i][width] = off;
            }

            Console.WriteLine();
            for (int i = 0; i < view.GetLength(0); i++)
                Console.WriteLine(view[i]);
        }
        #endregion

        #region Bulbs on display
        /// <summary>
        /// Части символа на экране
        /// </summary>
        private readonly Dictionary<int, bool[,]> parts = new Dictionary<int, bool[,]>()
        {
            {
                0, new [,]
                {
                    { false, true, true, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false }
                }
            },

            {
                1, new [,]
                {
                    { false, false, false, false },
                    { true, false, false, false },
                    { true, false, false, false },
                    { true, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false }
                }
            },

            {
                2, new [,]
                {
                    { false, false, false, false },
                    { false, false, false, true },
                    { false, false, false, true },
                    { false, false, false, true },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false }
                }
            },

            {
                3, new [,]
                {
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, true, true, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false }
                }
            },

            {
                4, new [,]
                {
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { true, false, false, false },
                    { true, false, false, false },
                    { true, false, false, false },
                    { false, false, false, false }
                }
            },

            {
                5, new [,]
                {
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, true },
                    { false, false, false, true },
                    { false, false, false, true },
                    { false, false, false, false }
                }
            },

            {
                6, new [,]
                {
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, true, true, false }
                }
            }
        };
        #endregion
    }
}
