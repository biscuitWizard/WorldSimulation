﻿using System;
using System.Collections.Generic;

using WorldSimulation.Entities;

namespace WorldSimulation.People.LifeEvents.LifeCycle
{
    public class DeathLifeEvent : ILifeEvent
    {
        public bool CanEncounter(Person person)
        {
            return false;
        }

        public float ScoreEncounter(Person enactor)
        {
            return 0;
        }

        public IList<Tuple<FacetTypeEnum, int>> ScorePersonalityEncounter()
        {
            return new[]
            {
                Tuple.Create(FacetTypeEnum.EasyGoingOrCareless, FacetInfluenceEnum.Minor.ToScore()),
                Tuple.Create(FacetTypeEnum.ConsistentOrCautious, FacetInfluenceEnum.Minor.ToScore()*-1)
            };
        }

        public bool Encounter(Person person)
        {
            person.DeathDate = Universe.CurrentUniverse.CurrentTime;

            return true;
        }
    }
}