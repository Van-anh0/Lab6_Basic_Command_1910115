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
        string cnString = "server =.; database = RestaurantManagement; Integrated Security = true; ";
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
            string query = "SELECT DISTINCT a.AccountName,Password,FullName,Email,Tell,DateCreated,Actived FROM Account a, RoleAccount r WHERE a.AccountName = r.AccountName AND Actived = 1";

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
            string query = "SELECT DISTINCT a.AccountName,Password,FullName,Email,Tell,DateCreated,Actived FROM Account a, RoleAccount r WHERE a.AccountName = r.AccountName AND Actived = 0";

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // thiết lập lệnh truy vấn cho đối tượng command
            ;
            sqlCommand.CommandText = "INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) Values (N'"+ txtName.Text+"', N'"
                +txtPass.Text+"', N'"+ txtFullName.Text+ "', N'" + txtEmail.Text + "', N'" + txtTell.Text + "','"+txtDateCreated.Text+"')";
            // mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //thực thi lệnh bằng phương phức ExcuteReader
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (" + txtRoleID.Text + ", N'" + txtName.Text + "', "
                + txtActived.Text + ", N'" + txtNotes.Text + "')";
              numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            //đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Thêm tài khoản thành công!");

                txtName.Text = "";
                txtPass.Text = "";
                txtFullName.Text = "";
                txtEmail.Text = "";
                txtTell.Text = "";
                
                txtDateCreated.Text = "";
                txtRoleID.Text = "";
                txtActived.Text = "";
                txtNotes.Text = "";
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại.");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            SqlConnection sqlConnection = new SqlConnection(cnString);

            //tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            //thiết lập lệnh truy vấn cho đối tượng command
            sqlCommand.CommandText = "UPDATE Account SET  Password = N'"
                + txtPass.Text + "', FullName = N'" + txtFullName.Text + "',Email = N'" + txtEmail.Text + "',Tell = N'" + txtTell.Text + "',DateCreated = '" + txtDateCreated.Text + "' WHERE AccountName = N'" + txtName.Text + "'";

            // mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //thực thi lệnh bằng phương thức ExcuteReader
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "UPDATE RoleAccount SET RoleID = " + txtRoleID.Text + " Actived = "
                + txtActived.Text + ",Notes = N'" + txtNotes.Text + "' WHERE AccountName = " + "N'" + txtName.Text + "'";
            //đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                //cập nhật lại dữ liệu trên Listview
                ListViewItem item = lvAccount.SelectedItems[0];

                item.SubItems[1].Text = txtName.Text;
                item.SubItems[2].Text = txtPass.Text;
                item.SubItems[3].Text = txtFullName.Text;
                item.SubItems[4].Text = txtEmail.Text;
                item.SubItems[5].Text = txtTell.Text;
                item.SubItems[6].Text = txtDateCreated.Text;
                
                //xóa các ô nhập
                txtName.Text = "";
                txtPass.Text = "";
                txtFullName.Text = "";
                txtEmail.Text = "";
                txtTell.Text = "";

                txtDateCreated.Text = "";
                txtRoleID.Text = "";
                txtActived.Text = "";
                txtNotes.Text = "";


                MessageBox.Show("Cập nhật thành công");

            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại");
            }
        }
    }

    
}
