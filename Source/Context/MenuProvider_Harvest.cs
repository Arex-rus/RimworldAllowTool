﻿using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AllowTool.Context {
	public class MenuProvider_Harvest : BaseDesignatorMenuProvider {
		private const string HarvestAllTextKey = "Designator_context_harvest";
		private const string HarvestHomeAreaTextKey = "Designator_context_harvest_home";
		
		public override string EntryTextKey {
			get { return HarvestAllTextKey; }
		}

		public override string SettingId {
			get { return "providerHarvest"; }
		}

		public override Type HandledDesignatorType {
			get { return typeof (Designator_PlantsHarvest); }
		}

		protected override ThingRequestGroup DesignatorRequestGroup {
			get { return ThingRequestGroup.Plant; }
		}

		protected override IEnumerable<FloatMenuOption> ListMenuEntries(Designator designator) {
			yield return MakeMenuOption(designator, HarvestAllTextKey, (des, map) => HarvestAction(designator, map, false));
			yield return MakeMenuOption(designator, HarvestHomeAreaTextKey, (des, map) => HarvestAction(designator, map, true));
		}

		public virtual void HarvestAction(Designator designator, Map map, bool homeAreaOnly) {
			int hitCount = 0;
			var homeArea = map.areaManager.Home;
			foreach (var thing in map.listerThings.ThingsInGroup(DesignatorRequestGroup)) {
				if(!ValidForDesignation(thing)) continue;
				var cellIndex = map.cellIndices.CellToIndex(thing.Position);
				if ((!homeAreaOnly || homeArea.GetCellBool(cellIndex)) && designator.CanDesignateThing(thing).Accepted) {
					designator.DesignateThing(thing);
					hitCount++;
				}
			}
			ReportActionResult(hitCount);
		}
	}
}