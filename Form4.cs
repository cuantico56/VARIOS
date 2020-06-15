﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            timer1.Start();
        }

        public string total = "";
        public List<string> secuencias = new List<string>();
        public List<int> numsecuencias = new List<int>();
        public List<string> NuSec = new List<string>();
        public int cont = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\UnoSoloTipo";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string direcccion = openFileDialog1.InitialDirectory;
                openFileDialog1.Dispose();
            }




            string[] allfiles = System.IO.Directory.GetFiles(dialog.InitialDirectory, "*.txt*", System.IO.SearchOption.AllDirectories);



            //******************************************************CAMBIAR SECUENCIA Y FECHA DE CADA ARCHIVO*****************************


            for(int j=0; j < allfiles.Length; j++){ 


            string camb = File.ReadAllText(allfiles[j]);
            string[] contenido = camb.Split('|');
            int secuencialviejo;
            int secuencialnuevo;
            string FechaVieja="";
            string FechaNueva = "";

            textBox2.Text = contenido[16];
            FechaVieja = contenido[17];

            secuencialviejo = Convert.ToInt32(contenido[16]);
            secuencialnuevo = secuencialviejo+allfiles.Length;
            int decimalLength = secuencialnuevo.ToString("D").Length + 6; 
            contenido[16]= secuencialnuevo.ToString("D" + decimalLength.ToString());            
            textBox1.Text = contenido[16];

            DateTime hoy = new DateTime();
            hoy = DateTime.Now;

            FechaNueva = hoy.Day.ToString().PadLeft(2, '0') + "-" + hoy.Month.ToString().PadLeft(2, '0') + "-" + hoy.Year.ToString().PadLeft(2, '0') + " " + hoy.Hour.ToString().PadLeft(2, '0') + ":" + hoy.Minute.ToString().PadLeft(2, '0') + ":" + hoy.Second.ToString().PadLeft(2, '0');
                textBox3.Text = FechaNueva;

            string cambiado = camb.Replace(Convert.ToString(secuencialviejo), Convert.ToString(secuencialnuevo));
             camb = cambiado;
            cambiado = camb.Replace(FechaVieja, FechaNueva);

            StreamWriter escribir = File.CreateText(allfiles[j]);
            escribir.Write(cambiado);
            escribir.Close();

                
             }
        }


//*****************************************FUNCION PING*****************************************

            private  async void Eco()
        {

            Ping p = new Ping();
            

            PingReply resp = await p.SendPingAsync("172.16.70.10");

            if (resp.Buffer != null)
            {
                label4.BackColor = Color.Green;
                

                label4.Text = "Ping BDD: " + Convert.ToString(resp.RoundtripTime);

            }
            else
            {
                label4.Text = "No hay ping";
            }


        }












        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Eco();
            timer1.Enabled = true;



        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection con =new MySqlConnection("SERVER= 172.16.70.10" +";" + "DATABASE=testecfel"+";" + "UID=scastillo" + ";" + "PASSWORD=Tfhka2019..@" + ";");
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText= "SELECT secuencial FROM testecfel.invoices x WHERE emisor_ruc = 1792433738001 and cod_establecimiento = 001 and punto_emision = 101 ORDER by secuencial ASC;";
            comand.Connection = con;
            try
            {
                con.Open();
                MessageBox.Show("Connection ok!");

                MySqlDataReader resp = comand.ExecuteReader();

                while (resp.Read())
                {
                     secuencias.Add(resp.GetString(0));
                    
                    

                }
            

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Connection Error string '" + "' [" + ex.Number + "]: " + ex.Message);
            }


            con.Close();

            for(int j = 0; j < secuencias.Count-1; j++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[j].Cells[0].Value = secuencias[j];

              
            }

            for (int j = 1; j < secuencias.Count - 1; j++)
            {
                numsecuencias.Add(Convert.ToInt32(secuencias[j]));

            }

            int max = numsecuencias.Max();
            int min = numsecuencias.Min();
            label5.Text = Convert.ToString(max);
            label6.Text = Convert.ToString(min);

            cont = min;
         
            for(int i = 0; i <= numsecuencias.Count; i++)
            {
                if (!numsecuencias.Contains(cont))
                {
                    NuSec.Add(Convert.ToString(i).PadLeft(9, '0'));

                }
                else
                {
                    
                }
                cont++;
             }

             for(int i = 0; i < NuSec.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = NuSec[i];


            }





        }
    }
}
    

