using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using AndroidX.Work;


namespace findU.Droid
{
    [Activity(Label = "findU", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequestLocationId = 0;

        readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);


            Constraints constraints = new Constraints();
            constraints.SetRequiresStorageNotLow(true);
            constraints.SetRequiresBatteryNotLow(true);
            constraints.RequiredNetworkType = NetworkType.Connected;

            PeriodicWorkRequest taxWorkRequest = PeriodicWorkRequest
                .Builder.From<locationBrain>(TimeSpan.FromMinutes(16))
                .SetConstraints(constraints).Build();

            String TAG = "locationWork";
            WorkManager.Instance.EnqueueUniquePeriodicWork(TAG, ExistingPeriodicWorkPolicy.Keep, taxWorkRequest);



            LoadApplication(new App());

            Common.IsUserOnline = true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnDestroy()
        {

            Common.IsUserOnline = false;
            base.OnDestroy();
        }


        protected override void OnPause()
        {
            Common.IsUserOnline = false;
            base.OnPause();
        }
    }
}