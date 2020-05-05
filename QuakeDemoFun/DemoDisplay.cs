using System;
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

        private ModelInfo DefaultInfo = new ModelInfo(Brushes.White, 32);
        private ModelInfo GreenArmor = new ModelInfo(Brushes.DarkGreen, 32, "A");
        private ModelInfo YellowArmor = new ModelInfo(Brushes.GreenYellow, 32, "A", Brushes.DarkGreen);
        private ModelInfo RedArmor = new ModelInfo(Brushes.DarkRed, 32, "A");

        private Dictionary<int, string> WeaponNames = new Dictionary<int, string>()
        {
            { 1, "S" },
            { 2, "SS" },
            { 4, "N" },
            { 8, "SN" },
            { 16, "GL" },
            { 32, "RL" },
            { 64, "Th" },
        };

        private readonly Dictionary<string, ModelInfo> ModelInfos = new Dictionary<string, ModelInfo>();

        private readonly Color[] QuakeColors = new Color[]
        {
            Color.White,
            Color.FromArgb(128, 64, 0),
            Color.FromArgb(128, 128, 255),
            Color.FromArgb(0, 64, 0),
            Color.FromArgb(128, 0, 0),
            Color.FromArgb(192, 192, 0),
            Color.FromArgb(255, 128, 0),
            Color.FromArgb(255, 192, 160),
            Color.FromArgb(144, 0, 80),
            Color.FromArgb(233, 88, 142),
            Color.FromArgb(255, 192, 144),
            Color.FromArgb(0, 128, 80),
            Color.FromArgb(255, 255, 0),
            Color.FromArgb(0, 0, 255),
        };

        public DemoDisplay()
        {
            InitializeComponent();
            DoubleBuffered = true;

            ModelInfos.Add("progs/boss.mdl", new ModelInfo(Brushes.Red, 256, "Ch'thon"));
            ModelInfos.Add("progs/demon.mdl", new ModelInfo(Brushes.Red, 64, "Fi"));
            ModelInfos.Add("progs/dog.mdl", new ModelInfo(Brushes.Red, 64, "d"));
            ModelInfos.Add("progs/enforcer.mdl", new ModelInfo(Brushes.Red, 32, "E"));
            ModelInfos.Add("progs/fish.mdl", new ModelInfo(Brushes.Red, 32, "f"));
            ModelInfos.Add("progs/hknight.mdl", new ModelInfo(Brushes.Red, 32, "HK"));
            ModelInfos.Add("progs/knight.mdl", new ModelInfo(Brushes.Red, 32, "K"));
            ModelInfos.Add("progs/ogre.mdl", new ModelInfo(Brushes.Red, 64, "Og"));
            ModelInfos.Add("progs/oldone.mdl", new ModelInfo(Brushes.Red, 256, "Shub"));
            ModelInfos.Add("progs/shalrath.mdl", new ModelInfo(Brushes.Red, 64, "Vo"));
            ModelInfos.Add("progs/shambler.mdl", new ModelInfo(Brushes.Red, 64, "Sham"));
            ModelInfos.Add("progs/soldier.mdl", new ModelInfo(Brushes.Red, 32, "s"));
            ModelInfos.Add("progs/tarbaby.mdl", new ModelInfo(Brushes.Red, 32, "Sp"));
            ModelInfos.Add("progs/wizard.mdl", new ModelInfo(Brushes.Red, 32, "Sc"));
            ModelInfos.Add("progs/zombie.mdl", new ModelInfo(Brushes.Red, 32, "Z"));
            // rogue
            ModelInfos.Add("progs/dragon.mdl", new ModelInfo(Brushes.Red, 64, "Dr"));
            ModelInfos.Add("progs/eel2.mdl", new ModelInfo(Brushes.Red, 32, "E"));
            ModelInfos.Add("progs/mummy.mdl", new ModelInfo(Brushes.Red, 32, "Mu"));
            ModelInfos.Add("progs/s_wrath.mdl", new ModelInfo(Brushes.Red, 32, "SW"));
            ModelInfos.Add("progs/wrath.mdl", new ModelInfo(Brushes.Red, 32, "Wr"));

            ModelInfos.Add("maps/b_bh10.bsp", new ModelInfo(Brushes.Green, 32, "h"));
            ModelInfos.Add("maps/b_bh25.bsp", new ModelInfo(Brushes.Green, 32, "H"));
            ModelInfos.Add("maps/b_bh100.bsp", new ModelInfo(Brushes.Green, 32, "M"));

            ModelInfos.Add("progs/g_light.mdl", new ModelInfo(Brushes.Blue, 32, "8"));
            ModelInfos.Add("progs/g_nail.mdl", new ModelInfo(Brushes.Blue, 32, "4"));
            ModelInfos.Add("progs/g_nail2.mdl", new ModelInfo(Brushes.Blue, 32, "5"));
            ModelInfos.Add("progs/g_rock.mdl", new ModelInfo(Brushes.Blue, 32, "6"));
            ModelInfos.Add("progs/g_rock2.mdl", new ModelInfo(Brushes.Blue, 32, "7"));
            ModelInfos.Add("progs/g_shot.mdl", new ModelInfo(Brushes.Blue, 32, "3"));

            ModelInfos.Add("progs/invisibl.mdl", new ModelInfo(Brushes.DarkGray, 32, "R", Brushes.Yellow));
            ModelInfos.Add("progs/invulner.mdl", new ModelInfo(Brushes.DarkRed, 32, "P", Brushes.Red));
            ModelInfos.Add("progs/quaddama.mdl", new ModelInfo(Brushes.DarkCyan, 32, "Q", Brushes.Cyan));
            ModelInfos.Add("progs/suit.mdl", new ModelInfo(Brushes.DarkGray, 32, "S", Brushes.Gray));

            ModelInfos.Add("maps/b_batt0.bsp", new ModelInfo(Brushes.Brown, 32, "c"));
            ModelInfos.Add("maps/b_batt1.bsp", new ModelInfo(Brushes.Brown, 32, "c"));
            ModelInfos.Add("maps/b_nail0.bsp", new ModelInfo(Brushes.Brown, 32, "n"));
            ModelInfos.Add("maps/b_nail1.bsp", new ModelInfo(Brushes.Brown, 32, "N"));
            ModelInfos.Add("maps/b_rock0.bsp", new ModelInfo(Brushes.Brown, 32, "r"));
            ModelInfos.Add("maps/b_rock1.bsp", new ModelInfo(Brushes.Brown, 32, "R"));
            ModelInfos.Add("maps/b_shell0.bsp", new ModelInfo(Brushes.Brown, 32, "s"));
            ModelInfos.Add("maps/b_shell1.bsp", new ModelInfo(Brushes.Brown, 32, "S"));
            // rogue
            ModelInfos.Add("maps/b_lnail0.bsp", new ModelInfo(Brushes.Brown, 32, "ln"));
            ModelInfos.Add("maps/b_lnail1.bsp", new ModelInfo(Brushes.Brown, 32, "LN"));
            ModelInfos.Add("maps/b_mrock0.bsp", new ModelInfo(Brushes.Brown, 32, "mr"));
            ModelInfos.Add("maps/b_mrock1.bsp", new ModelInfo(Brushes.Brown, 32, "MR"));
            ModelInfos.Add("maps/b_plas0.bsp", new ModelInfo(Brushes.Brown, 32, "pl"));
            ModelInfos.Add("maps/b_plas1.bsp", new ModelInfo(Brushes.Brown, 32, "PL"));

            ModelInfos.Add("progs/backpack.mdl", new ModelInfo(Brushes.OliveDrab, 16));

            ModelInfos.Add("progs/grenade.mdl", new ModelInfo(Brushes.Gray, 16));
            ModelInfos.Add("progs/missile.mdl", new ModelInfo(Brushes.Gray, 16));
            // rogue
            ModelInfos.Add("progs/mervup.mdl", new ModelInfo(Brushes.Gray, 16));
            ModelInfos.Add("progs/rockup_d.mdl", new ModelInfo(Brushes.Gray, 16));

            ModelInfos.Add("progs/gib1.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/gib2.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/gib3.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_demon.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_dog.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_guard.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_hellkn.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_knight.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_mega.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_ogre.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_player.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_shal.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_shams.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_wizard.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/h_zombie.mdl", new ModelInfo(Brushes.DarkRed, 8));
            // rogue
            ModelInfos.Add("progs/drggib01.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/drggib02.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/drggib03.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/eelgib.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/eelhead.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/s_wrtgb1.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/s_wrtgb2.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/s_wrtgb3.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/statgib1.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/statgib2.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/statgib3.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/timegib.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/wrthgib1.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/wrthgib2.mdl", new ModelInfo(Brushes.DarkRed, 8));
            ModelInfos.Add("progs/wrthgib3.mdl", new ModelInfo(Brushes.DarkRed, 8));

            ModelInfos.Add("progs/zom_gib.mdl", new ModelInfo(Brushes.DarkRed, 12));
            ModelInfos.Add("progs/laser.mdl", new ModelInfo(Brushes.OrangeRed, 12));
            ModelInfos.Add("progs/lavaball.mdl", new ModelInfo(Brushes.OrangeRed, 12));
            ModelInfos.Add("progs/k_spike.mdl", new ModelInfo(Brushes.Yellow, 8));
            ModelInfos.Add("progs/s_spike.mdl", new ModelInfo(Brushes.Gray, 8));
            ModelInfos.Add("progs/spike.mdl", new ModelInfo(Brushes.DarkGray, 8));
            ModelInfos.Add("progs/v_spike.mdl", new ModelInfo(Brushes.Purple, 12));
            ModelInfos.Add("progs/w_spike.mdl", new ModelInfo(Brushes.GreenYellow, 8));
            // rogue
            ModelInfos.Add("progs/lspike.mdl", new ModelInfo(Brushes.DarkOrange, 8));
            ModelInfos.Add("progs/plasma.mdl", new ModelInfo(Brushes.Cyan, 16));
            ModelInfos.Add("progs/w_ball.mdl", new ModelInfo(Brushes.Gray, 8));

            ModelInfos.Add("progs/b_g_key.mdl", new ModelInfo(Brushes.Gold, 32, "G", Brushes.DarkRed));
            ModelInfos.Add("progs/m_g_key.mdl", new ModelInfo(Brushes.Gold, 32, "G", Brushes.DarkRed));
            ModelInfos.Add("progs/w_g_key.mdl", new ModelInfo(Brushes.Gold, 32, "G", Brushes.DarkRed));
            ModelInfos.Add("progs/b_s_key.mdl", new ModelInfo(Brushes.Silver, 32, "S", Brushes.DarkBlue));
            ModelInfos.Add("progs/m_s_key.mdl", new ModelInfo(Brushes.Silver, 32, "S", Brushes.DarkBlue));
            ModelInfos.Add("progs/w_s_key.mdl", new ModelInfo(Brushes.Silver, 32, "S", Brushes.DarkBlue));

            ModelInfos.Add("progs/end1.mdl", new ModelInfo(Brushes.Purple, 32, "R"));
            ModelInfos.Add("progs/end2.mdl", new ModelInfo(Brushes.Purple, 32, "R"));
            ModelInfos.Add("progs/end3.mdl", new ModelInfo(Brushes.Purple, 32, "R"));
            ModelInfos.Add("progs/end4.mdl", new ModelInfo(Brushes.Purple, 32, "R"));

            ModelInfos.Add("progs/s_bubble.spr", new ModelInfo(Brushes.LightBlue, 8));
            ModelInfos.Add("progs/s_explod.spr", new ModelInfo(Brushes.Orange, 16));
            ModelInfos.Add("progs/s_light.mdl", new ModelInfo(Brushes.LightYellow, 8));
            ModelInfos.Add("progs/s_light.spr", new ModelInfo(Brushes.LightYellow, 8));
            ModelInfos.Add("progs/teleport.mdl", new ModelInfo(Brushes.Cyan, 16));

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

        public ParsedDemo Demo { get; set; }
        public Bsp Bsp { get; set; }
        public float Time { get; private set; }
        public double Zoom { get; private set; }

        public int PanX { get; private set; }
        public int PanY { get; private set; }

        public int CenterX => ClientRectangle.Width / 2;
        public int CenterY => ClientRectangle.Height / 2;

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
            if (Demo == null) return;
            g = e.Graphics;

            // update Zoom
            double width = Demo.MaxX - Demo.MinX + 40;
            double height = Demo.MaxY - Demo.MinY + 40;
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
            int y = 10;
            foreach (Player pl in Demo.Players.Values)
            {
                if (pl.Netname == null) break;
                int windex = state.Stat(wpstat);
                string hptext = state.Stats.ContainsKey(hpstat) ? state.Stat(hpstat).ToString() : "?";
                string wptext = WeaponNames.ContainsKey(windex) ? WeaponNames[windex] : "?";
                string amtext = state.Stats.ContainsKey(amstat) ? state.Stat(amstat).ToString() : "?";

                Entity ent = state.Entities[pl.Entity];
                DrawPlayer(e.Graphics, ent, pl, new Rectangle(x, y, 10, 10));
                e.Graphics.DrawString(pl.Netname, Font, Brushes.White, x + 12, y);
                e.Graphics.DrawString(hptext, Font, Brushes.White, x + 82, y);
                e.Graphics.DrawString(wptext, Font, Brushes.White, x + 102, y);
                e.Graphics.DrawString(amtext, Font, Brushes.White, x + 122, y);

                hpstat++;
                wpstat++;
                amstat++;
                y += 14;
            }

            // draw messages
            y = ClientRectangle.Height - 10;
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

            // draw entities
            foreach (Entity ent in state.Entities.Values.Reverse())
            {
                if (ent.ModelIndex == 0) continue;
                if (ent.Model[0] == '*') continue;

                if (ent.Model == "progs/eyes.mdl" || ent.Model == "progs/player.mdl")
                {
                    Player pl = Demo.GetPlayer((byte)(ent.Number - 1));
                    DrawPlayer(e.Graphics, ent, pl);
                    continue;
                }

                ModelInfo minf;

                if (ent.Model == "progs/armor.mdl")
                {
                    switch (ent.Skin)
                    {
                        case 0:
                            minf = GreenArmor;
                            break;

                        case 1:
                            minf = YellowArmor;
                            break;

                        default:
                            minf = RedArmor;
                            break;
                    }
                }
                else if (ModelInfos.ContainsKey(ent.Model))
                {
                    minf = ModelInfos[ent.Model];
                }
                else
                {
                    if (ent.Model[0] != '*' && !ent.Model.StartsWith("maps"))
                        Console.WriteLine(ent.Model);
                    minf = DefaultInfo;
                }

                Rectangle rect = GetDrawRect(ent, minf.Size);
                e.Graphics.FillRectangle(minf.Background, rect);

                if (minf.Label != null)
                {
                    SizeF size = e.Graphics.MeasureString(minf.Label, Font);
                    float lx = rect.Left + rect.Width / 2 - size.Width / 2;
                    float ly = rect.Top + rect.Height / 2 - size.Height / 2;

                    e.Graphics.DrawString(minf.Label, Font, minf.Foreground, lx, ly);
                }
            }

            // draw temps
            foreach (Temp t in state.Temps) t.Draw(this);
        }

        private Point Convert(QCoords co)
        {
            return Convert(co.X, co.Y);
        }

        private Point Convert(double x, double y)
        { 
            double rawx = x - Demo.MinX;
            double rawy = Demo.MaxY - y;

            int zx = (int)(rawx * Zoom);
            int zy = (int)(rawy * Zoom);

            return new Point(zx + PanX, zy + PanY);
        }

        private Rectangle GetDrawRect(Entity ent, int size)
        {
            Point p = Convert(ent.Origin);
            int scaled = (int)(size * Zoom);
            p.X -= scaled / 2;
            p.Y -= scaled / 2;

            return new Rectangle(p, new Size(scaled, scaled));
        }

        private void DrawPlayer(Graphics g, Entity ent, Player pl, Rectangle r = default)
        {
            if (r == default) r = GetDrawRect(ent, PlayerSize);
            Brush top = new SolidBrush(QuakeColors[pl.Top]);
            Brush bot = new SolidBrush(QuakeColors[pl.Pants]);

            g.FillRectangle(bot, r);
            g.FillRectangle(top, r.Left, r.Top, r.Width, r.Height / 2);
            g.DrawRectangle(Pens.White, r);
        }

        public void Cross(QCoords org, Color c, int size)
        {
            Point p = Convert(org);
            Pen n = new Pen(c);

            g.DrawLine(n, p.X - size, p.Y - size, p.X + size, p.Y + size);
            g.DrawLine(n, p.X + size, p.Y - size, p.X - size, p.Y + size);
        }

        public void Line(QCoords from, QCoords to, Color c)
        {
            Point f = Convert(from);
            Point t = Convert(to);

            g.DrawLine(new Pen(c), f, t);
        }

        struct ModelInfo
        {
            public ModelInfo(Brush bg, int size, string label = null, Brush fg = default)
            {
                Background = bg;
                Size = size;
                Label = label;
                Foreground = fg == default ? Brushes.White : fg;
            }

            public Brush Background { get; set; }
            public int Size { get; set; }
            public string Label { get; set; }
            public Brush Foreground { get; set; }
        }
    }
}
