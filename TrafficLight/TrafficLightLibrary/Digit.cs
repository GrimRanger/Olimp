using System.Collections.Generic;

namespace TrafficLight.Domain.Core
{
    public class Digit
    {
        public int Top { get; private set; }
        public int TopLeft { get; private set; }
        public int TopRight { get; private set; }
        public int Middle { get; private set; }
        public int BottomLeft { get; private set; }
        public int BottomRight { get; private set; }
        public int Bottom { get; private set; }

        public int Mask { get; private set; }

        public Digit(int mask)
        {
            Mask = mask;
            Top = mask ^ Masks[0];
            TopLeft = mask ^ Masks[1];
            TopRight = mask ^ Masks[2];
            Middle = mask ^ Masks[3];
            BottomLeft = mask ^ Masks[4];
            BottomRight = mask ^ Masks[5];
            Bottom = mask ^ Masks[6];
        }

        private static readonly List<int> Masks = new List<int>
			{
                64, //Top
				32, //TopLeft
				16, //TopRight
				8, //Middle
				4, //BottomLeft
                2, //BottomRight
                1 //Bottom
			};
    }
}
