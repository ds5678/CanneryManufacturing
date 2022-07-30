extern alias Hinterland;
using HarmonyLib;
using Hinterland;
using ModComponent.Utils;
using System;
using UnityEngine;

namespace CanneryManufacturing;

internal static class Patches
{
	//Change Gunpowder Crafting Location
	[HarmonyPatch(typeof(BlueprintDisplayItem), "Setup")]
	private static class RecipesInToolsRecipes
	{
		internal static void Postfix(BlueprintItem bpi)
		{
			if (bpi.m_CraftedResult?.name == "GEAR_GunpowderCan")
			{
				bpi.m_RequiredCraftingLocation = Settings.Instance.gunpowderLocationIndex switch
				{
					0 => CraftingLocation.Anywhere,
					1 => CraftingLocation.Workbench,
					2 => CraftingLocation.AmmoWorkbench,
					_ => throw new InvalidOperationException("Gunpowder setting returned an unacceptable value"),
				};
			}
		}
	}


	//This patch handles ruining firearms on start
	[HarmonyPatch(typeof(GearItem), "Awake")]
	private static class GearItem_Awake
	{
		private const string SCRAP_METAL_NAME = "GEAR_ScrapMetal";
		
		internal static void Postfix(GearItem __instance)
		{
			if (NameUtils.NormalizeName(__instance.name) == "GEAR_Crampons")
			{
				__instance.m_Millable = ComponentUtils.GetOrCreateComponent<Millable>(__instance.gameObject);

				__instance.m_Millable.m_CanRestoreFromWornOut = true;
				__instance.m_Millable.m_RecoveryDurationMinutes = 210;
				__instance.m_Millable.m_RepairDurationMinutes = 30;
				__instance.m_Millable.m_RepairRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
				__instance.m_Millable.m_RepairRequiredGearUnits = new int[] { 1 };
				__instance.m_Millable.m_RestoreRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
				__instance.m_Millable.m_RestoreRequiredGearUnits = new int[] { 4 };
				__instance.m_Millable.m_Skill = SkillType.None;
			}
			else if (NameUtils.NormalizeName(__instance.name) == "GEAR_Flaregun")
			{
				__instance.m_Millable = ComponentUtils.GetOrCreateComponent<Millable>(__instance.gameObject);

				__instance.m_Millable.m_CanRestoreFromWornOut = true;
				__instance.m_Millable.m_RecoveryDurationMinutes = 180;
				__instance.m_Millable.m_RepairDurationMinutes = 30;
				__instance.m_Millable.m_RepairRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
				__instance.m_Millable.m_RepairRequiredGearUnits = new int[] { 1 };
				__instance.m_Millable.m_RestoreRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
				__instance.m_Millable.m_RestoreRequiredGearUnits = new int[] { 3 };
				__instance.m_Millable.m_Skill = SkillType.None;
			}
			else if (__instance.m_BeenInspected)
			{
				return;
			}
			else if (Settings.Instance.flareGunsStartRuined && NameUtils.NormalizeName(__instance.name) == "GEAR_FlareGun")
			{
				__instance.ForceWornOut();
			}
			else if (Settings.Instance.revolversStartRuined && NameUtils.NormalizeName(__instance.name) == "GEAR_Revolver")
			{
				__instance.ForceWornOut();
			}
			else if (Settings.Instance.riflesStartRuined && NameUtils.NormalizeName(__instance.name) == "GEAR_Rifle")
			{
				__instance.ForceWornOut();
			}
		}
		
		private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
	}

	
	//Remove need for the cannery code
	[HarmonyPatch(typeof(Keypad), "Update")]
	private static class UnlockDoor
	{
		private static void Postfix(Keypad __instance)
		{
			if (Settings.Instance.startGameWithCanneryCode && __instance.m_Locked && GameManager.m_ActiveScene == "CanneryRegion")
			{
				__instance.m_Locked = false;
			}
		}
	}
}
