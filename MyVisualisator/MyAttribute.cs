using System;

namespace MyVisualisator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MyAttribute : System.Attribute
    {
        private System.Drawing.Color backColor = System.Drawing.Color.Plum;

        private System.Drawing.Font font = new System.Drawing.Font("Segoe Script", 9.75F,
                                                                   ((System.Drawing.FontStyle)
                                                                    (((System.Drawing.FontStyle.Bold |
                                                                       System.Drawing.FontStyle.Italic)
                                                                      | System.Drawing.FontStyle.Underline))),
                                                                   System.Drawing.GraphicsUnit.Point, ((byte) (0)));

        public System.Drawing.Color BackColor
        {
            get { return backColor; }
        }

        public System.Drawing.Font Font
        {
            get { return font; }
        }
    }
}
