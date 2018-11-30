using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuchInterfaces
{
    public interface IBuchSpeicher
    {
        void Speichern(List<IBuch> bücher);
        List<IBuch> Laden();
    }
}
