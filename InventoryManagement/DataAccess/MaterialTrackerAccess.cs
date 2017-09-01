using InventoryManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess {
    public class MaterialTrackerAccess {

        public async Task<PagedList<MaterialTracker>> GetStock(int skip, int take, MaterialTrackerParameter condition) {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    string searchCondition = "";

                    switch (condition.StockCondition)
                    {
                        case "LESSMIN":
                            searchCondition += String.Format(@"

                            AND ((w2.Min_Stock * 0.1) + w2.Min_Stock) >= w2.TOTAL_QTY

                            {0}

                            ", Environment.NewLine);
                            break;

                        case "ORDERNOW":
                            searchCondition += String.Format(@"

                            AND CAST(w2.FTLASTUPD AS DATE) = '{0}'
                            AND ((w2.Min_Stock * 0.1) + w2.Min_Stock) >= w2.TOTAL_QTY
                            AND w2.Min_Stock > 0
                            AND w2.Suggest_Order > 0

                            {1}

                            ", condition.CurrentDateEng, Environment.NewLine);
                            break;

                        case "ALL":
                            break;
                    }

                    if (!String.IsNullOrEmpty(condition.MatrialCodeFrom)&&String.IsNullOrEmpty(condition.MatrialCodeTo))
                    {
                        searchCondition += String.Format(@"AND w2.FCCODE = '{0}'{1}", condition.MatrialCodeFrom, Environment.NewLine);
                    }
                    if (!String.IsNullOrEmpty(condition.MatrialCodeFrom) && !String.IsNullOrEmpty(condition.MatrialCodeTo))
                    {
                        searchCondition += String.Format(@"AND w2.Product_Code BETWEEN '{0}' AND '{1}' {2}", condition.MatrialCodeFrom,condition.MatrialCodeTo, Environment.NewLine);
                    }
                    if (condition.MatrialName != "" && condition.MatrialName != null)
                    {
                        searchCondition += String.Format(@"AND w2.Product_Name LIKE '{0}'{1}", condition.MatrialName, Environment.NewLine);
                    }
                    if (condition.VendorName != "" && condition.VendorName != null)
                    {
                        searchCondition += String.Format(@"AND w2.Vendor LIKE '{0}'{1}", condition.VendorName, Environment.NewLine);
                    }
                    if (condition.SaleName != "" && condition.SaleName != null)
                    {
                        searchCondition += String.Format(@"AND w2.Order_to LIKE '{0}'{1}", condition.SaleName, Environment.NewLine);
                    }
                    if (condition.Usability != "" && condition.Usability != null)
                    {
                        searchCondition += String.Format(@"AND w2.Operation_Detail LIKE '{0}'{1}", condition.Usability, Environment.NewLine);
                    }
                    if (!condition.StockCondition.Equals("ALL")) {//กรณีที่ ALL ไม่ต้องแยก Group
                        searchCondition += " AND w2.Product_Code NOT IN (SELECT Product_Code FROM Products_Model_Detail) ";
                    }

                    string strQuery = String.Format(StaticVariable.mainCommand, searchCondition);

                    File.WriteAllText(@"D:\GetStock.txt", strQuery);
                    var records = (dynamic)null;
                    var entities = new List<MaterialTracker>();

                    using (PMEntities db = new PMEntities())
                    {
                        db.Database.CommandTimeout = 180;
                        var _query = db.Database.SqlQuery<MaterialTracker>(strQuery).ToList();
                        records = _query.Count();
                        entities = _query.Skip(skip).Take(take).ToList();
                    }

                    //Check BATCH AND MASTER Add To Dic
                    List<String> lsProductHaveBM = new List<string>();

                    MaterialBatchAndMasterAccess matAccBm = new MaterialBatchAndMasterAccess();

                    foreach (MaterialTracker p in entities)
                    {
                        if (!string.IsNullOrEmpty(p.Product_Stock_Type))
                        {
                            List<String> lsProductBM = new List<string>();
                            if (p.Product_Stock_Type.Trim().Contains("BATCH"))
                            {
                                //Get List Batch
                                lsProductBM = matAccBm.GetListProductBatch(p.Product_Code.Trim().ToLower());
                            }
                            else if (p.Product_Stock_Type.Trim().Contains("MASTER"))
                            {
                                //Get List Master
                                lsProductBM = matAccBm.GetListProductMaster(p.Product_Code.Trim().ToLower());
                            }
                            foreach (String s in lsProductBM)
                            {
                                //Add To List
                                lsProductHaveBM.Add(s);
                            }
                        }

                    }
                    if (lsProductHaveBM.Count > 0)
                    {
                        //Get Stock Product Batch and Master
                        List<MaterialTracker> lsResultBM = matAccBm.GetStockBM(lsProductHaveBM);//หาคงเหลือ

                        //Add To Dic
                        if (lsResultBM.Count > 0)
                        {
                            foreach (MaterialTracker p in lsResultBM)
                            {
                                if (!StaticVariable.dicBM.ContainsKey(p.Product_Code.Trim().ToLower()))
                                {
                                    if (p.TOTAL_QTY <= p.Min_Stock)
                                    {//ตรวจสอบน้อยกว่า Min ค่อยจะออก
                                        StaticVariable.dicBM.Add(p.Product_Code.Trim().ToLower(), p);//Static Batch And master
                                    }
                                }
                            }
                        }
                    }

                    return new PagedList<MaterialTracker>
                    {
                        Entities = entities,
                        Records = records,
                        Pages = Math.Ceiling((double)records / take),
                        HasNext = (skip + take < records),
                        HasPrevious = (skip > 0)
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public async Task<MTPM> GetProductsModel(MaterialTrackerParameter condition)
        {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    MTPM mtpm = new MTPM();

                    string searchCondition = "";

                    searchCondition += String.Format(@"
                    AND CAST(w2.FTLASTUPD AS DATE) = '{0}'
                    AND ((w2.Min_Stock * 0.1) + w2.Min_Stock) >= w2.TOTAL_QTY
                    AND w2.Min_Stock > 0
                    AND w2.Suggest_Order > 0
                    {1}
                    ", condition.CurrentDateEng, Environment.NewLine);

                    //if (condition.MatrialCode != "" && condition.MatrialCode != null)
                    //{
                    //    searchCondition += String.Format(@"AND w2.FCCODE LIKE '{0}'{1}", condition.MatrialCode, Environment.NewLine);
                    //}
                    //if (condition.MatrialName != "" && condition.MatrialName != null)
                    //{
                    //    searchCondition += String.Format(@"AND w2.Product_Name LIKE '{0}'{1}", condition.MatrialName, Environment.NewLine);
                    //}
                    //if (condition.VendorName != "" && condition.VendorName != null)
                    //{
                    //    searchCondition += String.Format(@"AND w2.Vendor LIKE '{0}'{1}", condition.VendorName, Environment.NewLine);
                    //}
                    //if (condition.SaleName != "" && condition.SaleName != null)
                    //{
                    //    searchCondition += String.Format(@"AND w2.Order_to LIKE '{0}'{1}", condition.SaleName, Environment.NewLine);
                    //}
                    //if (condition.Usability != "" && condition.Usability != null)
                    //{
                    //    searchCondition += String.Format(@"AND w2.Operation_Detail LIKE '{0}'{1}", condition.Usability, Environment.NewLine);
                    //}

                    string strQuery = String.Format(StaticVariable.mainCommand, searchCondition);
                    List<Products_Model> pmEntities = null;
                    using (PMEntities db = new PMEntities())
                    {
                        db.Database.CommandTimeout = 300;
                        List<MaterialTracker> materialTrackerList = db.Database.SqlQuery<MaterialTracker>(strQuery).ToList();

                        List<string> prodList = new List<string>();
                        foreach (MaterialTracker m in materialTrackerList)
                        {
                            prodList.Add(string.Format("'{0}'", m.Product_Code));
                        }
                        string prodL = string.Join(",", prodList);

                        pmEntities = db.Database.SqlQuery<Products_Model>(
                                    string.Format(
                                    @"
                                        SELECT
	                                        p_m.Model_Code,
	                                        p_m.Model_Name
                                        FROM Products_Model_Detail p_m_d
                                        JOIN Products_Model p_m ON p_m.Model_Code = p_m_d.Model_Code
                                        WHERE p_m_d.Product_Code
                                        IN
                                        (
                                            {0}
                                        )
                                        GROUP BY
                                        p_m.Model_Code,
                                        p_m.Model_Name
                                    ", string.IsNullOrEmpty(prodL) ? "NULL" : prodL.ToLower())).ToList();

                        mtpm.ProductModelsList = pmEntities;
                        mtpm.ProductsModelDetailList = db.Products_Model_Detail.ToList();

                        //using (FileStream fs = new FileStream(@"D:\ProductModels.txt", FileMode.Append, FileAccess.Write))
                        //using (StreamWriter sw = new StreamWriter(fs))
                        //{
                        //     var sql = string.Format(
                        //                @"
                        //                    SELECT
                        //                     p_m.Model_Code,
                        //                     p_m.Model_Name
                        //                    FROM Products_Model_Detail p_m_d
                        //                    JOIN Products_Model p_m ON p_m.Model_Code = p_m_d.Model_Code
                        //                    WHERE p_m_d.Product_Code
                        //                    IN
                        //                    (
                        //                        {0}
                        //                    )
                        //                    GROUP BY
                        //                    p_m.Model_Code,
                        //                    p_m.Model_Name
                        //                ", string.IsNullOrEmpty(prodL) ? "NULL" : prodL.ToLower());

                        //    sw.WriteLine(sql);
                        //}
                    }

                    return mtpm;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task<List<MaterialTracker>> GetProductsModelDetail(List<string> prodL)
        {
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    List<MaterialTracker> materialTrackerList = null;

                    using (PMEntities db = new PMEntities())
                    {
                        db.Database.CommandTimeout = 300;
                        
                        List<string> sl = new List<string>();

                        if (prodL.Count > 0)
                        {
                            foreach (string s in prodL)
                            {
                                sl.Add(string.Format("'{0}'", s));
                            }
                        }
                        else
                        {
                            sl.Add("NULL");
                        }
                        
                        string condition = string.Format(@"
                                                AND w2.Product_Code IN ( {0} )
                                           ", string.Join(",", sl));

                        string strQuery = String.Format(StaticVariable.mainCommandGroup, condition);
                        materialTrackerList = db.Database.SqlQuery<MaterialTracker>(strQuery).ToList();

                        File.WriteAllText(@"D:\WriteText11.txt", strQuery);
                    }

                    return materialTrackerList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        //public Dictionary<string, string> getBatchMasterProd() {
        //    Dictionary<string, string> dictBatchMasterProd = new Dictionary<string, string>();
        //    List<Products_Mix_Type> lsMixType = new List<Products_Mix_Type>();
        //    using (PMEntities db = new PMEntities())
        //    {
        //        var query = from data in db.Products_Mix_Type
        //                    select data;
        //        lsMixType = query.ToList();
        //    }
        //    foreach (Products_Mix_Type item in lsMixType)
        //    {
        //        if (dictBatchMasterProd.ContainsKey(item.Product_Code.ToUpper()))
        //        {
        //            dictBatchMasterProd[item.Product_Code.ToUpper()] = "ALL";
        //        }
        //        else {
        //            dictBatchMasterProd.Add(item.Product_Code.ToUpper(), item.Product_Stock_Type.Trim());
        //        }
        //    }
        //    return dictBatchMasterProd;
        //}
        public Dictionary<String, String> GetProductsNameCustom() {
            Dictionary<String, String> dictCustom = new Dictionary<String, String>();
            using (var db = new PMEntities()) {
                var pCustom = from d in db.Products_Customized
                              select d;
                foreach (Products_Customized item in pCustom.ToList()) {
                    dictCustom.Add(item.CustomID.ToString(), item.CustomName.Trim());
                }
            }
            return dictCustom;
        }
    }
}
