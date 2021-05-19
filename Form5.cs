using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using TEST.serviceobj;

namespace TEST
{
    public partial class Form5 : Form
    {
        string ambiente;

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
            int amb = comboBox1.SelectedIndex;
            
            

            

            switch (amb)
            {
                case 0:
                ambiente = "https://demointws.thefactoryhka.com.ec/Service.svc";
                    break;
                case 1:
                    ambiente = "http://testintws.thefactoryhka.com.ec/Service.svc";
                    break;

            }



            

            try
            {


                serviceobj.Service servicio = new serviceobj.Service(); //CREAMOS SERVICIO
                servicio.Url = ambiente;
                PeticionFactura pet = new PeticionFactura(); //Creamos una peticion
                Factura fact = new Factura();
                InfoTributaria infotrib = new InfoTributaria();
               
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

                InfoFactura infoFactura = new InfoFactura();
                fact.InfoFactura = infoFactura;
                infoFactura.FechaEmision = "01/01/2000";
                infoFactura.DirEstablecimiento = "dirEstablecimiento0";
                infoFactura.ContribuyenteEspecial = "Contribuyente especial";
                infoFactura.ObligadoContabilidad = "SI";


//************************CREAMOS OTROS RUBREOS TERCEROS*****************************
                Rubro rubro = new Rubro();   // se deben instanciar
                fact.OtrosRubrosTerceros = new List<Rubro>().ToArray();
                rubro.Total = "total1";

                for(int i = 0; i <3; i++)
                {
                    rubro.Concepto = "concepto"+i.ToString();
                    fact.OtrosRubrosTerceros.ToList().Add(rubro);

                }


                InfoCompRetencion inforet = new InfoCompRetencion();
                ImpuestoDetalleRetencion imp = new ImpuestoDetalleRetencion();

                //**********************************INFOADICIONAL**************************************

                infoAdicional info1 = new infoAdicional();
                infoAdicional info2 = new infoAdicional();
                info1.Nombre = "Inofrmacion";
                info1.Valor = "1234567";
                info2 = info1;

                fact.CampoAdicional = new List<infoAdicional>().ToArray();
                fact.CampoAdicional.ToList().Add(info1);
                fact.CampoAdicional.ToList().Add(info2);


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
                detalle2 = detalle1;
                detalle1.DetAdicional = new List<DetAdicional>().ToArray();
                var detAd1 = new DetAdicional();
                var detAd2 = new DetAdicional();
                fact.Detalles = new List<Detalle>().ToArray();
                fact.Detalles.ToList().Add(detalle1);
                fact.Detalles.ToList().Add(detalle2);


//***********************************PAGOS*********************************
                Pago pago = new Pago();
                pago.FormaPago = "01";
                pago.Total = "50.00";
                pago.Plazo = "3";
                pago.UnidadTiempo = "Dias";

                fact.InfoFactura.Pagos = new List<Pago>().ToArray();
                fact.InfoFactura.Pagos.ToList().Add(pago);
                fact.InfoFactura.Pagos.ToList().Add(pago);



//*****************************TOTAL CON IMPUESTOS**********************


                TotalConImpuesto totalConImpuesto = new TotalConImpuesto();
                totalConImpuesto.BaseImponible = "100.00";
                totalConImpuesto.Codigo = "01";
                totalConImpuesto.CodigoPorcentaje = "02";
                totalConImpuesto.DescuentoAdicional = "0.00";
                totalConImpuesto.Tarifa = "0.00";
                totalConImpuesto.Valor = "100.00";

                fact.InfoFactura.TotalConImpuestos = new List<TotalConImpuesto>().ToArray();
                fact.InfoFactura.TotalConImpuestos.ToList().Add(totalConImpuesto);



                //**********************************MAQUINAL FSICAL*******************
                maquinaFiscal maquina = new maquinaFiscal();

                maquina.marca = "marcaaaaaa";
                maquina.modelo = "modelazo";
                maquina.serie = "ASDFGHJ1234";

                fact.Maquinafiscal = new maquinaFiscal();
                fact.Maquinafiscal = maquina;



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
            serviceobj.Service servicio = new serviceobj.Service();
            servicio.Url = "https://demointws.thefactoryhka.com.ec/Service.svc";
            PeticionDescargaArchivo pet = new PeticionDescargaArchivo();
            pet.Clave = "CLAVE123456";
            pet.Documento = "01-001-002-000444222";
            pet.RUC = "1345623808765";
            pet.Extension = "Pdf";
            pet.Usuario = "ususario1";
            RepuestaDescargaArchivo resp = new RepuestaDescargaArchivo();
            resp = servicio.DescargaArchivo(pet); 
            richTextBox1.Text = "La respuesta es: \r\n" + "procesado: " + resp.Procesado.ToString()+"\r\n"+ "Mensaje: " + resp.Mensaje + "\r\n" +"UUID: "+"falta"+"\r\n" + "Codigo" + resp.Codigo + "\r\n" + "archivo: "+"\r\n" + resp.Archivo;
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            


        }

        private void button5_Click(object sender, EventArgs e)

        {

           

        }
    }
}
