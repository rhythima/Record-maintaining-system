using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BSc_Unit6_Demo
{
    public partial class RecordMaster : Form
    {
        static string connetionString = "Data Source=(local);Initial Catalog=BSc_CS_601;Integrated Security=True";
        SqlConnection myConnection = new SqlConnection(connetionString);
        DataTable dtdgvrecord = new DataTable();
        DataTable updatedid = new DataTable();
        DataTable deletedid = new DataTable();
        public RecordMaster()
        {
            InitializeComponent();
        }

        private void RecordMaster_Load(object sender, EventArgs e)
        {
            filldgvRegCustomer();
            fill_delete_id();
            fill_update_id();
        }

        //Method to fill record in datagridveiw.
        private void filldgvRegCustomer()
        {
            dtdgvrecord.Clear();
            SqlDataAdapter sdarecord = new SqlDataAdapter("select s_id as ID,Upper(s_name) as Name,s_dob as 'Date of Birth' ,s_mobile as 'Mobile Number', s_email As 'Email ID/User Name',s_pass as Password from tb_student order by s_id DESC", myConnection);
            sdarecord.Fill(dtdgvrecord);
            dgv_record.DataSource = dtdgvrecord;
            dgv_record.AutoGenerateColumns = false;
        }
            
        public void reset()
        {

            filldgvRegCustomer();
            fill_delete_id();
            fill_update_id();
            txt_name.Clear();
            txt_newname.Clear();
            txt_mobile.Clear();
            txt_newmobile.Clear();
            txt_email.Clear();
            txt_newemail.Clear();
            txt_pass.Clear();
            txt_newpass.Clear();
            dtp_dob.Value = System.DateTime.Now;
            dtp_newdob.Value = System.DateTime.Now;

        }

        //Method to get Record ID.
        private void fill_update_id()
        {
            try
            {
                updatedid.Clear();
                SqlDataAdapter sdastate = new SqlDataAdapter("select 0 s_id,'Select ID' as  s_name union select s_id,Convert(varchar(50),s_id)+'-'+s_name as s_name from tb_student ", myConnection);
                sdastate.Fill(updatedid);
                cb_updated_id.DataSource = updatedid;
                cb_updated_id.DisplayMember = "s_name";
                cb_updated_id.ValueMember = "s_id";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
        private void fill_delete_id()
        {
            try
            {
                deletedid.Clear();
                SqlDataAdapter sdastate = new SqlDataAdapter("select 0 s_id,'Select ID' as  s_name union select s_id,Convert(varchar(50),s_id)+'-'+s_name as s_name from tb_student ", myConnection);
                sdastate.Fill(deletedid);
                cb_deleted_id.DataSource = deletedid;
                cb_deleted_id.DisplayMember = "s_name";
                cb_deleted_id.ValueMember = "s_id";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        //Method to check valid email id.
        public bool varification(string verify)
        {
            return Regex.IsMatch(verify, "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
        }
        private void byn_insert_Click(object sender, EventArgs e)
        {
            SqlCommand myCommand = new SqlCommand("insert into tb_student (s_name,s_dob,s_mobile,s_email,s_pass) values('" + txt_name.Text + "','" + dtp_dob.Value.Date.ToString("yyyy-MM-dd") + "','" + txt_mobile.Text.ToString() + "','" + txt_email.Text.ToString() + "','" + txt_pass.Text.ToString() + "' )", myConnection);
            if (myConnection.State == ConnectionState.Open)
            { myConnection.Close(); }
            myConnection.Open();
            int row = myCommand.ExecuteNonQuery();
            if (row != 0)
            {
                MessageBox.Show("Record Inserted Successfully !");
                reset();
            }
            myCommand.Dispose();
        }
        private void btn_update_Click(object sender, EventArgs e)
        {
            SqlCommand myCommand = new SqlCommand("update tb_student set s_name='" + txt_newname.Text.ToString() + "',s_dob='" + dtp_newdob.Value.Date.ToString("yyyy-MM-dd") + "',s_mobile='" + txt_newmobile.Text.ToString() + "',s_email='" + txt_newemail.Text.ToString() + "',s_pass='" + txt_newpass.Text.ToString() + "' where s_id='" + Convert.ToInt32(cb_updated_id.SelectedValue) + "' ", myConnection);
            if (myConnection.State == ConnectionState.Open)
            { myConnection.Close(); }
            myConnection.Open();
            int row = myCommand.ExecuteNonQuery();
            if (row != 0)
            {
                MessageBox.Show("Record Update Successfully !");
                reset();
            }
            myCommand.Dispose();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Delete To Item !", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("delete from tb_student where s_id='" + Convert.ToInt32(cb_deleted_id.SelectedValue) + "' ", myConnection);
                if (myConnection.State == ConnectionState.Closed)
                {
                    myConnection.Open();
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                MessageBox.Show("Record Deleted Successfully !");
                reset();
            }
        }

        private void txt_mobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (System.Char)Keys.D1) || (e.KeyChar == (System.Char)Keys.D2) || (e.KeyChar == (System.Char)Keys.D3) || (e.KeyChar == (System.Char)Keys.D4) || (e.KeyChar == (System.Char)Keys.D5) || (e.KeyChar == (System.Char)Keys.D6) || (e.KeyChar == (System.Char)Keys.D7) || (e.KeyChar == (System.Char)Keys.D8) || (e.KeyChar == (System.Char)Keys.D9) || (e.KeyChar == (System.Char)Keys.D0) || (e.KeyChar == (System.Char)Keys.Back))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txt_newmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (System.Char)Keys.D1) || (e.KeyChar == (System.Char)Keys.D2) || (e.KeyChar == (System.Char)Keys.D3) || (e.KeyChar == (System.Char)Keys.D4) || (e.KeyChar == (System.Char)Keys.D5) || (e.KeyChar == (System.Char)Keys.D6) || (e.KeyChar == (System.Char)Keys.D7) || (e.KeyChar == (System.Char)Keys.D8) || (e.KeyChar == (System.Char)Keys.D9) || (e.KeyChar == (System.Char)Keys.D0) || (e.KeyChar == (System.Char)Keys.Back))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txt_email_Leave(object sender, EventArgs e)
        {
            if (txt_email.Text != "")
            {

                if (varification(txt_email.Text.Trim()) == false)
                {
                    epemail.SetError(txt_email, "Enter Valid E-Mail ID !");
                }
                else
                {
                    epemail.Clear();
                }
            }
        }

        

        private void txt_newemail_Leave(object sender, EventArgs e)
        {
            if (txt_newemail.Text != "")
            {

                if (varification(txt_newemail.Text.Trim()) == false)
                {
                    epuemail.SetError(txt_newemail, "Enter Valid E-Mail ID !");
                }
                else
                {
                    epuemail.Clear();
                }
            }
        }
    }
}
