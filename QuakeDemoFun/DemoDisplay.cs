﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuakeDemoFun
{
    public partial class DemoDisplay : UserControl, IDraw
    {
        const int PlayerSize = 32;

        private int mx, my;
        private Graphics g;

        public DemoDisplay()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Demos = new List<ParsedDemo>();
            MouseMove += DemoDisplay_MouseMove;
            Resize += DemoDisplay_Resize;
        }

        private void DemoDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Right))
            {
                Cursor = Cursors.Hand;
                PanX += (mx - e.X);
                PanY += (my - e.Y);
                Invalidate();
            }
            else Cursor = Cursors.Default;

            mx = e.X;
            my = e.Y;
        }

        private void DemoDisplay_Resize(object sender, EventArgs e)
        {
            Invalidate();
            Zoom = 0.2f;
        }

        public Bsp Bsp { get; set; }
        public float Time { get; private set; }
        public double Zoom { get; private set; }

        public int PanX { get; private set; }
        public int PanY { get; private set; }

        public int CenterX => ClientRectangle.Width / 2;
        public int CenterY => ClientRectangle.Height / 2;

        List<ParsedDemo> Demos { get; set; }
        ParsedDemo FirstDemo => Demos.First();

        public void Use(IEnumerable<ParsedDemo> demos)
        {
            Demos = new List<ParsedDemo>(demos);
        }

        public void ClearDemos()
        {
            Demos.Clear();
        }

        public void Goto(float time)
        {
            Time = time;
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Demos.Count < 1) return;
            g = e.Graphics;

            // update Zoom
            double width = FirstDemo.MaxX - FirstDemo.MinX + 40;
            double height = FirstDemo.MaxY - FirstDemo.MinY + 40;
            double xratio = ClientRectangle.Width / width;
            double yratio = ClientRectangle.Height / height;
            Zoom = xratio < yratio ? xratio : yratio;

            // draw bsp!
            if (Bsp != null)
            {
                foreach (var ed in Bsp.Edges)
                {
                    var va = Bsp.Vertices[ed.A];
                    var vb = Bsp.Vertices[ed.B];

                    Point pa = Convert(va.X, va.Y);
                    Point pb = Convert(vb.X, vb.Y);

                    e.Graphics.DrawLine(Pens.Gray, pa, pb);
                }
            }

            int keyY = 10;
            foreach (ParsedDemo Demo in Demos)
            {

                // find closest state
                GameState state = Demo.States[0];
                foreach (var pair in Demo.States)
                {
                    if (pair.Key > Time) break;
                    state = pair.Value;
                }

                // draw player key
                StatIndex hpstat = StatIndex.Player1HP;
                StatIndex wpstat = StatIndex.Player1Weapon;
                StatIndex amstat = StatIndex.Player1Ammo;
                int x = 10;
                foreach (Player pl in Demo.Players.Values)
                {
                    if (pl.Netname == null) break;
                    int windex = state.Stat(wpstat);
                    string hptext = state.Stats.ContainsKey(hpstat) ? state.Stat(hpstat).ToString() : "?";
                    string wptext = Info.WeaponNames.ContainsKey(windex) ? Info.WeaponNames[windex] : "?";
                    string amtext = state.Stats.ContainsKey(amstat) ? state.Stat(amstat).ToString() : "?";

                    Entity ent = state.Entities[pl.Entity];
                    DrawPlayer(e.Graphics, ent, pl, new Rectangle(x, keyY, 10, 10));
                    e.Graphics.DrawString(pl.Netname, Font, Brushes.White, x + 12, keyY);
                    e.Graphics.DrawString(hptext, Font, Brushes.White, x + 82, keyY);
                    e.Graphics.DrawString(wptext, Font, Brushes.White, x + 102, keyY);
                    e.Graphics.DrawString(amtext, Font, Brushes.White, x + 122, keyY);

                    hpstat++;
                    wpstat++;
                    amstat++;
                    keyY += 14;
                }

                // draw messages
                int y = ClientRectangle.Height - 10;
                y -= state.Messages.Count * 14;
                foreach (string msg in state.Messages)
                {
                    e.Graphics.DrawString(msg, Font, Brushes.White, x, y);
                    y += 14;
                }

                // draw counts
                x = ClientRectangle.Width - 100;
                y = ClientRectangle.Height - 24;
                e.Graphics.DrawString($"Kills: {state.Stat(StatIndex.KilledMonsters)} / {state.Stat(StatIndex.NumMonsters)}", Font, Brushes.White, x, y - 14);
                e.Graphics.DrawString($"Secrets: {state.Stat(StatIndex.FoundSecrets)} / {state.Stat(StatIndex.NumSecrets)}", Font, Brushes.White, x, y);

                // draw temps
                foreach (Temp t in state.Temps) t.Draw(this);

                // draw entities
                foreach (Entity ent in state.Entities.Values.Reverse())
                {
                    if (ent.Number == 0) continue;
                    if (ent.Model[0] == '*') continue;
                    if (ent.Model == "?") continue;

                    ModelInfo minf = Info.GetModelInfo(ent.Model, ent.Skin);
                    if (minf.Type == ModelType.Player)
                    {
                        Player pl = Demo.GetPlayer((byte)(ent.Number - 1));
                        DrawPlayer(e.Graphics, ent, pl);
                        DrawAngle(e.Graphics, ent);
                        continue;
                    }

                    if (minf.DeadFrames.Contains(ent.Frame))
                    {
                        Cross(ent.Origin, Color.DarkRed, minf.Size / 2);
                        continue;
                    }

                    Rectangle rect = GetDrawRect(ent, minf.Size);
                    e.Graphics.FillRectangle(minf.Background, rect);
                    if (minf.Type == ModelType.Enemy) DrawAngle(e.Graphics, ent);

                    if (minf.Label != null)
                    {
                        SizeF size = e.Graphics.MeasureString(minf.Label, Font);
                        float lx = rect.Left + rect.Width / 2 - size.Width / 2;
                        float ly = rect.Top + rect.Height / 2 - size.Height / 2;

                        e.Graphics.DrawString(minf.Label, Font, minf.Foreground, lx, ly);
                    }
                }
            }
        }

        private Point Convert(QCoords co)
        {
            return Convert(co.X, co.Y);
        }

        private Point Convert(double x, double y)
        { 
            double rawx = x - FirstDemo.MinX;
            double rawy = FirstDemo.MaxY - y;

            int zx = (int)(rawx * Zoom);
            int zy = (int)(rawy * Zoom);

            return new Point(zx + PanX, zy + PanY);
        }

        private Rectangle GetDrawRect(Entity ent, int size)
        {
            Point p = Convert(ent.Origin);
            int scaled = (int)(size * Zoom);
            if (scaled < 1) scaled = 1;
            p.X -= scaled / 2;
            p.Y -= scaled / 2;

            return new Rectangle(p, new Size(scaled, scaled));
        }

        private void DrawPlayer(Graphics g, Entity ent, Player pl, Rectangle r = default)
        {
            if (r == default) r = GetDrawRect(ent, PlayerSize);
            Brush top = new SolidBrush(Info.QuakeColors[pl.Top]);
            Brush bot = new SolidBrush(Info.QuakeColors[pl.Pants]);

            g.FillRectangle(bot, r);
            g.FillRectangle(top, r.Left, r.Top, r.Width, r.Height / 2);
            g.DrawRectangle(Pens.White, r);
        }

        private void DrawAngle(Graphics g, Entity ent, Pen pen = null, int size = 16)
        {
            Point p = Convert(ent.Origin);
            double yaw = ent.Angles.Y;
            double rad = yaw * Math.PI / 180;
            float dx = (float)(Math.Cos(rad) * size);
            float dy = (float)(Math.Sin(rad) * size);

            g.DrawLine(pen ?? Pens.White, p.X, p.Y, p.X + dx, p.Y - dy);
        }

        public void Cross(QCoords org, Color c, int size)
        {
            Point p = Convert(org);
            Pen n = new Pen(c);
            int scaled = (int)(size * Zoom);
            if (scaled < 1) scaled = 1;

            g.DrawLine(n, p.X - scaled, p.Y - scaled, p.X + scaled, p.Y + scaled);
            g.DrawLine(n, p.X + scaled, p.Y - scaled, p.X - scaled, p.Y + scaled);
        }

        public void Line(QCoords from, QCoords to, Color c)
        {
            Point f = Convert(from);
            Point t = Convert(to);

            g.DrawLine(new Pen(c), f, t);
        }

        public void Dot(QCoords org, Color c)
        {
            Point p = Convert(org);
            g.DrawRectangle(new Pen(c), p.X, p.Y, 1, 1);
        }
    }
}
