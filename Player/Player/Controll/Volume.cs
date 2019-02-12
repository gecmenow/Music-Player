using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Player.Controll
{
    class Volume
    {
        public void CreateVolumeBar(TrackBar trackBar)
        {
            Thickness t = new Thickness();
            t.Left = 40;
            trackBar.Top = Form1._Form1.AddButton.Bottom;
            trackBar.Left = (int)t.Left;
            trackBar.Orientation = Orientation.Vertical;
            trackBar.Height = 100;
            trackBar.Name = "VolumeBar";
            trackBar.Minimum = 0;
            trackBar.Maximum = 100;
            trackBar.Value = 50;
            trackBar.TickFrequency = 5;
            trackBar.TickStyle = TickStyle.None;
            trackBar.SmallChange = 2;
            trackBar.LargeChange = 10;
            //trackBar.BackColor = Color.White;
            Form1._Form1.Controls.Add(trackBar);
        }
    }
}
