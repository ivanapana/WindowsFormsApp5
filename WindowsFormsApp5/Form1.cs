﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;

        private DataSet ds = new DataSet();
        private string alamat, query;
        public Form1()
        {
            alamat = "server=localhost; database=db_vispro24; username=root; password=;";
            koneksi = new MySqlConnection(alamat);

            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                query = string.Format("select * from tbl_pengguna where username = '{0}'", TxtUsername.Text);
                ds.Clear();
                koneksi.Open();
                perintah = new MySqlCommand(query, koneksi);
                adapter = new MySqlDataAdapter(perintah);
                perintah.ExecuteNonQuery();
                adapter.Fill(ds);
                koneksi.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow kolom in ds.Tables[0].Rows)
                    {
                        string sandi;
                        string level;
                        level = kolom["level"].ToString();
                        sandi = kolom["password"].ToString();
                        if (sandi == TxtPassword.Text)
                        {
                            if (level == "1")
                            {
                                FrmAdmin frmadmin = new FrmAdmin();
                                frmadmin.Show();
                            }
                            else
                            {
                                FrmPengguna frm = new FrmPengguna();
                                frm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Anda salah input password");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Username tidak ditemukan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
