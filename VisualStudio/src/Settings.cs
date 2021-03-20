using ModSettings;

namespace CanneryManufacturing
{
	internal class CanneryManufacturingModSettings : JsonModSettings
	{
		[Name("Gunpowder Crafting Location")]
		[Description("The default location is at the Ammo Workbench.")]
		[Choice("Anywhere", "Workbench", "Ammo Workbench")]
		public int gunpowderLocationIndex = 2;
	}
	internal static class Settings
	{
		public static CanneryManufacturingModSettings options;
		public static void OnLoad()
		{
			options = new CanneryManufacturingModSettings();
			options.AddToModSettings("Cannery Manufacturing");
		}
	}
}
