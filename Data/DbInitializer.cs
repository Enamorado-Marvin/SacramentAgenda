using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SacramentAgenda.Models;

namespace SacramentAgenda.Data
{
    public class DbInitializer
    {
        public static void Initialize(UnitContext context)
        {
            context.Database.EnsureCreated();

            // Look for any agenda.
            if (context.Agendas.Any())
            {
                return;   // DB has been seeded
            }

            var agendas = new Agenda[]
            {
            new Agenda{SacramentMeetingDate=DateTime.Parse("2019-12-22"),PresidingLeader="Bishop Carson",ConductingLeader="Brother Alexander",Invocation="Sister Monson",SpecialNumber="Special number by the choir",Benediction="Brother Nelson"},
            new Agenda{SacramentMeetingDate=DateTime.Parse("2019-12-22"),PresidingLeader="Bishop Carson",ConductingLeader="Brother Alexander",Invocation="Sister Monson",SpecialNumber="Special number by the choir",Benediction="Brother Nelson"}
            };
            foreach (Agenda a in agendas)
            {
                context.Agendas.Add(a);
            }
            context.SaveChanges();

            var speakers = new Speaker[]
            {
            new Speaker{SpeakerID=1, SpeakerName="Brother Smith",Subject="Faith"},
            new Speaker{SpeakerID=1, SpeakerName="Sister Smith",Subject="Prayer"},
            new Speaker{SpeakerID=1, SpeakerName="Brother Calvin",Subject="Repentance"},
            new Speaker{SpeakerID=1, SpeakerName="Brother Benson",Subject="Ordinances"},
            new Speaker{SpeakerID=1, SpeakerName="Sister Williams",Subject="Teach the Gospel"},
            new Speaker{SpeakerID=1, SpeakerName="Brother Ramirez",Subject="Come unto Christ"}
            };
            foreach (Speaker s in speakers)
            {
                context.Speakers.Add(s);
            }
            context.SaveChanges();

            var assignments = new Assignment[]
            {
            new Assignment{AgendaID=1,SpeakerID=1},
            new Assignment{AgendaID=1,SpeakerID=2},
            new Assignment{AgendaID=1,SpeakerID=3},
            new Assignment{AgendaID=2,SpeakerID=4},
            new Assignment{AgendaID=2,SpeakerID=5},
            new Assignment{AgendaID=2,SpeakerID=6}
            };
            foreach (Assignment e in assignments)
            {
                context.Assignments.Add(e);
            }
            context.SaveChanges();            
        }
    }
}

