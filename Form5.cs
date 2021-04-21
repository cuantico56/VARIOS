using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TEST.OTEST;

namespace TEST
{
    public partial class Form5 : Form
    {

        
        public Form5()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            OTEST.Service servicio = new OTEST.Service(); //CREAMOS SERVICIO
            OTEST.PeticionFactura pet = new OTEST.PeticionFactura(); //Creamos una peticion
            Factura fact = new OTEST.Factura(); // Creamos una factura
            OTEST.infoFactura info = new OTEST.infoFactura(); //Creamos objeto infofactura            
            pet.Documento = fact; //Agregamos la factura a la clase documento
            pet.Documento.InfoFactura = info; // Se agrega info a la factura infofactura
            pet.Clave = "12345";
            pet.RUC = "17999888222";
            pet.Usuario = "usuario1";
            var resp = servicio.EnviarFactura(pet);


            MessageBox.Show(resp.Mensaje + "--"+resp.Codigo + "--" + "\r\n" + resp.Archivo);
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Service servicio1 = new Service();
            //****servicio1.DescargaArchivo();
            
            PeticionDescargaArchivo peticionDesc = new PeticionDescargaArchivo();
            peticionDesc.RUC = "1792433738001";
            peticionDesc.Usuario = "usuario1";
            peticionDesc.Clave = "dfacture";
            peticionDesc.Documento = "01-001-002-000000222";
            peticionDesc.Extension = "Pdf";

            var resp = servicio1.DescargaArchivo(peticionDesc);

            richTextBox1.Text = "La respuesta es: \r\n" + "procesado: " + resp.Procesado.ToString()+"\r\n"+ "Mensaje: " + resp.Mensaje + "\r\n" +"UUID: "+"falta"+"\r\n" + "Codigo" + resp.Codigo + "\r\n" + "archivo: "+"\r\n" + resp.Archivo;

            



        }

        private void button4_Click(object sender, EventArgs e)
        {
            Service servicio1 = new Service();
            PeticionFolios folios = new PeticionFolios();
            folios.Usuario = "usuario1";
            folios.RUC = "1792433738001";
            folios.Clave = "dfacture";

            var resp = servicio1.FoliosRestantes(folios);

            richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + "Folios Restantes: " + resp.FoliosRestantes+"\r\n"+ "Fecha Vencimiento"+resp.FechaVencimiento;



        }

        private void button5_Click(object sender, EventArgs e)
        {
            Service service = new Service()
;           PeticionEstatusDocumento peticion = new PeticionEstatusDocumento();
            peticion.Usuario = "usuario1";
            peticion.RUC = "1792433738001";
            peticion.Clave = "dfacture";
            peticion.Documento = "01-001-002-000000333";
            peticion.Extension = "xml";
            var resp = service.EstatusDocumento(peticion);
            richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje;

        }
    }
}
