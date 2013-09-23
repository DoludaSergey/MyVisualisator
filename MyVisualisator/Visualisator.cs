using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace MyVisualisator
{
    internal class Visualisator : UserControl
    {

        private object _someClass;
        private Type typeSomeClass;
        private Label lbl_ClassName;

        //Конструктор
        public Visualisator(object o)
        {
            this._someClass = o;

            InitializeComponent();
            ShowProperties();

        }

        //Рабочий метод
        public void ShowProperties()
        {
            typeSomeClass = _someClass.GetType();

            this.lbl_ClassName.Text = typeSomeClass.Name;

            PropertyInfo[] properties = typeSomeClass.GetProperties();


            int y = 70;

            //Вывод шапки таблицы
            var controls = new Control[]
                               {
                                   new TextBox()
                                       {
                                           Location = new Point(20, y),
                                           Name = "txtBox_PropertiesName",
                                           Size = new Size(150, 30),
                                           Text = "Properties Name",
                                           ReadOnly=true,
                                           Font =
                                               new System.Drawing.Font("Lucida Handwriting", 9.75F,
                                                                       System.Drawing.FontStyle.Bold,
                                                                       System.Drawing.GraphicsUnit.Point, ((byte) (0))),
                                           TextAlign = HorizontalAlignment.Center
                                       },
                                   new TextBox()
                                       {
                                           Location = new Point(175, y),
                                           Name = "txtBox_PropertiesType",
                                           Size = new Size(150, 30),
                                           Text = "Properties Type",
                                           ReadOnly=true,
                                           Font =
                                               new System.Drawing.Font("Lucida Handwriting", 9.75F,
                                                                       System.Drawing.FontStyle.Bold,
                                                                       System.Drawing.GraphicsUnit.Point, ((byte) (0))),
                                           TextAlign = HorizontalAlignment.Center
                                       },
                                   new TextBox()
                                       {
                                           Location = new Point(330, y),
                                           Name = "txtBox_PropertiesValue",
                                           Size = new Size(150, 30),
                                           Text = "Properties Value",
                                           ReadOnly=true,
                                           Font =
                                               new System.Drawing.Font("Lucida Handwriting", 9.75F,
                                                                       System.Drawing.FontStyle.Bold,
                                                                       System.Drawing.GraphicsUnit.Point, ((byte) (0))),
                                           TextAlign = HorizontalAlignment.Center
                                       }
                               };
            Controls.AddRange(controls);


            //Вывод таблицы
            foreach (var propertyInfo in properties)
            {
                y += 25;

                //Создаются контролы
                var newControlsRow = new Control[]
                                         {
                                             new TextBox()
                                                 {
                                                     Location = new Point(20, y),
                                                     Name = propertyInfo.Name,
                                                     Size = new Size(150, 30),
                                                     Text = propertyInfo.Name,
                                                     ReadOnly = true,
                                                     TextAlign = HorizontalAlignment.Center
                                                 },
                                             new TextBox()
                                                 {
                                                     Location = new Point(175, y),
                                                     Name = propertyInfo.PropertyType.ToString(),
                                                     Size = new Size(150, 30),
                                                     Text = propertyInfo.PropertyType.ToString(),
                                                     ReadOnly = true,
                                                     TextAlign = HorizontalAlignment.Center
                                                 }
                                         };
                //Контролы добавляются в контейнер
                Controls.AddRange(newControlsRow);
                //Создание и добавление контрола со значением свойства
                Controls.Add(CreateMyControl(propertyInfo, y));


            }

            //Console.WriteLine("\nSurprise!!!");
            //MethodInfo mi = typeSomeClass.GetMethod(
            //    "SomePrivateMethod",
            //    BindingFlags.Instance |
            //    BindingFlags.NonPublic);
            //mi.Invoke(_someClass, null);

            //Console.WriteLine("Now we change value on properties!!!\n");

            //foreach (var propertyInfo in properties)
            //{
            //    Console.WriteLine("|{0,17} | {1,15} | {2,30}|",
            //                      propertyInfo.Name,
            //                      propertyInfo.PropertyType,
            //                      (!propertyInfo.PropertyType.IsArray)
            //                          ? propertyInfo.GetValue(_someClass, null)
            //                          : ValueInArray(((object)propertyInfo.GetValue(_someClass, null))));

            //    Console.WriteLine("Are You wont change this property?(If yes-enter Y) ");
            //    char c;
            //    char.TryParse(Console.ReadLine().ToUpper(), out c);

            //    if (c == 'Y')
            //    {
            //        string s;

            //        if (!propertyInfo.PropertyType.IsArray)
            //        {
            //            Console.WriteLine("Enter new value of {0} type", propertyInfo.PropertyType);

            //            s = Console.ReadLine();

            //            if (propertyInfo.PropertyType == Type.GetType("System.String"))
            //            {
            //                propertyInfo.SetValue(_someClass, s, null);
            //            }
            //            //Console.WriteLine(propertyInfo.GetValue(_someClass, null));
            //            if (propertyInfo.PropertyType == Type.GetType("System.Int64"))
            //            {
            //                long temp;
            //                long.TryParse(s, out temp);
            //                propertyInfo.SetValue(_someClass, temp, null);
            //            }
            //            if (propertyInfo.PropertyType == Type.GetType("System.Double"))
            //            {
            //                double temp1;
            //                double.TryParse(s, out temp1);
            //                propertyInfo.SetValue(_someClass, temp1, null);
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("Enter new value of {0} type", propertyInfo.PropertyType);

            //            if (propertyInfo.PropertyType == Type.GetType("System.Byte[]"))
            //            {
            //                Console.WriteLine("Enter new value of {0} type", propertyInfo.PropertyType);

            //                byte[] param = new byte[countArByte];

            //                for (int j = 0; j < countArByte; j++)
            //                {
            //                    Console.WriteLine("Enter {0} el of array (For end input- n) ", j);
            //                    s = Console.ReadLine();

            //                    byte temp2;
            //                    byte.TryParse(s, out temp2);

            //                    param.SetValue(temp2, j);
            //                }

            //                propertyInfo.SetValue(_someClass, param, null);
            //            }

            //            if (propertyInfo.PropertyType == Type.GetType("System.String[]"))
            //            {
            //                Console.WriteLine("Enter new value of {0} type", propertyInfo.PropertyType);

            //                string[] param = new string[countArStr];

            //                for (int i = 0; i < countArStr; i++)
            //                {
            //                    Console.WriteLine("Enter {0} el of array (For end input- n) ", i);
            //                    s = Console.ReadLine();
            //                    param[i] = s;
            //                }

            //                propertyInfo.SetValue(_someClass, param, null);
            //            }
            //        }
            //    }
            //}
            //Console.WriteLine("Now we have new value on properties!!!\n");

            //foreach (var propertyInfo in properties)
            //{
            //    Console.WriteLine("|{0,17} | {1,15} | {2,30}|",
            //                      propertyInfo.Name,
            //                      propertyInfo.PropertyType,
            //                      (!propertyInfo.PropertyType.IsArray)
            //                          ? propertyInfo.GetValue(_someClass, null)
            //                          : ValueInArray(((object)propertyInfo.GetValue(_someClass, null))));
            //}
        }

        //Создание правильного контрола
        private Control CreateMyControl(PropertyInfo propertyInfo,int y)
        {
            if (propertyInfo.PropertyType.IsArray)
            {
                var myControl=new ComboBox()
                {
                    Location = new Point(330, y),
                    Name = "cmbBox_PropertiesValue",
                    Size = new Size(150, 30),
                    FormattingEnabled = true
                };

                if (propertyInfo.GetValue(_someClass, null) is byte[])
                {
                    var array = (byte[]) propertyInfo.GetValue(_someClass, null);
                    
                    foreach (var a in array)
                    {
                        myControl.Items.Add(a);
                    }
                }
                if (propertyInfo.GetValue(_someClass, null) is string[])
                {
                    var array = (string[])propertyInfo.GetValue(_someClass, null);

                    foreach (var b in array)
                    {
                        myControl.Items.Add(b);
                    }

                }

                myControl.Text = "Тут элементы массива";

                return myControl;
            }
                if(propertyInfo.GetValue(_someClass,null)is Bitmap)
                {
                    return new PictureBox()
                                  {
                                      Image = (Bitmap)propertyInfo.GetValue(_someClass, null),
                                      Location = new System.Drawing.Point(330, y),
                                      Name = "pictureBox1",
                                      Size = new System.Drawing.Size(200, 150),
                                      SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize,
                                     TabStop = false
                                  };
                }
                else
                {
                    return new TextBox()
                               {
                                   Location = new Point(330, y),
                                   Name = "txtBox_PropertiesValue",
                                   Size = new Size(150, 30),
                                   Text = propertyInfo.GetValue(_someClass, null).ToString(),
                                   TextAlign = HorizontalAlignment.Center
                               };
                }
            
        }

 
        // Create the controls.
        private Label lbl_info;
        private System.ComponentModel.IContainer components;


      // Initialize the control elements.
        public void InitializeComponent()
        {
            this.lbl_info = new System.Windows.Forms.Label();
            this.lbl_ClassName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(64, 45);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(376, 13);
            this.lbl_info.TabIndex = 1;
            this.lbl_info.Text = "На вход пришел экземпляр объекта с такими публичными свойствами :";
            // 
            // lbl_ClassName
            // 
            this.lbl_ClassName.AutoSize = true;
            this.lbl_ClassName.Location = new System.Drawing.Point(218, 20);
            this.lbl_ClassName.Name = "lbl_ClassName";
            this.lbl_ClassName.Size = new System.Drawing.Size(0, 13);
            this.lbl_ClassName.TabIndex = 6;
            // 
            // Visualisator
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.Controls.Add(this.lbl_ClassName);
            this.Controls.Add(this.lbl_info);
            this.Name = "Visualisator";
            this.Size = new System.Drawing.Size(504, 364);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
