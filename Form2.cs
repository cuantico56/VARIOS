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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e) //AQUI HACEMOS LLAMADO DEL HILO
        {
            timer1.Start();
            timer1.Enabled = true;
            


            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;


            Task<string> tarea = new Task<string>(EnviarCert);
            tarea.Start();
            string resultado = await tarea;
            timer1.Stop();

            progressBar1.Value = 0;









        }   // **********fin async***

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            
            dialog.InitialDirectory = "C:\\SAMIR\\ECUADOR\\INTEGRACION_ECUADOR\\DOC_TXT_PROBADOS";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = openFileDialog1.FileName;
                openFileDialog1.Dispose();
            }

            byte[] cert = File.ReadAllBytes(textBox1.Text);
            richTextBox1.Text = Convert.ToBase64String(cert);
        }






        //*******************FUNCIONC SET+********************
        public string EnviarCert()
        {
            WS_CERT.UtilService samir = new WS_CERT.UtilService();

            byte[] a = File.ReadAllBytes(textBox1.Text);  //convertimos el certificado en base64
            string CertConv = Convert.ToBase64String(a);
            string ruc = textBox3.Text;

            var b = System.Text.Encoding.UTF8.GetBytes(textBox2.Text); //convertimos la clave en base64
            string clave = Convert.ToBase64String(b);



            var resp = samir.SetCertificado("50d1ab43434f8f73d6b3d8ee7089cc48867c1b3f", "241f0bf1a781613a473df93ceab54c64260ee951", ruc, CertConv, clave);

           


            if (InvokeRequired)
                
            Invoke(new Action(() => richTextBox2.Text = Convert.ToString(resp.code) + "\n"+resp.message));

            return "ok";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (progressBar1.Value == 100)
            {

                timer1.Stop();
                progressBar1.Value = 0;

            }

            else
            {
                progressBar1.Value += 5;
                timer1.Enabled = true;
        

            }
            

        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (textBox2.Text == string.Empty)
            {
                e.Cancel = true;
                textBox2.Focus();
                errorProvider1.SetError(textBox2, "Debes ingresar contraseña");

            }
        }
        //************************************************************




    }
}
