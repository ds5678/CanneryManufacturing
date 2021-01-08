using Harmony;
using System;
using UnhollowerBaseLib;
using UnityEngine;

namespace CanneryManufacturing
{
	internal static class Patches
	{

		private const string IMPROVISED_KNIFE_NAME = "GEAR_KnifeImprovised";
		private const string HUNTING_KNIFE_NAME = "GEAR_Knife";
		private const string IMPROVISED_HATCHET_NAME = "GEAR_HatchetImprovised";
		private const string HUNTING_HATCHET_NAME = "GEAR_Hatchet";
		private const string DISTRESS_PISTOL_NAME = "GEAR_FlareGun";
		private const string SCRAP_METAL_NAME = "GEAR_ScrapMetal";
		private const string RED_FLARE_NAME = "GEAR_FlareA";
		private const string BLUE_FLARE_NAME = "GEAR_BlueFlare";
		private const string FLARE_AMMO_NAME = "GEAR_FlareGunAmmoSingle";
		private const string GUNPOWDER_CAN_NAME = "GEAR_GunpowderCan";
		private const string BIRCH_NAME = "GEAR_BirchSaplingDried";
		private const string ARROW_SHAFT_NAME = "GEAR_ArrowShaft";
		private const string HUNTING_KNIFE_CRAFTING_ICON_NAME = "ico_CraftItem__Knife";
		private const string HUNTING_HATCHET_CRAFTING_ICON_NAME = "ico_CraftItem__Hatchet";
		private const string FLARE_AMMO_CRAFTING_ICON_NAME = "ico_CraftItem__FlareGunAmmoSingle";

		//Convert improvised knives into hunting knives with scrap metal
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddKnifeConversionRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(2) { [0] = GetGearItemPrefab(IMPROVISED_KNIFE_NAME), [1] = GetGearItemPrefab(SCRAP_METAL_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(2) { [0] = 1, [1] = 1 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = null;
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(0);

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(HUNTING_KNIFE_NAME);
				blueprint.m_CraftedResultCount = 1;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.AmmoWorkbench;
				blueprint.m_DurationMinutes = 60;
				blueprint.m_CraftingAudio = "PLAY_CRAFTINGGENERIC";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
		}

		//Convert improvised hatchets into regular hatchets with scrap metal
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddHatchetConversionRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(2) { [0] = GetGearItemPrefab(IMPROVISED_HATCHET_NAME), [1] = GetGearItemPrefab(SCRAP_METAL_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(2) { [0] = 1, [1] = 1 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = null;
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(0);

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(HUNTING_HATCHET_NAME);
				blueprint.m_CraftedResultCount = 1;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.AmmoWorkbench;
				blueprint.m_DurationMinutes = 60;
				blueprint.m_CraftingAudio = "PLAY_CRAFTINGGENERIC";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
		}

		//More efficient arrow shaft crafting
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddArrowShaftRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(BIRCH_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 1 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HUNTING_KNIFE_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(3) { 
					[0] = GetToolsItemPrefab(IMPROVISED_KNIFE_NAME), 
					[1] = GetToolsItemPrefab(HUNTING_HATCHET_NAME), 
					[2] = GetToolsItemPrefab(IMPROVISED_HATCHET_NAME) };

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(ARROW_SHAFT_NAME);
				blueprint.m_CraftedResultCount = 4;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.AmmoWorkbench;
				blueprint.m_DurationMinutes = 60;
				blueprint.m_CraftingAudio = "PLAY_CRAFTINGGENERIC";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Make flare shells from gunpowder
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddGunpowderFlareAmmoRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(0) { };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(0) { };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0.05f;
				blueprint.m_RequiredTool = null;
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(0);

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(FLARE_AMMO_NAME);
				blueprint.m_CraftedResultCount = 1;
				
				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.AmmoWorkbench;
				blueprint.m_DurationMinutes = 15;
				blueprint.m_CraftingAudio = "Play_CraftingGunPowder";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Harvest red flares for gunpowder
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddRedFlareGunpowderRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(RED_FLARE_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 3 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HUNTING_KNIFE_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(1){ [0] = GetToolsItemPrefab(IMPROVISED_KNIFE_NAME) };

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(GUNPOWDER_CAN_NAME);
				blueprint.m_CraftedResultCount = 1;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.Workbench;
				blueprint.m_DurationMinutes = 90; //actually 45 minutes
				blueprint.m_CraftingAudio = "Play_CraftingGunPowder";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Harvest red flares to make flare shells
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddRedFlareFlareAmmoRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(RED_FLARE_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 1 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HUNTING_KNIFE_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(1) { [0] = GetToolsItemPrefab(IMPROVISED_KNIFE_NAME) };

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(FLARE_AMMO_NAME);
				blueprint.m_CraftedResultCount = 4;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.AmmoWorkbench;
				blueprint.m_DurationMinutes = 150; //actually 75 minutes, hunting knife halves it
				blueprint.m_CraftingAudio = "Play_CraftingGunPowder";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Harvest blue flares for gunpowder
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddBlueFlareGunpowderRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(BLUE_FLARE_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 3 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HUNTING_KNIFE_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(1) { [0] = GetToolsItemPrefab(IMPROVISED_KNIFE_NAME) };

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(GUNPOWDER_CAN_NAME);
				blueprint.m_CraftedResultCount = 1;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.Workbench;
				blueprint.m_DurationMinutes = 90; //actually 45 minutes
				blueprint.m_CraftingAudio = "Play_CraftingGunPowder";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Harvest blue flares to make flare shells
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddBlueFlareFlareAmmoRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(BLUE_FLARE_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 1 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HUNTING_KNIFE_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(1) { [0] = GetToolsItemPrefab(IMPROVISED_KNIFE_NAME) };

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(FLARE_AMMO_NAME);
				blueprint.m_CraftedResultCount = 4;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.AmmoWorkbench;
				blueprint.m_DurationMinutes = 150; //actually 75 minutes, 60 for the shells and 15 for opening the flare
				blueprint.m_CraftingAudio = "Play_CraftingGunPowder";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}


		[HarmonyPatch(typeof(Panel_Crafting), "ItemPassesFilter")]
		private static class ShowRecipesInToolsSection
		{
			internal static void Postfix(Panel_Crafting __instance, ref bool __result, BlueprintItem bpi)
			{
				if (bpi?.m_CraftedResult?.name == HUNTING_KNIFE_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
				if (bpi?.m_CraftedResult?.name == HUNTING_HATCHET_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
				if (bpi?.m_CraftedResult?.name == ARROW_SHAFT_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
				if (bpi?.m_CraftedResult?.name == GUNPOWDER_CAN_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
				if (bpi?.m_CraftedResult?.name == FLARE_AMMO_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
			}
		}

		[HarmonyPatch(typeof(BlueprintDisplayItem), "Setup")]
		private static class FixRecipeIcons
		{
			internal static void Postfix(BlueprintDisplayItem __instance, BlueprintItem bpi)
			{
				//Hunting Knife Crafting Icon fix
				if (bpi?.m_CraftedResult?.name == HUNTING_KNIFE_NAME)
				{
					Texture2D knifeTexture = Utils.GetCachedTexture(HUNTING_KNIFE_CRAFTING_ICON_NAME);
					if (!knifeTexture)
					{
						knifeTexture = CanneryManufacturingMod.assetBundle.LoadAsset(HUNTING_KNIFE_CRAFTING_ICON_NAME).Cast<Texture2D>();
						Utils.CacheTexture(HUNTING_KNIFE_CRAFTING_ICON_NAME, knifeTexture);
					}
					__instance.m_Icon.mTexture = knifeTexture;
				}
				//Hatchet Crafting Icon fix
				if (bpi?.m_CraftedResult?.name == HUNTING_HATCHET_NAME)
				{
					Texture2D hatchetTexture = Utils.GetCachedTexture(HUNTING_HATCHET_CRAFTING_ICON_NAME);
					if (!hatchetTexture)
					{
						hatchetTexture = CanneryManufacturingMod.assetBundle.LoadAsset(HUNTING_HATCHET_CRAFTING_ICON_NAME).Cast<Texture2D>();
						Utils.CacheTexture(HUNTING_HATCHET_CRAFTING_ICON_NAME, hatchetTexture);
					}
					__instance.m_Icon.mTexture = hatchetTexture;
				}
				//Flare Shell Crafting Icon fix
				if (bpi?.m_CraftedResult?.name == FLARE_AMMO_NAME)
				{
					Texture2D flareAmmoTexture = Utils.GetCachedTexture(FLARE_AMMO_CRAFTING_ICON_NAME);
					if (!flareAmmoTexture)
					{
						flareAmmoTexture = CanneryManufacturingMod.assetBundle.LoadAsset(FLARE_AMMO_CRAFTING_ICON_NAME).Cast<Texture2D>();
						Utils.CacheTexture(FLARE_AMMO_CRAFTING_ICON_NAME, flareAmmoTexture);
					}
					__instance.m_Icon.mTexture = flareAmmoTexture;
				}
			}
		}

		//Distress Pistol and Crampons repairable at the Milling Machine
		[HarmonyPatch(typeof(GearItem), "Awake")]

		private static class MillingMachineAdditions
		{
			internal static void Postfix(GearItem __instance)
			{
				String name = __instance.name;
				if ( name.ToLower().Contains("flaregun") && !name.ToLower().Contains("ammo") )
				{
					__instance.m_Millable = __instance.gameObject.AddComponent<Millable>();

					__instance.m_Millable.m_CanRestoreFromWornOut = true;
					__instance.m_Millable.m_RecoveryDurationMinutes = 180;
					__instance.m_Millable.m_RepairDurationMinutes = 30;
					__instance.m_Millable.m_RepairRequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RepairRequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 1 };
					__instance.m_Millable.m_RestoreRequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RestoreRequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 3 };
					__instance.m_Millable.m_Skill = SkillType.None;

				}
				if (name.ToLower().Contains("crampons"))
				{
					__instance.m_Millable = __instance.gameObject.AddComponent<Millable>();

					__instance.m_Millable.m_CanRestoreFromWornOut = true;
					__instance.m_Millable.m_RecoveryDurationMinutes = 210;
					__instance.m_Millable.m_RepairDurationMinutes = 30;
					__instance.m_Millable.m_RepairRequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RepairRequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 1 };
					__instance.m_Millable.m_RestoreRequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(SCRAP_METAL_NAME) };
					__instance.m_Millable.m_RestoreRequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 4 };
					__instance.m_Millable.m_Skill = SkillType.None;

				}
			}
			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
		}
	}
}
