using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuakeDemoFun
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Demo = new ParsedDemo();
            LoadedPaks = new List<PackFile>();
        }

        public List<PackFile> LoadedPaks { get; private set; }

        public ParsedDemo Demo { get; private set; }

        public float Time { get; private set; }

        public void ClearDemo()
        {
            if (Demo != null) Demo.Dispose();
            Demo = null;
            Display.Demo = null;
            GC.Collect();

            Display.Invalidate();
            ClockState(false);
        }

        public void Goto(float time)
        {
            if (time < Demo.Start) time = Demo.Start;
            if (time > Demo.End) time = Demo.End;
            Time = time;

            Timeline.Value = IntTime(time);
            Timeline_ValueChanged(this, null);
        }

        private void AddDemo(string filename)
        {
            QDemo dem = QDemo.Load(filename);
            if (Demo == null) Demo = new ParsedDemo();
            Demo.Parse(dem);
        }

        private void Ready()
        {
            Display.Demo = Demo;

            Timeline.Minimum = IntTime(Demo.Start);
            Timeline.Maximum = IntTime(Demo.End);
            Goto(0);

            ClockState(false);
        }

        private void Timeline_ValueChanged(object sender, EventArgs e)
        {
            float time = FloatTime(Timeline.Value);
            TimeLabel.Text = time.ToString();
            Display.Goto(time);
        }

        private int IntTime(float time)
        {
            return (int)(time * 1000);
        }
        private float FloatTime(int time)
        {
            return time / 1000f;
        }

        private void AutoloadBsp()
        {
            if (Demo == null || Demo.Models.Count < 1) return;

            foreach (PackFile pak in LoadedPaks)
            {
                string bspfile = Demo.Models[0];
                if (pak.Contains(bspfile))
                {
                    Bsp bsp = new Bsp(pak.GetFile(bspfile));
                    Display.Bsp = bsp;
                }
            }
        }

        private void TimeLabel_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(TimeLabel.Text, out float time))
            {
                TimeLabel.BackColor = SystemColors.Window;
                Goto(time);
            }
            else TimeLabel.BackColor = Color.Red;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (Time == Demo.End) Goto(Demo.Start);
            ClockState(true);
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            float next = Time + (Clock.Interval / 1000f);
            if (next > Demo.End)
            {
                next = Demo.End;
                ClockState(false);
            }

            Goto(next);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            ClockState(false);
        }

        private void ClockState(bool enabled)
        {
            if (Demo == null)
            {
                PlayButton.Enabled = false;
                StopButton.Enabled = false;
                TimeLabel.Enabled = false;
                Timeline.Enabled = false;
                Clock.Enabled = false;

                return;
            }

            PlayButton.Enabled = !enabled;
            StopButton.Enabled = enabled;
            TimeLabel.Enabled = !enabled;
            Timeline.Enabled = !enabled;
            Clock.Enabled = enabled;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            ClockState(false);
            Application.Exit();
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            ClockState(false);
            if (OpenDem.ShowDialog() == DialogResult.OK)
            {
                Display.Bsp = null;
                ClearDemo();
                AddDemo(OpenDem.FileName);
                AutoloadBsp();
                Ready();
            }
        }

        private void OverlayMenuItem_Click(object sender, EventArgs e)
        {
            ClockState(false);
            if (OpenDem.ShowDialog() == DialogResult.OK)
            {
                AddDemo(OpenDem.FileName);
                Ready();
            }
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            ClearDemo();
        }

        private void LoadBSPMenuItem_Click(object sender, EventArgs e)
        {
            ClockState(false);
            if (OpenBsp.ShowDialog() == DialogResult.OK)
            {
                Bsp bsp = new Bsp(OpenBsp.FileName);
                Display.Bsp = bsp;
                Display.Invalidate();
            }
        }

        private void AddPAKMenuItem_Click(object sender, EventArgs e)
        {
            ClockState(false);
            if (OpenPak.ShowDialog() == DialogResult.OK)
            {
                string fileName = OpenPak.FileName;
                if (LoadedPaks.Any(p => p.FileName == fileName))
                {
                    MessageBox.Show("Already loaded.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                PackFile pak = new PackFile(fileName);
                LoadedPaks.Add(pak);

                AutoloadBsp();
            }
        }
    }
}
