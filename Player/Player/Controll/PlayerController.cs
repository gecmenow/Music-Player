using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using Player.Storage;

using NAudio.Wave;

namespace Player.Controll
{
    class PlayerController
    {
        WaveOutEvent _outputDevice;
        AudioFileReader _audioFile;
        // private IWavePlayer _outputDevice;
        FileStorage FileStorage = new FileStorage();
        TrackBar _trackBar;

        Queue<string> playlist = new Queue<string>();

        public void Play(FileStorage filePath, TrackBar trackBar)
        {
            if (filePath.setAudioList.Count == 0)
            { return; }

            filePath.setSongList = new Queue<string>(filePath.setAudioList);

            FileStorage = filePath;

            PlaySong(trackBar);
        }

        public void PlaySong(TrackBar trackBar)
        {
            try
            {
                _trackBar = trackBar;
                playlist = FileStorage.setSongList;

                #region
                //string[] output = new string[Form1._Form1.MainDataGridView.RowCount - 1];
                //for (int i = 1; i < Form1._Form1.MainDataGridView.RowCount; i++)
                //{
                //    for (int j = 0; j < Form1._Form1.MainDataGridView.ColumnCount; j++)
                //    {
                //        output[i - 1] += Form1._Form1.MainDataGridView.Rows[i - 1].Cells[j].Value.ToString() + ";";
                //    }
                //}
                #endregion

                if (playlist == null)
                { return; }

                if (_outputDevice == null)
                {
                    _outputDevice = new WaveOutEvent();
                    _outputDevice.PlaybackStopped += OnPlaybackStopped;
                }

                if (_audioFile == null && playlist.Count != 0)
                {
                    //audioFile = new AudioFileReader(@"E:\My downloads\Brainstorm - Maybe.mp3");
                    _audioFile = new AudioFileReader(playlist.Dequeue());
                    _outputDevice.Init(_audioFile);
                }
                _outputDevice.Volume = _trackBar.Value / 100f;
                _trackBar.Scroll += TrackBar_Scroll;
                Form1._Form1.VolumeBox.KeyPress += TrackBar_Scroll;
                _outputDevice.Play();
                _outputDevice.PlaybackStopped += (s, a) => { PlaySong(_trackBar); };
                //MessageBox.Show("First add some songs");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "PlaySong");
            }
        }

        public void Pause()
        {
            if (FileStorage.setAudioList.Count == 0)
            { return; }
            _outputDevice?.Pause();
            _trackBar.Scroll += TrackBar_Scroll;
            Form1._Form1.VolumeBox.KeyPress += TrackBar_Scroll;
        }

        public void Stop()
        {
            if (FileStorage.setAudioList.Count == 0)
            { return; }
            //_outputDevice?.Stop();
            //_outputDevice?.Pause();
            _outputDevice.Pause();
            _audioFile.Position = 0;
            _trackBar.Scroll += TrackBar_Scroll;
            Form1._Form1.VolumeBox.KeyPress += TrackBar_Scroll;
        }

        public void Close()
        {
            if (_outputDevice != null)
            {
                _outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            Application.Exit();
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            try
            {
                _outputDevice.Dispose();
                _outputDevice = null;
                _audioFile.Dispose();
                _audioFile = null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "OnPlaybackStopped");
            }
        }

        #region Изменение значения в поле громкости, при движении ползунка громоксти
        TrackBar volumeTrackBar = new TrackBar();
        public void ChangeBoxVolume(TrackBar trackBar)
        {
            volumeTrackBar = trackBar;
            Form1._Form1.VolumeBox.Text = volumeTrackBar.Value.ToString();
            volumeTrackBar.Scroll += BoxVolumeChanging;
        }

        void BoxVolumeChanging(object sender, EventArgs e)
        {
            Form1._Form1.VolumeBox.Text = volumeTrackBar.Value.ToString();
        }
        #endregion

        #region Меняет значение на ползунке громкости, при его изменнеие в поле громкости
        public void ChangeVolumeBarValue(TrackBar trackBar)
        {
            //volumeTrackBar = trackBar;
            //volumeTrackBar.Value = Convert.ToInt32(Form1._Form1.VolumeBox.Text);
            Form1._Form1.VolumeBox.KeyPress += VolumeBarValueChanging;
        }

        void VolumeBarValueChanging(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                volumeTrackBar.Value = Convert.ToInt32(Form1._Form1.VolumeBox.Text);
            }
        }
        #endregion

        void TrackBar_Scroll(object sender, EventArgs e)
        {
            _outputDevice.Volume = _trackBar.Value / 100f;
        }

        
    }
}
