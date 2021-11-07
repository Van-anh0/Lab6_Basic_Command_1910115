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
    
    public partial class frmFood : Form
    {
        private int cateID;
        public frmFood()
        {
            InitializeComponent();
        }

        private void frmFood_Load(object sender, EventArgs e)
        {

        }

        public void LoadFood(int categoryID)
        {
            //tạo đối tượng kết nối
            string connectionString = "server = DESKTOP-E123JR5; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            //thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "SELECT Name FROM Category where ID = " + categoryID;

            //mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //gán tên nhóm sản phẩm cho tiêu đề
            string catName = sqlCommand.ExecuteScalar().ToString();
            this.Text = "Danh sách các món ăn thuộc nhóm: " + catName;

            sqlCommand.CommandText = "SELECT * FROM Food WHERE FoodCategoryID =  " + categoryID;

            // tạo đối tượng DataAdapter
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            // tạo datatable để chứa dữ liệu
            DataTable dt = new DataTable("Food");
            da.Fill(dt);

            //hiển thị danh sách món ăn lên Form
            dtgFood.DataSource = dt;

            //đóng kết nối và giải phóng bộ nhớ
            sqlConnection.Close();
            sqlConnection.Dispose();
            da.Dispose();

            cateID = categoryID;

        }

       

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // tạo đối tượng kết nối
            string connectionString = "server =.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // thiết lập lệnh truy vấn cho đối tượng Command
            sqlCommand.CommandText = "DELETE FROM Food WHERE ID = " +
            dtgFood.SelectedRows[0].Cells[0].Value;

            //mở kết nối tới cơ sở dữ liệu
            sqlConnection.Open();

            //thực jthi lệnh bằng phương thức ExcuteReader
            int numOfRowsEffected = sqlCommand.ExecuteNonQuery();

            //đóng kết nối
            sqlConnection.Close();
            if (numOfRowsEffected == 1)
            {
                // xóa tất cả dòng được chọn
                //foreach (DataGridViewRow row in dtgFood.SelectedRows)
                //{
                //    dtgFood.Rows.Remove(row);
                //}

                // xóa dòng được chọn đầu tiên
                dtgFood.Rows.Remove(dtgFood.SelectedRows[0]);
                MessageBox.Show("Xóa thành công");
            }
            else
                MessageBox.Show("Đã có lỗi xảy ra");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtgFood.SelectedRows.Count == 0) return;
            string connectionString = "server =.; database = RestaurantManagement; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            //tạo đối tượng thực thi lệnh
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

           if (dtgFood.SelectedRows[0].Cells[0].Value.ToString() == "")
            {
                sqlCommand.CommandText = "INSERT INTO Food(Name,Unit,FoodCategoryID,Price,Notes) VALUES (N'" + dtgFood.SelectedRows[0].Cells[1].Value.ToString() +
                   "',N'" + dtgFood.SelectedRows[0].Cells[2].Value.ToString() +
                   "'," + cateID +
                   "," + dtgFood.SelectedRows[0].Cells[4].Value +
                   ",N'" + dtgFood.SelectedRows[0].Cells[5].Value.ToString() + "')";

                //thiết lập lệnh truy vấn cho đối tượng command


                //mở kết nối tới csdl
                sqlConnection.Open();

                //thực thi lệnh bằng phương thức ExcuteReader
                int numOfRowsRffected = sqlCommand.ExecuteNonQuery();

                //đóng kết nối
                sqlConnection.Close();
                if (numOfRowsRffected == 1)
                {
                    MessageBox.Show("Thêm món ăn thành công!");
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra");
                }
            }
           else
            {
                sqlCommand.CommandText = "UPDATE Food SET Name = N'"+ dtgFood.SelectedRows[0].Cells[1].Value.ToString() + "'," +
                    "Unit = N'" + dtgFood.SelectedRows[0].Cells[2].Value.ToString() +
                   "', FoodCategoryID = " + cateID +
                   ", Price = " + dtgFood.SelectedRows[0].Cells[4].Value +
                   ", Notes = N'" + dtgFood.SelectedRows[0].Cells[5].Value.ToString() + "' WHERE ID = "+ dtgFood.SelectedRows[0].Cells[0].Value;

                //mở kết nối tới csdl
                sqlConnection.Open();

                //thực thi lệnh bằng phương thức ExcuteReader
                int numOfRowsRffected = sqlCommand.ExecuteNonQuery();

                //đóng kết nối
                sqlConnection.Close();
                if (numOfRowsRffected == 1)
                {
                    MessageBox.Show("Cập nhật món ăn thành công!");
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra");
                }
            }
            
        }
    }
}
