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

            // ��������� ���������� ��������� �����
            gMapControl1.Position = new PointLatLng(0, 0);

            // ��������� ����������� �����
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 2;

            gMapControl1.ShowCenter = true;

            // �������� ���������� OpenWeatherMapService
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
            if (apiChecked) // ���������, ��� API ��� ���� ���������
            {
                await Task.Delay(2000);

                var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(point.Lat, point.Lng);

                // ����������� ����������� � Label
                label2.Text = $"{weatherInfo.Main.Temperature}�C";
            }


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!IsNetworkAvailable())
            {
                MessageBox.Show("����������� ������� �����������. ��������� ���� ��������-���������� � ���������� �����.");
                return;
            }

            try
            {
                var weatherInfo = await openWeatherMapService.GetCurrentWeatherAsync(0, 0);
                MessageBox.Show($"API ��������. ������� �����������: {weatherInfo.Main.Temperature}�C");

                apiChecked = true; // ������������� ���� �������� API � true
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"������ ��� ��������� � API: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��������� ������: {ex.Message}");
            }
        }

        private bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
