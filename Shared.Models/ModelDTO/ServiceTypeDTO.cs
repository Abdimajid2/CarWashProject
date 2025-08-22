using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.ModelDTO
{
   public class ServiceTypeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public Decimal Price { get; set; }
    }
}
