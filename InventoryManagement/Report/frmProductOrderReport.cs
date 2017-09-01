using InventoryManagement.DataAccess;
using InventoryManagerment.Model;
using InventoryManagerment.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement.Report
{
    public partial class frmProductOrderReport : Form
    {
        public List<OrderListModel> lsOrder = new List<OrderListModel>();
        private PrintPreviewDialog previewDlg = null;
        private PrintDocument pd = new PrintDocument();
        CommonUtill comm = new CommonUtill();

        public frmProductOrderReport()
        {
            InitializeComponent();
        }

        private void frmProductOrderReport_Load(object sender, EventArgs e)
        {

        }

        public void print_Preview(List<OrderListModel> _lsOrder)
        {

            lsOrder = _lsOrder;
            previewDlg = new PrintPreviewDialog();

            ToolStripButton b = new ToolStripButton();
            b.ImageIndex = ((ToolStripButton)((ToolStrip)previewDlg.Controls[1]).Items[0]).ImageIndex;

            ((ToolStrip)previewDlg.Controls[1]).Items.Remove(((ToolStripButton)((ToolStrip)previewDlg.Controls[1]).Items[0]));
            b.Visible = true;
            ((ToolStrip)previewDlg.Controls[1]).Items.Insert(0, b);
            ((ToolStripButton)((ToolStrip)previewDlg.Controls[1]).Items[0]).Click += new System.EventHandler(buttonPrint_Click);


            previewDlg.PrintPreviewControl.Zoom = 1.0;
            previewDlg.WindowState = FormWindowState.Maximized;

            //Create a PrintDocument object
            PaperSize pkCustomSize1 = new PaperSize("A4", 827, 1169);
            pd.DefaultPageSettings.PaperSize = pkCustomSize1;
            //Add print-page event handler
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            //Set Document property of PrintPreviewDialog
            previewDlg.Document = pd;
            //printDocument1.PrinterSettings.PrinterName = printDialog1.PrinterSettings.PrinterName;
            previewDlg.ShowDialog();



        }

        int i = 0;
        int lineperpage = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                lineperpage = 0;
                int countOrder = lsOrder.Count;

                while (i < countOrder)
                {

                    OrderListModel order = lsOrder[i];

                    float y = (float)205 * lineperpage;

                    string createDate = order.CreateDate;
                    DateTime value = new DateTime(Convert.ToInt32(createDate.Substring(0, 4)) - 543, Convert.ToInt32(createDate.Substring(4, 2)), Convert.ToInt32(createDate.Substring(6, 2)));
                    int day = (int)value.DayOfWeek;

                    Font drawFont7T = new Font("Tahoma", 7);
                    Font drawFont9B = new Font("Tahoma", 9, FontStyle.Bold);
                    Font drawFont8T = new Font("Tahoma", 8, GraphicsUnit.Point);
                    Font drawFont8B = new Font("Tahoma", 8, FontStyle.Bold);

                    #region Line 1
                    float xTextLine1 = 5;
                    float yTextLine1 = 20 + y;

                    e.Graphics.PageUnit = GraphicsUnit.Point;
                    e.Graphics.DrawString("" + order.List_Num_Order.PadLeft(4, '0') + " ใบสั่งงานย่อย/ใบสั่งงาน ", drawFont9B, Brushes.Black, xTextLine1, yTextLine1);
                    e.Graphics.DrawString(comm.dayThaiOfWeek(day) + "-" + comm.convertIntToShortDate(createDate) + " :" + order.CreateTimestamp.Substring(0, 2) + "น.", drawFont8B, Brushes.Black, xTextLine1 + 130, yTextLine1 + 1);

                    // Set format of string.
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;
                    drawFormat.LineAlignment = StringAlignment.Far;//Near,Far,Center

                    StringFormat drawFormatLeft = new StringFormat();
                    drawFormatLeft.LineAlignment = StringAlignment.Center;//Near,Far,Center


                    float xBoxLin1 = 223;
                    float yBoxLine1 = 10 + y;
                    float wBoxLine1App1 = 35;
                    float hBoxLine1App1 = 20;
                    RectangleF rectFAppove1 = new RectangleF(xBoxLin1, yBoxLine1, wBoxLine1App1, hBoxLine1App1);
                    e.Graphics.DrawString("ผู้อนุมัติ", drawFont8B, Brushes.Black, rectFAppove1, drawFormat);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectFAppove1));
                    float wBoxLine1App2 = 70;
                    RectangleF rectFAppove2 = new RectangleF(xBoxLin1 + wBoxLine1App1, yBoxLine1, wBoxLine1App2, hBoxLine1App1);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectFAppove2));

                    float wBoxAudit1 = 50;
                    float xBoxAudit1 = xBoxLin1 + wBoxLine1App2 + 40;
                    RectangleF rectFAudit1 = new RectangleF(xBoxAudit1, yBoxLine1, wBoxAudit1, hBoxLine1App1);
                    e.Graphics.DrawString("ผู้ตรวจสอบ", drawFont8B, Brushes.Black, rectFAudit1, drawFormat);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectFAudit1));

                    float wBoxAudit2 = 60;
                    RectangleF rectFAudit2 = new RectangleF(xBoxAudit1 + wBoxAudit1, yBoxLine1, wBoxAudit2, hBoxLine1App1);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectFAudit2));

                    float wBoxLin1Store = 25;
                    RectangleF rectFAudit3 = new RectangleF(xBoxAudit1 + wBoxAudit1 + wBoxAudit2, yBoxLine1, wBoxLin1Store, hBoxLine1App1);
                    e.Graphics.DrawString("สโตร์", drawFont8B, Brushes.Black, rectFAudit3, drawFormat);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectFAudit3));

                    float wBoxPrepare1 = 35;
                    float xBoxPrepare1 = xBoxAudit1 + wBoxAudit1 + wBoxAudit2 + wBoxLin1Store + 5;
                    RectangleF rectFPrepare1 = new RectangleF(xBoxPrepare1, yBoxLine1, wBoxPrepare1, hBoxLine1App1);
                    e.Graphics.DrawString("ผู้เตรียม", drawFont8B, Brushes.Black, rectFPrepare1, drawFormat);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectFPrepare1));

                    float wBoxPrepare2 = 70;
                    RectangleF rectFPrepare2 = new RectangleF(xBoxPrepare1 + wBoxPrepare1, yBoxLine1, wBoxPrepare2+15, hBoxLine1App1);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectFPrepare2));

                    #endregion
                    #region Line2
                    float wBoxOrderTo = 90;
                    float xBoxOrderTo = 5;
                    float yTextLine2 = yTextLine1 + 20;

                    RectangleF rectOrderTo = new RectangleF(xBoxOrderTo, yTextLine2, wBoxOrderTo, hBoxLine1App1);
                    e.Graphics.DrawString("ถึง:คุณ ", drawFont8B, Brushes.Black, rectOrderTo, drawFormatLeft);
                    e.Graphics.DrawString("           " + order.ToOwner, drawFont9B, Brushes.Black, rectOrderTo, drawFormatLeft);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectOrderTo));

                    float xTextTopic = wBoxOrderTo + 10;
                    float yextTopic = yTextLine2 + 5;
                    e.Graphics.DrawString("เรื่อง : ", drawFont8B, Brushes.Black, xTextTopic, yextTopic);

                    float wBoxTopic = 250;
                    float xBoxTopic = xTextTopic;
                    RectangleF rectTopic = new RectangleF(xBoxTopic+30, yTextLine2, wBoxTopic, hBoxLine1App1);
                    e.Graphics.DrawString(order.Alert_Name+" : " + order.Topic, drawFont9B, Brushes.Black, rectTopic, drawFormatLeft);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectTopic));

                    float xSuggestOrder = xBoxAudit1;
                    float wSugestOrder = wBoxAudit1 + wBoxAudit2 + wBoxLin1Store;
                    RectangleF rectSuggestOrder = new RectangleF(xSuggestOrder+55, yTextLine2, wSugestOrder-20, hBoxLine1App1);
                    e.Graphics.DrawString("จำนวนสั่งทำ/ซื้อ", drawFont7T, Brushes.Black, rectSuggestOrder, drawFormatLeft);
                    e.Graphics.DrawString("                   " + order.Suggest_Order + " " + order.PUnit_Name, drawFont9B, Brushes.Black, rectSuggestOrder, drawFormatLeft);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectSuggestOrder));

                    float xMinStock = xBoxPrepare1;
                    float wMinStock = wBoxPrepare1 + wBoxPrepare2;
                    RectangleF rectMin = new RectangleF(xMinStock+35, yTextLine2, wMinStock-20, hBoxLine1App1);
                    e.Graphics.DrawString("min ", drawFont8B, Brushes.Black, rectMin, drawFormatLeft);
                    e.Graphics.DrawString("       " + order.MinumunStock + " " + order.PUnit_Name, drawFont9B, Brushes.Black, rectMin, drawFormatLeft);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectMin));
                    #endregion
                    #region Line3
                    float xLine3 = xBoxOrderTo;
                    float yLine3 = yTextLine2 + 37;

                    Point point1 = new Point((int)xLine3 + 40, (int)yLine3);
                    Point point2 = new Point((int)xMinStock + (int)wMinStock, (int)yLine3);

                    e.Graphics.DrawString(order.Product_code + "  " + order.Product_Name, drawFont8B, Brushes.Black, xLine3, yLine3 - 15);
                    e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), point1, point2);
                    #endregion
                    #region Line4
                    float xLine4 = xBoxOrderTo;
                    float yLine4 = yLine3 + 20;

                    Point point41 = new Point((int)xLine4, (int)yLine4);
                    Point point42 = new Point((int)xMinStock + (int)wMinStock, (int)yLine4);

                    e.Graphics.DrawString("use : " + order.Use, drawFont8B, Brushes.Black, xLine4, yLine4 - 15);
                    e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), point41, point42);
                    #endregion
                    #region

                    float wBoxtManager = 80;
                    float xBoxtManager = xLine4;
                    float yLineManager = yLine4;
                    float hBoxManager = 30;
                    RectangleF rectManager = new RectangleF(xBoxtManager, yLineManager, wBoxtManager, hBoxManager);
                    e.Graphics.DrawString("หัวหน้าแผนก หรือ\nผจก.ฝ่ายนั้นๆเซ็น", drawFont8T, Brushes.Black, rectManager, drawFormatLeft);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectManager));
                    e.Graphics.DrawString(".............................. ว/ด/ป ...................... จำนวนส่งงานเข้า ................... ผู้ส่ง ................. ว/ด/ป...............", drawFont8T, Brushes.Black, xBoxtManager + wBoxtManager, yLineManager + 15);
                    #endregion
                    #region Line5,6,7
                    float xLine5 = xBoxOrderTo;
                    float yLine5 = yLine4 + 47;

                    Point point51 = new Point((int)xLine5, (int)yLine5);
                    Point point52 = new Point((int)xSuggestOrder + (int)wSugestOrder, (int)yLine5);

                    e.Graphics.DrawString("Note Vender : " + order.Note_Vendor, drawFont8B, Brushes.Black, xLine5, yLine5 - 13);
                    e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), point51, point52);

                    float yLine6 = yLine5 + 17;
                    Point point61 = new Point((int)xLine5, (int)yLine6);
                    Point point62 = new Point((int)xSuggestOrder + (int)wSugestOrder, (int)yLine6);

                    e.Graphics.DrawString("ชื่อสั่งซื้อ : " + order.Purchase_Name, drawFont8B, Brushes.Black, xLine5, yLine6 - 13);
                    e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), point61, point62);

                    float yLine7 = yLine6 + 17;
                    Point point71 = new Point((int)xLine5, (int)yLine7);
                    Point point72 = new Point((int)xSuggestOrder + (int)wSugestOrder, (int)yLine7);

                    e.Graphics.DrawString("Code บ/ช : " + order.AccountCode, drawFont9B, Brushes.Black, xLine5, yLine7 - 13);
                    e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), point71, point72);

                    float xLine8 = xTextTopic;
                    float yLine8 = yLine7 + 7;
                    e.Graphics.DrawString("รับสำเนาแล้ว ลงชื่อ __________________(                       ) วันที่ __________________", drawFont8B, Brushes.Black, xLine8, yLine8);
                    #endregion
                    #region boxRight
                    float xBoxRight = xBoxPrepare1;
                    float yBoxRigth = yLine4;
                    float wBoxRight = 50;
                    float hBoxRight = 100;
                    float wBoxRight2 = 70;

                    RectangleF rectBoxRight1 = new RectangleF(xBoxRight, yBoxRigth, wBoxRight, hBoxRight);
                    RectangleF rectBoxRight2 = new RectangleF(xBoxRight + wBoxRight, yBoxRigth, wBoxRight2, hBoxRight);

                    e.Graphics.DrawString("เหลือ LP\nที่ตั้ง LP", drawFont9B, Brushes.Black, xBoxRight, yLine8 - 85);
                    e.Graphics.DrawString(order.RemainLP + "  " + order.SUnit_Name, drawFont9B, Brushes.Black, xBoxRight + 60, yLine8 - 85);
                    e.Graphics.DrawString("0", drawFont9B, Brushes.Black, xBoxRight + 60, yLine8 - 70);

                    e.Graphics.DrawString("เหลือ TD\nที่ตั้ง TD", drawFont9B, Brushes.Black, xBoxRight, yLine8 - 40);
                    e.Graphics.DrawString(order.ReaminTD + "  " + order.SUnit_Name, drawFont9B, Brushes.Black, xBoxRight + 60, yLine8 - 40);
                    e.Graphics.DrawString("0", drawFont9B, Brushes.Black, xBoxRight + 60, yLine8 - 25);


                    e.Graphics.DrawString("เหลือรวม", drawFont9B, Brushes.Black, xBoxRight, yLine8 - 3);
                    e.Graphics.DrawString(order.RemainAll + "  " + order.SUnit_Name, drawFont9B, Brushes.Black, xBoxRight + 60, yLine8 - 3);

                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectBoxRight1));




                    //e.Graphics.DrawString(order.RemainLP + "  " + order.SUnit_Name + "\n0\n" + order.ReaminTD + "  " + order.SUnit_Name + "\n0\n\n" + order.RemainAll + "  " + order.SUnit_Name + "", drawFont, Brushes.Black, rectBoxRight2);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 0.5f), Rectangle.Round(rectBoxRight2));


                    float xLineR1 = xBoxRight;
                    float yLineR1 = yBoxRigth + 40;
                    float xEndR1 = xBoxRight + wBoxRight + wBoxRight2;

                    Point pointR1 = new Point((int)xLineR1, (int)yLineR1);
                    Point pointR2 = new Point((int)xEndR1, (int)yLineR1);
                    e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), pointR1, pointR2);

                    float xLineR2 = xBoxRight;
                    float yLineR2 = yLineR1 + 40;


                    Point pointR3 = new Point((int)xLineR2, (int)yLineR2);
                    Point pointR4 = new Point((int)xEndR1, (int)yLineR2);
                    e.Graphics.DrawLine(new Pen(Color.Black, 0.5f), pointR3, pointR4);
                    #endregion

                    lineperpage++;
                    i++;
                    if (lineperpage >= 4)
                    {
                        lineperpage = 0;
                        e.HasMorePages = true;
                        return;
                    }
                }
                i = 0;
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {

            }

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            //printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                previewDlg.Document.Print();
            }
        }

        public void CreatePrintPreView(int Order_ID)
        {
            try
            {
                ProductOrderReportAccess prdReportAcc = new ProductOrderReportAccess();
                List<OrderListModel> lsOrder = prdReportAcc.getOrderListModel(Order_ID);
                print_Preview(lsOrder);
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
