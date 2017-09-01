using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Model
{
    public class MaterialTrackerParameter
    {
        public string CurrentDate { get; set; }
        public string CurrentDateEng { get; set; }

        public string MatrialCode { get; set; }
        public string MatrialCodeFrom { get; set; }
        public string MatrialCodeTo { get; set; }
        public string MatrialName { get; set; }
        public string SaleName { get; set; }
        public string VendorName { get; set; }
        public string Usability { get; set; }
        public string StockCondition { get; set; }
    }
}
