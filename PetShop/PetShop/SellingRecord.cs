using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
   public class SellingRecord
    {
        public int Id { get; set; }
        public string? petname { get; set; }
        public string BuyerName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string PetType { get; set; }
        public decimal profit { get; set; }
    }
}
