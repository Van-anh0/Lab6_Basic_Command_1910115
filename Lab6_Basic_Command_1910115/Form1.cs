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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //tạo chuỗi kết nối tới cơ sở dữ liệu RestaurantManagement
            string connectionString = "server = DESKTOP-E123JR5; database = RestaurantManagement; Integrated Security = true; ";

            //Tạo đối tượng kết nối
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // Tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //Thiết lập lệnh truy vấn cho đối tượng Command
            string query = "SELECT ID, Name, Type FROM category";

            sqlCommand.CommandText = query;

            //Mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //Thực thi lệnh bằng phương thức ExcuteReader
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            //Gọi hàm hiển thị dữ liệu lên màn hình
            this.DisplayCategory(sqlDataReader);

            // đóng kết nối
            sqlConnection.Close();
        }

        private void DisplayCategory(SqlDataReader reader)
        {
            lvCategory.Items.Clear();

            // đọc một dòng dữ liệu
            while (reader.Read())
            {
                //Tạo một dòng mới trong listView
                ListViewItem item = new ListViewItem(reader["ID"].ToString());

                lvCategory.Items.Add(item);

                //Bổ sung các thông tin khác cho ListViewItem
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "server =.; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // thiết lập lệnh truy vấn cho đối tượng command
            sqlCommand.CommandText = "INSERT INTO Category(Name, [Type])" + "VALUES (N'" + txtName.Text + "', " + txtType.Text + ")";

            // mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //thực thi lệnh bằng phương phức ExcuteReader
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            //đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                MessageBox.Show("Thêm nhóm món ăn thành công");

                //tải lại dữ liệu
                btnLoad.PerformClick();

                //xóa các ô nhập
                txtName.Text = "";
                txtType.Text = "";
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại.");
            }

        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            //lấy dòng được chọn trong listview
            ListViewItem item = lvCategory.SelectedItems[0];

            //hiển thị dữ liệu lên Textbox
            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[2].Text == "0" ? "Thức uống" : "Đồ ăn";

            //hiển thị nút cập nhật và xóa
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //tạo đối tượng kết nối
            string connectionstring = "server =.; database = RestaurantManagement; Integrated Security = true; ";
           
            SqlConnection sqlConnection = new SqlConnection(connectionstring);

            //tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();


            string txtText = "";
            if (txtType.Text == "0" || txtType.Text == "1")
            {
                txtText = txtType.Text;
            }
            else
            {
                txtText = txtType.Text == "Thức uống" ? "0" : "1";
            }
            //thiết lập lệnh truy vấn cho đối tượng command
            sqlCommand.CommandText = "UPDATE Category SET Name = N'" + txtName.Text +
                "', [Type] = " + txtText +
                " WHERE ID = " + txtID.Text;

            // mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //thực thi lệnh bằng phương thức ExcuteReader
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            //đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                //cập nhật lại dữ liệu trên Listview
                ListViewItem item = lvCategory.SelectedItems[0];

                item.SubItems[1].Text = txtName.Text;
                item.SubItems[2].Text = txtText;

                //xóa các ô nhập
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                //Disable các nút xóa và cập nhật
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                MessageBox.Show("Cập nhật nhóm món ăn thành công");

            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại");
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // tạo đối tượng kết nối
            string connectionString = "server =.; database = RestaurantManagement; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "DELETE FROM Category " +
                "WHERE ID = " + txtID.Text;

            //mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //thực thi lệnh bằng phương thức ExcuteReader
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            //đóng kết nối
            sqlConnection.Close();

            if (numOfRowsEffected == 1)
            {
                //cập nhật lại dữ liệu trên lv
                ListViewItem item = lvCategory.SelectedItems[0];
                lvCategory.Items.Remove(item);

                //xóa các ô nhập
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                MessageBox.Show("Xóa nhóm món ăn thành công");

            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại");
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0)
            {
                btnDelete.PerformClick();
            }
        }

        private void tsmiViewFood_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                frmFood foodForm = new frmFood();
                foodForm.Show(this);
                foodForm.LoadFood(int.Parse(txtID.Text));
            }
        }
    }
}
