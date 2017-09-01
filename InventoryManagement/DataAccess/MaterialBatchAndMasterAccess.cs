using InventoryManagement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess {
    public class MaterialBatchAndMasterAccess {
        public List<MaterialTracker> GetDataBatchAndMaster(string Product_Code, string mix_product_type) {

            List<MaterialTracker> _query = new List<MaterialTracker>();

            string sqlCondition = "";
            if (mix_product_type.Trim().Equals("BATCH"))
            {
                sqlCondition = StaticVariable.queryMasterOfBatch;
            }
            else {
                sqlCondition = StaticVariable.queryBatchOfMaster;
            }
            sqlCondition = String.Format(sqlCondition, Product_Code);
            string sqlQuery = String.Format(StaticVariable.mainCommand, " AND w2.Product_Code IN (" + sqlCondition + ") ");

            File.WriteAllText(@"D:\GetDataBatchAndMaster.txt", sqlQuery);

            using (PMEntities db = new PMEntities())
            {
                db.Database.CommandTimeout = 180;
                _query = db.Database.SqlQuery<MaterialTracker>(sqlQuery).ToList();
            }

            return _query;
        }
        public List<MaterialTracker> GetStockBM(List<String> lsProductBM) {


            List<MaterialTracker> lsMaterialResult = new List<MaterialTracker>();
            try
            {
                string cond = " AND h.Product_Code IN ({0})";
                string condParam = string.Join(",", lsProductBM.Select(x => "'" + x.ToString() + "'").ToArray());

                cond = String.Format(cond, condParam);

                string strQuery = String.Format(StaticVariable.mainCommand, cond);
                File.WriteAllText(@"D:\GetStockBM.txt", strQuery);

                using (PMEntities db = new PMEntities())
                {
                    db.Database.CommandTimeout = 180;
                    var _query = db.Database.SqlQuery<MaterialTracker>(strQuery).ToList();
                    lsMaterialResult = _query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lsMaterialResult;
        }
        public List<String> GetListProductBatch(string product_Code) {
            List<String> lsProduct = new List<string>();
            string strQuery = String.Format(StaticVariable.queryMasterOfBatch, product_Code);
            using (PMEntities db = new PMEntities())
            {
                db.Database.CommandTimeout = 180;
                var _query = from data in db.Products_Mix_Code
                             where data. Product_Code_Batch == "" + product_Code
                             select new { Product_Code = data.Product_Code_Master };
                var result = _query.ToList();
                foreach (var s in result)
                {
                    lsProduct.Add(s.Product_Code);
                }
            }
            return lsProduct;
        }
        public List<String> GetListProductMaster(string product_Code) {
            List<String> lsProduct = new List<string>();
            string strQuery = String.Format(StaticVariable.queryBatchOfMaster, product_Code);
            using (PMEntities db = new PMEntities())
            {
                db.Database.CommandTimeout = 180;
                var _query = from data in db.Products_Mix_Code
                             where data.Product_Code_Master == "" + product_Code
                             select new { Product_Code = data.Product_Code_Batch };
                var result = _query.ToList();
                foreach (var s in result) {
                    lsProduct.Add(s.Product_Code);
                }
            }
            return lsProduct;
        }

    }

}
