using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuchInterfaces;
using Newtonsoft.Json;

namespace GoogleBooksBibliotheken
{

    public class BuchSuchErgebnis
    {
        [JsonProperty("items")]
        public Buch[] bücher { get; set; }
    }

    public class Buch : IBuch
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }

        public virtual Volumeinfo volumeInfo { get; set; }

        public string Titel => volumeInfo?.title;

        public List<string> Autoren => volumeInfo?.authors?.ToList();

        public string ISBN
        {
            get
            {
                //Diese Prüfung sorgt dafür dass das DataGrid beim löschen von Einträgen
                //in der BindingList keine DataError-Exception mehr wirft
                if (volumeInfo?.industryIdentifiers?.Count > 0)
                {
                    return volumeInfo?.industryIdentifiers?.ElementAt(0)?.identifier;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Vorschaulink => volumeInfo?.previewLink;

        private bool _istFavorite = false;
        public bool IstFavorit
        {
            get
            {
                return _istFavorite;
            }
            set
            {
                if (value != _istFavorite)
                {
                    _istFavorite = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IstFavorit)));
                }
            }
        }

        public string AutorenAlsString
        {
            get
            {
                if (Autoren == null || Autoren.Count == 0)
                {
                    return string.Empty;
                }
                return string.Join(", ", Autoren);
            }
        }
    }

    public class Volumeinfo
    {
        private string _title;

        public int Id { get; set; }
        public string title
        {
            get => _title;
            set => _title = value;
        }
        public virtual List<Industryidentifier> industryIdentifiers { get; set; }
        public virtual Imagelinks imageLinks { get; set; }
        public string previewLink { get; set; }
        public string[] authors { get; set; }

        //Nur für Entitiy Framework
        public string ListString
        {
            get
            {

                if (authors == null)
                    return string.Empty;
                return string.Join(",", authors);
            }
            set
            {
                authors = value?.Split(',').ToArray();
            }
        }
    }

    public class Imagelinks
    {
        public int Id { get; set; }
        public string smallThumbnail { get; set; }
    }

    public class Industryidentifier
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string identifier { get; set; }
    }
}