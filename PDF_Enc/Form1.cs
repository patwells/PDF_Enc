using iTextSharp.text.exceptions;
using iTextSharp.text.pdf;

namespace PDF_Enc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            button1.Enabled= false;

            StreamWriter sw = new StreamWriter(Path.Combine(ofd.SelectedPath, "pdf_enc_" + DateTime.Now.ToFileTime() + ".txt"));

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PerFile(ofd.SelectedPath,"*.pdf", sw);
            }
            else
            {
                button1.Enabled = true;
            }
            sw.Close();
            
        }

        public static void PerFile (string path, string ext, StreamWriter sw)
        {
            foreach (var item in Directory.GetFiles(path, ext))
            {
                IsPasswordProtected(item, sw);
            }
        }
         
        public static void IsPasswordProtected (string pdfFullName, StreamWriter sw)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(pdfFullName);
                sw.WriteLine(pdfReader.IsOpenedWithFullPermissions);
            }
            catch (BadPasswordException)
            {
                sw.WriteLine(pdfFullName.ToString()+" -- ERROR: ENC");
            }
        }
    }
}