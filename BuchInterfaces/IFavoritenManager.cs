using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuchInterfaces
{
    public interface IFavoritenManager
    {
        List<IBuch> Favoriten { get; }
        void FügeAlsFavoritHinzu(IBuch buch);
        void EntferneAlsFavorit(IBuch buch);
        IList<IBuch> SyncList { get; set;  }
    }
}
