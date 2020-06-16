using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST
{



    


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        

        public List<string> dir = new List<string>(); //aqui almacenamos los numero de los documentos
        public List<string> data = new List<string>(); //Aqui almacenamos los mensajes de los objetos factura
        public int codigo = 0;
        public int cod1 = 95;
        public int cod2 = 95;
        public int cod3 = 95;
        public int cod4 = 95;
        public string ment = " ";
        public string ment2 = " ";
        public string ment3 = " ";
        public string ment4 = " ";
        public string MenEstatus = " ";
        public string rep;
        



        //********************************************ABRIMOS LOS ARCHIVOS**********************************************


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\CUATRO_DOC";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = openFileDialog1.FileName;
                openFileDialog1.Dispose();
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog2 = new OpenFileDialog();
            dialog2.InitialDirectory = "C:\\CUATRO_DOC";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox2.Text = openFileDialog1.FileName;
                openFileDialog1.Dispose();
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\CUATRO_DOC";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox3.Text = openFileDialog1.FileName;
                openFileDialog1.Dispose();
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\CUATRO_DOC";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox4.Text = openFileDialog1.FileName;
                openFileDialog1.Dispose();
            }
        }



//****************************************ENVIAR TAREAS ASINCRONICAS*************************************************************


        private async void  button2_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            progressBar4.Value = 0;
            timer1.Start();
            timer2.Start();
            timer5.Start();
            timer6.Start();
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer5.Enabled = true;
            timer6.Enabled = true;

            richTextBox1.Text = string.Empty;
                richTextBox2.Text = string.Empty;
                richTextBox6.Text = string.Empty;
                richTextBox8.Text = string.Empty;
                Task<string> tarea = new Task<string>(EnviarFactura);  //Lamamos una tarea para hacerla asincrona
                Task<string> tarea2 = new Task<string>(EnviarFactura2);
                Task<string> tarea3 = new Task<string>(EnviarFactura3);
                Task<string> tarea4 = new Task<string>(EnviarFactura4);
                tarea.Start();
                tarea2.Start();
                tarea3.Start();
                tarea4.Start();
                progressBar1.Visible = true;
                progressBar2.Visible = true;
                progressBar3.Visible = true;
                progressBar4.Visible = true;

                string resultado = await tarea;
                timer1.Stop();
                progressBar1.Value = 100;

                string resultado2 = await tarea2;
                timer2.Stop();
                progressBar2.Value = 100;

                string resultado3 = await tarea3;
                timer5.Stop();
                progressBar3.Value = 100;

                string resultado4 = await tarea4;
                timer6.Stop();
                progressBar4.Value = 100;

        }





 // *********************************************************funciones******************************************************

        public string EnviarFactura()
        {
            WS_Ecuador.Integracion samir = new WS_Ecuador.Integracion();
            
                byte[] a = File.ReadAllBytes(textBox1.Text);


            

                var resp = samir.Factura("1792433738001", "1792433738001_INT", "X1X339SF13BA", a, "facturaTEST");
                



                if (InvokeRequired)
                    Invoke(new Action(() => richTextBox1.Text = resp.MensajeError + "\n"));
                Invoke(new Action(() => richTextBox2.Text = resp.NumeroError + "\n"));

                return "ok";
        }


 //***************************************************ENVIAR SEGUNDA FACTURA******************************************

        public string EnviarFactura2()
        {
            WS_Ecuador.Integracion samir = new WS_Ecuador.Integracion();

            
                byte[] b = File.ReadAllBytes(textBox2.Text);


                var resp = samir.Factura("1792433738001", "1792433738001_INT", "X1X339SF13BA", b, "facturaTEST2");
                



                if (InvokeRequired)
                    Invoke(new Action(() => richTextBox3.Text = "SEGUNDO: " + resp.MensajeError + "\n"));
                Invoke(new Action(() => richTextBox4.Text = "SEGUNDO: " + resp.NumeroError + "\n"));

               
            
            return "ok";
        }


//****************************************************ENVIAR tercera FACTURA*****************************************

        public string EnviarFactura3()
        {
            WS_Ecuador.Integracion samir = new WS_Ecuador.Integracion();


            byte[] c = File.ReadAllBytes(textBox3.Text);


      

            var resp = samir.Factura("1792433738001", "1792433738001_INT", "X1X339SF13BA", c, "facturaTEST3");




            if (InvokeRequired)
                Invoke(new Action(() => richTextBox6.Text = "TERCERO: " + resp.MensajeError + "\n"));
            Invoke(new Action(() => richTextBox7.Text = "TERCERO: " + resp.NumeroError + "\n"));



            return "ok";
        }

//*************************************************ENVIAR CUARTA FACTURA**********************************

        public string EnviarFactura4()
        {
            WS_Ecuador.Integracion samir = new WS_Ecuador.Integracion();


            byte[] d = File.ReadAllBytes(textBox4.Text);




            var resp = samir.Factura("1792433738001", "1792433738001_INT", "X1X339SF13BA", d, "facturaTEST4");




            if (InvokeRequired)
                Invoke(new Action(() => richTextBox8.Text = "CUARTO: " + resp.MensajeError + "\n"));
            Invoke(new Action(() => richTextBox9.Text = "CUARTO: " + resp.NumeroError + "\n"));



            return "ok";
        }







 // ***********************************************ENVIAR CONSULTA ESTATUS DOCUMENTO*****************************
        public string Estatus(string num)
        {

            try
            {

                WS_Ecuador.Integracion mario = new WS_Ecuador.Integracion();
                var res = mario.EstatusDocumento("1792433738001", "1792433738001_INT", "X1X339SF13BA", num);
                codigo = res.codigo;
                MenEstatus = "codigo: " + Convert.ToString(codigo) + "\n" + "Mensaje: " + res.mensaje;
                mario.Dispose();

            }
            catch (Exception e)
            {

                MessageBox.Show("Hubo un error: " + e);
            }
            return "ok";
        }



        //****************************************************LIMPIAR VALORES BARRAS**********************************

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            richTextBox3.Text = string.Empty;
            richTextBox4.Text = string.Empty;
            richTextBox5.Text = string.Empty;
            richTextBox6.Text = string.Empty;
            richTextBox7.Text = string.Empty;
            richTextBox8.Text = string.Empty;
            richTextBox9.Text = string.Empty;
            dir.Clear();
           

            progressBar1.Visible = false;
            progressBar2.Visible = false;
            progressBar3.Visible = false;
            progressBar4.Visible = false;
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            progressBar4.Value = 0;
        }







        public void CargaDir()
        {
            try
            {

                string ese = File.ReadAllText(textBox1.Text);
                string[] contenido = ese.Split('|');
                dir.Add(contenido[18] + "-" + contenido[14] + "-" + contenido[15] + "-" + contenido[16]);


                ese = File.ReadAllText(textBox2.Text);
                contenido = ese.Split('|');
                dir.Add(contenido[18] + "-" + contenido[14] + "-" + contenido[15] + "-" + contenido[16]);

                ese = File.ReadAllText(textBox3.Text);
                contenido = ese.Split('|');
                dir.Add(contenido[18] + "-" + contenido[14] + "-" + contenido[15] + "-" + contenido[16]);

                ese = File.ReadAllText(textBox4.Text);
                contenido = ese.Split('|');
                dir.Add(contenido[18] + "-" + contenido[14] + "-" + contenido[15] + "-" + contenido[16]);


                richTextBox5.Text = dir[0] + "\r\n" + dir[1] + "\r\n" + dir[2] + "\r\n" + dir[3];

                timer4.Enabled = true;
            }

            catch (Exception a)
            {
                timer3.Stop();
                MessageBox.Show("NO puede dejar en blanco: ");
            }


        }



//****************************************TIMER PROGRESO BARRAS***********************************

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value <= 100)
            {
                timer1.Interval = 1000;
                progressBar1.Value += 5;
                timer1.Enabled = true;

            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (progressBar2.Value <= 100)
            {
                timer2.Interval = 1000;
                progressBar2.Value += 5;
                timer2.Enabled = true;

            }

        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (progressBar3.Value <= 100)
            {
                timer5.Interval = 1000;
                progressBar3.Value += 5;
                timer5.Enabled = true;

            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {

            if (progressBar4.Value <= 100)
            {
                timer6.Interval = 1000;
                progressBar4.Value += 5;
                timer6.Enabled = true;

            }

        }




        //****************************DOCUMENTOS CONSULTANDO********************************************
        private void button4_Click(object sender, EventArgs e)
        {
            timer3.Start();
            
            timer4.Start();

            CargaDir();
        }






        
//*****************************PRINT CONSULTA DOCUMENTOS***************************************

        private void timer3_Tick(object sender, EventArgs e)
        {

            CargaDir();

            if (cod1 != 0 & cod2 != 0 & cod3 != 0 & cod4 != 0)
            {
                richTextBox2.Text = "Buscando " + dir[0];
                richTextBox4.Text = "Buscando " + dir[1];
                richTextBox7.Text = "Buscando " + dir[2];
                richTextBox9.Text = "Buscando " + dir[3];
                timer3.Enabled = false;

                Estatus(dir[0]);
                ment = MenEstatus;
                cod1 = codigo;
                richTextBox1.Text = MenEstatus;

                Estatus(dir[1]);
                ment2 = MenEstatus;
                cod2 = codigo;
                richTextBox3.Text = MenEstatus;

                Estatus(dir[2]);
                ment3 = MenEstatus;
                cod3 = codigo;
                richTextBox6.Text = MenEstatus;

                Estatus(dir[3]);
                ment4 = MenEstatus;
                cod4 = codigo;
                richTextBox8.Text = MenEstatus;

                timer3.Enabled = true;

            }

            else
            {
                richTextBox1.Text = "Codigo: "+Convert.ToString(cod1)+"\n"+"Mensaje: "+ment;
                richTextBox3.Text = "Codigo: " + Convert.ToString(cod2) + "\n" + "Mensaje: " + ment2;
                richTextBox6.Text = "Codigo: " + Convert.ToString(cod3) + "\n" + "Mensaje: " + ment3;
                richTextBox8.Text = "Codigo: " + Convert.ToString(cod4) + "\n" + "Mensaje: " + ment4;

                timer3.Stop();
                timer4.Stop();
                cod1 = 95;
                cod2 = 95;
                cod3 = 95;
                cod4 = 95;
            }


        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Enabled = false;
            timer4.Interval = 1000;

            if (button4.BackColor == Color.Silver)
            {
                button4.BackColor = Color.Aquamarine;
            }

  
            else
            {
                button4.BackColor = Color.Silver;
            }
           
            timer4.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }


//*******************************CARGAMOS VARIOS ARCHIVOS********************
        private async void button10_Click(object sender, EventArgs e)
        {
            data.Clear();
            

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            


            if (dialog.ShowDialog() == DialogResult.OK)
            {

                string direcccion = dialog.SelectedPath;
                folderBrowserDialog1.Dispose();
            }



            
            string[] allfiles = System.IO.Directory.GetFiles(dialog.SelectedPath, "*.txt*", System.IO.SearchOption.AllDirectories);
            
            int cuantos = allfiles.Length;
            rep = "";
            for (int i = 0; i < cuantos; i++)
            {

               rep+= allfiles[i] +"\n";
               

            }
            richTextBox5.Text = rep;


            //******************************************************CAMBIAR SECUENCIA Y FECHA DE CADA ARCHIVO*****************************
/*
            string camb = File.ReadAllText(allfiles[0]);
            string[] contenido = camb.Split('|');
            int secuencial;


            secuencial = Convert.ToInt32(contenido[16]);
            secuencial += secuencial;
            contenido[16] = Convert.ToString(secuencial);

*/


            //********************************* AQUI SE ENVIAN LA CANTIDAD DE ARCHIVOS****************************************     

            WS_Ecuador.Integracion envi = new WS_Ecuador.Integracion();
           
            timer7.Start();
                                          
            List<Task> Tareas = new List<Task>();
            List<WS_Ecuador.RespuestaTimbradoTXT> resp = new List<WS_Ecuador.RespuestaTimbradoTXT>();

            for (int j= 0; j<allfiles.Length; j++)
            {
                byte[] d = File.ReadAllBytes(allfiles[j]);
                Tareas.Add( Task.Run(() => { resp.Add(envi.Factura("1792433738001", "1792433738001_INT", "X1X339SF13BA", d, "facturaTEST4")); }));
                

            }

            progressBar5.Value = 0;
            timer7.Enabled = true;



                await Task.WhenAll(Tareas.ToArray());

            timer7.Enabled = false;
            progressBar5.Value = 100;
            


            for (int i = 0; i < Tareas.Count; i++)
            {

                WS_Ecuador.RespuestaTimbradoTXT r = resp[i];

                
                data.Add(r.UUID+": Mensaje Error: "+r.MensajeError +"Numero Error: "+r.NumeroError+"\n");


                StreamWriter doc = File.AppendText(@"C:\SALIDA\ArchivoSalidas.txt");
                doc.WriteLine(r.UUID + ": Mensaje Error: " + r.MensajeError + "Numero Error: " + r.NumeroError +" Fecha: "+ r.FechaHora + " Procesado: "+ r.Procesado +"\n");
                doc.Close();


            }

            rep = "";
            for (int i = 0; i < data.Count; i++)
            {
                rep += data[i]+"\n";


            }


            richTextBox5.Text = rep;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            timer7.Enabled = false;

            if (progressBar5.Value < 100)
            {
                progressBar5.Value += 5;
                timer7.Enabled = true;
            }

            else 
            {
                timer7.Enabled = false;
            
            
            }
        }
    }
    }

