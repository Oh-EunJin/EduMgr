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
using MySql.Data.MySqlClient;

namespace A_5조_EduMgr
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private string Constr = "server=localhost;uid=31DB;pwd=5DB!@;database=0301DB";
        //SQL 연결문자열 "server=localhost;uid=oooo;pwd=p12345!@;database=oooo";

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var Conn = new MySqlConnection(Constr);
            Conn.Open();
            var Comm = new MySqlCommand("Select userid from t_userinfo where userid = '" + this.textBox1.Text + "'", Conn);
            var myRead = Comm.ExecuteReader();

            var Conn2 = new MySqlConnection(Constr);
            Conn2.Open();
            String edm = this.textBox3.Text;
            int f = int.Parse(edm);
            var strSQL = "insert into t_userinfo(userid,edunum,name,birth,email,phone)" +
                "values('" + this.textBox1.Text + "'," + f + ",'" + this.textBox4.Text + "'" +
                ",'" + this.textBox5.Text + "','" + this.textBox6.Text + "','" + this.textBox7.Text + "')";
            var strSQL2 = "insert into t_login(userid,userpwd) values('" + this.textBox1.Text + "','" + this.textBox2.Text + "')";
            var myCom = new MySqlCommand(strSQL, Conn2);
            var myCom2 = new MySqlCommand(strSQL2, Conn2);
            myCom.ExecuteNonQuery();
            myCom2.ExecuteNonQuery();

            if (myRead.Read())
            {
                string struid = myRead[0].ToString();
                if (struid != this.textBox1.Text)
                {
                    myRead.Close();
                    Conn.Close();
                    Form1 frm1 = new Form1();
                    frm1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("이미 사용중인 아이디입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClear();
                }
            }

            myRead.Close();
            Conn.Close();
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
            Conn2.Close();

        }

        private void txtClear()
        {
            this.textBox1.Text = "";
        }
    }
}
