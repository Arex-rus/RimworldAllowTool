﻿using AllowTool.Context;
using Harmony;
using Verse;

namespace AllowTool.Patches {
	/// <summary>
	/// Intercepts right clicks on supported designators, ignores other interactions
	/// </summary>
	[HarmonyPatch(typeof(Designator))]
	[HarmonyPatch("ProcessInput")]
	internal class Designator_ProcessInput_Patch {
		[HarmonyPrefix]
		public static bool InterceptRightClicksOnSupportedDesignators(Designator __instance) {
			return !DesignatorContextMenuController.TryProcessDesignatorInput(__instance);
		}	 
	}
}