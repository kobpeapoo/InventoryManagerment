using InventoryManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement {
    public static class StaticVariable {
        public static string mainCommand {
            get{
                return @"
                    WITH with1 AS
                    (
	                    SELECT DISTINCT
		                    b.FCCODE,
		                    CAST(a.FTLASTUPD AS DATE) AS FTLASTUPD,
		                    f.Min_Stock,
		                    ISNULL
		                    (
			                    (
				                    SELECT
					                    SUM
					                    (
						                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
							                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
						                    END
					                    ) AS LP_MV
				                    FROM REFPROD_PM sub1
				                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
				                    WHERE 1=1 
				                    AND sub1.FTLASTUPD = CAST(a.FTLASTUPD AS DATE)
				                    AND sub2.FCCODE = b.FCCODE
				                    AND sub1.FCWHOUSE = 'Xัฆd(((-'
				                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
			                    ) , 0
		                    ) AS LP_MV,
		                    ISNULL
		                    (
			                    (
				                    NULL -- need null for emtpy data
			                    ) , 0
		                    ) AS TD_MV,
		                    ISNULL
		                    (
			                    (
				                    SELECT
					                    SUM
					                    (
						                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
							                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
						                    END
					                    ) AS LP_MV
				                    FROM REFPROD_PM sub1
				                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
				                    WHERE 1=1 
				                    AND sub1.FTLASTUPD <= CAST(a.FTLASTUPD AS DATE)
				                    AND sub2.FCCODE = b.FCCODE
				                    AND sub1.FCWHOUSE = 'Xัฆd(((-'
				                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
			                    ) , 0
		                    ) AS LP_QTY,
		                    ISNULL
		                    (
			                    (
				                    NULL -- need null for emtpy data
			                    ) , 0
		                    ) AS TD_QTY,
		                    (
			                    ISNULL
			                    (
				                    (
					                    SELECT
						                    SUM
						                    (
							                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
								                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
							                    END
						                    ) AS LP_MV
					                    FROM REFPROD_PM sub1
					                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
					                    WHERE 1=1 
					                    AND sub1.FTLASTUPD <= CAST(a.FTLASTUPD AS DATE)
					                    AND sub2.FCCODE = b.FCCODE
					                    AND sub1.FCWHOUSE = 'Xัฆd(((-'
					                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
				                    ) , 0
			                    )
			                    +
			                    ISNULL
			                    (
				                    (
					                    SELECT
						                    SUM
						                    (
							                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
								                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
							                    END
						                    ) AS LP_MV
					                    FROM REFPROD_PM sub1
					                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
					                    WHERE 1=1 
					                    AND sub1.FTLASTUPD <= CAST(a.FTLASTUPD AS DATE)
					                    AND sub2.FCCODE = b.FCCODE
					                    AND sub1.FCWHOUSE = 'npgQ((()'
					                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
				                    ) , 0
			                    )
		                    ) AS 'TOTAL_QTY',
		                    f.Product_Type,
		                    f.Product_Name,
		                    f.Stock_Unit,
		                    f.PurchASe_Unit,
		                    f.Suggest_Order,
		                    f.FACTOR,
		                    f.PUnit_Name,
		                    f.SUnit_Name,
		                    f.Operation_Detail,
		                    f.Check_LP,
		                    f.Check_TD,
		                    f.Note_Unit_Convert,
		                    f.Note_PurchASe,
		                    f.Order_to,
		                    g.Note_Vendor,
		                    g.P_Note,
		                    g.P0,
		                    g.P1,
		                    g.P2,
		                    g.P3,
		                    g.Product_Code,
		                    g.S_Note,
		                    g.S0,
		                    g.S1,
		                    g.S2,
		                    g.S3,
		                    g.vat,
		                    g.Vendor,
		                    g.Vendor_to_PurchASe,
		                    g.QC_Form
	                    FROM REFPROD_PM a
	                    JOIN PROD b ON a.FCPROD = b.FCSKID
	                    LEFT JOIN V_PRODUCT_MONITOR_PROPERTIES f ON LOWER(b.FCCODE) = LOWER(f.Product_Code) COLLATE Thai_CI_AI 
	                    LEFT JOIN
	                    (
		                    SELECT a.*
		                    FROM Stock_By_Date_History a 
		                    JOIN
		                    (
			                    SELECT
				                    Product_Code,
				                    MAX(History_Date) AS  History_Date
			                    FROM Stock_By_Date_History
			                    GROUP BY Product_Code
		                    ) b ON a.Product_Code = b.Product_Code AND a.History_Date = b.History_Date
	                    ) g ON LOWER(b.FCCODE) = LOWER(g.Product_Code) COLLATE Thai_CI_AI
	                    WHERE 1=1
	                    AND (a.FCWHOUSE = 'Xัฆd(((-' OR a.FCWHOUSE = 'npgQ((()') 
	                    AND f.Product_Type = 4
	                    AND (f.Suggest_Order > 0)
	                    GROUP BY
		                    a.FCWHOUSE,
		                    b.FCCODE,
		                    CAST(a.FTLASTUPD AS DATE),
		                    a.FCIOTYPE,
		                    f.Product_Type,
		                    f.Product_Name,
		                    f.Order_to,
		                    f.Stock_Unit,
		                    f.PurchASe_Unit,
		                    f.Min_Stock,
		                    f.Suggest_Order,
		                    f.FACTOR,
		                    f.PUnit_Name,
		                    f.SUnit_Name,
		                    f.Operation_Detail,
		                    f.Check_LP,
		                    f.Check_TD,
		                    f.Note_Unit_Convert,
		                    f.Note_PurchASe,
		                    f.Order_to,
		                    g.Note_Vendor,
		                    g.P_Note,
		                    g.P0,
		                    g.P1,
		                    g.P2,
		                    g.P3,
		                    g.Product_Code,
		                    g.S_Note,
		                    g.S0,
		                    g.S1,
		                    g.S2,
		                    g.S3,
		                    g.vat,
		                    g.Vendor,
		                    g.Vendor_to_PurchASe,
		                    g.QC_Form
                    ),
                    with2 AS
                    (
	                    SELECT
		                    ROW_NUMBER() OVER(PARTITION BY w1.FCCODE ORDER BY MAX(CAST(tb1.FTLASTUPD AS DATE)) DESC) AS RN,
		                    w1.FCCODE,
		                    MAX(CAST(tb1.FTLASTUPD AS DATE)) AS FTLASTUPD,
		                    CAST(w1.Min_Stock AS DECIMAL(20,3)) AS Min_Stock,
		                    CAST(w1.LP_MV AS DECIMAL(20,3)) AS LP_MV,
		                    CAST(w1.LP_QTY AS DECIMAL(20,3)) AS LP_QTY,
		                    CAST(w1.TD_MV AS DECIMAL(20,3)) AS TD_MV,
		                    CAST(w1.TD_QTY AS DECIMAL(20,3)) AS TD_QTY,
		                    CAST(w1.TOTAL_QTY AS DECIMAL(20,3)) AS TOTAL_QTY,
		                    w1.Product_Type,
		                    w1.Product_Name,
		                    w1.Stock_Unit,
		                    w1.PurchASe_Unit,
		                    w1.Suggest_Order,
		                    w1.FACTOR,
		                    w1.PUnit_Name,
		                    w1.SUnit_Name,
		                    w1.Operation_Detail,
		                    w1.Check_LP,
		                    w1.Check_TD,
		                    w1.Note_Unit_Convert,
		                    w1.Note_PurchASe,
		                    w1.Order_to,
		                    w1.Note_Vendor,
		                    w1.P_Note,
		                    w1.P0,
		                    w1.P1,
		                    w1.P2,
		                    w1.P3,
		                    w1.Product_Code,
		                    w1.S_Note,
		                    w1.S0,
		                    w1.S1,
		                    w1.S2,
		                    w1.S3,
		                    w1.vat,
		                    w1.Vendor,
		                    w1.Vendor_to_PurchASe,
		                    w1.QC_Form
	                    FROM REFPROD_PM tb1
	                    JOIN PROD tb2 ON tb1.FCPROD = tb2.FCSKID
	                    JOIN with1 w1 ON w1.FCCODE = tb2.FCCODE AND w1.FTLASTUPD = tb1.FTLASTUPD
	                    GROUP BY
		                    tb2.FCCODE,
		                    w1.FCCODE,
		                    w1.Min_Stock,
		                    w1.LP_MV,
		                    w1.LP_QTY,
		                    w1.TD_MV,
		                    w1.TD_QTY,
		                    w1.TOTAL_QTY,
		                    w1.Product_Type,
		                    w1.Product_Name,
		                    w1.Stock_Unit,
		                    w1.PurchASe_Unit,
		                    w1.Suggest_Order,
		                    w1.FACTOR,
		                    w1.PUnit_Name,
		                    w1.SUnit_Name,
		                    w1.Operation_Detail,
		                    w1.Check_LP,
		                    w1.Check_TD,
		                    w1.Note_Unit_Convert,
		                    w1.Note_PurchASe,
		                    w1.Order_to,
		                    w1.Note_Vendor,
		                    w1.P_Note,
		                    w1.P0,
		                    w1.P1,
		                    w1.P2,
		                    w1.P3,
		                    w1.Product_Code,
		                    w1.S_Note,
		                    w1.S0,
		                    w1.S1,
		                    w1.S2,
		                    w1.S3,
		                    w1.vat,
		                    w1.Vendor,
		                    w1.Vendor_to_PurchASe,
		                    w1.QC_Form
                    ),
					with3 AS 
					(
						SELECT DISTINCT a.Product_Code,
							CASE WHEN ( SELECT COUNT(*) FROM Products_Mix_Type b WHERE a.Product_Code = b.Product_Code) = 2 THEN 'ALL'
							WHEN ( SELECT COUNT(*) FROM Products_Mix_Type b WHERE a.Product_Code = b.Product_Code) = 1 THEN a.Product_Stock_Type
							END AS Product_Stock_Type
							from Products_Mix_Type a 
						GROUP BY a.Product_Code,a.Product_Stock_Type
					)
                    SELECT w2.*,h.Product_Stock_Type
                    FROM with2 w2
					LEFT JOIN with3 h ON LOWER(w2.FCCODE) = LOWER(h.Product_Code) COLLATE Thai_CI_AI
                    WHERE w2.RN = 1

                    {0}

                    ";
            }
            }
        public static string mainCommandGroup
        {
            get {
                return @"
                    WITH with1 AS
                    (
	                    SELECT DISTINCT
		                    b.FCCODE,
		                    CAST(a.FTLASTUPD AS DATE) AS FTLASTUPD,
		                    f.Min_Stock,
		                    ISNULL
		                    (
			                    (
				                    SELECT
					                    SUM
					                    (
						                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
							                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
						                    END
					                    ) AS LP_MV
				                    FROM REFPROD_PM sub1
				                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
				                    WHERE 1=1 
				                    AND sub1.FTLASTUPD = CAST(a.FTLASTUPD AS DATE)
				                    AND sub2.FCCODE = b.FCCODE
				                    AND sub1.FCWHOUSE = 'Xัฆd(((-'
				                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
			                    ) , 0
		                    ) AS LP_MV,
		                    ISNULL
		                    (
			                    (
				                    NULL -- need null for emtpy data
			                    ) , 0
		                    ) AS TD_MV,
		                    ISNULL
		                    (
			                    (
				                    SELECT
					                    SUM
					                    (
						                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
							                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
						                    END
					                    ) AS LP_MV
				                    FROM REFPROD_PM sub1
				                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
				                    WHERE 1=1 
				                    AND sub1.FTLASTUPD <= CAST(a.FTLASTUPD AS DATE)
				                    AND sub2.FCCODE = b.FCCODE
				                    AND sub1.FCWHOUSE = 'Xัฆd(((-'
				                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
			                    ) , 0
		                    ) AS LP_QTY,
		                    ISNULL
		                    (
			                    (
				                    NULL -- need null for emtpy data
			                    ) , 0
		                    ) AS TD_QTY,
		                    (
			                    ISNULL
			                    (
				                    (
					                    SELECT
						                    SUM
						                    (
							                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
								                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
							                    END
						                    ) AS LP_MV
					                    FROM REFPROD_PM sub1
					                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
					                    WHERE 1=1 
					                    AND sub1.FTLASTUPD <= CAST(a.FTLASTUPD AS DATE)
					                    AND sub2.FCCODE = b.FCCODE
					                    AND sub1.FCWHOUSE = 'Xัฆd(((-'
					                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
				                    ) , 0
			                    )
			                    +
			                    ISNULL
			                    (
				                    (
					                    SELECT
						                    SUM
						                    (
							                    CASE WHEN sub1.FCIOTYPE ='O' THEN -sub1.FNQTY
								                    WHEN sub1.FCIOTYPE='I' THEN sub1.FNQTY ELSE 0
							                    END
						                    ) AS LP_MV
					                    FROM REFPROD_PM sub1
					                    JOIN PROD sub2 ON sub1.FCPROD = sub2.FCSKID
					                    WHERE 1=1 
					                    AND sub1.FTLASTUPD <= CAST(a.FTLASTUPD AS DATE)
					                    AND sub2.FCCODE = b.FCCODE
					                    AND sub1.FCWHOUSE = 'npgQ((()'
					                    GROUP BY sub2.FCCODE, sub1.FCWHOUSE
				                    ) , 0
			                    )
		                    ) AS 'TOTAL_QTY',
		                    f.Product_Type,
		                    f.Product_Name,
		                    f.Stock_Unit,
		                    f.PurchASe_Unit,
		                    f.Suggest_Order,
		                    f.FACTOR,
		                    f.PUnit_Name,
		                    f.SUnit_Name,
		                    f.Operation_Detail,
		                    f.Check_LP,
		                    f.Check_TD,
		                    f.Note_Unit_Convert,
		                    f.Note_PurchASe,
		                    f.Order_to,
		                    g.Note_Vendor,
		                    g.P_Note,
		                    g.P0,
		                    g.P1,
		                    g.P2,
		                    g.P3,
		                    g.Product_Code,
		                    g.S_Note,
		                    g.S0,
		                    g.S1,
		                    g.S2,
		                    g.S3,
		                    g.vat,
		                    g.Vendor,
		                    g.Vendor_to_PurchASe,
		                    g.QC_Form
	                    FROM REFPROD_PM a
	                    JOIN PROD b ON a.FCPROD = b.FCSKID
	                    LEFT JOIN V_PRODUCT_MONITOR_PROPERTIES f ON LOWER(b.FCCODE) = LOWER(f.Product_Code) COLLATE Thai_CI_AI 
	                    LEFT JOIN
	                    (
		                    SELECT a.*
		                    FROM Stock_By_Date_History a 
		                    JOIN
		                    (
			                    SELECT
				                    Product_Code,
				                    MAX(History_Date) AS  History_Date
			                    FROM Stock_By_Date_History
			                    GROUP BY Product_Code
		                    ) b ON a.Product_Code = b.Product_Code AND a.History_Date = b.History_Date
	                    ) g ON LOWER(b.FCCODE) = LOWER(g.Product_Code) COLLATE Thai_CI_AI
	                    WHERE 1=1
	                    AND (a.FCWHOUSE = 'Xัฆd(((-' OR a.FCWHOUSE = 'npgQ((()') 
	                    AND f.Product_Type = 4
	                   
	                    GROUP BY
		                    a.FCWHOUSE,
		                    b.FCCODE,
		                    CAST(a.FTLASTUPD AS DATE),
		                    a.FCIOTYPE,
		                    f.Product_Type,
		                    f.Product_Name,
		                    f.Order_to,
		                    f.Stock_Unit,
		                    f.PurchASe_Unit,
		                    f.Min_Stock,
		                    f.Suggest_Order,
		                    f.FACTOR,
		                    f.PUnit_Name,
		                    f.SUnit_Name,
		                    f.Operation_Detail,
		                    f.Check_LP,
		                    f.Check_TD,
		                    f.Note_Unit_Convert,
		                    f.Note_PurchASe,
		                    f.Order_to,
		                    g.Note_Vendor,
		                    g.P_Note,
		                    g.P0,
		                    g.P1,
		                    g.P2,
		                    g.P3,
		                    g.Product_Code,
		                    g.S_Note,
		                    g.S0,
		                    g.S1,
		                    g.S2,
		                    g.S3,
		                    g.vat,
		                    g.Vendor,
		                    g.Vendor_to_PurchASe,
		                    g.QC_Form
                    ),
                    with2 AS
                    (
	                    SELECT
		                    ROW_NUMBER() OVER(PARTITION BY w1.FCCODE ORDER BY MAX(CAST(tb1.FTLASTUPD AS DATE)) DESC) AS RN,
		                    w1.FCCODE,
		                    MAX(CAST(tb1.FTLASTUPD AS DATE)) AS FTLASTUPD,
		                    CAST(w1.Min_Stock AS DECIMAL(20,3)) AS Min_Stock,
		                    CAST(w1.LP_MV AS DECIMAL(20,3)) AS LP_MV,
		                    CAST(w1.LP_QTY AS DECIMAL(20,3)) AS LP_QTY,
		                    CAST(w1.TD_MV AS DECIMAL(20,3)) AS TD_MV,
		                    CAST(w1.TD_QTY AS DECIMAL(20,3)) AS TD_QTY,
		                    CAST(w1.TOTAL_QTY AS DECIMAL(20,3)) AS TOTAL_QTY,
		                    w1.Product_Type,
		                    w1.Product_Name,
		                    w1.Stock_Unit,
		                    w1.PurchASe_Unit,
		                    w1.Suggest_Order,
		                    w1.FACTOR,
		                    w1.PUnit_Name,
		                    w1.SUnit_Name,
		                    w1.Operation_Detail,
		                    w1.Check_LP,
		                    w1.Check_TD,
		                    w1.Note_Unit_Convert,
		                    w1.Note_PurchASe,
		                    w1.Order_to,
		                    w1.Note_Vendor,
		                    w1.P_Note,
		                    w1.P0,
		                    w1.P1,
		                    w1.P2,
		                    w1.P3,
		                    w1.Product_Code,
		                    w1.S_Note,
		                    w1.S0,
		                    w1.S1,
		                    w1.S2,
		                    w1.S3,
		                    w1.vat,
		                    w1.Vendor,
		                    w1.Vendor_to_PurchASe,
		                    w1.QC_Form
	                    FROM REFPROD_PM tb1
	                    JOIN PROD tb2 ON tb1.FCPROD = tb2.FCSKID
	                    JOIN with1 w1 ON w1.FCCODE = tb2.FCCODE AND w1.FTLASTUPD = tb1.FTLASTUPD
	                    GROUP BY
		                    tb2.FCCODE,
		                    w1.FCCODE,
		                    w1.Min_Stock,
		                    w1.LP_MV,
		                    w1.LP_QTY,
		                    w1.TD_MV,
		                    w1.TD_QTY,
		                    w1.TOTAL_QTY,
		                    w1.Product_Type,
		                    w1.Product_Name,
		                    w1.Stock_Unit,
		                    w1.PurchASe_Unit,
		                    w1.Suggest_Order,
		                    w1.FACTOR,
		                    w1.PUnit_Name,
		                    w1.SUnit_Name,
		                    w1.Operation_Detail,
		                    w1.Check_LP,
		                    w1.Check_TD,
		                    w1.Note_Unit_Convert,
		                    w1.Note_PurchASe,
		                    w1.Order_to,
		                    w1.Note_Vendor,
		                    w1.P_Note,
		                    w1.P0,
		                    w1.P1,
		                    w1.P2,
		                    w1.P3,
		                    w1.Product_Code,
		                    w1.S_Note,
		                    w1.S0,
		                    w1.S1,
		                    w1.S2,
		                    w1.S3,
		                    w1.vat,
		                    w1.Vendor,
		                    w1.Vendor_to_PurchASe,
		                    w1.QC_Form
                    ),
					with3 AS 
					(
						SELECT DISTINCT a.Product_Code,
							CASE WHEN ( SELECT COUNT(*) FROM Products_Mix_Type b WHERE a.Product_Code = b.Product_Code) = 2 THEN 'ALL'
							WHEN ( SELECT COUNT(*) FROM Products_Mix_Type b WHERE a.Product_Code = b.Product_Code) = 1 THEN a.Product_Stock_Type
							END AS Product_Stock_Type
							from Products_Mix_Type a 
						GROUP BY a.Product_Code,a.Product_Stock_Type
					)
                    SELECT w2.*,h.Product_Stock_Type
                    FROM with2 w2
					LEFT JOIN with3 h ON LOWER(w2.FCCODE) = LOWER(h.Product_Code) COLLATE Thai_CI_AI
                    WHERE w2.RN = 1

                    {0}

                    ";
            }
        }
        public static Dictionary<string, MaterialTracker> dicBM = new Dictionary<string, MaterialTracker>();
        public static Dictionary<string, MaterialTracker> dicBMRemove = new Dictionary<string, MaterialTracker>();
        public static string queryMasterOfBatch => @" SELECT Product_Code_Master
                                    FROM Products_Mix_Code
                                    where Product_Code_Batch = '{0}'";
        public static string queryBatchOfMaster => @" SELECT Product_Code_Batch
                                    FROM Products_Mix_Code
                                    where Product_Code_Master = '{0}'";
    }
}
