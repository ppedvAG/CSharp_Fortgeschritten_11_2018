using BuchInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoogleBooksBibliotheken
{
    public class GoogleBooksAPI : IWebservice
    {
        public async Task<List<IBuch>> SucheBücher(string suchbegriff)
        {
            if (string.IsNullOrWhiteSpace(suchbegriff))
                return new List<IBuch>();

            HttpClient client = new HttpClient();
            Task<string> task = client.GetStringAsync($"https://www.googleapis.com/books/v1/volumes?q={suchbegriff}&fields=items(volumeInfo(authors%2CimageLinks%2FsmallThumbnail%2CindustryIdentifiers%2CpreviewLink%2Ctitle))");

            //await wartet in einem parallelen Thread auf das Ergebnis der Task
            string jsonString = await task;

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Auto;

            BuchSuchErgebnis ergebnis = JsonConvert.DeserializeObject<BuchSuchErgebnis>(jsonString, settings);
            //Kopierkonstruktor
            if (ergebnis?.bücher == null || ergebnis.bücher.Length == 0)
                throw new Exception("Keine Bücher gefunden!");

            return new List<IBuch>(ergebnis.bücher.ToList());
        }
    }
}
