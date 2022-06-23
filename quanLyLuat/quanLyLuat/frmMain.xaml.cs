using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace quanLyLuat
{
    /// <summary>
    /// Interaction logic for frmMain.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        private DataSet rs;
        SqlConnection dbcon = new SqlConnection(@"Data Source=DESKTOP-2022PQT\SQLEXPRESS;Initial Catalog=loginDB;Integrated Security=True");
        public frmMain()
        {
            InitializeComponent();
            loadGird();
        }
        public void loadGird()
        {
            SqlCommand cmd = new SqlCommand("select * from dieuLuat", dbcon);
            DataTable dt = new DataTable();
            dbcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dbcon.Close();
            dataGrid.ItemsSource = dt.DefaultView;
            cbbox.SelectedIndex = 0;
        }
        public bool isValid()
        {
            if (txt_muc.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN MỤC", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_ndmuc.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN NỘI DUNG MỤC", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_dieu.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN ĐIỀU", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_nd_dieu.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN NỘI DUNG ĐIỀU", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_khoan.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN KHOẢN", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txt_ndk.Text == String.Empty)
            {
                MessageBox.Show("CHƯA ĐIỀN NỘI DUNG KHOẢN", "FAILED", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("BẠN CÓ CHẮC CHẮN MUỐN THÊM KHÔNG? ", "THÊM DỮ LIỆU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO dieuLuat VALUES (@Muc, @noiDungMuc, @Dieu, @noiDungDieu, @Khoan, @noiDungKhoan)", dbcon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Muc", txt_muc.Text);
                        cmd.Parameters.AddWithValue("@noiDungMuc", txt_ndmuc.Text);
                        cmd.Parameters.AddWithValue("@Dieu", txt_dieu.Text);
                        cmd.Parameters.AddWithValue("@noiDungDieu", txt_nd_dieu.Text);
                        cmd.Parameters.AddWithValue("@Khoan", txt_khoan.Text);
                        cmd.Parameters.AddWithValue("@noiDungKhoan", txt_ndk.Text);
                        dbcon.Open();
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                        loadGird();
                        MessageBox.Show("ADD SUCCESS","ĐÃ LƯU" , MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("VUI LÒNG NHẬP LẠI THEO ĐÚNG YÊU CẦU");
                }
            }
            else
            {
                dbcon.Close();

            }
        }
        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("BẠN CÓ CHẮC CHẮN MUỐN SỬA KHÔNG? ", "SỬA DỮ LIỆU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (isValid())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE dieuLuat set noiDungMuc = @noiDungMuc, Dieu = @Dieu, noiDungDieu = @noiDungDieu, Khoan = @Khoan, noiDungKhoan = @noiDungKhoan WHERE Muc = @Muc",dbcon);
                        cmd.Parameters.AddWithValue("@Muc", txt_muc.Text);
                        cmd.Parameters.AddWithValue("@noiDungMuc", txt_ndmuc.Text);
                        cmd.Parameters.AddWithValue("@Dieu", txt_dieu.Text);
                        cmd.Parameters.AddWithValue("@noiDungDieu", txt_nd_dieu.Text);
                        cmd.Parameters.AddWithValue("@Khoan", txt_khoan.Text);
                        cmd.Parameters.AddWithValue("@noiDungKhoan", txt_ndk.Text);
                        dbcon.Open();
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                        loadGird();
                        MessageBox.Show("FIX SUCCESS", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch(SqlException ex)
                {
                    MessageBox.Show("KHÔNG CẬP NHẬT ĐƯỢC! VUI LÒNG KIỂM TRA LẠI PHẦN NHẬP CẦN SỬA HOẶC HỆ THỐNG");
                }
            }
            else
            {
                dbcon.Close();

            }
        }

        private void btn_Delete_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("BẠN CÓ CHẮC CHẮN MUỐN XÓA KHÔNG? ", "XÓA DỮ LIỆU", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                dbcon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM dieuLuat WHERE Muc = " + txt_muc.Text + "AND Dieu = " + txt_dieu.Text + "AND Khoan = " + txt_khoan.Text, dbcon);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("DELETE SUCCESS", "ĐÃ LƯU", MessageBoxButton.OK, MessageBoxImage.Information);
                    dbcon.Close();
                    loadGird();
                    dbcon.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("KHÔNG XÓA ĐƯỢC!VUI LÒNG KIỂM TRA LẠI");
                }
                finally
                {
                    dbcon.Close();
                }
            }
            else
            {
                dbcon.Close();
            }
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dbcon.State == ConnectionState.Closed)
                    dbcon.Open();
                using (DataTable dt = new DataTable("dieuLuat"))
                {
                    string query = "SELECT * FROM dieuLuat ";
                    if (cbbox.SelectedIndex == 0)
                    {
                        query += "WHERE Muc =" + txt_Search.Text.Trim().ToUpper();
                    }
                    else
                    {
                        if (cbbox.SelectedIndex == 1)
                        {
                            query += "WHERE noiDungMuc LIKE '%" + txt_Search.Text.Trim().ToUpper() + "%'";
                        }
                        if (cbbox.SelectedIndex == 2)
                        {
                            query += "WHERE Dieu = " + txt_Search.Text;
                        }
                        if (cbbox.SelectedIndex == 3)
                        {
                            query += "WHERE noiDungDieu LIKE '%" + txt_Search.Text.Trim().ToUpper() + "%'";
                        }
                        if (cbbox.SelectedIndex == 4)
                        {
                            query += "WHERE Khoan  =" + txt_Search.Text;
                        }
                        if (cbbox.SelectedIndex == 5)
                        {
                            query += "WHERE noiDungKhoan LIKE '%" + txt_Search.Text.Trim().ToUpper() + "%'";
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand(query, dbcon))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbcon.Close();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGrid.SelectedIndex.ToString() != null)
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                if(drv != null)
                {
                    txt_muc.Text = drv[0].ToString();
                    txt_ndmuc.Text = drv[1].ToString();
                    txt_dieu.Text = drv[2].ToString();
                    txt_nd_dieu.Text = drv[3].ToString();
                    txt_khoan.Text = drv[4].ToString();
                    txt_ndk.Text = drv[5].ToString();
                }
            }
        }

        
    }
}
