using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSchoolApp
{
    public partial class Form2 : Form
    {
        public string user_name;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataSet ds;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lbl_admin.Text = user_name;
            GetData();
        }
        public void GetData()
        {
            con = new SqlConnection("server=DESKTOP-G46VU0J; Initial Catalog=ogr; Integrated Security=True");
            da = new SqlDataAdapter("Select *From ogrenci", con);

            ds = new DataSet();
            con.Open();
            da.Fill(ds, "ogrenci");

            data_grid_view.DataSource = ds.Tables["ogrenci"];
            con.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into ogrenci(number,name,surname) values (" + txt_number.Text + ",'" + txt_username.Text + "','" + txt_surname.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            GetData();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (data_grid_view.SelectedRows.Count > 0)
            {

                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "DELETE FROM ogrenci WHERE number=" + data_grid_view.CurrentRow.Cells[0].Value.ToString();
                cmd.ExecuteNonQuery();
                con.Close();
                data_grid_view.Rows.RemoveAt(data_grid_view.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Lüffen silinecek satırı seçin.");
            }
        }
        int sayi = 0;
        private void btn_update_Click(object sender, EventArgs e)
        {
            if (sayi == 0)
            {
                if (data_grid_view.SelectedRows.Count > 0)
                {
                    btn_update.Text = "UPDATE";
                    txt_number.Text = data_grid_view.CurrentRow.Cells[0].Value.ToString();
                    txt_username.Text = data_grid_view.CurrentRow.Cells[1].Value.ToString();
                    txt_surname.Text = data_grid_view.CurrentRow.Cells[2].Value.ToString();
                    sayi++;
                }
                else
                {
                    MessageBox.Show("Güncellemek için veri seçin");
                }
            }
            else
            {

                con = new SqlConnection("server=DESKTOP-G46VU0J; Initial Catalog=ogr; Integrated Security=True");
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE ogrenci SET number = @num, name = @name, surname = @surn Where number = "
                    + data_grid_view.CurrentRow.Cells[0].Value.ToString(); ;
                cmd.Parameters.AddWithValue("@num", txt_number.Text);
                cmd.Parameters.AddWithValue("@name", txt_username.Text);
                cmd.Parameters.AddWithValue("@surn", txt_surname.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                GetData();
                btn_update.Text = "CHOOSE";
                txt_number.Text = "";
                txt_surname.Text = "";
                txt_username.Text = "";
                sayi = 0;
            }
        }
        
    }
}


