﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage.Core.Entity
{
    public class Assurance
    {
       
            public int Id { get; set; }
            public decimal Montant { get; set; }
            public TypeAssurance TypeAssurance { get; set; }
    }
    public enum TypeAssurance { Annulation = 1}
}
