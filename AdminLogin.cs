using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;




namespace BSc_Unit6_Demo
{
    public partial class AdminLogin : Form
    {
        static string connetionString = "Data Source=(local);Initial Catalog=BSc_CS_601;Integrated Security=True";
        SqlConnection myConnection = new SqlConnection(connetionString);
       
        public AdminLogin()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand myCommand = new SqlCommand("(SELECT count(s_id) FROM tb_student WHERE s_email='" + txtUserName.Text + "' and s_pass='" + txtPass.Text + "' )", myConnection);
            if (myConnection.State == ConnectionState.Closed)
            {
                myConnection.Open();
            }
            if (myCommand.ExecuteScalar().ToString() != "0")
            {
                this.Hide();
                RecordMaster rm = new RecordMaster();
                rm.Show();
            }
            else
            {
                MessageBox.Show("The Username or Password You Entered Is Incorrect. !");
                txtUserName.Text = "";
                txtPass.Text = "";
            }

            myCommand.Clone();
        }

      

        
    }
}
