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
                OTEST.ServiceClient servicio = new OTEST.ServiceClient(); //CREAMOS SERVICIO
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
                Rubro rubro = new Rubro();   // se deben instanciar
                fact.OtrosRubrosTerceros = new List<Rubro>();
                rubro.Total = "total1";

                for(int i = 0; i <3; i++)
                {
                    rubro.Concepto = "concepto"+i.ToString();
                    fact.OtrosRubrosTerceros.Add(rubro);

                }

                



//**********************************INFOADICIONAL**************************************

                infoAdicional info1 = new infoAdicional();
                infoAdicional info2 = new infoAdicional();
                info1.Nombre = "Inofrmacion";
                info1.Valor = "1234567";
                info2 = info1;

                fact.CampoAdicional = new List<infoAdicional>();
                fact.CampoAdicional.Add(info1);
                fact.CampoAdicional.Add(info2);


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
                detalle1.DetAdicional = new List<DetAdicional>();
                var detAd1 = new DetAdicional();
                var detAd2 = new DetAdicional();
                fact.Detalles = new List<Detalle>();
                fact.Detalles.Add(detalle1);
                fact.Detalles.Add(detalle2);


//***********************************PAGOS*********************************
                Pago pago = new Pago();
                pago.FormaPago = "01";
                pago.Total = "50.00";
                pago.Plazo = "3";
                pago.UnidadTiempo = "Dias";

                fact.InfoFactura.Pagos = new List<Pago>();
                fact.InfoFactura.Pagos.Add(pago);
                fact.InfoFactura.Pagos.Add(pago);



//*****************************TOTAL CON IMPUESTOS**********************


                TotalConImpuesto totalConImpuesto = new TotalConImpuesto();
                totalConImpuesto.BaseImponible = "100.00";
                totalConImpuesto.Codigo = "01";
                totalConImpuesto.CodigoPorcentaje = "02";
                totalConImpuesto.DescuentoAdicional = "0.00";
                totalConImpuesto.Tarifa = "0.00";
                totalConImpuesto.Valor = "100.00";

                fact.InfoFactura.TotalConImpuestos = new List<TotalConImpuesto>();
                fact.InfoFactura.TotalConImpuestos.Add(totalConImpuesto);



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
            ServiceClient servicio1 = new ServiceClient();
            //****servicio1.DescargaArchivo();
            
            PeticionDescargaArchivo peticionDesc = new PeticionDescargaArchivo();
            peticionDesc.RUC = "1792433738001";
            peticionDesc.Usuario = "usuario1";
            peticionDesc.Clave = "dfacture";
            peticionDesc.Documento = "01-001-002-000000222";
            peticionDesc.Extension = "Xml";

            var resp = servicio1.DescargaArchivo(peticionDesc);

            richTextBox1.Text = "La respuesta es: \r\n" + "procesado: " + resp.Procesado.ToString()+"\r\n"+ "Mensaje: " + resp.Mensaje + "\r\n" +"UUID: "+"falta"+"\r\n" + "Codigo" + resp.Codigo + "\r\n" + "archivo: "+"\r\n" + resp.Archivo;

            



        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServiceClient servicio1 = new ServiceClient();
            PeticionFolios folios = new PeticionFolios();
            folios.Usuario = "usuario1";
            folios.RUC = "1792433738001";
            folios.Clave = "dfacture";

            var resp = servicio1.FoliosRestantes(folios);

            richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + "Folios Restantes: " + resp.FoliosRestantes+"\r\n"+ "Fecha Vencimiento"+resp.FechaVencimiento;



        }

        private void button5_Click(object sender, EventArgs e)
        {
            ServiceClient service = new ServiceClient()
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
