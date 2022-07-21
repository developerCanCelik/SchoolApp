using System.Data.SqlClient;

namespace WinFormsSchoolApp
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM tblUser where usr=@user AND psw=@pass";
            con = new SqlConnection("server=DESKTOP-G46VU0J; Initial Catalog=tblUser; Integrated Security=True");
            cmd = new SqlCommand(sorgu, con);

            cmd.Parameters.AddWithValue("user", txt_username.Text);
            cmd.Parameters.AddWithValue("pass", txt_password.Text);

            con.Open();

            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                //MessageBox.Show("Tebrikler Baþarýlý Bir Þekilde Giriþ Yaptýnýz");
                Form2 frm = new Form2();
                frm.user_name = txt_username.Text;
                frm.Show();
                this.Hide();
            }
            else
            {

                MessageBox.Show("Kullanýcý Adýnýzý ve Þifrenizi Kontrol Ediniz");

            }
            con.Close();
        }
    }
}