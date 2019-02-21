using System;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace BakkiefyDriver.Views
{
    public partial class OnlinePage : ContentPage
    {
        public double SliderValue { get; set; } = 5;
        public Xamarin.Forms.GoogleMaps.Circle fence { get; set; }
        public OnlinePage()
        {
            InitializeComponent();
            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
            Move();
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            fence = new Xamarin.Forms.GoogleMaps.Circle();
            fence.StrokeWidth = 2f;
            fence.StrokeColor = Color.Green;

            fence.FillColor = Color.Transparent;

            fence.Center = new Xamarin.Forms.GoogleMaps.Position(e.Position.Latitude, e.Position.Longitude);
            fence.Radius = Distance.FromKilometers(SliderValue);
            fence.ZIndex = 1;
            map.Circles.Add(fence);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(e.Position.Latitude, e.Position.Longitude), Distance.FromMiles(5)));
            map.Pins.Clear();
            map.Pins.Add(new Pin { Rotation = 33.3f, Address = "", Tag = "id_location", Label = "Im here", Position = new Xamarin.Forms.GoogleMaps.Position(App.Position.Latitude, App.Position.Longitude) });
        }
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SliderValue = e.NewValue;
            if (map.Circles.Count > 0)
            {
                map.Circles.Remove(fence);
                CreateCircle(e.NewValue);
            }
            else
            {
                CreateCircle(2);
            }
        }
        private async void Move()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                App.Position = await locator.GetPositionAsync(new TimeSpan(10000));


                CreateCircle(2);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(App.Position.Latitude, App.Position.Longitude), Distance.FromMiles(2)));
                map.Pins.Clear();
                map.Pins.Add(new Pin { Rotation = 33.3f, Address = "", Tag = "id_location", Label = "Im here", Position = new Xamarin.Forms.GoogleMaps.Position(App.Position.Latitude, App.Position.Longitude) });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private void CreateCircle(double value)
        {
            fence = new Xamarin.Forms.GoogleMaps.Circle();
            fence.StrokeWidth = 2f;
            fence.StrokeColor = Color.Green;

            fence.FillColor = Color.Transparent;

            fence.Center = new Position(App.Position.Latitude, App.Position.Longitude);
            fence.Radius = Distance.FromKilometers(value);
            fence.ZIndex = 1;
            map.Circles.Add(fence);
        }
    }
}
