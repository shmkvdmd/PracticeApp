using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using OpenWeatherMap;
using OpenWeatherMap.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace PracticeApp
{
    public partial class Form1 : Form
    {
        private OpenWeatherMapService openWeatherMapService;
        private bool apiChecked = false;
        private System.Windows.Forms.Timer timer;
        private PointLatLng currentPoint;

        public Form1()
        {
            InitializeComponent();
            gMapControl1.MapProvider = GMapProviders.GoogleMap;

            // Установка начального положения карты в центре
            gMapControl1.Position = new PointLatLng(0, 0);

            // Настройка отображения карты
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 2;

            gMapControl1.ShowCenter = true;

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var logger = loggerFactory.CreateLogger<OpenWeatherMapService>();
            var openWeatherMapConfiguration = new OpenWeatherMapConfiguration
            {
                ApiEndpoint = "https://api.openweathermap.org",
                ApiKey = "ffc8252ec80ac9b92fce652adb34888e",
                UnitSystem = "metric",
                Language = "en"
            };
            openWeatherMapService = new OpenWeatherMapService(logger, openWeatherMapConfiguration);


            timer = new System.Windows.Forms.Timer();
            timer.Interval = 30;
            timer.Tick += Timer_Tick;
        }

        private async void gMapControl1_OnPositionChanged(PointLatLng point)
        {
            label3.Text = $"{point.Lat:F2}";
            label4.Text = $"{point.Lng:F2}";

            currentPoint = point; // Сохраняем текущую позицию карты

            if (apiChecked) // Проверяем, что API уже было проверено
            {
                progressBar1.Value = 0; // Сбрасываем состояние ProgressBar
                timer.Start(); // Запускаем таймер для обновления ProgressBar
                label2.Text = "";
            }
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 25;

            if (progressBar1.Value >= progressBar1.Maximum)
            {
                timer.Stop();

                try
                {
                    var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(currentPoint.Lat, currentPoint.Lng);
                    label2.Text = $"{weatherInfo.Main.Temperature}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при выполнении запроса: {ex.Message}");
                }
                finally
                {
                    progressBar1.Value = 0;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!IsNetworkAvailable())
            {
                MessageBox.Show("Отсутствует сетевое подключение. Проверьте свое интернет-соединение и попробуйте снова.");
                return;
            }

            try
            {
                var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(0, 0);
                MessageBox.Show($"API работает. Текущая температура: {weatherInfo.Main.Temperature}");

                apiChecked = true;

                pictureBox1.BackgroundImage = Properties.Resources.haveconnection;
                pictureBox1.Visible = true;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка при обращении к API: {ex.Message}");

                pictureBox1.BackgroundImage = Properties.Resources.noconnection;
                pictureBox1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
                pictureBox1.BackgroundImage = Properties.Resources.noconnection;
                pictureBox1.Visible = true;
            }
        }

        private bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!apiChecked)
            {
                MessageBox.Show("Сначала проверьте состояние.");
                return;
            }
            if (!ValidateLatitude() || !ValidateLongitude())
            {
                return;
            }

            try
            {
                var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(double.Parse(textBox1.Text), double.Parse(textBox2.Text));
                label2.Text = $"{weatherInfo.Main.Temperature}";

                pictureBox1.BackgroundImage = Properties.Resources.haveconnection;
                pictureBox1.Visible = true;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка при обращении к API: {ex.Message}");

                pictureBox1.BackgroundImage = Properties.Resources.noconnection;
                pictureBox1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
                pictureBox1.BackgroundImage = Properties.Resources.noconnection;
                pictureBox1.Visible = true;
            }
        }

        private bool ValidateLatitude()
        {
            if (double.TryParse(textBox1.Text, out double latitude))
            {
                if (latitude < -85.05 || latitude > 85.05)
                {
                    MessageBox.Show("Значение широты должно быть в диапазоне от -85.05 до 85.05.");
                    textBox1.Focus();
                    textBox1.SelectAll();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите корректное значение широты.");
                textBox1.Focus();
                textBox1.SelectAll();
                return false;
            }
            return true;
        }

        private bool ValidateLongitude()
        {
            if (double.TryParse(textBox2.Text, out double longitude))
            {
                if (longitude < -180 || longitude > 180)
                {
                    MessageBox.Show("Значение долготы должно быть в диапазоне от -180 до 180.");
                    textBox2.Focus();
                    textBox2.SelectAll();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите корректное значение долготы.");
                textBox2.Focus();
                textBox2.SelectAll();
                return false;
            }
            return true;
        }

    }
}
