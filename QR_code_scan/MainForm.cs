using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
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

        private void StartStop_button_Click(object sender, EventArgs e)
        {

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

            // send image from camera in ZXing class, and decode them
            Result result = reader.Decode((Bitmap)eventArgs.Frame.Clone());
            if (result != null && result.Text != TempQrDecode)
            {
                Invoke((MethodInvoker)delegate
                      {
                          QrDecode_textBox.AppendText("\r\n" + result.Text);
                      });
                TempQrDecode = result.Text;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(videoSource != null)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }
    }
}
