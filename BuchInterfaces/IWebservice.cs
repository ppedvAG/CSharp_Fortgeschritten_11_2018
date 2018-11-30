using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuchInterfaces
{
    public interface IWebservice
    {
        Task<List<IBuch>> SucheBücher(string suchbegriff);
    }
}
