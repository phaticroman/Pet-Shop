using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class Cage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int? spaceremain { get; set; }
        public ICollection<PetDetails> pets { get; set; }

    }
}
