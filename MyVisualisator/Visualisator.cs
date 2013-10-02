using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using MyVisualisator.Properties;

namespace MyVisualisator
{
    internal class Visualisator : UserControl
    {
        #region Fields
        private object _someClass;
        private MyAttribute myAttribute;
        private Type _typeSomeClass;
        private PropertyInfo[] _properties;
        private string _curentValue;
        private OpenFileDialog _openFileDialog1;
        private int _curentIndex;
        private string _filePath;
        // Create the controls.
        private Label lbl_info;
        private Label lbl_ClassName;
        private System.ComponentModel.IContainer components;
        #endregion

        #region Constractor
        //Конструктор
        public Visualisator(object o)
        {
            _someClass = o;

            InitializeComponent();
        }
        #endregion

        #region ShowProperties
        /// <summary>
        /// Рабочий метод.Выводит данные о публичных свойствах полученного объекта.
        /// </summary>
        public void ShowProperties()
        {
            //Получаем тип объекта и список его свойств
            _typeSomeClass = _someClass.GetType();
            _properties = _typeSomeClass.GetProperties();
            
            //Выводим имя полученного класса
            this.lbl_ClassName.Text = _typeSomeClass.Name;

            //Проверяем наличие нашего атрибута
            myAttribute = (MyAttribute)Attribute.GetCustomAttribute(_typeSomeClass, typeof(MyAttribute));

            int y = 70;//Исходная координата по У

            //Формирование шапки таблицы
            var controls = new Control[]
                               {
                                   //Создаем динамически контролы для шапки таблицы
                                   new TextBox()
                                       {
                                           Location = new Point(20, y),
                                           Name = "txtBox_PropertyName",
                                           Size = new Size(150, 30),
                                           Text = Resources.PropertyName,
                                           ReadOnly = true,
                                           Font =
                                               new Font("Lucida Handwriting", 9.75F,
                                                                       FontStyle.Bold,
                                                                       GraphicsUnit.Point, ((byte) (0))),
                                           TextAlign = HorizontalAlignment.Center
                                       },
                                   new TextBox()
                                       {
                                           Location = new Point(175, y),
                                           Name = "txtBox_PropertyType",
                                           Size = new Size(150, 30),
                                           Text = Resources.PropertyType,
                                           ReadOnly = true,
                                           Font =
                                               new Font("Lucida Handwriting", 9.75F,
                                                                       FontStyle.Bold,
                                                                       GraphicsUnit.Point, ((byte) (0))),
                                           TextAlign = HorizontalAlignment.Center
                                       },
                                   new TextBox()
                                       {
                                           Location = new Point(330, y),
                                           Name = "txtBox_PropertyValue",
                                           Size = new Size(150, 30),
                                           Text = Resources.PropertyValue,
                                           ReadOnly = true,
                                           Font =
                                               new Font("Lucida Handwriting", 9.75F,
                                                                       FontStyle.Bold,
                                                                       GraphicsUnit.Point, ((byte) (0))),
                                           TextAlign = HorizontalAlignment.Center
                                       },
                                       new TextBox()
                                       {
                                           Location = new Point(485, y),
                                           Name = "txtBox_ChangeValue",
                                           Size = new Size(150, 30),
                                           Text = Resources.ChangeValue,
                                           ReadOnly = true,
                                           Font =
                                               new Font("Lucida Handwriting", 9.75F,
                                                                       FontStyle.Bold,
                                                                       GraphicsUnit.Point, ((byte) (0))),
                                           TextAlign = HorizontalAlignment.Center
                                       }
                               };
            //Добавляем контролы в контейнер
            Controls.AddRange(controls);

            //Формирование таблицы
            foreach (var propertyInfo in _properties)
            {
                y += 25;

                //Создаются информационные контролы в виде строки таблицы

                var txt1 = new TextBox() //Для имени свойства
                               {
                                   Location = new Point(20, y),
                                   Name = "txtPropertyName/" + propertyInfo.Name,
                                   Size = new Size(150, 30),
                                   Text = propertyInfo.Name,
                                   ReadOnly = true,
                                   TextAlign = HorizontalAlignment.Center
                               };

                var txt2 = new TextBox() //Для типа свойства
                               {
                                   Location = new Point(175, y),
                                   Name = "txtPropertyType/" + propertyInfo.Name,
                                   Size = new Size(150, 30),
                                   Text = propertyInfo.PropertyType.ToString(),
                                   ReadOnly = true,
                                   TextAlign = HorizontalAlignment.Center
                               };

                //Если атрибут назначен, используем его значения
                if (myAttribute != null)
                {
                    txt1.BackColor = myAttribute.BackColor;
                    txt1.Font = myAttribute.Font;
                    txt2.BackColor = myAttribute.BackColor;
                    txt2.Font = myAttribute.Font;
                }

                var newControlsRow = new Control[]
                                         {
                                             txt1, txt2
                                         };

                //Контролы добавляются в контейнер
                Controls.AddRange(newControlsRow);
                //Создание и добавление контрола со значением свойства
                Controls.AddRange(CreateMyControl(propertyInfo, y));
            }
        }
        #endregion

        #region CreateMyControl
        /// <summary>
        /// Создание правильного контрола
        /// </summary>
        /// <param name="propertyInfo">Информация о свойстве</param>
        /// <param name="y">Текущая координата по У</param>
        /// <returns></returns>
        private Control[] CreateMyControl(PropertyInfo propertyInfo, int y)
        {
            //Создаем список контролов
            var controls = new List<Control>();

            

            //Если свойство оперирует с массивом, отображаем его в ComboBox
            if (propertyInfo.PropertyType.IsArray)
            {
                var myCmB = new ComboBox()
                                    {
                                        Location = new Point(330, y),
                                        Name = "cmbBox_PropertyValue/"+propertyInfo.Name,
                                        Size = new Size(150, 30),
                                        FormattingEnabled = true
                                    };

                //Если атрибут назначен, используем его значения
                if (myAttribute != null)
                {
                    myCmB.BackColor = myAttribute.BackColor;
                    myCmB.Font = myAttribute.Font;
                }
                //Если это массив типа byte[]
                if (propertyInfo.PropertyType == typeof( byte[]))
                {
                    //Приобразуем к нужному типу и добавляем в контейнер
                    var array = (byte[]) propertyInfo.GetValue(_someClass, null);

                    foreach (var a in array)
                    {
                        myCmB.Items.Add(a);
                    }
                }//Если это массив типа String[], делаем аналогично
                if (propertyInfo.PropertyType== typeof(String[]))
                {
                    var array = (string[]) propertyInfo.GetValue(_someClass, null);

                    foreach (var b in array)
                    {
                        myCmB.Items.Add(b);
                    }
                }
                //Назначаем текст и обработчик события
                myCmB.Text = Resources.Items;
                myCmB.SelectedValueChanged += new EventHandler(cmB_SelectedValueChanged);

                controls.Add(myCmB);
            }//Если это рисунок, отображаем его в PictureBox
            if (propertyInfo.PropertyType == typeof(Bitmap))
            {
                var pb = new PictureBox()
                           {
                               Image = (Bitmap)propertyInfo.GetValue(_someClass, null),
                               Location = new Point(330, y),
                               Name = "pictureBox/"+propertyInfo.Name,
                               Size = new Size(150, 120),
                               SizeMode = PictureBoxSizeMode.Zoom,
                               TabStop = false
                           };
                pb.Click += new EventHandler(pb_Click);
                controls.Add(pb);
            }
            else//Иначе отоброжаем в TextBox
            {
                var txt = new TextBox()
                              {
                                  Location = new Point(330, y),
                                  Name = "txtPropertyValue/" + propertyInfo.Name,
                                  Size = new Size(150, 30),
                                  Text = propertyInfo.GetValue(_someClass, null).ToString(),
                                  TextAlign = HorizontalAlignment.Center,
                              };

                txt.MouseClick += new MouseEventHandler(this.textBox_MouseClick);
                
                //Если атрибут назначен, используем его значения
                if(myAttribute!=null)
                {
                    txt.BackColor = myAttribute.BackColor;
                    txt.Font = myAttribute.Font;
                }
                //Добавляем контрол в список
                controls.Add(txt);
            }
            //Добавляем кнопку редактирования свойства
            var btn = new Button()
                          {
                              Location = new Point(485, y),
                              Name = "btn_Change/" + propertyInfo.Name,
                              Size = new Size(155, 25),
                              Text = Resources.ChangeValue,
                              UseVisualStyleBackColor = true,
                              Enabled = false
                          };
            btn.Click += new System.EventHandler(this.btn_Click);

            //Если атрибут назначен, используем его значения
            if (myAttribute != null)
            {
                btn.BackColor = myAttribute.BackColor;
                btn.Font = myAttribute.Font;
            }

            controls.Add(btn);

            //Возвращаем список контролов
            return controls.ToArray();
        }
        #endregion

        #region EventHandlers
        #region pb_Click
        /// <summary>
        /// Обработчик события клика по рисунку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pb_Click(object sender, EventArgs e)
        {
            //Приводим к нужному типу
            var pb = (PictureBox) sender;
            //Вызаваем диалоговое окно выбора файла
            _openFileDialog1.Filter = Resources.FileFilter;
            var file = _openFileDialog1.ShowDialog();
            if (file == DialogResult.OK)
            {
                //Запоминаем выбранный файл
                _filePath = _openFileDialog1.FileName;
            }
            //Выделяем имя файла
            string name = pb.Name.Substring(pb.Name.LastIndexOf(@"/") + 1);

            //Блокируем все кнопки редактирования
            foreach (var btn in Controls.OfType<Button>())
            {
                btn.Enabled = false;
            }
            //Находим кнопку редактирования выбранного свойства
            var bt = Controls.OfType<Button>().SingleOrDefault<Button>(b => b.Name.Contains(name));
            if (bt == null)
            {
                MessageBox.Show(Resources.Error);
                return;
            }
            //Делаем ее доступной
            bt.Enabled = true;
        }
        #endregion

        #region textBox_MouseClick
        /// <summary>
        /// Обработчик события клика по текстовому полю
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_MouseClick(object sender, MouseEventArgs e)
        {
            //Приводим к нужному типу
            var control=((TextBox)sender);

            //Запоминаем исходное значение
            _curentValue = control.Text;

            //Выделяем имя оперируемого свойства
            string name = control.Name.Substring(control.Name.LastIndexOf(@"/" )+1);

            //Блокируем все кнопки редактирования
            foreach (var btn in Controls.OfType<Button>())
            {
                btn.Enabled = false;
            }
            
            //Находим нужную кнопку
            Button bt = Controls.OfType<Button>().SingleOrDefault<Button>(b => b.Name.Contains(name));
            if(bt==null)
            {
                MessageBox.Show(Resources.Error);
            }
            //Делаем ее доступной
            bt.Enabled = true;
        }
        #endregion

        #region cmB_SelectedValueChanged
        /// <summary>
        /// Обработчик события выбора значения в ComboBox.
        /// Аналогичен предидущим.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmB_SelectedValueChanged(object sender, EventArgs e)
        {
            var control = ((ComboBox)sender);
            _curentValue = control.SelectedItem.ToString();
            _curentIndex = control.SelectedIndex;
            string name = control.Name.Substring(control.Name.LastIndexOf(@"/") + 1);

            foreach (var btn in Controls.OfType<Button>())
            {
                btn.Enabled = false;
            }

            Button bt = Controls.OfType<Button>().SingleOrDefault<Button>(b => b.Name.Contains(name));
            if(bt==null)
            {
                MessageBox.Show(Resources.Error);
                return;
            }
            bt.Enabled = true;
        }
        #endregion

        #region btn_Click
        /// <summary>
        /// Обработчик события клика по кнопке редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {
            //Приводим к нужному типу
            var control = ((Button)sender);

            if (control.Name.Length > control.Name.LastIndexOf(@"/") + 1)
            {
                //Выделяем имя свойства
                string propertiesName = control.Name.Substring(control.Name.LastIndexOf(@"/") + 1);
                string propertiesType = "";
                string str = "";

                //Находим свойство по имени в списке свойств
                var propInfo = _properties.FirstOrDefault(p => p.Name == propertiesName);
                if (propInfo == null)
                {
                    MessageBox.Show(Resources.Error);
                    return;
                }
                //Определяем его тип
                propertiesType = propInfo.PropertyType.ToString();
                if(propertiesType=="")
                {
                    MessageBox.Show(Resources.Error);
                    return;
                }

                //Если это рисунок
                if(propertiesType == "System.Drawing.Bitmap")
                {
                    //Находим в таблице PictureBox
                    PictureBox myPcBox = Controls.OfType<PictureBox>().SingleOrDefault(
                        b => b.Name.Contains("pictureBox/" + propertiesName));
                    if (myPcBox == null)
                    {
                        MessageBox.Show(Resources.Error);
                        return;
                    }

                    //Запоминаем его рисунок
                    var curentImg = (Bitmap)myPcBox.Image;

                    Bitmap newImg = null;
                    if (_filePath != "")
                    {
                        //Создаем рисунок с выбранного файла
                        newImg = new Bitmap(_filePath);
                    }

                    //Если новый рисунок не выбран, сообщаем это и выходим
                    if (newImg==null ||curentImg == newImg)
                    {
                        MessageBox.Show(Resources.ChangeImege);
                        return;
                    }

                    //Пробуем произвести замену
                    try
                    {
                        propInfo.SetValue(_someClass, newImg, null);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Resources.Can_tChange);
                        return;
                    }

                    //Если все нормально, меняем изображение в таблице
                    myPcBox.Image = newImg;
                }
                else if (propertiesType == "System.Byte[]" || propertiesType == "System.String[]")
                {
                    //Находим нужный ComboBox
                    var myCmbBox = Controls.OfType<ComboBox>().SingleOrDefault(
                        b => b.Name.Contains("cmbBox_PropertyValue/" + propertiesName));

                    if (myCmbBox == null)
                    {
                        MessageBox.Show(Resources.Error);
                        return;
                    }

                    //Считываем новое значение
                    str = myCmbBox.Text;

                    //Если оно не изменилось, выходим
                    if (str == _curentValue)
                    {
                        MessageBox.Show(Resources.ChangeTheValue);
                        return;
                    }

                    if (propertiesType == "System.Byte[]")
                    {
                        //Приводим новое значение к нужному типу
                        Byte tempByte;
                        if (!Byte.TryParse(str, out tempByte))
                        {
                            MessageBox.Show(Resources.InvalidValue);
                            return;
                        }

                        //Создаем список параметров для свойства из текущего ComboBox
                        Byte[] param = new Byte[myCmbBox.Items.Count];

                        for (int i = 0; i < myCmbBox.Items.Count; i++)
                        {
                            param[i] = (Byte)myCmbBox.Items[i];
                        }

                        //Меняем значение нового параметра
                        param[_curentIndex] = tempByte;

                        //Пытаемся произвести замену
                        try
                        {
                            propInfo.SetValue(_someClass, param, null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(Resources.Can_tChange);
                            return;
                        }
                    }
                    else//аналогично для массива строк
                    {
                        string[] param = new string[myCmbBox.Items.Count];

                        for (int i = 0; i < myCmbBox.Items.Count; i++)
                        {
                            param[i] = (string)myCmbBox.Items[i];
                        }

                        param[_curentIndex] = str;

                        try
                        {
                            propInfo.SetValue(_someClass, param, null);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(Resources.Can_tChange);
                            return;
                        }
                    }

                    //Если все нормально, меняем значение в ComboBox
                    myCmbBox.Items[_curentIndex] = str;
                }
                else//Аналогично и с текстовым полем
                {
                    var txt =
                        Controls.OfType<TextBox>().SingleOrDefault<TextBox>(
                            b => b.Name.Contains("txtPropertyValue/" + propertiesName));

                    if (txt == null)
                    {
                        MessageBox.Show(Resources.Error);
                        return;
                    }

                    str = txt.Text;

                    if (str == _curentValue)
                    {
                        MessageBox.Show(Resources.ChangeTheValue);
                        return;
                    }

                    switch (propertiesType)
                    {
                        case "System.String":
                            {
                                try
                                {
                                    propInfo.SetValue(_someClass, str, null);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(Resources.Error);
                                    txt.Text = _curentValue;
                                    return;
                                }
                                break;
                            }
                        case "System.Int64":
                            {
                                long temp;
                                if (!long.TryParse(str, out temp))
                                {
                                    MessageBox.Show(Resources.InvalidValue);
                                    txt.Text = _curentValue;
                                    return;
                                }

                                try
                                {
                                    propInfo.SetValue(_someClass, temp, null);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(Resources.Error);
                                    txt.Text = _curentValue;
                                    return;
                                }
                                break;
                            }
                        case "System.Double":
                            {
                                double tempDbl;

                                if (!double.TryParse(str, out tempDbl))
                                {
                                    MessageBox.Show(Resources.InvalidValue);
                                    txt.Text = _curentValue;
                                    return;
                                }

                                try
                                {
                                    propInfo.SetValue(_someClass, tempDbl, null);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(Resources.Error);
                                    txt.Text = _curentValue;
                                    return;
                                }
                                break;
                            }
                        default:
                            {
                                MessageBox.Show(Resources.Error);
                                return;
                            }
                    }
                }
            }
            MessageBox.Show(Resources.ChangeDone);
        }
        #endregion
        #endregion

        //************************************************************************

        #region Initialize
        // Initialize the control elements.
        public void InitializeComponent()
        {
            this.lbl_info = new System.Windows.Forms.Label();
            this.lbl_ClassName = new System.Windows.Forms.Label();
            this._openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(128, 42);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(376, 13);
            this.lbl_info.TabIndex = 1;
            this.lbl_info.Text = "На вход пришел экземпляр объекта с такими публичными свойствами :";
            // 
            // lbl_ClassName
            // 
            this.lbl_ClassName.AutoSize = true;
            this.lbl_ClassName.Location = new System.Drawing.Point(282, 17);
            this.lbl_ClassName.Name = "lbl_ClassName";
            this.lbl_ClassName.Size = new System.Drawing.Size(0, 13);
            this.lbl_ClassName.TabIndex = 6;
            // 
            // _openFileDialog1
            // 
            this._openFileDialog1.FileName = "openFileDialog1";
            // 
            // Visualisator
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.YellowGreen;
            this.Controls.Add(this.lbl_ClassName);
            this.Controls.Add(this.lbl_info);
            this.Name = "Visualisator";
            this.Size = new System.Drawing.Size(660, 388);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
