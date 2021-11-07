using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Lab6_Basic_Command_1910115
{
    public partial class BillsForm : Form
    {
        public BillsForm()
        {
            InitializeComponent();
        }

        private void BillsForm_Load(object sender, EventArgs e)
        {
            //tạo chuỗi kết nối tới cơ sở dữ liệu RestaurantManagement
            string connectionString = "server = DESKTOP-E123JR5; database = RestaurantManagement; Integrated Security = true; ";

            //Tạo đối tượng kết nối
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //Thiết lập lệnh truy vấn cho đối tượng Command
            string query = "SELECT * FROM Bills";

            sqlCommand.CommandText = query;

            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //Thực thi lệnh bằng phương thức ExcuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Gọi hàm hiển thị dữ liệu lên màn hình
            this.DisplayBills(sqlDataReader);

            // đóng kết nối
            sqlConnection.Close();
        }

        private void DisplayBills(SqlDataReader reader)
        {
            lvBills.Items.Clear();

            // đọc một dòng dữ liệu
            while (reader.Read())
            {
                //Tạo một dòng mới trong listView
                ListViewItem item = new ListViewItem(reader["ID"].ToString());

                lvBills.Items.Add(item);

                //Bổ sung các thông tin khác cho ListViewItem
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["TableID"].ToString());
                item.SubItems.Add(reader["Amount"].ToString());
                item.SubItems.Add(reader["Discount"].ToString());
                item.SubItems.Add(reader["Tax"].ToString());
                item.SubItems.Add(reader["Status"].ToString());
                item.SubItems.Add(reader["CheckoutDate"].ToString());
                item.SubItems.Add(reader["Account"].ToString());

            }
        }

        private void lvBills_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int cout = lvBills.SelectedItems.Count;
            if (cout > 0)
            {
                string ma = lvBills.SelectedItems[0].SubItems[0].Text;
                BillDetailsForm billForm= new BillDetailsForm();
                billForm.Show(this);
                billForm.LoadBillDetails(int.Parse(ma));
            }
        }
    }
}
