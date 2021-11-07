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
    public partial class AccountManagerForm : Form
    {
        public AccountManagerForm()
        {
            InitializeComponent();
        }

        private void AccountManager_Load(object sender, EventArgs e)
        {
            
        }

        private void DisplayAccount(SqlDataReader reader)
        {
            lvAccount.Items.Clear();

            // đọc một dòng dữ liệu
            while (reader.Read())
            {
                //Tạo một dòng mới trong listView
                ListViewItem item = new ListViewItem(reader["AccountName"].ToString());

                lvAccount.Items.Add(item);

                //Bổ sung các thông tin khác cho ListViewItem
                
                item.SubItems.Add(reader["Password"].ToString());
                item.SubItems.Add(reader["FullName"].ToString());
                item.SubItems.Add(reader["Email"].ToString());
                item.SubItems.Add(reader["Tell"].ToString());
                item.SubItems.Add(reader["DateCreated"].ToString());
                item.SubItems.Add(reader["Actived"].ToString());
                item.SubItems.Add(reader["RoleName"].ToString());

            }
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            //tạo chuỗi kết nối tới cơ sở dữ liệu RestaurantManagement
            string connectionString = "server = DESKTOP-E123JR5; database = RestaurantManagement; Integrated Security = true; ";

            //Tạo đối tượng kết nối
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //Thiết lập lệnh truy vấn cho đối tượng Command
            string query = "SELECT a.AccountName,Password,FullName,Email,Tell,DateCreated,Actived,RoleName FROM Account a, RoleAccount r, Role l WHERE r.RoleID = l.ID AND a.AccountName = r.AccountName AND Actived = 1";

            sqlCommand.CommandText = query;

            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //Thực thi lệnh bằng phương thức ExcuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Gọi hàm hiển thị dữ liệu lên màn hình
            this.DisplayAccount(sqlDataReader);

            // đóng kết nối
            sqlConnection.Close();
        }

        private void btnUnActive_Click(object sender, EventArgs e)
        {
            //tạo chuỗi kết nối tới cơ sở dữ liệu RestaurantManagement
            string connectionString = "server = DESKTOP-E123JR5; database = RestaurantManagement; Integrated Security = true; ";

            //Tạo đối tượng kết nối
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //Thiết lập lệnh truy vấn cho đối tượng Command
            string query = "SELECT a.AccountName,Password,FullName,Email,Tell,DateCreated,Actived,RoleName FROM Account a, RoleAccount r, Role l WHERE r.RoleID = l.ID AND a.AccountName = r.AccountName AND Actived = 0";

            sqlCommand.CommandText = query;

            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //Thực thi lệnh bằng phương thức ExcuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Gọi hàm hiển thị dữ liệu lên màn hình
            this.DisplayAccount(sqlDataReader);

            // đóng kết nối
            sqlConnection.Close();
        }
    }
}
