using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ThucHanh_6_11_MH
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /*
        public static void HienCongDan()
        {
            string cnnString = ConfigurationManager.ConnectionStrings["db_Shop4Training"].ConnectionString;
            string proGetKH = "spKhachhang_get";
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand(proGetKH, cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable KhachhangTable = new DataTable("tblKhachhang");
                        da.Fill(KhachhangTable);
                        if (KhachhangTable.Rows.Count == 0)
                        {
                            Console.WriteLine("Chua co du lieu");

                        }
                        else
                        {

                            Console.WriteLine("Danh sach khach hang");
                            foreach (DataRow dr in KhachhangTable.Rows)
                            {
                                Console.WriteLine("Cong dan :{0} sinh ngay {1:yyyy} ",
                                    dr["sHoten"], dr["tNgaysinh"]);
                            }
                        }
                    }//adt
                }//cmd
            }//cnn


            Console.ReadKey();

        }
        */
    }
}
