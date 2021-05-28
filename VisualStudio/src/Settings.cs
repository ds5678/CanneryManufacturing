using ModSettings;

namespace CanneryManufacturing
{
	internal class Settings : JsonModSettings
	{
		public static Settings instance = new Settings();

		[Section("Gameplay Settings")]
		[Name("Gunpowder Crafting Location")]
		[Description("The default location is at the Ammo Workbench.")]
		[Choice("Anywhere", "Workbench", "Ammo Workbench")]
		public int gunpowderLocationIndex = 2;

		[Name("Start Game With Cannery Code Sheet")]
		[Description("Removes the need to find the code sheet.")]
		public bool startGameWithCanneryCode = false;

		[Name("Revolvers start ruined")]
		[Description("All the rifles you find will be ruined when you find them. You must take them to the Cannery Workshop to be repaired at the Milling Machine.")]
		public bool revolversStartRuined = false;

		[Name("Rifles start ruined")]
		[Description("All the rifles you find will be ruined when you find them. You must take them to the Cannery Workshop to be repaired at the Milling Machine.")]
		public bool riflesStartRuined = false;

		[Name("Distress Pistols start ruined")]
		[Description("All the distress pistols you find will be ruined when you find them. You must take them to the Cannery Workshop to be repaired at the Milling Machine. WARNING: ruined distress pistols get automatically deleted when you place them in a container.")]
		public bool flareGunsStartRuined = false;

		[Section("Spawn Settings")]
		[Name("Pilgram / Very High Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float pilgramSpawnProbability = 70f;

		[Name("Voyager / High Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float voyagerSpawnProbability = 40f;

		[Name("Stalker / Medium Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float stalkerSpawnProbability = 20f;

		[Name("Interloper / Low Loot Custom")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float interloperSpawnProbability = 8f;

		[Name("Wintermute")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float storySpawnProbability = 70f;

		[Name("Challenges")]
		[Description("The percent probability of finding a modded spawn from Cannery Manufacturing on this game mode. Setting to zero disables them on this game mode. If rifles or revolvers are disabled on this mode, their corresponding items will be too.")]
		[Slider(0f, 100f, 101)]
		public float challengeSpawnProbability = 50f;
	}
}
