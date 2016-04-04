using System;

namespace Alexander_Mironych
{
    public class SymbolState
    {
        #region Constants
        /// <summary>
        /// Количество ячеек на дисплее
        /// </summary>
        public const int StateLength = 7;
        #endregion


        #region Class fields
        /// <summary>
        /// Матрица состояния дисплея
        /// </summary>
        private readonly bool[] state;
        #endregion


        #region Constructor
        public SymbolState(bool[] state)
        {
            if (state.Length > StateLength)
                throw new Exception("Wrong state length!"); // TODO

            this.state = new bool[StateLength];
            for (int i = 0; i < StateLength; i++)
                this.state[i] = state[i];
        }
        #endregion


        #region Public methods
        /// <summary>
        /// Проверяет, является ли состояние other подмножеством текущего состояния
        /// </summary>
        public bool ContainsState(bool[] other)
        {
            for (int i = 0; i < StateLength; i++)
                if (other[i] && !state[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Возвращает текущее состояние, пропущенное через маску
        /// </summary>
        public bool[] Masked(bool[] mask)
        {
            bool[] result = new bool[StateLength];

            if (mask.Length < StateLength)
                throw new Exception("Illegal mask length");

            for (int i = 0; i < StateLength; i++)
                result[i] = mask[i] && state[i];

            return result;
        }
        #endregion
    }
}
