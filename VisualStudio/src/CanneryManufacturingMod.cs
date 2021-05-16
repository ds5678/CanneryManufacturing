using MelonLoader;
using UnityEngine;

namespace CanneryManufacturing
{
    public static class BuildInfo
    {
        public const string Name = "CanneryManufacturing"; // Name of the Mod.  (MUST BE SET)
        public const string Description = "A mod to make the Cannery Workshop more useful."; // Description for the Mod.  (Set as null if none)
        public const string Author = "ds5678"; // Author of the Mod.  (MUST BE SET)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.3.2"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }
    internal class CanneryManufacturingMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            Settings.OnLoad();
            SpawnProbabilities.AddToModComponent();
        }
    }
}
