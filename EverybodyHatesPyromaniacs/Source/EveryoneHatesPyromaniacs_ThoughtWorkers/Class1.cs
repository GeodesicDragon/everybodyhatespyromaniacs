using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RimWorld;
using Verse;

namespace EveryoneHatesPyromaniacs
{

    [DefOf]
    public static class MyDefOf
    {
        public static TraitDef Pyromaniac;

        static MyDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MyDefOf));
        }
    }

    // EVERYONE HATES PYROMANIACS!

    public class ThoughtWorker_EveryoneHatesPyromaniacs : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (!p.RaceProps.Humanlike)
            {
                return false;
            }
            if (!otherPawn.RaceProps.Humanlike)
            {
                return false;
            }
            if (!p.story.traits.HasTrait(MyDefOf.Pyromaniac) & (!otherPawn.story.traits.HasTrait(MyDefOf.Pyromaniac))) // If neither pawn has the trait
                {
                return false;
            }
            if (p.story.traits.HasTrait(MyDefOf.Pyromaniac) & (otherPawn.story.traits.HasTrait(MyDefOf.Pyromaniac))) // If both pawns hav the trait
            {
                return false;
            }
            if (!p.story.traits.HasTrait(MyDefOf.Pyromaniac) & (otherPawn.story.traits.HasTrait(MyDefOf.Pyromaniac))) // If one pawn doesn't have the trait but the other does
            {
                return true;
            }
            return true;
        }
    }

    public class Thought_PyromaniacHatedByEveryone : Thought_SituationalSocial
    {
        public override float OpinionOffset()
        {
            if (ThoughtUtility.ThoughtNullified(pawn, def))
            {
                return 0f;
            }

            if (otherPawn.story.traits.HasTrait(MyDefOf.Pyromaniac))
            {
                return -20f;
            }
            return 0f;
        }
    }
}