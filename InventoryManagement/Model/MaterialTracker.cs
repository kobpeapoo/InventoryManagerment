using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Model {
    public class MaterialTracker {

        public bool Chk { get; set; }
        public string Product_Code { get; set; }
        public string Product_Name { get; set; }
        public string Operation_Detail { get; set; }
        public decimal Min_Stock { get; set; }
        public string Order_to { get; set; }
        public string Check_TD { get; set; }
        public string Check_LP { get; set; }
        public decimal TD_QTY { get; set; }
        public decimal LP_QTY { get; set; }
        public decimal TD_MV { get; set; }
        public decimal LP_MV { get; set; }
        public string IN_OUT { get; set; }
        public decimal TOTAL_QTY { get; set; }
        public string SUnit_Name { get; set; }
        public double? Total_ConvertQTY { get; set; }
        public string PUnit_Name { get; set; }
        public double? FACTOR { get; set; }
        public string Note_Unit_Convert { get; set; }
        public double? Suggest_Order { get; set; }
        public string Note_Purchase { get; set; }
        public decimal FNAVGCOST { get; set; }

        public string History_Date { get; set; }
        public string Vendor { get; set; }
        public string Note_Vendor { get; set; }
        public string Vendor_to_Purchase { get; set; }
        public string P0 { get; set; }
        public string P1 { get; set; }
        public string P2 { get; set; }
        public string P3 { get; set; }
        public string P_Note { get; set; }
        public string Vat { get; set; }
        public string S0 { get; set; }
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S_Note { get; set; }
        public string QC_Form { get; set; }
        public string Product_Stock_Type { get; set; }
        public string BATCH
        {
            get {
                if (!String.IsNullOrEmpty(this.Product_Stock_Type))
                {
                    if (this.Product_Stock_Type.Trim().Equals("BATCH"))
                    {
                        return Product_Stock_Type.Trim();
                    }
                    else if (this.Product_Stock_Type.Trim().Equals("ALL")) {
                        return "BATCH";
                    }
                    else {
                        return "-";
                    }
                }
                else
                {
                    return "-";
                }
            }

        }
        public string MASTER
        {
            get {
                if (!String.IsNullOrEmpty(this.Product_Stock_Type))
                {
                    if (this.Product_Stock_Type.Trim().Equals("MASTER"))
                    {
                        return Product_Stock_Type.Trim();
                    }
                    else if (this.Product_Stock_Type.Trim().Equals("ALL"))
                    {
                        return "MASTER";
                    }
                    else {
                        return "-";
                    }
                }
                else {
                    return "-";
                }
                    

            }
        }

    }
}
