using MelonLoader;
using UnityEngine;

namespace CanneryManufacturing
{

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
