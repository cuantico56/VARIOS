using MySql.Data.MySqlClient;
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
            button1.Enabled = false;
        }
        
        public string total = "";
        public List<string> secuencias = new List<string>();
        public List<int> numsecuencias = new List<int>();
        public List<int> NuSec = new List<int>();
        public string[] allfiles;
        public int cont = 0;
        int tope = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            


            if (dialog.ShowDialog() == DialogResult.OK)
            {

                string direccion = folderBrowserDialog1.SelectedPath;
                
               
                folderBrowserDialog1.Dispose();
            }
            allfiles = System.IO.Directory.GetFiles(dialog.SelectedPath, "*.txt*", System.IO.SearchOption.AllDirectories);    //CAPTUREN EXCEPCION







            //******************************************************CAMBIAR SECUENCIA Y FECHA DE CADA ARCHIVO*****************************


            for (int j=0; j < allfiles.Length; j++){ 


            string camb = File.ReadAllText(allfiles[j]);
            string[] contenido = camb.Split('|');
            int secuencialviejo;
            int secuencialnuevo;
            string FechaVieja="";
            string FechaNueva = "";

            textBox2.Text = contenido[16];
            FechaVieja = contenido[17];

            secuencialviejo = Convert.ToInt32(contenido[16]);

            secuencialnuevo =NuSec[j] ;
            
            //int decimalLength = secuencialnuevo.ToString("D").Length + 6; 
            contenido[16]= NuSec.ToString().PadLeft(9,'0');            
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
            escribir.Dispose();
            escribir.Close();
            
                
             }

            



        }


//*****************************************FUNCION PING*****************************************

            private  async void Eco()
        {

            Ping p = new Ping();

            try
            {
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

            catch (Exception e)
            {


                MessageBox.Show("Algun problema de red :/  "+e);
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
            comand.CommandText= "SELECT secuencial FROM testecfel.invoices x WHERE emisor_ruc = 1792433738001 and cod_establecimiento = 001 and punto_emision = 300 ORDER by secuencial ASC;";
            comand.Connection = con;
           
            try
            {
                con.Open();
                

                MySqlDataReader resp = comand.ExecuteReader();

                while (resp.Read())
                {
                     secuencias.Add(resp.GetString(0));
                    
                    

                }
                
                resp.Dispose();
                

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Connection Error string '" + "' [" + ex.Number + "]: " + ex.Message);
            }

            con.Dispose();
            con.Close();

            label7.Text = secuencias.Count.ToString();

            for(int j = 0; j < secuencias.Count; j++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[j].Cells[0].Value = secuencias[j];

              
            }




            for (int j = 0; j < secuencias.Count ; j++)
            {
                numsecuencias.Add(Convert.ToInt32(secuencias[j]));

            }
            secuencias.Clear();

            int max = numsecuencias.Max();        //try catch  hacer
            int min = numsecuencias.Min();
            label5.Text = Convert.ToString(max);
            label6.Text = Convert.ToString(min);

            cont = min;
            tope =0;
           
            
            NuSec.Clear();

            for(int i = 0; i <=max-min; i++)
            {
                if (numsecuencias.Contains(cont)==false)
                {
                    tope++;
                    NuSec.Add(cont);
                    
                    
                    
                }
                if (tope == 50)
                {
                    break;
                }


                cont++;
                
                
             }
            label9.Text = max.ToString();
            label8.Text = min.ToString();

            for (int i = 0; i < NuSec.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = NuSec[i].ToString().PadLeft(9,'0');


            }

            button1.Enabled = true;



        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                NuSec.Clear();
                dataGridView2.Rows.Clear();
                int min = Convert.ToInt32(textBox4.Text);
                int max = Convert.ToInt32(textBox5.Text);
                int tope = max - min;
                for (int i = 0; i < tope + 1; i++)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = min.ToString().PadLeft(9, '0');
                    NuSec.Add(min);
                    ++min;

                }
                button1.Enabled = true;
            }

            catch (Exception a)
            {
              
                MessageBox.Show("NO puede dejar en blanco: " + a);
            }



        }

     
    }
}
    

