using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeDemoFun
{
    public class GameState
    {
        const int Limit = 10;

        public GameState(ParsedDemo dem, float time)
        {
            Accumulator = "";
            Entities = new Dictionary<short, Entity>();
            Messages = new List<string>();
            Parent = dem;
            Stats = new Dictionary<StatIndex, int>();
            Temps = new List<Temp>();
            Time = time;
        }

        public string Accumulator { get; private set; }
        public Dictionary<short, Entity> Entities { get; private set; }
        public List<string> Messages { get; private set; }
        public ParsedDemo Parent { get; private set; }
        public Dictionary<StatIndex, int> Stats { get; private set; }
        public List<Temp> Temps { get; private set; }
        public float Time { get; private set; }

        public int Stat(StatIndex i) => Stats.ContainsKey(i) ? Stats[i] : 0;

        public GameState Advance(float time)
        {
            float delta = time - Time;
            GameState next = new GameState(Parent, time);
            foreach (var e in Entities)
                next.Entities.Add(e.Key, e.Value.Clone());

            foreach (var m in Messages)
                next.Messages.Add(m);

            foreach (var sp in Stats)
                next.Stats[sp.Key] = sp.Value;

            foreach (var t in Temps)
            {
                if (t.Duration >= delta)
                {
                    var tclone = t.Clone();
                    tclone.Duration = t.Duration - delta;
                    next.Temps.Add(tclone);
                }
            }

            return next;
        }

        public void Apply(QMessage msg)
        {
            switch (msg.ID)
            {
                case QMessageID.Centerprint:
                    Centerprint(msg as QCenterprintMessage);
                    break;

                case QMessageID.FoundSecret:
                    Stats[StatIndex.FoundSecrets]++;
                    break;

                case QMessageID.KilledMonster:
                    Stats[StatIndex.KilledMonsters]++;
                    break;

                case QMessageID.Print:
                    Print(msg as QPrintMessage);
                    break;

                case QMessageID.SpawnBaseline:
                    SpawnBaseline(msg as QSpawnBaselineMessage);
                    break;

                case QMessageID.TempEntity:
                    TempEntity(msg as QTempEntityMessage);
                    break;

                case QMessageID.UpdateEntity:
                    UpdateEntity(msg as QUpdateEntityMessage);
                    break;

                case QMessageID.UpdateStat:
                    UpdateStat(msg as QUpdateStatMessage);
                    break;
            }
        }

        private void Centerprint(QCenterprintMessage msg)
        {
            AddMessage(msg.Message + "\n");
        }

        private void Print(QPrintMessage msg)
        {
            AddMessage(msg.Text);
        }

        private void SpawnBaseline(QSpawnBaselineMessage msg)
        {
            Entities[msg.Entity] = new Entity()
            {
                Parent = this,
                Number = msg.Entity,
                Angles = msg.Angles,
                Colormap = msg.Colormap,
                Frame = msg.Frame,
                ModelIndex = msg.ModelIndex,
                Origin = msg.Origin,
                Skin = msg.Skin,
            };
        }

        private void TempEntity(QTempEntityMessage msg)
        {
            switch (msg.Type)
            {
                case TempEntityType.Lightning1:
                case TempEntityType.Lightning2:
                case TempEntityType.Lightning3:
                case TempEntityType.Unknown_13:
                    Temps.Add(new Lightning(msg.Origin, msg.TraceEndpos));
                    break;

                case TempEntityType.Gunshot:
                case TempEntityType.Spike:
                case TempEntityType.SuperSpike:
                case TempEntityType.WizSpike:
                case TempEntityType.KnightSpike:
                    Temps.Add(new Gunshot(msg.Origin));
                    break;
            }
        }

        private void UpdateEntity(QUpdateEntityMessage msg)
        {
            Entity e;
            if (!Entities.ContainsKey(msg.Entity))
            {
                e = new Entity() { Parent = this, Number = msg.Entity };
                Entities[msg.Entity] = e;
            }
            else
                e = Entities[msg.Entity];

            if (msg.AnglesX.HasValue) e.Angles.X = msg.AnglesX.Value;
            if (msg.AnglesY.HasValue) e.Angles.Y = msg.AnglesY.Value;
            if (msg.AnglesZ.HasValue) e.Angles.Z = msg.AnglesZ.Value;
            if (msg.Colormap.HasValue) e.Colormap = msg.Colormap.Value;
            if (msg.Effects.HasValue) e.Effects = msg.Effects.Value;
            if (msg.Frame.HasValue) e.Frame = msg.Frame.Value;
            if (msg.ModelIndex.HasValue) e.ModelIndex = msg.ModelIndex.Value;
            if (msg.OriginX.HasValue) e.Origin.X = msg.OriginX.Value;
            if (msg.OriginY.HasValue) e.Origin.Y = msg.OriginY.Value;
            if (msg.OriginZ.HasValue) e.Origin.Z = msg.OriginZ.Value;
            if (msg.Skin.HasValue) e.Skin = msg.Skin.Value;
        }

        private void UpdateStat(QUpdateStatMessage msg)
        {
            Stats[msg.Index] = msg.Value;
        }

        private void AddMessage(string text)
        {
            Accumulator += text;
            if (Accumulator.EndsWith("\n"))
            {
                Messages.Add(Accumulator);
                Accumulator = "";
                if (Messages.Count > Limit) Messages.RemoveAt(0);
            }
        }
    }
}
