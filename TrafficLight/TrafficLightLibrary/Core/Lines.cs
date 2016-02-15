using System.Collections.Generic;

namespace TrafficLight.Domain.Core.Core
{
    //public class Top : ILine
    //{
    //    public int Mask
    //    {
    //        get { return 64; }
    //    }
    //}

    //public class TopLeft : ILine
    //{
    //    public int Mask
    //    {
    //        get { return 32; }
    //    }
    //}

    //public class TopRight : ILine
    //{
    //    public int Mask
    //    {
    //        get { return 16; }
    //    }
    //}

    //public class Middle : ILine
    //{
    //    public int Mask
    //    {
    //        get { return 8; }
    //    }
    //}

    //public class BottomLeft : ILine
    //{
    //    public int Mask
    //    {
    //        get { return 4; }
    //    }
    //}

    //public class BottomRight : ILine
    //{
    //    public int Mask
    //    {
    //        get { return 2; }
    //    }
    //}

    //public class Bottom : ILine
    //{
    //    public int Mask
    //    {
    //        get { return 1; }
    //    }
    //}

    public class Lines
    {
        //public static List<ILine> LineMasks = new List<ILine> { new Top(), new TopLeft(), new TopRight(), new Middle(), new BottomLeft(), new BottomRight(), new Bottom() };
        public static readonly List<int> Masks = new List<int>
            {
              
                32, //TopLeft
                16, //TopRight
                8, //Middle
                4, //BottomLeft
                2, //BottomRight
                1 //Bottom
            };
    }
}