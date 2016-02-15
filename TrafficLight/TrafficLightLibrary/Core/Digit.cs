using System.Collections.Generic;

namespace TrafficLight.Domain.Core.Core
{
    public class Digit
    {
        public List<int> GlowingLines = new List<int>();
        // public List<ILine> GlowingLines = new List<ILine>();

        public int Mask { get; private set; }

        public Digit(int mask)
        {
            Mask = mask;
            //for (var i = 0; i < Lines.Lines.LineMasks.Count; ++i)
            //{
            //    if ((Lines.Lines.LineMasks[i].Mask ^ mask) > 0)
            //        GlowingLines.Add(Lines.Lines.LineMasks[i]);
            //}
            for (var i = 0; i < Lines.Masks.Count; ++i)
            {
                if ((Lines.Masks[i] ^ mask) > 0)
                    GlowingLines.Add(i);
            }
        }
    }
}
