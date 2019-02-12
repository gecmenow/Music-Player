using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Player.Storage;
using Player.Controll;


namespace Player
{
    public partial class Form1 : Form
    {
        public static Form1 _Form1;

        public Form1()
        {
            InitializeComponent();
            _Form1 = this;
        }

        FileStorage file = new FileStorage();

        PlayerController play = new PlayerController();
        TrackBar trackBar = new TrackBar();

        void PlayButton_Click(object sender, EventArgs e)
        {
            play.Play(file,trackBar);
        }

        void PauseButton_Click(object sender, EventArgs e)
        {
            play.Pause();
        }

        void StopButton_Click(object sender, EventArgs e)
        {
            play.Stop();
        }

        void AddButton_Click(object sender, EventArgs e)
        {
            var add = new AddFile();
            add.Add(file);
        }

        void CloseButton_Click(object sender, EventArgs e)
        {
            play.Close();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            var volume = new Volume();

            volume.CreateVolumeBar(trackBar);

            play.ChangeBoxVolume(trackBar);
            
            play.ChangeVolumeBarValue(trackBar);

            //LocationChange();
        }

        #region Move borderless Form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);              
            }         
        }
       
        #endregion
    }
}
