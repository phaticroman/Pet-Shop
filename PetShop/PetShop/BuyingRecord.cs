using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
   public class BuyingRecord
    {
        public int Id { get; set; }
        public string customerName { get; set; }
        public string? petName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string PetType { get; set; }
        public bool IsAddedToStore { get; set; }

    }
}
