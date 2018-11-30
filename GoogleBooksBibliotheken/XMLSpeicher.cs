using BuchInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GoogleBooksBibliotheken
{
    public class XMLSpeicher : IBuchSpeicher
    {
        const string Dateipfad = "Favoriten.fbs";

        public List<IBuch> Laden()
        {
            if(!File.Exists(Dateipfad))
            {
                return new List<IBuch>();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Buch>));
            using (FileStream datei = new FileStream(Dateipfad, FileMode.Open))
            {
                try
                {
                    List<Buch> result = (List<Buch>)serializer.Deserialize(datei);
                    return new List<IBuch>(result);
                }
                catch (Exception)
                {
                    return new List<IBuch>();
                }  
            }
        }

        public void Speichern(List<IBuch> bücher)
        {
            List<Buch> bücherKonkret = new List<Buch>();
            foreach (var item in bücher)
            {
                bücherKonkret.Add((Buch)item);
            }
            SerializeAsync(bücherKonkret);

            
        }

        public async void SerializeAsync(List<Buch> bücherKonkret)
        {
            await Task.Delay(0);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Buch>));
            using (FileStream datei = new FileStream(Dateipfad, FileMode.Create))
            {

                serializer.Serialize(datei, bücherKonkret);
            }
        }
    }
}
