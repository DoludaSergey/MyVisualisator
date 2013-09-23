using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyVisualisator
{
    public partial class MainForm : Form
    {
        private Visualisator myVisualisator;

 
        public MainForm()
        {
            InitializeComponent();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            byte[] tempB = { 5, 8, 8, 4 };
            string[] tempS = { "kjhgjgjg", "ljjlj" ,"fytdtdtyjkhk"};
            Bitmap tempBmp = new Bitmap(Properties.Resources.веб);

            SomeClass someClass = new SomeClass("sjjghgdf", 5354, 787, tempB, tempS, tempBmp);

            myVisualisator = new Visualisator(someClass);   

            // Add the controls to the user control.
            Controls.AddRange(new System.Windows.Forms.Control[] 
         {
            myVisualisator
         });

            // Size the user control.
            Size = new System.Drawing.Size(550, 420);
        }
    }

    //Произвольный класс
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

        private void SomePrivateMethod()
        {
            Console.WriteLine("Working private method!!!!");
        }
    }
}
