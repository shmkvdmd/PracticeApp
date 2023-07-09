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
using static GMap.NET.Entity.OpenStreetMapGraphHopperGeocodeEntity;

namespace PracticeApp
{
    public partial class Form1 : Form
    {
        private OpenWeatherMapService openWeatherMapService;
        private bool apiChecked = false;

        public Form1()
        {

            InitializeComponent();
            gMapControl1.MapProvider = GMapProviders.GoogleMap;

            // Установка начального положения карты
            gMapControl1.Position = new PointLatLng(0, 0);

            // Настройка отображения карты
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 2;

            gMapControl1.ShowCenter = true;

            // Создание экземпляра OpenWeatherMapService
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
        }

        private async void gMapControl1_OnPositionChanged(PointLatLng point)
        {
            label3.Text = $"{point.Lat:F2}";
            label4.Text = $"{point.Lng:F2}";
            if (apiChecked) // Проверяем, что API уже было проверено
            {
                await Task.Delay(2000);

                var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(point.Lat, point.Lng);

                // Отображение температуры в Label
                label2.Text = $"{weatherInfo.Main.Temperature}°C";
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
                MessageBox.Show($"API работает. Текущая температура: {weatherInfo.Main.Temperature}°C");

                apiChecked = true; // Устанавливаем флаг проверки API в true
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка при обращении к API: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
