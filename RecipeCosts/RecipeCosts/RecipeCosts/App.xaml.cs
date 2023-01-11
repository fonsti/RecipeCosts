
using Google.Cloud.Firestore;
using RecipeCosts.Services;
using RecipeCosts.Views;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCosts
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "google-services.json");

            var resourcePrefix = "RecipeCosts.";

#if __IOS__
            var resourcePrefix = "RecipeCosts.iOS.";
#endif
#if __ANDROID__
            var resourcePrefix = "RecipeCosts.Droid.";
#endif

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "recipecosts-service.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            File.WriteAllText(path, text);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
