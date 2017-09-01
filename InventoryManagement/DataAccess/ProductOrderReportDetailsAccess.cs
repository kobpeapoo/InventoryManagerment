using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess
{
    public class ProductOrderReportDetailsAccess
    {
        public bool addProductOrderReportDetails(Product_Order_Report_Details prdReportDetail)
        {
            bool result = false;

            try
            {
                using (PMEntities db = new PMEntities())
                {
                    db.Product_Order_Report_Details.Add(prdReportDetail);
                    db.SaveChanges();
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;

        }
    }
}
