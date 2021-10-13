using GearSpawner;
using System;

namespace CanneryManufacturing
{
	internal static class SpawnProbabilities
	{
		internal static void AddToModComponent()
		{
			SpawnTagManager.AddToTaggedFunctions("CanneryManufacturing", new Func<DifficultyLevel, FirearmAvailability, GearSpawnInfo, float>(GetProbability));
		}
		private static float GetProbability(DifficultyLevel difficultyLevel, FirearmAvailability firearmAvailability, GearSpawnInfo gearSpawnInfo)
		{
			if (firearmAvailability == FirearmAvailability.None && gearSpawnInfo.PrefabName != "gear_smallgunpowdercan") return 0f;
			if (firearmAvailability == FirearmAvailability.Revolver && gearSpawnInfo.PrefabName == "gear_riflereloadingbox") return 0f;
			if (firearmAvailability == FirearmAvailability.Rifle && gearSpawnInfo.PrefabName == "gear_revolverreloadingbox") return 0f;
			switch (difficultyLevel)
			{
				case DifficultyLevel.Pilgram:
					return Settings.instance.pilgramSpawnProbability;
				case DifficultyLevel.Voyager:
					return Settings.instance.voyagerSpawnProbability;
				case DifficultyLevel.Stalker:
					return Settings.instance.stalkerSpawnProbability;
				case DifficultyLevel.Interloper:
					return Settings.instance.interloperSpawnProbability;
				case DifficultyLevel.Challenge:
					return Settings.instance.challengeSpawnProbability;
				case DifficultyLevel.Storymode:
					return Settings.instance.storySpawnProbability;
				default:
					return 0f;
			}
		}
	}
}
