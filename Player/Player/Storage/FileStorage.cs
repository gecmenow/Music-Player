using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Storage
{
    class FileStorage
    {
        List<string> audioList = new List<string>();

        public List<string> setAudioList
        {
            get { return audioList; }
            set { audioList = value; }
        }

        Queue<string> SongList = new Queue<string>();

        public Queue<string> setSongList
        {
            get { return SongList; }
            set { SongList = value; }
        }
    }
}
