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

            // ��������� ����������� ������ ����� ����� �� ��������
            Image earthImage = Properties.Resources.WorldMap;

            // ��������� ����������� �� PictureBox
            g.DrawImage(earthImage, 0, 0, pictureBox.Width, pictureBox.Height);

            // ��������� ��������� �����
            int gridSize = 100; // ������ ����� (�������)
            Color gridColor = Color.FromArgb(90, Color.Gray); // ���� � ������������ ����� �����

            using (Pen gridPen = new Pen(gridColor))
            {
                // ������� ������������ ����� �����
                for (int x = 0; x < pictureBox.Width; x += gridSize)
                {
                    g.DrawLine(gridPen, x, 0, x, pictureBox.Height);
                }

                // ������� �������������� ����� �����
                for (int y = 0; y < pictureBox.Height; y += gridSize)
                {
                    g.DrawLine(gridPen, 0, y, pictureBox.Width, y);
                }
            }
            e.Dispose();
        }
       private void pictureBox_MouseMove(object sender, MouseEventArgs e)
            {
                // �������������� ���������� ����� �����������
                double topLeftLatitude = 51.0; // ������ �������� ������ ���� �����������
                double topLeftLongitude = -0.5; // ������� �������� ������ ���� �����������
                double bottomRightLatitude = 50.0; // ������ ������� ������� ���� �����������
                double bottomRightLongitude = 1.0; // ������� ������� ������� ���� �����������

                // ������� ����������� � ��������
                int imageWidth = pictureBox.Width;
                int imageHeight = pictureBox.Height;

                // ��������� ������� ������� � �������� ������������ �������� ������ ���� �����������
                int cursorX = e.X;
                int cursorY = e.Y;

                // ���������� ��������� ������������ ������ � ������ �����������
                double percentX = (double)cursorX / imageWidth;
                double percentY = (double)cursorY / imageHeight;

                // ���������� ������ � ������� �� ������ ���������
                double latitude = topLeftLatitude + percentY * (bottomRightLatitude - topLeftLatitude);
                double longitude = topLeftLongitude + percentX * (bottomRightLongitude - topLeftLongitude);

                // ����������� ��������� � ������ ��� ������ ��������� ����������
                label2.Text = "������: " + latitude.ToString();
                label3.Text = "�������: " + longitude.ToString();
            }

        }

    }
