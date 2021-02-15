using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QuakeDemoFun
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Demos = new List<ParsedDemo>();
            LoadedPaks = new List<PackFile>();
        }

        public List<PackFile> LoadedPaks { get; private set; }

        public List<ParsedDemo> Demos { get; private set; }

        public float Time { get; private set; }
        public float MinTime { get; private set; }
        public float MaxTime { get; private set; }

        public void ClearDemo()
        {
            foreach (ParsedDemo Demo in Demos) Demo.Dispose();
            Demos.Clear();
            Display.ClearDemos();
            GC.Collect();

            Display.Invalidate();
            ClockState(false);
        }

        public void Goto(float time)
        {
            if (time < MinTime) time = MinTime;
            if (time > MaxTime) time = MaxTime;
            Time = time;

            Timeline.Value = IntTime(time);
            Timeline_ValueChanged(this, null);
        }

        private void OverlayDemo(string filename)
        {
            QDemo dem = QDemo.Load(filename);
            ParsedDemo demo;

            if (Demos.Count > 0) demo = Demos[0];
            else
            {
                demo = new ParsedDemo();
                Demos.Add(demo);
                MinTime = 100;
                MaxTime = 0;
            }
            demo.Parse(dem);

            MinTime = Math.Min(MinTime, demo.Start);
            MaxTime = Math.Max(MaxTime, demo.End);
        }

        private void MergeDemo(string filename)
        {
            QDemo dem = QDemo.Load(filename);
            ParsedDemo demo = new ParsedDemo();

            if (Demos.Count == 0) 
            {
                MinTime = 100;
                MaxTime = 0;
            }
            demo.Parse(dem);
            Demos.Add(demo);

            MinTime = Math.Min(MinTime, demo.Start);
            MaxTime = Math.Max(MaxTime, demo.End);
        }

        private void Ready()
        {
            Display.Use(Demos);

            Timeline.Minimum = IntTime(MinTime);
            Timeline.Maximum = IntTime(MaxTime);
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
            var demo = Demos.First();
            if (demo == null || demo.Models.Count < 1) return;

            foreach (PackFile pak in LoadedPaks)
            {
                string bspfile = demo.Models[0];
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
            if (Time == MaxTime) Goto(MinTime);
            ClockState(true);
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            float next = Time + (Clock.Interval / 1000f);
            if (next > MaxTime)
            {
                next = MaxTime;
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
            if (Demos.Count < 1)
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
                if (Path.GetExtension(OpenDem.FileName).ToUpper() == ".DZ")
                {
                    DZip dz = new DZip(OpenDem.FileName);
                    return;
                }

                Display.Bsp = null;
                ClearDemo();
                OverlayDemo(OpenDem.FileName);
                AutoloadBsp();
                Ready();
            }
        }


        private void MergeMenuItem_Click(object sender, EventArgs e)
        {
            ClockState(false);
            if (OpenDem.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(OpenDem.FileName).ToUpper() == ".DZ")
                {
                    DZip dz = new DZip(OpenDem.FileName);
                    return;
                }

                MergeDemo(OpenDem.FileName);
                Ready();
            }
        }

        private void OverlayMenuItem_Click(object sender, EventArgs e)
        {
            ClockState(false);
            if (OpenDem.ShowDialog() == DialogResult.OK)
            {
                OverlayDemo(OpenDem.FileName);
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
