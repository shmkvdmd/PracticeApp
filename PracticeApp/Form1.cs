using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PracticeApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseMove += pictureBox_MouseMove;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Загрузите изображение полной карты Земли из ресурсов
            Image earthImage = Properties.Resources.WorldMap;

            // Отрисуйте изображение на PictureBox
            g.DrawImage(earthImage, 0, 0, pictureBox.Width, pictureBox.Height);

            // Настройте параметры сетки
            int gridSize = 100; // Размер сетки (пиксели)
            Color gridColor = Color.FromArgb(90, Color.Gray); // Цвет и прозрачность линий сетки

            using (Pen gridPen = new Pen(gridColor))
            {
                // Рисуйте вертикальные линии сетки
                for (int x = 0; x < pictureBox.Width; x += gridSize)
                {
                    g.DrawLine(gridPen, x, 0, x, pictureBox.Height);
                }

                // Рисуйте горизонтальные линии сетки
                for (int y = 0; y < pictureBox.Height; y += gridSize)
                {
                    g.DrawLine(gridPen, 0, y, pictureBox.Width, y);
                }
            }
            e.Dispose();
        }
       private void pictureBox_MouseMove(object sender, MouseEventArgs e)
            {
                // Географические координаты углов изображения
                double topLeftLatitude = 51.0; // Широта верхнего левого угла изображения
                double topLeftLongitude = -0.5; // Долгота верхнего левого угла изображения
                double bottomRightLatitude = 50.0; // Широта нижнего правого угла изображения
                double bottomRightLongitude = 1.0; // Долгота нижнего правого угла изображения

                // Размеры изображения в пикселях
                int imageWidth = pictureBox.Width;
                int imageHeight = pictureBox.Height;

                // Получение позиции курсора в пикселях относительно верхнего левого угла изображения
                int cursorX = e.X;
                int cursorY = e.Y;

                // Вычисление процентов относительно ширины и высоты изображения
                double percentX = (double)cursorX / imageWidth;
                double percentY = (double)cursorY / imageHeight;

                // Вычисление широты и долготы на основе процентов
                double latitude = topLeftLatitude + percentY * (bottomRightLatitude - topLeftLatitude);
                double longitude = topLeftLongitude + percentX * (bottomRightLongitude - topLeftLongitude);

                // Отображение координат в метках или других элементах управления
                label2.Text = "Широта: " + latitude.ToString();
                label3.Text = "Долгота: " + longitude.ToString();
            }

        }

    }
