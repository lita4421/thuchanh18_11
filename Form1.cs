using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ThucHanh_6_11_MH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hienKhachhang();
        }

        private void hienKhachhang()
        {
            using (DataTable tblKhachhang = getKhachhang())
            {
                DataView dvKhachhang = new DataView(tblKhachhang);
                dgrKhachhang.AutoGenerateColumns = false;
                dgrKhachhang.ReadOnly = true;
                dgrKhachhang.DataSource = dvKhachhang;

            }
        }

        private DataTable getKhachhang()
        {

            string cnnString = ConfigurationManager.ConnectionStrings["db_Shop4Training"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                using (SqlCommand cmd = new SqlCommand("spKhachhang_get ", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable KhachhangTable = new DataTable("tblKhachhang");
                        da.Fill(KhachhangTable);
                        return KhachhangTable;

                    }//adt
                }//cmd
            }//cnn

            throw new NotImplementedException();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string cnnString = ConfigurationManager.ConnectionStrings["db_Shop4Training"].ConnectionString;

            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
               
                using (SqlCommand cmd = new SqlCommand(" ", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!(btnThem.Tag == null || string.IsNullOrEmpty(btnThem.ToString())))
                    {
                        cmd.CommandText = " spKhachhang_update";
                        cmd.Parameters.AddWithValue("@PK_iKhachhangID", btnThem.Tag);
                    }
                    else
                    {
                        cmd.CommandText = "spKhachhang_insert";
                        cmd.Parameters.Add("@PK_iKhachhangID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    }
                    cmd.Parameters.AddWithValue("sHoten", txtHoten.Text);
                    cmd.Parameters.AddWithValue("sDiachi", txtDiachi.Text);
                    cmd.Parameters.AddWithValue("sDienthoai", txtSodienthoai.Text);
                    cmd.Parameters.AddWithValue("tNgaysinh", Convert.ToDateTime(dtNgaysinh.Text));
                    cmd.Parameters.AddWithValue("sTencoquan", txtTencoquan.Text);
                    cmd.Parameters.AddWithValue("bGioitinh", gtNam.Checked);
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    hienKhachhang();
                }//cmd
            }//cnn

        }

        private void edit_Click(object sender, EventArgs e)
        {
            DataView dvkhachhang = (DataView)dgrKhachhang.DataSource;
            DataRowView row = dvkhachhang[dgrKhachhang.CurrentRow.Index];
            txtHoten.Text = row["sHoten"].ToString();
            dtNgaysinh.Text = row["tNgaysinh"].ToString();
            if (Convert.ToBoolean(row["bGioitinh"]))
            {
                gtNam.Checked = true;
            }
            else
            {
                gtNu.Checked = true;
            }

            txtDiachi.Text = row["sDiachi"].ToString();
           txtSodienthoai.Text = row["sDienthoai"].ToString();
          txtTencoquan.Text = row["sTencoquan"].ToString();
            btnThem.Text = "Ghi nhan";
            dgrKhachhang.Enabled = btnEdit.Enabled = btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string cnnString = ConfigurationManager.ConnectionStrings["db_Shop4Training"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    cmd.Parameters.AddWithValue("sHoten", txtHoten.Text);
                    cmd.Parameters.AddWithValue("sDiachi", txtDiachi.Text);
                    cmd.Parameters.AddWithValue("sDienthoai", txtSodienthoai.Text);
                    cmd.Parameters.AddWithValue("tNgaysinh", DateTime.ParseExact(dtNgaysinh.Text, "dd/MM/yyyy", null));
                    ; ;\
1]=                    cmd.Parameters.AddWithValue("sTencoquan", txtTencoquan.Text);
                    cmd.Parameters.AddWithValue("bGioitinh", gtNam.Checked);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spKhachhang_Delete";
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    hienKhachhang();
                }
            }//cnn
        }
    }
}
