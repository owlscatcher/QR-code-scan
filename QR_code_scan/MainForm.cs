using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace QR_code_scan
{
    public partial class MainForm : Form
    {
        private FilterInfoCollection videoDevice;
        private VideoCaptureDevice videoSource;
        private BarcodeReader reader;

        public bool StartClickState { get; set; }

        public string TempQrDecode { get; set; }
        private List<string> scans = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // search available webCam
            videoDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevice.Count != 0)
                foreach (FilterInfo device in videoDevice)
                    Device_listBox.Items.Add(device.Name);
            Device_listBox.SelectedIndex = 0;

            // init QR code reader
            reader = new BarcodeReader();
            reader.Options.PossibleFormats = new List<BarcodeFormat>
            {
                BarcodeFormat.QR_CODE
            };

            StartClickState = false;
            TempQrDecode = string.Empty;
        }

        private bool isExit = false;
        private bool isCan;
        private void StartStop_button_Click(object sender, EventArgs e)
        {
            scans.Clear(); //очищаем коллекцию сканов

            //запускаем поток установки флага сканирования
            Task.Factory.StartNew(() =>
            {
                while (!isExit)
                {
                    isCan = true;
                    Thread.Sleep(1000);
                }
            });

            if (!StartClickState)
            {
                // start camera stream
                videoSource = new VideoCaptureDevice(videoDevice[Device_listBox.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(stream);
                videoSource.Start();

                StartStop_button.Text = "Stop scan";
                StartClickState = true;
            }
            else
            {
                //флаг закрытия потока
                isExit = true;
                if (videoSource != null)
                {
                    // stop camera stream
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();

                    StartClickState = false;
                    StartStop_button.Text = "Start scan";
                    Stream_pictureBox.Image = null;
                    QrDecode_textBox.Text = null;
                }
            }
        }

        // Decode event
        private void stream(object sender, NewFrameEventArgs eventArgs)
        {
            // NewFrameEventArgs -- event from AForge.Video class
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            Stream_pictureBox.Image = bitmap;

            //если сканирование производилось уже в течении делея то выходим
            if (!isCan) return;

            // send image from camera in ZXing class, and decode them
            Result result = reader.Decode((Bitmap)eventArgs.Frame.Clone());

            //если в коллекции сканов уже есть такой текст, то выходим
            if (result != null && !scans.Contains(result.Text))
            {
                //установка флага что уже отсканировано
                isCan = false;
                //добавление уникального значения
                scans.Add(result.Text);
                Invoke((MethodInvoker)delegate
                {
                    QrDecode_textBox.AppendText("\r\n" + result.Text);
                });
                TempQrDecode = result.Text;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //флаг остановки потока при закрытии формы
            isExit = true;
            if (videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }
    }
}
