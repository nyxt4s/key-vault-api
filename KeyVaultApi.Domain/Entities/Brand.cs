using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultApi.Domain.Entities
{
    public class Brand
    {
        public int BrandId { get; set; }
        public int BusinessID { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }

    }
}
