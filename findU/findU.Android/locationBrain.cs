using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Work;
using Xamarin.Essentials;

namespace findU.Droid
{
    public class locationBrain : Worker
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public locationBrain(Context context, WorkerParameters workerParameters) : base(context, workerParameters)
        {

        }

        public override Result DoWork()
        {
            //Android.Util.Log.Debug("CalculatorWorker", $"Your Tax Return is: 1");
            getlocation();
            return Result.InvokeSuccess();
        }

        private async void getlocation()
        {

            while (1 == 1)
            {

                
                try
                {
                    //get local location
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    }

                    //update Datbase
                    await firebaseHelper.UpdateLocalUserLocation(location.Latitude, location.Longitude, false);

                    if (!Common.IsUserOnline)
                    {
                        await Task.Delay(10000);
                    }
                    else
                    {
                        await Task.Delay(20000);
                    }

                    
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to get location
                }
            }



        }
    }
}