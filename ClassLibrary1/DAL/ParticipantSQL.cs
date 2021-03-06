﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVoyage.Core.Entity;

namespace BoVoyage.Core.DAL
{
    class ParticipantSQL
    {

        public List<Participant> GetList()
        {

            using (var contexte = new Contexte())
            {
                return contexte.Participant.ToList();
            }
        }
        public Participant Ajouter(Participant participant)
        {
            using (var contexte = new Contexte())
            {
                contexte.Participant.Add(participant);
                contexte.SaveChanges();
            }
            return participant;
        }
        public void CreerParticipant(Participant participant)
        {
            using (var contexte = new Contexte())
            {
                contexte.Participant.Add(participant);

                contexte.SaveChanges();
            }
        }
        public List<Participant> ListerParticipant()
        {
            using (var contexte = new Contexte())
            {
                return contexte.Participant.
                    OrderBy(x => x.Nom).

                     ToList();
            }

        }

        public void ModifierParticipant(Participant Participant)

        {
            using (var contexte = new Contexte())
            {
                contexte.Participant.Attach(Participant);
                contexte.Entry(Participant).State = EntityState.Modified;
                contexte.SaveChanges();

            }
        }

        public void SupprimerParticipant(int id)
        {
            using (var contexte = new Contexte())
            {
                var Participant = contexte.Participant.Find(id);
                contexte.Entry(Participant).State = EntityState.Deleted;
                contexte.SaveChanges();
            }
        }
    }
}
