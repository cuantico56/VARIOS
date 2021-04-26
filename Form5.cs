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

            try
            {
                OTEST.Service servicio = new OTEST.Service(); //CREAMOS SERVICIO
                OTEST.PeticionFactura pet = new OTEST.PeticionFactura(); //Creamos una peticion
                Factura fact = new OTEST.Factura(); // Creamos una factura            
                OTEST.InfoTributaria infotrib = new InfoTributaria();
                fact.InfoTributaria = infotrib;
                pet.Documento = fact;
                fact.InfoTributaria.NombreComercial = "ECUAGOCH";
                fact.InfoTributaria.AgenteRetencion = "SI";
                fact.InfoTributaria.RazonSocial = "ECUAGOCH";
                fact.InfoTributaria.Ruc = "1700000000001";
                fact.InfoTributaria.CodigoNumerico = "01";
                fact.InfoTributaria.Estab = "001";
                fact.InfoTributaria.PtoEmi = "001";
                fact.InfoTributaria.Secuencial = "000000222";
                fact.InfoTributaria.DirMatriz = "En un lugar de la mancha...";

                OTEST.InfoFactura infoFactura = new InfoFactura();
                fact.InfoFactura = infoFactura;
                infoFactura.FechaEmision = "01/01/2000";
                infoFactura.DirEstablecimiento = "dirEstablecimiento0";
                infoFactura.ContribuyenteEspecial = "Contribuyente especial";
                infoFactura.ObligadoContabilidad = "SI";


//************************CREAMOS OTROS RUBREOS TERCEROS*****************************
                Rubro rubro1 = new Rubro();   // se deben instanciar
                Rubro rubro2 = new Rubro();
                Rubro rubro3 = new Rubro();
                fact.OtrosRubrosTerceros = new Rubro[3]; // se instancia un array tipo <rubro
                fact.OtrosRubrosTerceros[0] = rubro1;  // se coloca  cada rubro en el array
                fact.OtrosRubrosTerceros[1] = rubro2;  // se coloca  cada rubro en el array
                fact.OtrosRubrosTerceros[2] = rubro3;  // se coloca  cada rubro en el array


//**********************************INFOADICIONAL**************************************

                infoAdicional info1 = new infoAdicional();
                infoAdicional info2 = new infoAdicional();
                info1.Nombre = "Inofrmacion";
                info1.Valor = "1234567";
                info2 = info1;

                fact.CampoAdicional = new infoAdicional[2];
                fact.CampoAdicional[0] = info1;
                fact.CampoAdicional[1] = info2;


                //*****************************DETALLES***********************************************
                
                Detalle detalle1 = new Detalle();
                Detalle detalle2 = new Detalle();
                detalle1.CodigoPrincipal = "Detalle detalle1 = new Detalle()";
                detalle1.CodigoAuxiliar = "codigo auxiliar";
                detalle1.Descripcion = "descriplkugyjf";
                detalle1.UnidadMedida = "UND";
                detalle1.Cantidad = "1";
                detalle1.PrecioUnitario = "10.00";
                detalle1.PrecioSinSubsidio = "0.00";
                detalle1.Descuento = "0.00";
                detalle1.PrecioTotalSinImpuesto = "10.00";
                detalle1.DetAdicional = new DetAdicional[2];
                var detAd1 = new DetAdicional();
                var detAd2 = new DetAdicional();
                



                fact.Detalles = new Detalle[2];
                fact.Detalles[0] = detalle1;
                fact.Detalles[1] = detalle2;














//**********************************ENVIO PETICION***************************
                pet.Clave = "12345";
                pet.RUC = "17999888222";
                pet.Usuario = "usuario1";
                var resp = servicio.EnviarFactura(pet);


                MessageBox.Show(resp.Mensaje + "--" + resp.Codigo + "--" + "\r\n" + resp.Archivo);
            }


            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.ToString());


            }

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
