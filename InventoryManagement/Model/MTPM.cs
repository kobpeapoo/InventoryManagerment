using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Model
{
    public class MTPM
    {
        public List<Products_Model> ProductModelsList { get; set; }
        public List<Products_Model_Detail> ProductsModelDetailList { get; set; }
    }
}
