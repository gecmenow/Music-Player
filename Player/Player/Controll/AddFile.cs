using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Player.Storage;

namespace Player.Controll
{
    class AddFile
    {
        public void Add(FileStorage file)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            // result = fdlg.ShowDialog();
            fdlg.Title = "Choose audio you want to play.";
            fdlg.InitialDirectory = @"E:\My downloads\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = false;
            //fdlg.Multiselect = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                file.setAudioList.Add(fdlg.FileName);

                file.setSongList.Enqueue(fdlg.FileName);

                var _priceDataArray = from row in file.setAudioList select new { Songs = row.ToString() };

                string lastWord = file.setAudioList.Last().ToString();
                string songName = Path.GetFileNameWithoutExtension(lastWord);
                MessageBox.Show(songName);

                Form1._Form1.MainDataGridView.DataSource = songName;
            }
        }

        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strEnd, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
