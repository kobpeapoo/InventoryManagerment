using InventoryManagerment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess
{
    public class ProductOrderReportAccess
    {
        public bool addProductOrderReport(Product_Order_Report prdReportDetail, List<Product_Order_Report_Details> pordList)
        {
            bool result = false;

            try
            {
                using (PMEntities db = new PMEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Product_Order_Report.Add(prdReportDetail);
                            db.SaveChanges();

                            foreach (Product_Order_Report_Details item in pordList) {
                                item.Order_ID = prdReportDetail.Order_ID;
                                db.Product_Order_Report_Details.Add(item);
                                db.SaveChanges();
                            }

                            dbContextTransaction.Commit();
                        }
                        catch
                        {
                            dbContextTransaction.Rollback();
                        }
                    }                    
                    
                }
                result = true;
            }
            catch (Exception ex) {
                throw ex;
            }

            return result;
        }

        public List<OrderListModel> getOrderListModel(int Order_ID)
        {
            List<OrderListModel> lsOrder = new List<OrderListModel>();
            try
            {
                using (var db = new PMEntities())
                {
                    var dataOrder = from a in db.Product_Order_Report
                                    join b in db.Product_Order_Report_Details
                                    on a.Order_ID equals b.Order_ID
                                    join c in db.V_PRODUCT_MONITOR_PROPERTIES
                                    on b.Product_Code.ToLower() equals c.Product_Code.ToLower()
                                    where a.Order_ID == Order_ID
                                    select new OrderListModel
                                    {
                                        OrderNumber = b.List_Num_Order.ToString(),
                                        List_Num_Order = b.List_Num_Order.ToString(),
                                        CreateDate = a.CreateDate,
                                        CreateTimestamp = a.CreateTimestamp,
                                        Product_code = b.Product_Code,
                                        Product_Name = c.Product_Name,
                                        Use = b.Operation_Detail,
                                        Topic = b.Vendor,
                                        ReaminTD = (double)b.TD_QTY,
                                        RemainLP = (double)b.LP_QTY,
                                        RemainAll = (double)b.TOTAL_QTY,
                                        Suggest_Order = (double)b.Suggest_Order,
                                        PUnit_Name = b.PUnit_Name,
                                        SUnit_Name = b.SUnit_Name,
                                        AccountCode = "ราคาตั้ง : ",
                                        MinumunStock = (double)b.Min_Stock,
                                        ToOwner = b.Order_to,
                                        Note_Vendor = b.Note_Vendor,
                                        Purchase_Name = b.Purchase_Name,
                                        Alert_Name = (b.Alert_ID.ToString().Equals("1")&&!String.IsNullOrEmpty(b.Alert_ID.ToString())) ? "สั่งทำ" : "สั่งซื้อ"
                                    };

                    //Console.WriteLine(dataOrder);
                    //Console.ReadKey();
                    lsOrder = dataOrder.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lsOrder;
        }
    }
}
