using Prism;
using Prism.Ioc;
using BakkiefyDriver.ViewModels;
using BakkiefyDriver.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Iconize;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BakkiefyDriver
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public static Plugin.Geolocator.Abstractions.Position Position { get; set; }
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Iconize
                .With(new Plugin.Iconize.Fonts.MaterialModule());
            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<AddBakkiePage, AddBakkiePageViewModel>();
            containerRegistry.RegisterForNavigation<OnlinePage, OnlinePageViewModel>();
            containerRegistry.RegisterForNavigation<QoutePage, QoutePageViewModel>();
            containerRegistry.RegisterForNavigation<AccountsPage, AccountsPageViewModel>();
            containerRegistry.RegisterForNavigation<TripPage, TripPageViewModel>();
            containerRegistry.RegisterForNavigation<TrucksPage, TrucksPageViewModel>();
            containerRegistry.RegisterForNavigation<RequestPage, RequestPageViewModel>();
        }
    }
}
