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
            DoIt(); 
        }

        #region DoIt
        /// <summary>
        /// Рабочий метод
        /// </summary>
        private void DoIt()
        {
            //Исходные данные для свойств экземпляра класса
            byte[] ByteArray = {5, 8, 8, 4};
            string[] StringArray = {"kjhgjgjg", "ljjlj", "fytdtdtyjkhk"};
            Bitmap tempBmp = new Bitmap(Properties.Resources.веб);

            //Создание экземпляра класса
            SomeClass someClass = new SomeClass("Some text", 5354, 787.23, ByteArray, StringArray, tempBmp);

            //Создание экземпляра визуализатора и передча ему в качестве параметра созданный объект
            myVisualisator = new Visualisator(someClass);
            myVisualisator.ShowProperties();

            // Отображение визуализатора
            Controls.Add(myVisualisator);
            // Size the user control.
            Size = new System.Drawing.Size(665, 400);

            this.start_btn.Visible = false;
        }
        #endregion
    }
}
