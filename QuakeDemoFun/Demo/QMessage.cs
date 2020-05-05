using System;
using System.IO;

namespace QuakeDemoFun
{
    public class QMessage
    {
        public QMessageID ID { get; protected set;  }

        public static QMessage Read(BinaryReader br)
        {
            byte code = br.ReadByte();
            if (code >= 0x80) return new QUpdateEntityMessage(br, code);

            QMessageID type = (QMessageID)code;
            switch (type)
            {
                case QMessageID.Bad:
                case QMessageID.Nop:
                case QMessageID.Disconnect:
                case QMessageID.KilledMonster:
                case QMessageID.FoundSecret:
                case QMessageID.Intermission:
                case QMessageID.SellScreen:
                    return new QMessage() { ID = type };

                case QMessageID.CDTrack: return new QCDTrackMessage(br);
                case QMessageID.Centerprint: return new QCenterprintMessage(br);
                case QMessageID.ClientData: return new QClientDataMessage(br);
                case QMessageID.Damage: return new QDamageMessage(br);
                case QMessageID.LightStyle: return new QLightStyleMessage(br);
                case QMessageID.Particle: return new QParticleMessage(br);
                case QMessageID.Print: return new QPrintMessage(br);
                case QMessageID.ServerInfo: return new QServerInfoMessage(br);
                case QMessageID.SetAngle: return new QSetAngleMessage(br);
                case QMessageID.SetPause: return new QSetPauseMessage(br);
                case QMessageID.SetView: return new QSetViewMessage(br);
                case QMessageID.SignOnNum: return new QSignOnNumMessage(br);
                case QMessageID.Sound: return new QSoundMessage(br);
                case QMessageID.SpawnBaseline: return new QSpawnBaselineMessage(br);
                case QMessageID.SpawnStatic: return new QSpawnStaticMessage(br);
                case QMessageID.SpawnStaticSound: return new QSpawnStaticSoundMessage(br);
                case QMessageID.Stufftext: return new QStufftextMessage(br);
                case QMessageID.UpdateColors: return new QUpdateColorsMessage(br);
                case QMessageID.UpdateFrags: return new QUpdateFragsMessage(br);
                case QMessageID.UpdateName: return new QUpdateNameMessage(br);
                case QMessageID.UpdateStat: return new QUpdateStatMessage(br);
                case QMessageID.TempEntity: return new QTempEntityMessage(br);
                case QMessageID.Time: return new QTimeMessage(br);

                default:
                    throw new NotSupportedException($"Don't know how to read message type: {type}");
            }
        }

        public override string ToString() => ID.ToString();
    }
}