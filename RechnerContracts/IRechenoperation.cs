﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerContracts
{
    public interface IRechenoperation
    {
        double Berechne(IGeparsteAufgabe formel);

        char Symbol { get; }
    }
}
