﻿using RimWorld;
using Verse;

namespace AllowTool {
	/// <summary>
	/// Forbids all forbiddable things in the designated area
	/// </summary>
	public class Designator_Forbid : Designator_SelectableThings {
		public Designator_Forbid(ThingDesignatorDef def) : base(def) {
		}

		public override AcceptanceReport CanDesignateThing(Thing thing) {
			if (thing.Position.Fogged(thing.Map)) return false;
			var comp = thing is ThingWithComps ? (thing as ThingWithComps).GetComp<CompForbiddable>() : null;
			return comp != null && !comp.Forbidden;
		}


		public override void DesignateSingleCell(IntVec3 cell) {
			numThingsDesignated = AllowToolUtility.ToggleForbiddenInCell(cell, Find.VisibleMap, true);
		}
	}
}