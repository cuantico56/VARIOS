using System;
using System.Collections.Generic;
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



            string subPath = "C:\\XML\\"; // Esta es la ruta de CARPETA donde guardamos los xml 
            string subPath1 = "C:\\XML\\FACTURA\\"; 
            string subPath2 = "C:\\XML\\CREDITO\\";
            string subPath3 = "C:\\XML\\DEBITO\\";
            string subPath4 = "C:\\XML\\RETENCION\\";
            string subPath5 = "C:\\XML\\GUIA\\";
            string subPath6 = "C:\\XML\\LIQUIDACION\\";
            string subPath7 = "C:\\XML\\ATS\\";

            bool exists = System.IO.Directory.Exists(subPath);

            DateTime tiempo = DateTime.Now;
            textBox1.Text = tiempo.Day.ToString().PadLeft(2,'0') + "/" + tiempo.Month.ToString().PadLeft(2, '0') + "/" + tiempo.Year.ToString();
            textBox50.Text= tiempo.Day.ToString().PadLeft(2, '0') + "/" + tiempo.Month.ToString().PadLeft(2, '0') + "/" + tiempo.Year.ToString();
            try
            {

                if (!exists)  //Si la carpeta NO exite la creamos
                {
                    System.IO.Directory.CreateDirectory(subPath);                   
                    System.IO.Directory.CreateDirectory(subPath1);
                    System.IO.Directory.CreateDirectory(subPath2);
                    System.IO.Directory.CreateDirectory(subPath3);
                    System.IO.Directory.CreateDirectory(subPath4);
                    System.IO.Directory.CreateDirectory(subPath5);
                    System.IO.Directory.CreateDirectory(subPath6);
                    System.IO.Directory.CreateDirectory(subPath7);
                    MessageBox.Show("Se ha creado la carpeta C:\\XML y subcarpetas","********CREACION CARPETAS*******",MessageBoxButtons.OK,MessageBoxIcon.Information);



                }
            }
            catch(Exception m){

                MessageBox.Show("Borre la carpeta XML y todo su contenido, vaya a inicio y vuelva a entrar a WS_OBJ \r\n"+m);


            }

            
      




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
                servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());

               



                PeticionFactura pet = new PeticionFactura(); //Creamos una peticion

                
                Factura fact = new Factura(); // Creamos una factura


               


                //******************************INFOTRIBUTARIA*******************************************                                                 
                InfoTributaria infotrib = new InfoTributaria();
                fact.InfoTributaria = InformacionTrib();
                pet.Documento = fact;
               
                fact.Version = textBox20.Text;
                fact.CorreoNotificar = textBox76.Text;
                fact.Notificar = "SI";

                fact.DirCliente = "Direccion del cliente";

                


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
                infoFactura.PuertoDestino = textBox28.Text;
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




                infoFactura.TotalConImpuestos.Add(TotImp());

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
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    infoAdicional info = new infoAdicional(); //se debe instanciar y rellenar cada item de la lista
                    info.Nombre = dataGridView1.Rows[i].Cells[0].Value.ToString();  // aregamos el nombre de la celda
                    info.Valor = dataGridView1.Rows[i].Cells[1].Value.ToString();  //agregamos el valor de la celda
                    fact.CampoAdicional.Add(info);  //introducimos el info en la lista Campo adicional
                }



                //*****************************DETALLES***********************************************




                fact.Detalles = new List<DetalleFactura>();
                DetalleFactura det = new DetalleFactura()
                {

                CodigoPrincipal = textBox67.Text,
                CodigoAuxiliar = textBox68.Text,
                Descripcion = textBox69.Text,
                Cantidad = textBox70.Text,
                PrecioUnitario = textBox71.Text,
                UnidadMedida = textBox72.Text,
                Descuento = textBox73.Text,
                PrecioSinSubsidio = textBox74.Text,
                PrecioTotalSinImpuesto = textBox75.Text


                };







                det.Impuestos = new List<ImpuestoDetalle>();
              

                det.Impuestos.Add(ImpuestDet());
                




                det.DetAdicional = new List<DetAdicional>();
                DetAdicional detAd = new DetAdicional
                {
                    Nombre = "Nombre1",
                    Valor = "12345678"
                };
                det.DetAdicional.Add(detAd);

                fact.Detalles.Add(det);
                fact.Detalles.Add(det);





                //***********************************PAGOS*********************************
                Pago pago = new Pago
                {
                    FormaPago = textBox63.Text,
                    Total = textBox64.Text,
                    Plazo = textBox65.Text,
                    UnidadTiempo = textBox66.Text

                };

                fact.InfoFactura.Pagos = new List<Pago>();
                fact.InfoFactura.Pagos.Add(pago);
                fact.InfoFactura.Pagos.Add(pago);
                



                //*********************TIPO NEGOCIABLE*********************************

                fact.Negociable = new TipoNegociable
                {
                    Correo = textBox76.Text
                };
                                                                                                                   

                //*****************************TOTAL CON IMPUESTOS**********************




                fact.InfoFactura.TotalConImpuestos = new List<TotalConImpuesto>();
                fact.InfoFactura.TotalConImpuestos.Add(TotImp());



                //**********************************MAQUINAL FSICAL*******************
                //MaquinaFiscal maquina = new MaquinaFiscal
                //{
                //    marca =textBox77.Text,
                //    modelo = textBox78.Text,
                //    serie = textBox79.Text
                //};

                //fact.MaquinaFiscal = new MaquinaFiscal();
                //fact.MaquinaFiscal = maquina;




                //**********************************ENVIO PETICION***************************
                pet.Clave = "LE7,S4GW3KSN";
                pet.RUC = "1792433738001";
                pet.Usuario = "1792433738001_INT";
                var resp = servicio.EnviarFactura(pet);
                servicio.Close();

                

                richTextBox1.Text = resp.Mensaje + "--" + resp.Codigo + "--" + "\r\n" + resp.Archivo;


                if (resp.Codigo == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resp.Archivo);
                    xmlDoc.Save("C:\\XML\\FACTURA\\Factura_"+ FormatoFecha()+".xml");
                }

                servicio.Close();

                textBox82.Text = textBox18.Text;
                comboBox1.Text = "01";

            }


            catch (Exception ex)
            {
                MessageBox.Show("ERROR GENERAL: "+ex.ToString());


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {


     
            try { 
            ServiceClient servicio = new ServiceClient();
            servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());



                PeticionDescargaArchivo petarch = new PeticionDescargaArchivo
            {
                RUC = "1792433738001",
                Clave = "dfacture",
                Usuario = "usuario1",
                //Documento = comboBox1.GetItemText(comboBox1.SelectedItem) +"-" + textBox80.Text + "-" + textBox81.Text + "-" + textBox82.Text,
                Documento="01-001-001-000000105",
                Extension="pdf",
                

            };

            var  resp2 = servicio.DescargaArchivo(petarch);


            var base64EncodedBytes = System.Convert.FromBase64String(resp2.Archivo);

            richTextBox1.Text = "Codigo: "+Convert.ToByte(resp2.Codigo) + "\r\n" + "Archivo: " + System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch(Exception d)
            {
                MessageBox.Show("Mensaje: " + d);
                    

            }

           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServiceClient servicio1 = new ServiceClient();
            PeticionFolios folios = new PeticionFolios();
            folios.Usuario = "usuario1";
            folios.RUC = "1792433738001";
            folios.Clave = "dfacture";

            var resp = servicio1.FoliosRestantes(folios);

            richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + "Folios Restantes: " + resp.FoliosRestantes + "\r\n" + "Fecha Vencimiento" + resp.FechaVencimiento;



        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceClient service = new ServiceClient();
                service.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());
                PeticionEstatusDocumento peticion = new PeticionEstatusDocumento();
                peticion.Usuario = "usuario1";
                peticion.RUC = "1792433738001";
                peticion.Clave = "dfacture";
                peticion.Documento = comboBox1.GetItemText(comboBox1.SelectedItem) + "-" + textBox80.Text + "-" + textBox81.Text + "-" + textBox82.Text;
                var resp = service.EstatusDocumento(peticion);
                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Mensaje error: " + ex);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {


            try {

                
                ServiceClient servicio = new ServiceClient();
                servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());
                PeticionNotaCredito pet = new PeticionNotaCredito();

                

                pet.Clave = "dfacture";
                pet.RUC = "1792433738001";
                pet.Usuario = "usuario1";
                pet.Documento = new NotaCredito();
                NotaCredito not = new NotaCredito();


                //******************************INFOTRIBUTARIA*********************************
                not.InfoTributaria = new InfoTributaria();
                not.InfoTributaria = InformacionTrib();
                not.CorreoNotificar = textBox76.Text;

                not.DirCliente = "Direccopn del clinete";



                //*******************************INFONOTACREDITO*********************************
                not.InfoNotaCredito = new InfoNotaCredito();
                InfoNotaCredito infonot = new InfoNotaCredito
                {
                    CodDocModificado = textBox47.Text,
                    ContribuyenteEspecial = textBox48.Text,
                    DirEstablecimiento = textBox49.Text,
                    FechaEmision = textBox50.Text,
                    IdentificacionComprador = textBox51.Text,
                    ObligadoContabilidad = textBox52.Text,
                    Rise = textBox53.Text,
                    FechaEmisionDocSustento = textBox54.Text,
                    NumDocModificado = textBox55.Text,
                    RazonSocialComprador = textBox56.Text,
                    TotalSinImpuestos = textBox57.Text,
                    TipoIdentificacionComprador = textBox58.Text,
                    Motivo = textBox59.Text,
                    Moneda = textBox60.Text,
                    ValorModificacion = textBox61.Text,
                    
                    



                };

                ImpuestoTotalNotaCredito totalConImpuesto = new ImpuestoTotalNotaCredito
                {
                    BaseImponible = textBox35.Text,
                    Codigo = textBox36.Text,
                    CodigoPorcentaje = textBox37.Text,
                    Valor = textBox40.Text,
                    ValorDevolucionIva = "0.00"
                    
                };

                infonot.TotalConImpuestos = new List<ImpuestoTotalNotaCredito>();
                infonot.TotalConImpuestos.Add(totalConImpuesto);

                not.InfoNotaCredito = infonot;





                //**********************************************DETALLES*********************************
                not.Detalles = new List<DetalleNotaCredito>();

                DetalleNotaCredito det = new DetalleNotaCredito
                {
                    CodigoAdicional = textBox67.Text,
                    CodigoInterno = textBox68.Text,
                    Descripcion = textBox69.Text,
                    Cantidad = textBox70.Text,
                    PrecioUnitario = textBox71.Text,              
                    Descuento = textBox73.Text,                
                    PrecioTotalSinImpuesto = textBox75.Text,
                    
                    

                };

                det.DetAdicional = new List<DetAdicional>();
                DetAdicional detAd = new DetAdicional();
                detAd.Nombre = "NOmbredetadicioanl";
                detAd.Valor = "valordetadicional";
                det.DetAdicional.Add(detAd);

                det.Impuestos = new List<ImpuestoDetalle>();
            
                 det.Impuestos.Add(ImpuestDet());


                not.Detalles.Add(det);



                not.Notificar = "SI";
                not.Version = "1.1.0";

                pet.Documento = not;



                var resp = servicio.EnviarNotaCredito(pet);


                servicio.Close();

                richTextBox1.Text = resp.Mensaje + "--" + resp.Codigo + "--" + "\r\n" + resp.Archivo;

              

                if (resp.Codigo == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resp.Archivo);
                    xmlDoc.Save("C:\\XML\\CREDITO\\Credito_"+FormatoFecha()+".xml");
                }


                textBox82.Text = textBox18.Text;
                comboBox1.Text = "04";



            }

            catch (Exception a)
            {

                MessageBox.Show("mensaje: " + a);


            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try {

                ServiceClient servicio = new ServiceClient();
                servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());
                PeticionNotaDebito pet = new PeticionNotaDebito();
                pet.Clave = "dfacture";
                pet.RUC = "1792433738001";
                pet.Usuario = "usuario1";
                pet.Documento = new NotaDebito();
                NotaDebito deb = new NotaDebito();
                pet.Documento = deb;
                deb.DirCliente = "DIRECCION DEL CLIENTE";

                //****************************************INFO TRIBUTARIA***********************
                deb.InfoTributaria = new InfoTributaria();
                deb.InfoTributaria = InformacionTrib();
                deb.Notificar = "SI";



                deb.Motivos = new List<Motivo>();
                Motivo mot = new Motivo()
                {
                    Razon = "raxoness",
                    Valor = "100.00"
                };

                deb.Motivos.Add(mot);





                //************************************INFO NOTA DEBITO***************************
                deb.InfoNotaDebito = new InfoNotaDebito();
                InfoNotaDebito info = new InfoNotaDebito
                {
                    CodDocModificado = textBox47.Text,
                    ContribuyenteEspecial = textBox48.Text,
                    DirEstablecimiento = textBox49.Text,
                    FechaEmision = textBox1.Text,
                    IdentificacionComprador = textBox51.Text,
                    ObligadoContabilidad = textBox52.Text,
                    Rise = textBox53.Text,
                    FechaEmisionDocSustento = textBox54.Text,
                    NumDocModificado = textBox55.Text,
                    RazonSocialComprador = textBox56.Text,
                    TotalSinImpuestos = textBox57.Text,
                    TipoIdentificacionComprador = textBox58.Text,
                    ValorTotal = textBox62.Text,
                    
                    



                };
                
                
                




                info.Impuestos = new List<ImpuestoTotalNotaDebito>();
                ImpuestoTotalNotaDebito imp = new ImpuestoTotalNotaDebito()

                {
                    BaseImponible = textBox35.Text,
                    Codigo = textBox36.Text,
                    CodigoPorcentaje = textBox37.Text,
                    Tarifa = textBox39.Text,
                    Valor = textBox40.Text,
                    ValorDevolucionIva = "0.00",
                    


                };
                info.Impuestos.Add(imp);

                info.Pagos = new List<Pago>();
                Pago pago = new Pago()
                {
                    FormaPago = textBox63.Text,
                    Total = textBox64.Text,
                    Plazo = textBox65.Text,
                    UnidadTiempo = textBox66.Text
                };
                info.Pagos.Add(pago);

                deb.Version = "1.0.0";

                deb.InfoNotaDebito = info;



                var resp = servicio.EnviarNotaDebito(pet);

                servicio.Close();

                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;
                if (resp.Codigo == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resp.Archivo);
                    xmlDoc.Save("C:\\XML\\DEBITO\\Debito_"+ FormatoFecha()+".xml");
                }

                textBox82.Text = textBox18.Text;
                comboBox1.Text = "05";





            }
            catch (Exception q)
            {
                MessageBox.Show("mensaje: " + q);
            }





        }

        private void button8_Click(object sender, EventArgs e)
        {
            try {

                ServiceClient servicio = new ServiceClient();
                servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());
                PeticionRetencion pet = new PeticionRetencion()
                {
                    Clave = "dfacture",
                    RUC = "1792433738001",
                    Usuario = "usuario1"
                };

                pet.Documento = new ComprobanteRetencion();

                ComprobanteRetencion ret = new ComprobanteRetencion();


                //******************************INFOTRIBUTARIA*********************************
                ret.InfoTributaria = new InfoTributaria();
                ret.InfoTributaria = InformacionTrib();
                ret.CorreoNotificar = textBox76.Text;
                ret.DirCliente = "Direccioncliente";
                ret.CPCliente = "CPcliente";
                ret.TelefonoCliente = "234567890";
                


                //**************************************INFO RETENCION*********************************


                ret.InfoRetencion = new InfoCompRetencion()
                {

                    ContribuyenteEspecial = "contrubuyebte",
                    DirEstablecimiento = "direstableciemie",
                    FechaEmision = "05/08/2021",
                    IdentificacionSujetoRetenido = textBox7.Text,
                    ObligadoContabilidad = "SI",
                    PeriodoFiscal = "07/2021",
                    RazonSocialSujetoRetenido = "razon social sujeto detenido",
                    TipoIdentificacionSujetoRetenido = "04"
                    
                    
                };

                ret.Impuestos = new List<ImpuestoDetalleRetencion>();
                ImpuestoDetalleRetencion imp = new ImpuestoDetalleRetencion
                {
                    
                    CodDocSustento = "01",
                    BaseImponible = "100.00",
                    Codigo = "3",
                    CodigoPorRet = "346",                  
                    Valor = "1.75",
                    FechaEmisionDocSustento = "05/07/2021",
                    NumDocSustento = "001002333444555",
                    PorcentajeRetener = "1",
       
                };
                

                ret.Impuestos.Add(imp);
                ret.Impuestos.Add(imp);

                ret.Version = "1.0.0";


                pet.Documento = ret;

                var resp = servicio.EnviarRetencion(pet);

                servicio.Close();

                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;



                if (resp.Codigo == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resp.Archivo);
                    xmlDoc.Save("C:\\XML\\RETENCION\\Retencion_"+ FormatoFecha()+".xml");
                }

                textBox82.Text = textBox18.Text;
                comboBox1.Text = "07";



            }
            catch (Exception a)
            {

                MessageBox.Show("Mensaje: " + a);


            }











        }

        private void button9_Click(object sender, EventArgs e)
        {



            try
            {
                ServiceClient servicio = new ServiceClient();
                servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());
                PeticionGuiaRemision pet = new PeticionGuiaRemision()
                {
                    Clave = "dfacture",
                    RUC = "1792433738001",
                    Usuario = "usuario1"
                };

                pet.Documento = new GuiaRemision();



                //********************************************INFOREMISION**********************************
                GuiaRemision guia = new GuiaRemision();
                guia.InfoGuia = new InfoGuiaRemision();

                InfoGuiaRemision info = new InfoGuiaRemision
                {
                    ContribuyenteEspecial = "Contribuyente",
                    DirEstablecimiento = "dir establecimineto",
                    DirPartida = "direccion de partida",
                    FechaIniTransporte = "10/08/2021",
                    FechaFinTransporte = "11/08/2021",
                    ObligadoContabilidad = "SI",
                    Placa = "ADV345",
                    RazonSocialTransportista = "eazon social transportista",
                    Rise = "RISE",
                    RucTransportista = "1789762345678",
                    TipoIdentificacionTransportista = "04",


                };

                guia.InfoGuia = info;

                guia.DirCliente = "direccion del cleinte";

                //********************************************INFOTRIBUTARIA*****************************
                guia.InfoTributaria = new InfoTributaria();
                guia.InfoTributaria = InformacionTrib();
                
                guia.Destinatarios = new List<Destinatario>();
                Destinatario dest = new Destinatario
                {
                    CodDocSustento = "04",
                    CodEstabDestino = "002",
                    DirDestinatario = "dierccion destin¿tatario",
                    FechaEmisionDocSustento = "01/08/2021",
                    IdentificacionDestinatario = "1789087654321",
                    MotivoTraslado = "raparacion motivo",
                    NumAutDocSustento = "000999888777666555444333222111",
                    NumDocSustento = "001-002-000000123",
                    RazonSocialDestinatario = "razon social destinatario",
                    Ruta = "ruta:___",
                    DocAduaneroUnico = "0099887",
                    

                };
                guia.DirCliente = "direccion del cleinte";
                guia.CPCliente = "CPcliente";
                

                dest.Detalles = new List<DetalleGuia>();
                DetalleGuia detg = new DetalleGuia
                {
                    CodigoAdicional = textBox67.Text,
                   CodigoInterno = textBox68.Text,
                    Descripcion = textBox69.Text,
                    Cantidad = textBox70.Text,
                    
   
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
                guia.CorreoNotificar = textBox76.Text;

                pet.Documento = guia;


                var resp = servicio.EnviarGuiaRemision(pet);
                servicio.Close();
                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;


                if (resp.Codigo == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resp.Archivo);
                    xmlDoc.Save("C:\\XML\\GUIA\\Remision_"+ FormatoFecha()+".xml");
                }







            }
            catch (Exception a)
            {
                MessageBox.Show("Mensaje error: " + a);
            }





        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceClient servicio = new ServiceClient();
                servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());
                PeticionLiquidacion pet = new PeticionLiquidacion
                {
                    Clave = "MDKLCE6VNWN6",
                    RUC = "1792433738001",
                    Usuario = "1792433738001_INT"
                };

                pet.Documento = new Liquidacion();
                Liquidacion liq = new Liquidacion();



                //***************************************INFOTRIBUTARIA************************************
                liq.InfoTributaria = new InfoTributaria();
                liq.InfoTributaria = InformacionTrib();
                liq.Version = "1.1.0";
                liq.CorreoNotificar = textBox76.Text;
                liq.DirCliente = "Direccion del cliente";



                //*********************************************INFOLIQUIDACION*****************************
                liq.InfoLiquidacion = new InfoLiquidacionCompra();
                InfoLiquidacionCompra info = new InfoLiquidacionCompra
                {
                    CodDocReembolso = "04",
                    ContribuyenteEspecial = "CONTRIBUYENTE",
                    DireccionProveedor = "direccion proveedor",
                    DirEstablecimiento = "dir establecimineto",
                    FechaEmision = textBox1.Text,
                    IdentificacionProveedor = "0993264032001",
                    TipoIdentificacionProveedor = "04",
                    RazonSocialProveedor = "INDYA C.A",
                    ObligadoContabilidad = "SI",
                    Moneda = "USD",
                    ImporteTotal = "112.00",
                    TotalSinImpuestos = "100.00",
                    TotalBaseImponibleReembolso = "10.00",
                    TotalComprobantesReembolso = "10.00",
                    TotalDescuento = "0.00",
                    TotalImpuestoReembolso = "0.00"

                };

                info.Pagos = new List<Pago>();
                Pago pago = new Pago
                {
                    FormaPago = textBox63.Text,
                    Total = textBox64.Text,
                    Plazo = textBox65.Text,
                    UnidadTiempo = textBox66.Text
                };
                info.Pagos.Add(pago);


                info.TotalConImpuestos = new List<ImpuestoTotalLiquidacion>();
                ImpuestoTotalLiquidacion imp = new ImpuestoTotalLiquidacion
                {
                    BaseImponible = textBox35.Text,
                    Codigo = textBox36.Text,
                    CodigoPorcentaje = textBox37.Text,
                    Tarifa = textBox39.Text,
                    Valor = textBox40.Text,
                    DescuentoAdicional="0.00"
                };

                info.TotalConImpuestos.Add(imp);
                liq.InfoLiquidacion = info;


                //******************************************+DETALLES*************************
                liq.Detalles = new List<Detalle>();
                Detalle det = new Detalle
                {
                    CodigoPrincipal = textBox67.Text,
                    CodigoAuxiliar = textBox68.Text,
                    Descripcion = textBox69.Text,
                    Cantidad = textBox70.Text,
                    PrecioUnitario = textBox71.Text,
                    UnidadMedida = textBox72.Text,
                    Descuento = textBox73.Text,
                    PrecioSinSubsidio = textBox74.Text,
                    PrecioTotalSinImpuesto = textBox75.Text,
                    

                };

                det.Impuestos = new List<ImpuestoDetalle>();
                det.Impuestos.Add(ImpuestDet());
                liq.Detalles.Add(det);



                liq.CorreoNotificar = textBox76.Text;
                liq.CampoAdicional = new List<infoAdicional>();
                infoAdicional inf = new infoAdicional
                {
                    Nombre = "nombre adicional",
                    Valor = "1876543"
                };
                liq.CampoAdicional.Add(inf);

                liq.Reembolsos = new List<ReembolsoDetalle>();
                ReembolsoDetalle rem = new ReembolsoDetalle
                {
                    CodDocReembolso = "04",
                    CodPaisPagoProveedorReembolso = "345",
                    EstabDocReembolso = "001",
                    PtoEmiDocReembolso = "002",
                    SecuencialDocReembolso = "000000887",
                    FechaEmisionDocReembolso = "05/08/2021",
                    IdentificacionProveedorReembolso = "0993264032001",
                    TipoIdentificacionProveedorReembolso = "04",                                    
                    TipoProveedorReembolso = "01",
                    NumeroautorizacionDocReemb= "1108202101179204823000120010010000047837000070019"

                };

                rem.DetalleImpuestos = new List<ImpuestoDetalle>();

                rem.DetalleImpuestos.Add(ImpuestDet());
                liq.Reembolsos.Add(rem);

                pet.Documento = liq;

                var resp = servicio.EnviarLiquidacion(pet);
                servicio.Close();



                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;

                if (resp.Codigo == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resp.Archivo);
                    xmlDoc.Save("C:\\XML\\LIQUIDACION\\Liquidacion_"+ FormatoFecha()+".xml");
                }


                textBox82.Text = textBox18.Text;
                comboBox1.Text = "03";



            }

            catch (Exception a)
            {
                MessageBox.Show("Mensaje error: " + a);


            }




        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceClient servicio = new ServiceClient();
                servicio.Endpoint.Address = new System.ServiceModel.EndpointAddress(Url());
                PeticionRetencionATS pet = new PeticionRetencionATS
                {
                    Clave = "dfacture",
                    RUC = "1792433738001",
                    Usuario = "usuario1"
                };

                pet.Documento = new RetencionATS();
                RetencionATS ret = new RetencionATS();
                ret.InfoTributaria = new InfoTributaria();
                ret.InfoTributaria = InformacionTrib();  //Usamor el metodo

                ret.infoRetencionATS = new InfoCompRetencionATS();
                InfoCompRetencionATS inforet = new InfoCompRetencionATS
                {
                    ContribuyenteEspecial = "contr",
                    DirEstablecimiento= "DIR ESTABLECIMIENTO",
                    FechaEmision="06/06/2021",
                    IdentificacionSujetoRetenido="05",
                    ObligadoContabilidad="SI",
                    ParteRel="NO",
                    PeriodoFiscal="05/2021",
                    RazonSocialSujetoRetenido="RAZON SOCIALES SUJETO RETENIDO",
                    TipoIdentificacionSujetoRetenido="04",
                    TipoSujetoRetenido="01"
                 
                };
                ret.infoRetencionATS = inforet;

                ret.docsSustento = new List<DocSustento>();
                DocSustento doc = new DocSustento
                {
                    CodDocSustento = "4",
                    CodSustento="04",
                    AplicConvDobTrib="SI",
                    FechaEmisionDocSustento="06/05/2021",
                    FechaRegistroContable="05/05/2021",
                    ImporteTotal="100.00",
                    NumAutDocSustento="876543212345",
                    NumDocSustento="001002000888777",
                    PagExtSujRetNorLeg="SI",
                    PagoLocExt= "01",
                    PagoRegFis="NO",
                    PaisEfecPago="365",
                    TipoRegi="01",
                    TotalBaseImponibleReembolso="100.00",
                    TotalComprobantesReembolso="100.00",
                    TotalImpuestoReembolso="100.00",
                    TotalSinImpuestos="100.00"
                    
                                                      
                };
               

                doc.Pagos = new List<PagoRetencionATS>();
                PagoRetencionATS pago = new PagoRetencionATS
                {
                    FormaPago = textBox63.Text,
                    Total = textBox64.Text,
             
                };
                doc.Pagos.Add(pago);

                doc.ImpuestoDocSustento = new List<ImpuestoDetalle>();
                doc.ImpuestoDocSustento.Add(ImpuestDet());
                ret.docsSustento.Add(doc);

                doc.Retenciones = new List<RetencionesATS>();
                RetencionesATS retATS = new RetencionesATS()
                {
                    
                    BaseImponible = textBox35.Text,
                    Codigo = textBox36.Text,
                    CodigoPorcentaje = textBox37.Text,
                    Tarifa = textBox39.Text,
                    Valor = textBox40.Text,
                    PorcentajeRetener="12",
                    Dividendos = new Dividendo()
                    {
                        EjerFisUtDiv = "2021",
                        FechaPagoDiv = "01/01/2021",
                        ImRentaSoc = "0.00"
                    },

                CompraCajBan=new CompraCajBanano()
                {
                    NumCajBan="34567",
                    PrecCajBan="3456"
                }
               

                };


                doc.Retenciones.Add(retATS);

                ret.Version = "2.0.0";
                ret.MaquinaFiscal = new MaquinaFiscal
                {
                    marca = textBox77.Text,
                    modelo = textBox78.Text,
                    serie = textBox79.Text
                };

                ret.CorreoNotificar = textBox76.Text;


                pet.Documento = ret;


                var resp = servicio.EnviarRetencionATS(pet);

                servicio.Close();

                richTextBox1.Text = "Codigo: " + resp.Codigo + "\n\r" + "Mensaje: " + resp.Mensaje + "\r\n" + resp.Archivo;

                if (resp.Codigo == "0")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(resp.Archivo);
                    xmlDoc.Save("C:\\XML\\ATS\\ATS_"+ FormatoFecha()+".xml");
                }







            }
            catch (Exception a)
            {
                MessageBox.Show("mesnasje error: " + a);
            }










        }

        //*******************************************************************************************
        //*******************************************FUNCIONES COMUNES*******************************
        //*******************************************************************************************



 //*******************************************DEVUELVE INFORMACION TRIBUTARIA*******************
        public InfoTributaria InformacionTrib()
        {
            InfoTributaria ret = new InfoTributaria
            {
                NombreComercial = textBox11.Text,
                AgenteRetencion = textBox12.Text,
                RazonSocial = textBox13.Text,
                CodigoNumerico = textBox15.Text,
                Estab = textBox16.Text,
                PtoEmi = textBox17.Text,
                Secuencial = textBox18.Text,
                DirMatriz = textBox19.Text,
                RegimenMicroempresas = textBox14.Text,
                
                
            };

            return ret;
         }

//***********************************IMPUESTO DETALLE******************************
        public ImpuestoDetalle ImpuestDet()
        {
            ImpuestoDetalle ImpD = new ImpuestoDetalle()
            {
                Codigo = textBox42.Text,
                BaseImponible = textBox43.Text,
                CodigoPorRet = textBox44.Text,
                Tarifa = textBox45.Text,
                Valor = textBox46.Text
            };

            return ImpD;
        }


//***************************************TOTAL CON IMPUESTO********************************************
        public TotalConImpuesto TotImp()
        {
            TotalConImpuesto tot = new TotalConImpuesto
            {
                BaseImponible = textBox35.Text,
                Codigo = textBox36.Text,
                CodigoPorcentaje = textBox37.Text,
                Tarifa = textBox39.Text,
                Valor = textBox40.Text,
                DescuentoAdicional = textBox38.Text,
                ValorDevolucionIva = textBox41.Text

            };
            return tot;

        }


//***************************************PARA FORMATO DEL LOS ARCHIVOS+*******************************
        public string FormatoFecha()
        {

            DateTime tx = DateTime.Now;

            string tiemp = tx.ToLongTimeString() + tx.ToShortDateString();

            tiemp = string.Join("", tiemp.Split(':', '\'', '/'));

            return tiemp;

        }




//*********************************************CON ESTO CAMBIAMOS LA URL DEL MBIENTE*******************
        public string Url()
        {

            string url = "";

            if (radioButton1.Checked)
            {
                url = "http://demointws.thefactoryhka.com.ec/Service.svc";

            }
            else
            {
                url = "http://testintws.thefactoryhka.com.ec/Service.svc";
            }

            return url;


        }




    }
}
