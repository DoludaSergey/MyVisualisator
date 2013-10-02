using System;
using System.Drawing;


namespace MyVisualisator
{
    #region SomeClass
    [My]
    public class SomeClass
    {
        public string SomeString { get; set; }
        public long SomeLongNumber { get; set; }
        public double SomeDoubleNumber { get; set; }
        public byte[] SomeBytes { get; set; }
        public string[] Strings { get; set; }
        public Bitmap SomeBitmap { get; set; }

        public SomeClass(string name, long lng, double dbl, byte[] bytes, string[] strings, Bitmap bmp)
        {
            SomeString = name;
            SomeLongNumber = lng;
            SomeDoubleNumber = dbl;
            SomeBytes = bytes;
            Strings = strings;
            SomeBitmap = bmp;
        }
    }
    #endregion
}
