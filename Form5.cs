using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
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
                ServiceClient servicio = new ServiceClient(); //CREAMOS SERVICIO
                PeticionFactura pet = new PeticionFactura(); //Creamos una peticion
                Factura fact = new Factura(); // Creamos una factura





 //******************************INFOTRIBUTARIA*******************************************                                                 
                InfoTributaria infotrib = new InfoTributaria();
                fact.InfoTributaria = infotrib;
                pet.Documento = fact;
                fact.InfoTributaria.NombreComercial = textBox11.Text;
                fact.InfoTributaria.AgenteRetencion = textBox12.Text;
                fact.InfoTributaria.RazonSocial = textBox13.Text;
                infotrib.RegimenMicroempresas = textBox14.Text;
                fact.CorreoNotificar = "scastillo@gmail.com";
                fact.InfoTributaria.CodigoNumerico = textBox15.Text;
                fact.InfoTributaria.Estab = textBox16.Text;
                fact.InfoTributaria.PtoEmi = textBox17.Text;
                fact.InfoTributaria.Secuencial = textBox18.Text;
                fact.InfoTributaria.DirMatriz = textBox19.Text;
                fact.Version = textBox20.Text;
 
                
                




                //**********************************INFOFACTURA*********************************************
                InfoFactura infoFactura = new InfoFactura();
                fact.InfoFactura = infoFactura;              
                infoFactura.FechaEmision = textBox1.Text;
                infoFactura.DirEstablecimiento = textBox2.Text;
                infoFactura.ContribuyenteEspecial = textBox3.Text;
                infoFactura.ObligadoContabilidad = textBox4.Text;
                infoFactura.TipoIdentificacionComprador = textBox5.Text;
                infoFactura.RazonSocialComprador = textBox6.Text;
                infoFactura.IdentificacionComprador = textBox7.Text;
                infoFactura.DireccionComprador = textBox8.Text;
                infoFactura.TotalSinImpuestos = textBox9.Text;
                infoFactura.ImporteTotal = textBox10.Text;
                infoFactura.ComercioExterior = textBox21.Text;
                infoFactura.IncoTermFactura = "A";
                infoFactura.LugarIncoTerm = textBox22.Text;
                infoFactura.PaisOrigen = textBox23.Text;
                infoFactura.PuertoEmbarque = textBox24.Text;
                infoFactura.PuertoDestino = textBox25.Text;
                infoFactura.PaisDestino = textBox26.Text;
                infoFactura.PaisAdquisicion = textBox27.Text;
                infoFactura.GuiaRemision = textBox25.Text;
                infoFactura.TotalSubsidio = textBox29.Text;
                infoFactura.IncoTermTotalSinImpuestos = textBox30.Text;
                infoFactura.TotalDescuento = textBox31.Text;
                infoFactura.CodDocReembolso = textBox32.Text;
                infoFactura.TotalBaseImponibleReembolso = textBox33.Text;
                infoFactura.TotalImpuestoReembolso = textBox34.Text;









                infoFactura.TotalConImpuestos = new List<TotalConImpuesto>();
                TotalConImpuesto totc = new TotalConImpuesto()
                {
                    Codigo = "2",
                    BaseImponible = "100.00",
                    DescuentoAdicional = "0.00",
                    CodigoPorcentaje = "2",
                    Valor = "100",
                    Tarifa = "0.00",
                    ValorDevolucionIva = "0.00"


                };
                

 /*               infoFactura.Compensaciones = new List<Compensacion>();
                Compensacion comp = new Compensacion()
                {
                    Codigo = "12",
                    Tarifa = "0.00",
                    Valor = "0.00",

                };
                infoFactura.Compensaciones.Add(comp);

    */
                infoFactura.TotalConImpuestos.Add(totc);

                infoFactura.TotalDescuento = "0.00";


                infoFactura.TotalSubsidio = "0.00";


 //************************CREAMOS OTROS RUBREOS TERCEROS*****************************

                fact.OtrosRubrosTerceros = new List<Rubro>();
                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {
                    Rubro rub = new Rubro();
                    rub.Concepto = dataGridView3.Rows[i].Cells[0].Value.ToString();
                    rub.Total = dataGridView3.Rows[i].Cells[1].Value.ToString();
                    fact.OtrosRubrosTerceros.Add(rub);

                }





//**********************************INFOADICIONAL**************************************
                
                fact.CampoAdicional = new List<infoAdicional>();
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    infoAdicional info = new infoAdicional(); //se debe instanciar y rellenar cada item de la lista
                    info.Nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();  // aregamos el nombre de la celda
                    info.Valor = dataGridView1.Rows[i].Cells[1].Value.ToString();  //agregamos el valor de la celda
                    fact.CampoAdicional.Add(info);  //introducimos el info en la lista Campo adicional
                }



                //*****************************DETALLES***********************************************

               


                    fact.Detalles = new List<DetalleFactura>();
                    DetalleFactura det = new DetalleFactura();
                    /* det.CodigoPrincipal = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    det.CodigoAuxiliar= dataGridView2.Rows[i].Cells[1].Value.ToString();
                    det.Descripcion= dataGridView2.Rows[i].Cells[2].Value.ToString();
                    det.Cantidad= dataGridView2.Rows[i].Cells[3].Value.ToString();
                    det.PrecioUnitario= dataGridView2.Rows[i].Cells[4].Value.ToString();
                    det.UnidadMedida= dataGridView2.Rows[i].Cells[5].Value.ToString();
                    det.Descuento= dataGridView2.Rows[i].Cells[6].Value.ToString();
                    det.PrecioSinSubsidio = dataGridView2.Rows[i].Cells[7].Value.ToString();
                    det.PrecioTotalSinImpuesto= dataGridView2.Rows[i].Cells[8].Value.ToString(); */







                    det.CodigoPrincipal = "001";
                    det.CodigoAuxiliar = "002";
                    det.Descripcion = "Descrip";
                    det.Cantidad = "1";
                    det.PrecioUnitario = "100.00";
                    det.UnidadMedida = "UND";
                    det.Descuento = "0.00";
                    det.PrecioSinSubsidio = "0.00";
                    det.PrecioTotalSinImpuesto = "100.00";



                    det.Impuestos = new List<ImpuestoDetalle>();
                    ImpuestoDetalle imp = new ImpuestoDetalle()
                    {
                        Codigo = "2",
                        BaseImponible = "100",
                        CodigoPorRet = "0",
                        Tarifa = "0",
                        Valor = "100"

                    };

                    det.Impuestos.Add(imp);




                    det.DetAdicional = new List<DetAdicional>();
                    DetAdicional detAd = new DetAdicional
                    {
                        Nombre = "Nombre1",
                        Valor = "12345678"
                    };
                    det.DetAdicional.Add(detAd);

                    fact.Detalles.Add(det);






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


                TotalConImpuesto totalConImpuesto = new TotalConImpuesto
                {
                    BaseImponible = "100.00",
                    Codigo = "2",
                    CodigoPorcentaje = "2",
                    DescuentoAdicional = "0.00",
                    Tarifa = "0.00",
                    Valor = "100.00",ValorDevolucionIva="0"
                };

                fact.InfoFactura.TotalConImpuestos = new List<TotalConImpuesto>();
                fact.InfoFactura.TotalConImpuestos.Add(totalConImpuesto);



//**********************************MAQUINAL FSICAL*******************
                MaquinaFiscal maquina = new MaquinaFiscal();

                maquina.marca = "marcaaaaaa";
                maquina.modelo = "modelazo";
                maquina.serie = "ASDFGHJ1234";

                fact.MaquinaFiscal = new MaquinaFiscal();
                fact.MaquinaFiscal = maquina;




//**********************************ENVIO PETICION***************************
                pet.Clave = "12345";
                pet.RUC = "1792433738001";
                pet.Usuario = "usuario1";
                var resp = servicio.EnviarFactura(pet);
                servicio.Close();

                richTextBox1.Text=resp.Mensaje + "--" + resp.Codigo + "--" + "\r\n" + resp.Archivo;


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resp.Archivo);
                xmlDoc.Save("C:\\XML\\factura_1.xml");

                servicio.Close();

            }


            catch (Exception ex)
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

        private void button6_Click(object sender, EventArgs e)
        {


            try {

                ServiceClient servicio = new ServiceClient();
                PeticionNotaCredito pet = new PeticionNotaCredito();

                pet.Clave = "iuytrew";
                pet.RUC = "1792433738001";
                pet.Usuario = "ususario1";
                pet.Documento = new NotaCredito();
                NotaCredito not = new NotaCredito();


//******************************INFOTRIBUTARIA*********************************
                not.InfoTributaria = new InfoTributaria();
                not.InfoTributaria.NombreComercial = textBox11.Text;
                not.InfoTributaria.AgenteRetencion = textBox12.Text;
                not.InfoTributaria.RazonSocial = textBox13.Text;
                not.CorreoNotificar = "scastillo@gmail.com";
                not.InfoTributaria.CodigoNumerico = textBox15.Text;
                not.InfoTributaria.Estab = textBox16.Text;
                not.InfoTributaria.PtoEmi = textBox17.Text;
                not.InfoTributaria.Secuencial = textBox18.Text;
                not.InfoTributaria.DirMatriz = textBox19.Text;
                not.InfoTributaria.RegimenMicroempresas = textBox14.Text;






                //*******************************INFONOTACREDITO*********************************
                not.InfoNotaCredito = new InfoNotaCredito();
                InfoNotaCredito infonot = new InfoNotaCredito
                {
                    CodDocModificado = "04",
                    ContribuyenteEspecial = "Contrubuyente",
                    DirEstablecimiento = "Direstablecicmiento lejos..",
                    FechaEmision = "03/05/2021",
                    IdentificacionComprador = "14979665",
                    Motivo = "Cambio zapatos",
                    Moneda = "USD",
                    ObligadoContabilidad = "SI",
                    Rise = "rise",
                    ValorModificacion = "10.00",
                    FechaEmisionDocSustento = "04/05/2021",
                    NumDocModificado = "001-002-000888113",
                    RazonSocialComprador = "samcastle c.a",
                    TotalSinImpuestos = "100.00",
                    TipoIdentificacionComprador = "04",
                    


            };

                ImpuestoTotalNotaCredito totalConImpuesto = new ImpuestoTotalNotaCredito
                {
                    BaseImponible = "100",
                    Codigo="2",
                    CodigoPorcentaje="2",
                    Valor="1.20",
                    ValorDevolucionIva="0.00",
                };

                infonot.TotalConImpuestos = new List<ImpuestoTotalNotaCredito>();
                infonot.TotalConImpuestos.Add(totalConImpuesto);

                not.InfoNotaCredito = infonot;





//**********************************************DETALLES*********************************
                not.Detalles = new List<DetalleNotaCredito>();

                DetalleNotaCredito det = new DetalleNotaCredito
                {
                    Cantidad = "1",
                    CodigoAdicional = "01",
                    CodigoInterno = "003",
                    Descripcion = "Descridico",
                    Descuento="0.00",
                    PrecioTotalSinImpuesto="100.00",
                    PrecioUnitario="100.00",
                    
                };

                det.DetAdicional = new List<DetAdicional>();
                DetAdicional detAd = new DetAdicional();
                detAd.Nombre = "NOmbredetadicioanl";
                detAd.Valor = "valordetadicional";
                det.DetAdicional.Add(detAd);

                det.Impuestos = new List<ImpuestoDetalle>();
                ImpuestoDetalle imp = new ImpuestoDetalle()
                {
                    Codigo = "2",
                    BaseImponible = "100",
                    CodigoPorRet = "0",
                    Tarifa = "0",
                    Valor = "100"

                };

                det.Impuestos.Add(imp);


                not.Detalles.Add(det);



                not.Notificar = "SI";
                not.Version = "1.1.0";

                pet.Documento = not;



                var resp = servicio.EnviarNotaCredito(pet);

                
                servicio.Close();

                richTextBox1.Text = resp.Mensaje + "--" + resp.Codigo + "--" + "\r\n" + resp.Archivo;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resp.Archivo);
                xmlDoc.Save("C:\\XML\\credito_1.xml");





            }

            catch(Exception a)
            {

                MessageBox.Show("mensaje: "+a);


            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try { 

                ServiceClient servicio = new ServiceClient();
                PeticionNotaDebito pet = new PeticionNotaDebito();
                pet.Clave = "iuytrew";
                pet.RUC = "1792433738001";
                pet.Usuario = "ususario1";               
                pet.Documento = new NotaDebito();
                NotaDebito deb = new NotaDebito();
                pet.Documento = deb;

//****************************************INFO TRIBUTARIA***********************
                deb.InfoTributaria = new InfoTributaria
                {
                 NombreComercial = textBox11.Text,
                AgenteRetencion = textBox12.Text,
                RazonSocial = textBox13.Text,
               CodigoNumerico = textBox15.Text,
                Estab = textBox16.Text,
                PtoEmi = textBox17.Text,
                Secuencial = textBox18.Text,
                DirMatriz = textBox19.Text,
                RegimenMicroempresas= textBox14.Text




            };
                deb.Notificar = "SI";



                deb.Motivos = new List<Motivo>();
                Motivo mot = new Motivo()
                {
                    Razon="raxoness",
                    Valor="123456"
                };

                deb.Motivos.Add(mot);





//************************************INFO NOTA DEBITO***************************
                deb.InfoNotaDebito = new InfoNotaDebito();
                InfoNotaDebito info = new InfoNotaDebito
                {
                    CodDocModificado = "04",
                    ContribuyenteEspecial="contribuyente",
                    DirEstablecimiento="sabana Grande",
                    FechaEmision="01/05/2021",
                    FechaEmisionDocSustento="04/04/2021",
                    IdentificacionComprador="149796665",
                    NumDocModificado="001-002-999777222",
                    ObligadoContabilidad="SI",
                    RazonSocialComprador="razon socials comprador",
                    Rise="RISE",
                    TipoIdentificacionComprador="05",
                    TotalSinImpuestos="100.00",
                    ValorTotal="100"
                    
                    

                };
                
                



                info.Impuestos = new List<ImpuestoTotalNotaDebito>();
                ImpuestoTotalNotaDebito imp = new ImpuestoTotalNotaDebito()
                
                {
                    BaseImponible="100.00",
                    Codigo="2",
                    CodigoPorcentaje="0",
                    Tarifa="0.00",
                    Valor="100.00",
                    ValorDevolucionIva="0.00",
                    

                };
                info.Impuestos.Add(imp);

                info.Pagos = new List<Pago>();
                Pago pago = new Pago()
                {
                    FormaPago="01",
                    Plazo="05",
                    UnidadTiempo="Dias",
                    Total="10.00"
                };
                info.Pagos.Add(pago);

                deb.Version = "1.0.0";

                deb.InfoNotaDebito = info;



                var resp = servicio.EnviarNotaDebito(pet);

                servicio.Close();

                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resp.Archivo);
                xmlDoc.Save("C:\\XML\\Debito_1.xml");







            }
            catch(Exception q)
            {
                MessageBox.Show("mensaje: " + q);
            }





        }

        private void button8_Click(object sender, EventArgs e)
        {
            try {

                ServiceClient servicio = new ServiceClient();
                PeticionRetencion pet = new PeticionRetencion()
                {
                    Clave = "iuytrew",
                    RUC = "1792433738001",
                    Usuario = "ususario1"
                };

                pet.Documento = new ComprobanteRetencion();

                ComprobanteRetencion ret = new ComprobanteRetencion();


//******************************INFOTRIBUTARIA*********************************
                ret.InfoTributaria = new InfoTributaria();
                ret.InfoTributaria.NombreComercial = textBox11.Text;
                ret.InfoTributaria.AgenteRetencion = textBox12.Text;
                ret.InfoTributaria.RazonSocial = textBox13.Text;
                ret.CorreoNotificar = "scastillo@gmail.com";
                ret.InfoTributaria.CodigoNumerico = textBox15.Text;
                ret.InfoTributaria.Estab = textBox16.Text;
                ret.InfoTributaria.PtoEmi = textBox17.Text;
                ret.InfoTributaria.Secuencial = textBox18.Text;
                ret.InfoTributaria.DirMatriz = textBox19.Text;
                ret.InfoTributaria.RegimenMicroempresas = textBox14.Text;




 //**************************************INFO RETENCION*********************************


                ret.InfoRetencion = new InfoCompRetencion()
                {

                    ContribuyenteEspecial = "contrubuyebte",
                    DirEstablecimiento = "direstableciemie",
                    FechaEmision = "05/05/2021",
                    IdentificacionSujetoRetenido = "14979665",
                    ObligadoContabilidad = "SI",
                    PeriodoFiscal = "05/2021",
                    RazonSocialSujetoRetenido = "razon social sujeto detenido",
                    TipoIdentificacionSujetoRetenido = "04"
                };

                ret.Impuestos = new List<ImpuestoDetalleRetencion>();
                ImpuestoDetalleRetencion imp = new ImpuestoDetalleRetencion
                {
                    BaseImponible = "100.00",
                    CodDocSustento="01",
                    Codigo="2",
                    CodigoPorRet="2",
                    FechaEmisionDocSustento="05/04/2021",
                    NumDocSustento="001002333444555",
                    PorcentajeRetener="12",
                    Valor="100.00"
                };

                ret.Impuestos.Add(imp);

                ret.Version = "1.0.0";


                pet.Documento = ret;

                var resp = servicio.EnviarRetencion(pet);

                servicio.Close();

                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;



                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resp.Archivo);
                xmlDoc.Save("C:\\XML\\retencion_1.xml");





            }
            catch(Exception a)
            {

                MessageBox.Show("Mensaje: " + a);


            }











        }

        private void button9_Click(object sender, EventArgs e)
        {



            try
            {
                ServiceClient servicio = new ServiceClient();
                PeticionGuiaRemision pet = new PeticionGuiaRemision()
                {
                    Clave = "iuytrew",
                    RUC = "1792433738001",
                    Usuario = "ususario1"
                };

                pet.Documento = new GuiaRemision();
                


//********************************************INFOREMISION**********************************
                GuiaRemision guia = new GuiaRemision();
                guia.InfoGuia = new InfoGuiaRemision();

                InfoGuiaRemision info = new InfoGuiaRemision
                {
                    ContribuyenteEspecial = "Contribuyente",
                    DirEstablecimiento="dir establecimineto",
                    DirPartida="direccion de partida",
                    FechaIniTransporte="01/06/2021",
                    FechaFinTransporte="02/06/2021",
                    ObligadoContabilidad="SI",
                    Placa="ADV345",
                    RazonSocialTransportista="eazon social transportista",
                    Rise="RISE",
                    RucTransportista="1789762345678",
                    TipoIdentificacionTransportista="04",
                    

                };

                guia.InfoGuia = info;


//********************************************INFOTRIBUTARIA*****************************
                guia.InfoTributaria = new InfoTributaria();
                InfoTributaria trib = new InfoTributaria
                {
                NombreComercial = textBox11.Text,
                AgenteRetencion = textBox12.Text,
                RazonSocial = textBox13.Text,            
                CodigoNumerico = textBox15.Text,
                Estab = textBox16.Text,
                PtoEmi = textBox17.Text,
                Secuencial = textBox18.Text,
                DirMatriz = textBox19.Text,
                RegimenMicroempresas = textBox14.Text

            };
                guia.InfoTributaria = trib;




                guia.Destinatarios = new List<Destinatario>();
                Destinatario dest = new Destinatario
                {
                    CodDocSustento = "04",
                    CodEstabDestino="002",
                    DirDestinatario="dierccion destin¿tatario",                  
                    FechaEmisionDocSustento="01/05/2021",
                    IdentificacionDestinatario="1789087654321",
                    MotivoTraslado="raparacion motivo",
                    NumAutDocSustento="000999888777666555444333222111",
                    NumDocSustento="001-002-000000123",
                    RazonSocialDestinatario="razon social destinatario",
                    Ruta="ruta:___",
                    DocAduaneroUnico="0099887"
                   
                };

                dest.Detalles = new List<DetalleGuia>();
                DetalleGuia detg = new DetalleGuia
                {
                    Cantidad = "1",
                    CodigoAdicional="002",
                    CodigoInterno="009876",
                    Descripcion="descripcion del detalle",
                    
                };

                detg.DetallesAdicionales = new List<DetAdicional>();
                DetAdicional d = new DetAdicional();
                d.Nombre = "nombre detadicional";
                d.Valor = "9876";

                detg.DetallesAdicionales.Add(d);
                dest.Detalles.Add(detg);
                guia.Destinatarios.Add(dest);



                guia.Notificar = "SI";
                guia.Version = "1.0.0";
                guia.CorreoNotificar = "scastillo.com";

                pet.Documento = guia;


                var resp = servicio.EnviarGuiaRemision(pet);
                servicio.Close();
                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resp.Archivo);
                xmlDoc.Save("C:\\XML\\Remision_1.xml");







            }
            catch(Exception a)
            {
                MessageBox.Show("Mensaje error: " + a);
            }





        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceClient servicio = new ServiceClient();
                PeticionLiquidacion pet = new PeticionLiquidacion
                {
                    Clave = "iuytrew",
                    RUC = "1792433738001",
                    Usuario = "ususario1"
                };

                pet.Documento = new Liquidacion();
                Liquidacion liq = new Liquidacion();



                //***************************************INFOTRIBUTARIA************************************
                liq.InfoTributaria = new InfoTributaria();
                liq.InfoTributaria.NombreComercial = textBox11.Text;
                liq.InfoTributaria.AgenteRetencion = textBox12.Text;
                liq.InfoTributaria.RazonSocial = textBox13.Text;
                liq.CorreoNotificar = "scastillo@gmail.com";
                liq.InfoTributaria.CodigoNumerico = textBox15.Text;
                liq.InfoTributaria.Estab = textBox16.Text;
                liq.InfoTributaria.PtoEmi = textBox17.Text;
                liq.InfoTributaria.Secuencial = textBox18.Text;
                liq.InfoTributaria.DirMatriz = textBox19.Text;
                liq.InfoTributaria.RegimenMicroempresas = textBox14.Text;
                liq.Version = "1.1.0";




//*********************************************INFOLIQUIDACION*****************************
                liq.InfoLiquidacion = new InfoLiquidacionCompra();
                InfoLiquidacionCompra info = new InfoLiquidacionCompra
                {
                    CodDocReembolso = "04",
                    ContribuyenteEspecial="contribuyente especial",
                    DireccionProveedor="direccion proveedor",
                    DirEstablecimiento="dir establecimineto",
                    FechaEmision="07/06/2021",
                    IdentificacionProveedor="14098765",
                    TipoIdentificacionProveedor="05",
                    RazonSocialProveedor="Razon social proveeedor",
                    ObligadoContabilidad="SI",
                    Moneda="USD",
                    ImporteTotal="100.00",
                    TotalSinImpuestos="100.00",
                    TotalBaseImponibleReembolso="10.00",
                    TotalComprobantesReembolso="10.00",
                    TotalDescuento="0.00",
                    TotalImpuestoReembolso="0.00"
                    
                };

                info.Pagos = new List<Pago>();
                Pago pago = new Pago();
                pago.FormaPago = "01";
                pago.Plazo = "5";
                pago.UnidadTiempo = "Dias";
                pago.Total = "110.00";
                info.Pagos.Add(pago);
                

                info.TotalConImpuestos = new List<ImpuestoTotalLiquidacion>();
                ImpuestoTotalLiquidacion imp = new ImpuestoTotalLiquidacion
                {
                    Codigo = "2",
                    BaseImponible="10.00",
                    CodigoPorcentaje="2",
                    DescuentoAdicional="0.00",
                    Tarifa="0.00",
                    Valor="10.00"
                };

                info.TotalConImpuestos.Add(imp);
                liq.InfoLiquidacion = info;


//******************************************+DETALLES*************************
                liq.Detalles = new List<Detalle>();
                Detalle det = new Detalle
                {
                    Cantidad = "1",
                    CodigoAuxiliar="001",
                    CodigoPrincipal="002",
                    Descripcion="descrpcionnn",
                    Descuento="0.00",
                    PrecioSinSubsidio="100.00",
                    PrecioTotalSinImpuesto="100.00",
                    PrecioUnitario="100.00",
                    UnidadMedida="UND"
                    
                };

                det.Impuestos = new List<ImpuestoDetalle>();
                ImpuestoDetalle impdet = new ImpuestoDetalle
                {
                    Codigo = "2",
                    BaseImponible="100",
                    CodigoPorRet="2",
                    Tarifa="0.00",
                    Valor="100.00"
                };
                det.Impuestos.Add(impdet);
                liq.Detalles.Add(det);



                liq.CorreoNotificar = "SI";
                liq.CampoAdicional = new List<infoAdicional>();
                infoAdicional inf = new infoAdicional
                {
                    Nombre = "nombre adicional",
                    Valor="1876543"
                };
                liq.CampoAdicional.Add(inf);

                liq.Reembolsos = new List<ReembolsoDetalle>();
                ReembolsoDetalle rem = new ReembolsoDetalle
                {
                    CodDocReembolso = "04",
                    CodPaisPagoProveedorReembolso="345",
                    EstabDocReembolso="001",
                    PtoEmiDocReembolso="002",
                    SecuencialDocReembolso="000000887",
                    FechaEmisionDocReembolso="05/06/2021",
                    IdentificacionProveedorReembolso="14987654",
                    TipoIdentificacionProveedorReembolso="05",
                    NumeroautorizacionDocReemb="87654323456987654",
                    TipoProveedorReembolso="02"
                   
                };

                rem.DetalleImpuestos = new List<ImpuestoDetalle>();
                ImpuestoDetalle impdetliq = new ImpuestoDetalle()
                {
                    Codigo = "2",
                    BaseImponible = "100",
                    CodigoPorRet = "0",
                    Tarifa = "0",
                    Valor = "100"

                };
                rem.DetalleImpuestos.Add(impdetliq);
                liq.Reembolsos.Add(rem);

                pet.Documento = liq;

                var resp = servicio.EnviarLiquidacion(pet);
                servicio.Close();

                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(resp.Archivo);
                xmlDoc.Save("C:\\XML\\liquidacion_1.xml");






            }

            catch(Exception a)
            {
                MessageBox.Show("Mensaje error: " + a);


            }




        }

   
    }
}
