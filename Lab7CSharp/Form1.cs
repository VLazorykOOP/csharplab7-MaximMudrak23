using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        private Timer timer = new Timer();
        private float animationTime = 0f; // Час анімації
        public Form1()
        {
            InitializeComponent();

            // Task1
            comboBox1.Items.Add("Точка");
            comboBox1.Items.Add("Коло");
            comboBox1.Items.Add("Квадрат");
            // Task1

            // Task2
            timer.Interval = 100; // Інтервал таймера в мілісекундах
            timer.Tick += Timer_Tick; // Підписка на подію Tick таймера
            // Task2

            // Task3
            comboBox2.Items.Add("Прямокутник із заокругленими кутами");
            comboBox2.Items.Add("Ромб");
            comboBox2.Items.Add("Коло");
            comboBox2.Items.Add("Дуга");

            comboBox3.Items.Add("Оранжевий");
            comboBox3.Items.Add("Чорний");
            comboBox3.Items.Add("Синій");
            comboBox3.Items.Add("Жовтий");
            comboBox3.Items.Add("Зелений");
            // Task3
        }

        // Task1
        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Random random = new Random();
            string selectedFigure = comboBox1.SelectedItem.ToString();
            string selectedSize = textBox1.Text; // or random.Next(80 + 20);

            Pen pen = new Pen(Color.Black);
            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));

            int x = 150;
            int y = 150;

            int ellipseWidth = 0;
            int ellipseHeight = 0;
            if (int.TryParse(selectedSize, out int size))
            {
                ellipseWidth = size;
                ellipseHeight = size;
            }

            switch (selectedFigure)
            {
                case "Точка":
                    graphics.FillEllipse(solidBrush, x, y, ellipseWidth, ellipseHeight);
                    break;
                case "Коло":
                    graphics.DrawEllipse(pen, x, y, ellipseWidth, ellipseHeight);
                    break;
                case "Квадрат":
                    graphics.FillRectangle(solidBrush, x, y, ellipseWidth, ellipseHeight);
                    break;
                default:
                    break;
            }

            pictureBox1.Image = bitmap;
        }

        // Task1
        // Task2

        private void button2_Click(object sender, EventArgs e)
        {
            // Показываем диалог выбора шрифта и цвета
            DialogResult result = fontDialog1.ShowDialog();

            // Проверяем результат диалога
            if (result == DialogResult.OK)
            {
                // Очищаем PictureBox перед построением нового графика
                pictureBox1.Refresh();

                // Применяем выбранный шрифт и цвет к тексту TextBox
                textBox2.Font = fontDialog1.Font;
                textBox2.ForeColor = fontDialog1.Color;
                textBox2.Text = "y=sin(x)/x(0<x<4)";

                // Запускаємо таймер для анімації
                animationTime = 0f;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int secondsToAnimate = int.Parse(textBox8.Text); // Змінна для зберігання кількості секунд для анімації
            // Останавливаем таймер, якщо час анімації завершено
            if (animationTime >= secondsToAnimate)
            {
                timer.Stop();
                return;
            }

            // Очищаємо PictureBox перед початком малювання нового кадру
            pictureBox1.Refresh();

            // Получаем объект Graphics для рисования на PictureBox
            Graphics g = pictureBox1.CreateGraphics();

            // Определяем ширину и высоту PictureBox
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            // Определяем масштаб по X и Y
            float scaleX = width / 4f; // Для x от 0 до 4
            float scaleY = height / 2f; // Для y от -2 до 2

            // Создаем объект Pen для рисования графика с выбранным цветом и шрифтом
            Pen pen = new Pen(fontDialog1.Color, 1);

            // Рисуем график функции y=sin(x)/x для поточного часу анімації
            for (float x = 0.01f; x < 4f; x += 0.01f)
            {
                float y = (float)(Math.Sin(x) / x) * (animationTime / secondsToAnimate); // Використовуйте час анімації для зміни висоти графіка
                                                                                         // Переводим координаты в пиксели и рисуем точку
                float pixelX = x * scaleX;
                float pixelY = height / 2 - y * scaleY;
                g.DrawRectangle(pen, pixelX, pixelY, 1, 1);
            }

            // Збільшуємо час анімації на інтервал таймера
            animationTime += timer.Interval / 1000f;

            // Освобождаем ресурсы объекта Graphics
            g.Dispose();
        }

        // Task2
        // Task3

        class Figure
        {
            protected string type;
            protected int x;
            protected int y;
            protected int width;
            protected int height;
            protected Color color;
            public Figure(string type, int x, int y, int width, int height, Color color)
            {
                this.type = type;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
                this.color = color;
            }
            public string GetFigureType { get { return type; } }
            public int GetFigureX { get { return x; } }
            public int GetFigureY { get { return y; } }
            public int GetFigureWidth { get { return width; } }
            public int GetFigureHeight { get { return height; } }
            public Color GetFigureColor { get { return color; } }
        }
        class RectRoundedCorners : Figure
        {
            public int radius;

            public RectRoundedCorners(string type, int x, int y, int width, int height, Color color, int radius) : base(type, x, y, width, height, color)
            {
                this.radius = radius;
            }
            public int GetFigureRadius { get { return radius; } }
        }

        class Romb : Figure
        {
            public Romb(string type, int x, int y, int width, int height, Color color) : base(type, x, y, width, height, color)
            {
            }
        }

        class Circle : Figure
        {
            public Circle(string type, int x, int y, int width, int height, Color color) : base(type, x, y, width, height, color)
            {
            }
        }

        class Arc : Figure
        {
            public int startAngle;
            public int sweepAngle;
            public Arc(string type, int x, int y, int width, int height, Color color, int startAngle, int sweepAngle) : base(type, x, y, width, height, color)
            {
                this.startAngle = startAngle;
                this.sweepAngle = sweepAngle;
            }
            public int GetStartAngle { get { return startAngle; } }
            public int GetSweepAngle { get { return sweepAngle; } }
        }

        List<Figure> figuresList = new List<Figure>();

        private void button3_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string figureType = comboBox2.SelectedItem.ToString();
            int figureX = random.Next(pictureBox2.Width);
            int figureY = random.Next(pictureBox2.Height);
            string figureWidth = textBox5.Text;
            string figureHeight = textBox6.Text;
            string figureColorName = comboBox3.SelectedItem.ToString();
            string figureRadius = textBox3.Text;
            string figureStartAngle = textBox4.Text;
            string figureSweepAngle = textBox7.Text;

            if (figureRadius == null || figureRadius == "")
            {
                figureRadius = $"{random.Next(90 + 11)}";
            }

            Color figureColor;
            switch (figureColorName)
            {
                case "Оранжевий":
                    figureColor = Color.Orange;
                    break;
                case "Чорний":
                    figureColor = Color.Black;
                    break;
                case "Синій":
                    figureColor = Color.Blue;
                    break;
                case "Жовтий":
                    figureColor = Color.Yellow;
                    break;
                case "Зелений":
                    figureColor = Color.Green;
                    break;
                default:
                    figureColor = Color.Black;
                    break;
            }

            if (figureType == "Прямокутник із заокругленими кутами")
            {
                RectRoundedCorners rect = new RectRoundedCorners(figureType, figureX, figureY, int.Parse(figureWidth), int.Parse(figureHeight), figureColor, int.Parse(figureRadius));
                figuresList.Add(rect);
            }
            else if (figureType == "Ромб")
            {
                Romb romb = new Romb(figureType, figureX, figureY, int.Parse(figureWidth), int.Parse(figureHeight), figureColor);
                figuresList.Add(romb);
            }
            else if (figureType == "Коло")
            {
                Circle circle = new Circle(figureType, figureX, figureY, int.Parse(figureWidth), int.Parse(figureHeight), figureColor);
                figuresList.Add(circle);
            }
            else if (figureType == "Дуга")
            {
                Arc arc = new Arc(figureType, figureX, figureY, int.Parse(figureWidth), int.Parse(figureHeight), figureColor, int.Parse(figureStartAngle), int.Parse(figureSweepAngle));
                figuresList.Add(arc);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            for (int i = 0; i < figuresList.Count; i++)
            {
                Pen pen = new Pen(figuresList[i].GetFigureColor);
                SolidBrush solidBrush = new SolidBrush(figuresList[i].GetFigureColor);

                if (figuresList[i].GetFigureType == "Прямокутник із заокругленими кутами")
                {
                    RectRoundedCorners roundedCorners = (RectRoundedCorners)figuresList[i];
                    int radius = roundedCorners.GetFigureRadius;
                    textBox2.Text = $"{radius}";
                    graphics.FillRectangle(solidBrush, figuresList[i].GetFigureX, figuresList[i].GetFigureY + radius, figuresList[i].GetFigureWidth, figuresList[i].GetFigureHeight - 2 * radius);
                    graphics.FillRectangle(solidBrush, figuresList[i].GetFigureX + radius, figuresList[i].GetFigureY, figuresList[i].GetFigureWidth - 2 * radius, radius);
                    graphics.FillRectangle(solidBrush, figuresList[i].GetFigureX + radius, figuresList[i].GetFigureY + figuresList[i].GetFigureHeight - radius, figuresList[i].GetFigureWidth - 2 * radius, radius);

                    graphics.FillPie(solidBrush, figuresList[i].GetFigureX, figuresList[i].GetFigureY, 2 * radius, 2 * radius, 180, 90); // Верхній лівий кут
                    graphics.FillPie(solidBrush, figuresList[i].GetFigureX + figuresList[i].GetFigureWidth - 2 * radius, figuresList[i].GetFigureY, 2 * radius, 2 * radius, 270, 90); // Верхній правий кут
                    graphics.FillPie(solidBrush, figuresList[i].GetFigureX, figuresList[i].GetFigureY + figuresList[i].GetFigureHeight - 2 * radius, 2 * radius, 2 * radius, 90, 90); // Нижній лівий кут
                    graphics.FillPie(solidBrush, figuresList[i].GetFigureX + figuresList[i].GetFigureWidth - 2 * radius, figuresList[i].GetFigureY + figuresList[i].GetFigureHeight - 2 * radius, 2 * radius, 2 * radius, 0, 90); // Нижній правий кут
                }
                else if (figuresList[i].GetFigureType == "Ромб")
                {
                    int x = figuresList[i].GetFigureX;
                    int y = figuresList[i].GetFigureY;
                    int width = figuresList[i].GetFigureWidth;
                    int height = figuresList[i].GetFigureHeight;

                    Point[] points = new Point[]
                    {
                        new Point(x + width / 2, y),               // Верхній центр
                        new Point(x + width, y + height / 2),      // Правий центр
                        new Point(x + width / 2, y + height),      // Нижній центр
                        new Point(x, y + height / 2)               // Лівий центр
                    };

                    graphics.FillPolygon(solidBrush, points);
                }
                else if (figuresList[i].GetFigureType == "Коло")
                {
                    int x = figuresList[i].GetFigureX;
                    int y = figuresList[i].GetFigureY;
                    int diameter = Math.Min(figuresList[i].GetFigureWidth, figuresList[i].GetFigureHeight); // Діаметр кола - мінімальна сторона

                    graphics.FillEllipse(new SolidBrush(figuresList[i].GetFigureColor), x, y, diameter, diameter); // Заливка кола кольором
                }
                else if (figuresList[i].GetFigureType == "Дуга")
                {
                    Arc angles = (Arc)figuresList[i];

                    // Кут початку дуги і кут замкнення (в градусах)
                    int startAngle = angles.GetStartAngle;
                    int sweepAngle = angles.GetSweepAngle;

                    int x = figuresList[i].GetFigureX;
                    int y = figuresList[i].GetFigureY;
                    int width = figuresList[i].GetFigureWidth;
                    int height = figuresList[i].GetFigureHeight;

                    // Малюємо дугу
                    graphics.FillPie(solidBrush, x, y, width, height, startAngle, sweepAngle);
                }
            }

            pictureBox2.Image = bitmap;
            pictureBox2.Refresh();
        }

        // Task3
    }
}
