using MySql.Data.MySqlClient;
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
            label5.Enabled = false;
            label5.Visible = false;
            label6.Enabled = false;
            label6.Visible = false;
            label7.Enabled = false;
            label7.Visible = false;
            textBox4.Enabled = false;
            textBox4.Visible = false;
            textBox5.Enabled = false;
            textBox5.Visible = false;
            textBox6.Enabled = false;
            textBox6.Visible = false;

        }




        public List<string> dir = new List<string>(); //aqui almacenamos los numero de los documentos
        public List<string> data = new List<string>(); //Aqui almacenamos los mensajes de los objetos factura
        public List<string> numFact = new List<string>();
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
        public string ruc;
        public string usuario;
        public string contraseña;
        public string Id_Max;
        public byte[] blit;
        public string punto_emision;
        public string cod_establecimiento;
        public string secuencial;
        public int secuencia;
        public string mensaje = "19";
        public string facturaNueva;
        public string fecha;



        /****************************************ENVIAR TAREAS ASINCRONICAS*************************************************************


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


        ********************************************************************************************************************/


        /*********************************************************funciones******************************************************

               public string EnviarFactura()
               {
                   WS_Ecuador.Integracion samir = new WS_Ecuador.Integracion();

                       byte[] a = File.ReadAllBytes(textBox1.Text);




                       var resp = samir.Factura("1792433738001", "1792433738001_INT", "UF5L1AWUA1U2", a, "facturaTEST");




                       if (InvokeRequired)
                           Invoke(new Action(() => richTextBox1.Text = resp.MensajeError + "\n"));
                       Invoke(new Action(() => richTextBox2.Text = resp.NumeroError + "\n"));

                       return "ok";
               }


        //*********************************************************************************************/








        // ***********************************************ENVIAR CONSULTA ESTATUS DOCUMENTO*****************************
        public string Estatus(string num)
        {

            try
            {

                WS_Ecuador.Integracion mario = new WS_Ecuador.Integracion();
                var res = mario.EstatusDocumento("1792433738001", "1792433738001_INT", "UF5L1AWUA1U2", num);
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



        /****************************************************LIMPIAR VALORES BARRAS**********************************

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



   ***************************************************************************************/



        /*       public void CargaDir()
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
                       MessageBox.Show("NO puede dejar en blanco: "+a);
                   }


               }

       *****************************************************************************************************/

        /****************************************TIMER PROGRESO BARRAS***********************************

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

                ***************************************************************************************/


        /****************************DOCUMENTOS CONSULTANDO********************************************
        private void button4_Click(object sender, EventArgs e)
        {
            timer3.Start();
            
            timer4.Start();

            CargaDir();
        }

***********************************************************************************************/





        /*****************************PRINT CONSULTA DOCUMENTOS***************************************

                private void timer3_Tick(object sender, EventArgs e)
                {

                    // CargaDir();

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
                ******************************************************************************/


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


            try {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                dialog.SelectedPath = "C:\\LISTA";

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

                    rep += allfiles[i] + "\n";


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


                
                

                if (comboBox1.SelectedIndex == 0)  // Si seleccionamos TEST
                {
                    MessageBox.Show("Usted escogió: " + Convert.ToString(comboBox1.SelectedIndex));


                    var envi = new WS_Ecuador.Integracion();
                    var resp = new List<WS_Ecuador.RespuestaTimbradoTXT>();



                    ruc = textBox1.Text;
                    usuario = textBox2.Text;
                    contraseña = textBox3.Text;




                    timer7.Start();

                    List<Task> Tareas = new List<Task>();


                    for (int j = 0; j < allfiles.Length; j++)
                    {
                        byte[] d = File.ReadAllBytes(allfiles[j]);
                        Tareas.Add(Task.Run(() => { resp.Add(envi.Factura(ruc, usuario, contraseña, d, "facturaTEST4")); }));


                    }


                    progressBar5.Value = 0;
                    timer7.Enabled = true;



                    await Task.WhenAll(Tareas.ToArray());

                    timer7.Enabled = false;
                    progressBar5.Value = 100;



                    for (int i = 0; i < Tareas.Count; i++)
                    {

                        var r = resp[i];


                        data.Add(r.UUID + ": Mensaje Error: " + r.MensajeError + "Numero Error: " + r.NumeroError + "\n");


                        StreamWriter doc = File.AppendText(@"C:\SALIDA\ArchivoSalidas.txt");
                        doc.WriteLine(r.UUID + ": Mensaje Error: " + r.MensajeError + "Numero Error: " + r.NumeroError + " Fecha: " + r.FechaHora + " Procesado: " + r.Procesado + "\n");
                        doc.Close();


                    }

                    rep = "";
                    for (int i = 0; i < data.Count; i++)
                    {
                        rep += data[i] + "\n";


                    }


                    richTextBox5.Text = rep;
                }

                if (comboBox1.SelectedIndex==1)    //si seleccionamos DEMO

                    {
                    MessageBox.Show("Usted escogió: " + Convert.ToString(comboBox1.SelectedIndex));
                    var envi = new WS_DEMO.Integracion();
                        var resp = new List<WS_DEMO.RespuestaTimbradoTXT>();



                        ruc = textBox1.Text;
                        usuario = textBox2.Text;
                        contraseña = textBox3.Text;




                        timer7.Start();

                        List<Task> Tareas = new List<Task>();


                        for (int j = 0; j < allfiles.Length; j++)
                        {
                            byte[] d = File.ReadAllBytes(allfiles[j]);
                            Tareas.Add(Task.Run(() => { resp.Add(envi.Factura(ruc, usuario, contraseña, d, "facturaTEST4")); }));


                        }


                        progressBar5.Value = 0;
                        timer7.Enabled = true;



                        await Task.WhenAll(Tareas.ToArray());

                        timer7.Enabled = false;
                        progressBar5.Value = 100;



                        for (int i = 0; i < Tareas.Count; i++)
                        {

                            var r = resp[i];


                            data.Add(r.UUID + ": Mensaje Error: " + r.MensajeError + "Numero Error: " + r.NumeroError + "\n");


                            StreamWriter doc = File.AppendText(@"C:\SALIDA\ArchivoSalidas.txt");
                            doc.WriteLine(r.UUID + ": Mensaje Error: " + r.MensajeError + "Numero Error: " + r.NumeroError + " Fecha: " + r.FechaHora + " Procesado: " + r.Procesado + "\n");
                            doc.Close();


                        }

                        rep = "";
                        for (int i = 0; i < data.Count; i++)
                        {
                            rep += data[i] + "\n";


                        }


                        richTextBox5.Text = rep;
                    }


                if (comboBox1.SelectedIndex == -1)
                {

                    MessageBox.Show("NO fue procesada la petición, escoja una plataforma TEST/DEMO");
                


                }


              }

            catch (Exception a)
            {
              
                MessageBox.Show("Debes escojer una carpeta: ERRROR:   "+ a);
            }

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

        private void button1_Click(object sender, EventArgs e)
        {


            //***************LEEMOS EN LA BDD EL ULTIMO SECUENCIAL ENVIADO e incrementamos*************

            numFact = Bdd();
            
            numFact = Next(numFact);
            

            // ****************AQUI ENVIAMOS UNA FACTURA RAPIDA*******************
            
            mensaje = "19";


            while (mensaje == "19")
            {

                if (checkBox1.Checked == false)
                {
                    
                    fecha = Fechas();  
                    facturaNueva = "01|SAMIR CASTILLO|04/1791282183001|scastillo@thefactoryhka.com|0011223344|CALLE PRINCIPAL|PICHINCHA|PICHINCHA2|PICHINCHA3|ECUADOR|12345|SI||\r\n02|" + numFact[0] + "|" + numFact[1] + "|" + numFact[2] + "|" + fecha + "|01|||||00||70000700|3||||||||||||0.00|10.00|00.00|00.00|10.00|0.00|0.00|10.00|77777.77|0.00|0.00|10.00|DOLAR|||||||||||||||||01/01/01|10.00/0/0|0/0/0|\r\n03|0000000000002|CEBOLLA|1.000|KGM|10.00|0.00|10.00||0|0.00|0.00|0|0.00|0.00|0.00||0.00|0.00|0.00|||||||||||||AGREGUE COMENT|AGREGUE COMENT|";
                }
                if (checkBox1.Checked==true)
                {
                    fecha = Fechas();
                    numFact[0] = textBox4.Text;
                    numFact[1] = textBox5.Text;
                    numFact[2] = textBox6.Text;
                    facturaNueva = "01|SAMIR CASTILLO|04/1791282183001|scastillo@thefactoryhka.com|0011223344|CALLE PRINCIPAL|PICHINCHA|PICHINCHA2|PICHINCHA3|ECUADOR|12345|SI||\r\n02|" + numFact[0] + "|" + numFact[1] + "|" + numFact[2] + "|"+fecha+"|01|||||00||70000700|3||||||||||||0.00|10.00|00.00|00.00|10.00|0.00|0.00|10.00|77777.77|0.00|0.00|10.00|DOLAR|||||||||||||||||01/01/01|10.00/0/0|0/0/0|\r\n03|0000000000002|CEBOLLA|1.000|KGM|10.00|0.00|10.00||0|0.00|0.00|0|0.00|0.00|0.00||0.00|0.00|0.00|||||||||||||AGREGUE COMENT|AGREGUE COMENT|";

                }

            

                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                
                
            
                blit = Encoding.ASCII.GetBytes(facturaNueva);  //Se lleva a bytes

                try
                {
                    WS_DEMO.Integracion FactNu = new WS_DEMO.Integracion();
                    var res = FactNu.Factura("1792433738001", "usuario1", "dfacture", blit, "FacturaRapida");
                    MenEstatus = "codigo: " + Convert.ToString(res.MensajeError) + "\n" + "Mensaje: " + res.NumeroError + "\n" + "UUID: " + res.UUID;
                    mensaje = res.NumeroError;
                    richTextBox5.Text = MenEstatus;
                    if (mensaje == "19")
                    {

                        numFact = Next(numFact);
                        textBox4.Text = numFact[0];
                        textBox5.Text = numFact[1];
                        textBox6.Text = numFact[2];
                        richTextBox5.Text= "Se enviara otra secuencia...";
                                               
                    }

                    if (mensaje=="95") {

                        MessageBox.Show("Finalizado");
                        numFact.Clear();
                        return;
                    
                    }
                    
                    FactNu.Dispose();

                }
                catch (Exception v)
                {

                    MessageBox.Show("Hubo un error: " + v);
                }
                

            }

            MessageBox.Show("Se envio Factura: "+ numFact[0]+"-"+numFact[1]+"-"+numFact[2]);
            numFact.Clear();



        }



        private void richTextBox5_Click(object sender, EventArgs e)
        {
            richTextBox5.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {



            if (checkBox1.Checked == true)
            {
                label5.Enabled = true;
                label5.Visible = true;
                label6.Enabled = true;
                label6.Visible = true;
                label7.Enabled = true;
                label7.Visible = true;
                textBox4.Enabled = true;
                textBox4.Visible = true;
                textBox5.Enabled = true;
                textBox5.Visible = true;
                textBox6.Enabled = true;
                textBox6.Visible = true;

            }
            else
            {
                label5.Enabled = false;
                label5.Visible = false;
                label6.Enabled = false;
                label6.Visible = false;
                label7.Enabled = false;
                label7.Visible = false;
                textBox4.Enabled = false;
                textBox4.Visible = false;
                textBox5.Enabled = false;
                textBox5.Visible = false;
                textBox6.Enabled = false;
                textBox6.Visible = false;

            }

        }

//**************************FUNCION QUE ENTREGA STRING CON FECHA FORMATO *****************
        public string Fechas()
        {
            string fecha;
            DateTime fe = new DateTime();
            fe = DateTime.Now;
            fecha = fe.Day.ToString().PadLeft(2, '0') + "-" + fe.Month.ToString().PadLeft(2, '0') + "-" + fe.Year.ToString().PadLeft(2, '0') + " " + fe.Hour.ToString().PadLeft(2, '0') + ":" + fe.Minute.ToString().PadLeft(2, '0') + ":" + fe.Second.ToString().PadLeft(2, '0');

            return fecha;
        }



        //****************************FUNCION LEER BDD Y ENTREGAR LISTA***************
        //***** La funcion entrega una lista donde contiene sucursal,punto emision, secuencial


        public List<string> Bdd()
        {
            List<string> next = new List<string>();
            MySqlConnection con = new MySqlConnection("SERVER= 172.16.70.4" + ";" + "DATABASE=demoecfel" + ";" + "UID=scastillo" + ";" + "PASSWORD=Tfhka2019..@" + ";");
            MySqlCommand comand = new MySqlCommand();
            comand.CommandText = "SELECT cod_establecimiento,punto_emision,secuencial FROM demoecfel.invoices WHERE id=(SELECT MAX(id) FROM demoecfel.invoices WHERE activo =1 limit 1);";
            comand.Connection = con;

            try
            {
                con.Open();


                MySqlDataReader resp = comand.ExecuteReader();

                while (resp.Read())
                {
                    next.Add(resp.GetString(0));
                    next.Add(resp.GetString(1));
                    next.Add(resp.GetString(2));
                    

                }

                resp.Dispose();


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Connection Error string '" + "' [" + ex.Number + "]: " + ex.Message);
            }

            con.Dispose();
            con.Close();
            return next;
            

        }


//****************USAREMOS ESTA FUNCION PARA INCREMENTAR LOS SECUENCIALES DE LAS LISTAS*******
        private List<string> Next(List<string> boo)
        {
            int secuencia = Convert.ToInt32(boo[2]);
            secuencia = secuencia + 1;

            boo[2] = secuencia.ToString().PadLeft(9, '0');


            return boo;
        }



//**************BUSCAMOS EN LA BDD TODOS LOS ULTIMOS INDICES FACT,NC,ND,GUIA,RETE,LIQU *******************
        private void button2_Click(object sender, EventArgs e)
        {
         
                List<string> next = new List<string>();
                MySqlConnection con = new MySqlConnection("SERVER= 172.16.70.4" + ";" + "DATABASE=demoecfel" + ";" + "UID=scastillo" + ";" + "PASSWORD=Tfhka2019..@" + ";");
                MySqlCommand comand = new MySqlCommand();
            comand.CommandText = "SELECT cod_establecimiento ,punto_emision ,secuencial FROM demoecfel.invoices WHERE id=(SELECT MAX(id) FROM demoecfel.invoices WHERE activo =1) UNION ALL SELECT cod_establecimiento, punto_emision, secuencial FROM demoecfel.credit_notes cn WHERE id = (SELECT MAX(id) FROM demoecfel.credit_notes WHERE activo = 1) UNION ALL SELECT cod_establecimiento, punto_emision, secuencial FROM demoecfel.debt_notes WHERE id = (SELECT MAX(id) FROM demoecfel.debt_notes WHERE activo = 1) UNION ALL SELECT cod_establecimiento, punto_emision, secuencial FROM demoecfel.reference_guides WHERE id = (SELECT MAX(id) FROM demoecfel.reference_guides WHERE activo = 1) UNION ALL SELECT cod_establecimiento, punto_emision, secuencial FROM demoecfel.retention_receipts WHERE id = (SELECT MAX(id) FROM demoecfel.retention_receipts WHERE activo = 1) UNION ALL SELECT cod_establecimiento, punto_emision, secuencial FROM demoecfel.psclearances WHERE id = (SELECT MAX(id) FROM demoecfel.psclearances WHERE activo = 1);";
                comand.Connection = con;

                try
                {
                    con.Open();


                    MySqlDataReader resp = comand.ExecuteReader();
               
                MessageBox.Show("Llego mensaje");

                    while (resp.Read())
                    {
                    
                        next.Add(resp.GetString(0));
                        next.Add(resp.GetString(1));
                        next.Add(resp.GetString(2));           

                }


                    resp.Dispose();


                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Connection Error string '" + "' [" + ex.Number + "]: " + ex.Message);
                }

            int t = next.Count;
            int i = 0;
            for (i = 0; i < t; i+=3)
            {
                MessageBox.Show(next[i] + "-" + next[i + 1] + "-" + next[i + 2]);

            }

           
                con.Dispose();
                con.Close();
           


            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form5 = new Form5();
            form5.Show();
            this.Hide();
        }
    }
    }

