﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class FeedSchedule
    {
        public int Id { get; set; }
        public int CageId { get; set; }
        public Cage Cage { get; set; }
        public DateTime Time { get; set; }
        public string Food { get; set; }
    }
}
