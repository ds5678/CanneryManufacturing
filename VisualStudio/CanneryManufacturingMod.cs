using MelonLoader;

namespace CanneryManufacturing;

internal class CanneryManufacturingMod : MelonMod
{
	public override void OnApplicationStart()
	{
		SpawnProbabilities.AddToModComponent();
		Settings.Instance.AddToModSettings("Cannery Manufacturing");
	}
}
