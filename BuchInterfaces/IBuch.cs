using System.Collections.Generic;
using System.ComponentModel;

namespace BuchInterfaces
{
    public interface IBuch : INotifyPropertyChanged
    {
        string Titel { get; }
        List<string> Autoren { get; }
        string AutorenAlsString { get; }
        string ISBN { get; }
        string Vorschaulink { get; }
        bool IstFavorit { get; set; }
    }
}