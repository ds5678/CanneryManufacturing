using Harmony;
using UnityEngine;

namespace CanneryManufacturing
{
    internal static class Patches
    {
        private const string SCRAP_METAL_NAME = "GEAR_ScrapMetal";

        //Change Gunpowder Crafting Location
        [HarmonyPatch(typeof(Panel_Crafting), "ItemPassesFilter")]
        private static class RecipesInToolsRecipes
        {
            internal static void Postfix(Panel_Crafting __instance, BlueprintItem bpi)
            {
                if (bpi?.m_CraftedResult?.name == "GEAR_GunpowderCan")
                {
                    switch (Settings.options.gunpowderLocationIndex)
                    {
                        case 0:
                            bpi.m_RequiredCraftingLocation = CraftingLocation.Anywhere;
                            break;
                        case 1:
                            bpi.m_RequiredCraftingLocation = CraftingLocation.Workbench;
                            break;
                        case 2:
                            bpi.m_RequiredCraftingLocation = CraftingLocation.AmmoWorkbench;
                            break;
                        default:
                            MelonLoader.MelonLogger.LogError("Gunpowder setting returned an unacceptable value");
                            break;
                    }
                }
            }
        }


        //Crampons repairable at the Milling Machine
        //Distress Pistol handled with existing json because it's more efficient
        //This patch also handles ruining firearms on start
        [HarmonyPatch(typeof(GearItem), "Awake")]

        private static class GearItem_Awake
        {
            internal static void Postfix(GearItem __instance)
            {
                if (__instance.name == "GEAR_Crampons(Clone)")
                {
                    __instance.m_Millable = __instance.gameObject.AddComponent<Millable>();

                    __instance.m_Millable.m_CanRestoreFromWornOut = true;
                    __instance.m_Millable.m_RecoveryDurationMinutes = 210;
                    __instance.m_Millable.m_RepairDurationMinutes = 30;
                    __instance.m_Millable.m_RepairRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
                    __instance.m_Millable.m_RepairRequiredGearUnits = new int[] { 1 };
                    __instance.m_Millable.m_RestoreRequiredGear = new GearItem[] { GetGearItemPrefab(SCRAP_METAL_NAME) };
                    __instance.m_Millable.m_RestoreRequiredGearUnits = new int[] { 4 };
                    __instance.m_Millable.m_Skill = SkillType.None;
                }

                if (__instance.m_BeenInspected) return;
                if (Settings.options.flareGunsStartRuined && __instance.name == "GEAR_FlareGun(Clone)") __instance.ForceWornOut();
                else if (Settings.options.revolversStartRuined && __instance.name == "GEAR_Revolver(Clone)") __instance.ForceWornOut();
                else if (Settings.options.riflesStartRuined && __instance.name == "GEAR_Rifle(Clone)") __instance.ForceWornOut();
            }
            private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
        }


        //Remove need for the cannery code
        [HarmonyPatch(typeof(Keypad), "Update")]
        internal class UnlockDoor
        {
            private static void Postfix(Keypad __instance)
            {
                if (Settings.options.startGameWithCanneryCode && __instance.m_Locked && GameManager.m_ActiveScene == "CanneryRegion")
                {
                    __instance.m_Locked = false;
                }
            }
        }
    }
}
