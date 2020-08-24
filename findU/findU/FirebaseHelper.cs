using findU.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace findU
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient(Common.FirebaseURL);
        public async Task<List<Person>> GetAllPersons()
        {

            return (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Select(item => new Person
              {
                  Name = item.Object.Name,
                  PersonId = item.Object.PersonId,
                  IsOnline =item.Object.IsOnline,
                  Message=item.Object.Message,
                  Latitude =item.Object.Latitude,
                  Longitude =item.Object.Longitude,
                  LastUpdatedTime = item.Object.LastUpdatedTime,
                  
                  
              }).ToList();
        }

        public async Task<bool> checkExistingUser(string personName)
        {
            var allPersons = await GetAllPersons();
            await firebase.Child("Persons").OnceAsync<Person>();
            int res = allPersons.Where(a => a.Name == personName).Count();
            if (res > 0)
            {
                return true;
            }

            return false;
            
        }

        public async Task AddPerson(int personId, string name, bool isOnline, string latitude, string longitude, string message)
        {

            await firebase
              .Child("Persons")
              .PostAsync(new Person() { PersonId = personId, Name = name, IsOnline= isOnline, Latitude= latitude, Longitude= longitude, Message= message, LastUpdatedTime=DateTime.Now.ToString() });
        }

        public async Task<Person> GetPerson(int personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<Person>();
            return allPersons.Where(a => a.PersonId == personId).FirstOrDefault();
        }

        public async Task UpdatePerson(int personId, string name)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Person() { PersonId = personId, Name = name });
        }

        public async Task DeletePerson(int personId)
        {
            var toDeletePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();

        }


        public async Task UpdateLocalUserLocation(double latitude, double longitude, bool isOnline)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.PersonId == Common.LocalUserId).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Person() 
              {
                  PersonId = Common.LocalUserId,
                  Latitude = latitude.ToString(),
                  Longitude = longitude.ToString(),
                  LastUpdatedTime= DateTime.Now.ToString(),
                  IsOnline = isOnline
              });
        }


        public async Task<int> GetMaxUserID()
        {
            var allPersons = await GetAllPersons();
            await firebase .Child("Persons").OnceAsync<Person>();
            return allPersons.Max(x=>x.PersonId);

        }



    }
}
