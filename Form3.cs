using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace TEST
{
    public partial class Form3 : Form
    {


        public string Texto;  // Aqui guardaremos los errores y excepciones


        public Form3()
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
            OpenFileDialog xml = new OpenFileDialog();

            xml.InitialDirectory = "C:\\SAMIR\\ECUADOR\\INTEGRACION_ECUADOR\\XSD_ECUADOR";



            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = openFileDialog1.FileName;
                openFileDialog1.Dispose();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog xml2 = new OpenFileDialog();

            xml2.InitialDirectory = "C:\\SAMIR\\ECUADOR\\INTEGRACION_ECUADOR\\XSD_ECUADOR";



            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox2.Text = openFileDialog1.FileName;
                openFileDialog1.Dispose();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var xml = XmlReader.Create(textBox1.Text);
            
            ValidateXmlDocument(xml,textBox2.Text);

            TextoComp.Text = Texto;
            MessageBox.Show("Finalizado!");
                                                  
        }





        public void ValidateXmlDocument(XmlReader documentToValidate, string schemaPath)
        {

            XmlSchema schema;
            using (var schemaReader = XmlReader.Create(schemaPath))
            {
                schema = XmlSchema.Read(schemaReader, ValidationEventHandler);
            }

            var schemas = new XmlSchemaSet();
            schemas.Add(schema);

            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemas;
            settings.ValidationFlags =
                XmlSchemaValidationFlags.ProcessIdentityConstraints |
                XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += ValidationEventHandler;

            try
            {
                using (var validationReader = XmlReader.Create(documentToValidate, settings))
                {
                    while (validationReader.Read())
                    {
                    }
                }
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.Message.ToString());
                Texto = e.Message.ToString();



            }

        }

                               
        private static void ValidationEventHandler(object sender, ValidationEventArgs args)
        {
            {

                RichTextBox TextoComp = new RichTextBox();
                if (args.Severity == XmlSeverityType.Error)
                {
                    throw args.Exception;
                    
                }

                MessageBox.Show(args.Message);
            

             }
         }



    }
}
