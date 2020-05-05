using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class ParsedDemo : IDisposable
    {
        public ParsedDemo()
        {
            Clear();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        { 
            Models.Clear();
            Players.Clear();
            Sounds.Clear();
            States.Clear();
        }

        public void Clear()
        {
            Models = new List<string>();
            Players = new Dictionary<byte, Player>();
            Sounds = new List<string>();
            States = new Dictionary<float, GameState>();

            Time = 0;
            MinX = 10000;
            MaxX = -10000;
            MinY = 10000;
            MaxY = -10000;
        }

        public Dictionary<float, GameState> States { get; private set; }
        public string Mapname { get; private set; }
        public double MinX { get; private set; }
        public double MaxX { get; private set; }
        public double MinY { get; private set; }
        public double MaxY { get; private set; }
        public List<string> Models { get; private set; }
        public Dictionary<byte, Player> Players { get; private set; }
        public List<string> Sounds { get; private set; }
        public float Start { get; private set; }
        public float End { get; private set; }

        public Player GetPlayer(byte index)
        {
            if (Players.ContainsKey(index)) return Players[index];

            Player p = new Player() { Entity = (short)(index + 1) };
            Players[index] = p;
            return p;
        }

        private GameState State { get; set; }
        private float Time { get; set; }

        public void Parse(QDemo dem)
        {
            short viewent = 0;

            if (!States.ContainsKey(0))
            {
                State = new GameState(this, 0);
                States[0] = State;
            }
            Time = 0;

            foreach (QBlock block in dem.Blocks)
            {
                foreach (QMessage msg in block.Messages)
                {
                    switch (msg.ID)
                    {
                        case QMessageID.ClientData:
                            ClientData(msg as QClientDataMessage, viewent);
                            break;

                        case QMessageID.ServerInfo:
                            ServerInfo(msg as QServerInfoMessage);
                            break;

                        case QMessageID.SetView:
                            viewent = (msg as QSetViewMessage).Entity;
                            break;

                        case QMessageID.Time:
                            TimeMessage(msg as QTimeMessage);
                            break;

                        case QMessageID.UpdateColors:
                            UpdateColors(msg as QUpdateColorsMessage);
                            break;

                        case QMessageID.UpdateName:
                            UpdateName(msg as QUpdateNameMessage);
                            break;

                        default:
                            State.Apply(msg);
                            break;
                    }
                }
            }
        }

        private void ClientData(QClientDataMessage msg, short entity)
        {
            StatIndex hpstat = (StatIndex)(100 + entity - 1);
            StatIndex wpstat = (StatIndex)(110 + entity - 1);
            StatIndex amstat = (StatIndex)(120 + entity - 1);

            State.Stats[hpstat] = msg.Health;
            State.Stats[wpstat] = msg.Weapon;
            State.Stats[amstat] = msg.Currentammo;
        }

        private void ServerInfo(QServerInfoMessage msg)
        {
            Mapname = msg.Mapname;
            Models = msg.Models.ToList();
            Sounds = msg.Sounds.ToList();
        }

        private void TimeMessage(QTimeMessage msg)
        {
            if (msg.Time > Time)
            {
                Time = msg.Time;

                if (Start == 0) Start = Time;
                End = Time;

                UpdateExtents();

                if (States.ContainsKey(Time))
                {
                    State = States[Time];
                }
                else
                {
                    State = State.Advance(Time);
                    States[Time] = State;
                }
            }
        }

        private void UpdateColors(QUpdateColorsMessage msg)
        {
            Player p = GetPlayer(msg.Player);
            if (msg.Colors != 0)
                p.Colors = msg.Colors;
        }
        
        private void UpdateName(QUpdateNameMessage msg)
        {
            Player p = GetPlayer(msg.Player);
            if (msg.Netname.Length > 0)
                p.Netname = msg.Netname;
        }

        private void UpdateExtents()
        {
            foreach (Entity e in State.Entities.Values)
            {
                if (e.Origin.X < MinX) MinX = e.Origin.X;
                if (e.Origin.X > MaxX) MaxX = e.Origin.X;
                if (e.Origin.Y < MinY) MinY = e.Origin.Y;
                if (e.Origin.Y > MaxY) MaxY = e.Origin.Y;
            }
        }
    }
}
