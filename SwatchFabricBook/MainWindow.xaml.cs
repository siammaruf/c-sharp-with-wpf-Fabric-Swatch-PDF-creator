using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SwatchFabricBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            this.itemName.Text = "";
            this.compositionText.Text = "";
            this.constructionText.Text = "";
            this.weightText.Text = "";
            this.commentsText.Text = "";
        }

        private void formMinimiz_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void createSwatch_Click(object sender, RoutedEventArgs e)
        {
            var itemName = this.itemName.Text;
            var composition = this.compositionText.Text;
            var construction = this.constructionText.Text;
            var weight = this.weightText.Text;
            var comments = this.commentsText.Text;
            
            if (itemName != "")
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                var pdfName = itemName.Replace(" ", "_");
                dlg.FileName = pdfName;
                dlg.DefaultExt = ".pdf";
                dlg.Filter = "pdf file (.pdf)|*.pdf";

                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;
                    //string existingFile = "tmp.pdf";
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string existingFile = System.IO.Path.Combine(currentDirectory, "tmp.pdf");
                    var existingFileStream = new FileStream(existingFile, FileMode.Open);


                    var pdfReader = new PdfReader(existingFileStream);
                    var pdfStamper = new PdfStamper(pdfReader, new FileStream(filename, FileMode.Create));

                    AcroFields pdfFormFields = pdfStamper.AcroFields;

                    pdfFormFields.SetField("itemName", itemName);
                    pdfFormFields.SetField("composition", composition);
                    pdfFormFields.SetField("construction", construction);
                    pdfFormFields.SetField("weight", weight);
                    pdfFormFields.SetField("comments", comments);

                    MessageBox.Show("Finished");
                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();


                    //var par = iTextSharp.text.Paragraph.GetInstance(itemName);
                    //Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                    //PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
                    //doc.Open();
                    //doc.Add(par);
                    //doc.Close();
                }
            }
            else
            {
               MessageBox.Show("Please enter Fabrics Name !");
            }
        }
    }
}
