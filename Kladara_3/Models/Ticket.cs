﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kladara_3.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Wager { get; set; }
        public int PossibleGain { get; set; }
        public string Pairs { get; set; }
    }
}
