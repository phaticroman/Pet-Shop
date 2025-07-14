using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class PetDetails
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CageId { get; set; }
        public Cage Cage { get; set; }
        public bool IsSold { get; set; }

        public int BuyingRecordId { get; set; }
        public BuyingRecord BuyingRecord { get; set; }

    }
}
