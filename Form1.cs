using FellowOakDicom;
using FellowOakDicom.Imaging;
using SixLabors.ImageSharp.Formats.Bmp;
using System.Diagnostics;
namespace dicom_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        String dicomFilePath = @"C:\MyCsharpProjects\dicom_app\C9A064F8\05D8C20A\05E39B9A.dcm";
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                //openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                //openFileDialog.FilterIndex = 2;
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    dicomFilePath = openFileDialog.FileName;
                    tbFilePath.Text = dicomFilePath;
                    new DicomSetupBuilder()
                           .RegisterServices(s => s.AddFellowOakDicom().AddImageManager<ImageSharpImageManager>())
                       .Build();

                    var dicomFile = new DicomImage(dicomFilePath);
                    Debug.WriteLine(dicomFile);
                    Debug.WriteLine(dicomFile.NumberOfFrames);
                    Debug.WriteLine("teste");

                    var file = DicomFile.Open(dicomFilePath);
                    var patientid = file.Dataset.GetString(DicomTag.PatientID);
                    var patientName = file.Dataset.GetString(DicomTag.PatientName);
                    labelName.Text = patientName;
                    labelPatientid.Text = patientid;

                    var shartimage = dicomFile.RenderImage().AsSharpImage();
                    // render onto an Image
                    var stream = new System.IO.MemoryStream();
                    shartimage.Save(stream, new BmpEncoder());
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

                    // dispose the old image before displaying the new one
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = img;

                }
            }

           
        }
    }
}