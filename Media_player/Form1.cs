using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Media_player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            trackBar1.Value = 50;
            lbl_volume.Text = trackBar1.Value.ToString() + "%";
        }
        string[] paths, files;

        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            player.URL = paths[track_list.SelectedIndex];

            player.Ctlcontrols.play();
            lbl_msg.Text = "Playing...";
            timer1.Start();
            //try
            //{
            //    var file = TagLib.File.Create(paths[track_list.SelectedIndex]);
            //    var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
            //    pic_art.Image = Image.FromStream(new MemoryStream(bin));
            //}
            //catch
            //{
            //    //pic_art.Image = null;
            //}
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.play();
            if (player.URL != String.Empty)
            {
                lbl_msg.Text = "Playing...";
            }
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
            if (player.URL != String.Empty)
            {
                lbl_msg.Text = "Pause";
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            progressBar1.Value = 0;
            if (player.URL != String.Empty)
            {
                lbl_msg.Text = "Stop";
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex < track_list.Items.Count - 1)

            {

                track_list.SelectedIndex = track_list.SelectedIndex + 1;

            }
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex > 0)

            {

                track_list.SelectedIndex = track_list.SelectedIndex - 1;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)

                {
                    progressBar1.Maximum = (int)player.Ctlcontrols.currentItem.duration;

                    progressBar1.Value = (int)player.Ctlcontrols.currentPosition;
                    lbl_track_start.Text = player.Ctlcontrols.currentPositionString;
                }
                //lbl_track_start.Text = player.Ctlcontrols.currentPositionString;
                lbl_track_end.Text = player.Ctlcontrols.currentItem.durationString.ToString();
                if (player.URL != String.Empty)
                {
                    if (track_list.SelectedIndex < track_list.Items.Count && lbl_track_start.Text == lbl_track_end.Text)
                    {
                        track_list.SelectedIndex = track_list.SelectedIndex + 1;
                    }
                }
            }
            catch
            {

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = trackBar1.Value;

            lbl_volume.Text = trackBar1.Value.ToString() + "%";
        }

        private void progressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            player.Ctlcontrols.currentPosition = player.currentMedia.duration * e.X / progressBar1.Width;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)

            {

                files = ofd.SafeFileNames;

                paths = ofd.FileNames;

                for (int x = 0; x < files.Length; x++)

                {

                    track_list.Items.Add(files[x]);

                }

            }
        }
    }
}
