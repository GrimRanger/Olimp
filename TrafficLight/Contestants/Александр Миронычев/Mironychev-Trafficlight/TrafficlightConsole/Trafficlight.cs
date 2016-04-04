using System;
using TrafficLight.Domain.Core.Interfaces;

namespace Alexander_Mironych
{
    /// <summary>
    /// Класс, реализующий ITrafficlight интерфейс
    /// </summary>
    class Trafficlight : ITrafficLight
    {
        #region Class fields
        private readonly bool[] firstWorking;
        private readonly bool[] secondWorking;
        private int time;
        public int? answer { get; private set; }
        #endregion


        #region Constructor
        public Trafficlight(bool[] firstWorking, bool[] secondWorking)
        {
            this.firstWorking = firstWorking;
            this.secondWorking = secondWorking;
        }
        #endregion


        #region Public methods
        /// <summary>
        /// Начать обратный отсчет
        /// </summary>
        public void StartCountDown(int time)
        {
            if (time < 0)
                throw new Exception("Start time less than zero!");

            this.time = time;
            answer = null;
        }
        #endregion


        #region Implements ITrafficlight
        public bool GetNext()
        {
            if (time == 0)
                return false;

            time--;
            return true;
        }

        public Tuple<bool[], bool[]> Current
        {
            get
            {
                bool[] first = StatusDeterminer.references[time/10].Masked(firstWorking);
                bool[] second = StatusDeterminer.references[time%10].Masked(secondWorking);

                return new Tuple<bool[], bool[]>(first, second);
            }
        }

        public void Answer(int value)
        {
            answer = value;
        }
        #endregion
    }
}
