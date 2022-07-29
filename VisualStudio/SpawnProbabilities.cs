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
			if (firearmAvailability == FirearmAvailability.None && gearSpawnInfo.PrefabName != "gear_smallgunpowdercan")
			{
				return 0f;
			}

			if (firearmAvailability == FirearmAvailability.Revolver && gearSpawnInfo.PrefabName == "gear_riflereloadingbox")
			{
				return 0f;
			}

			if (firearmAvailability == FirearmAvailability.Rifle && gearSpawnInfo.PrefabName == "gear_revolverreloadingbox")
			{
				return 0f;
			}

			return difficultyLevel switch
			{
				DifficultyLevel.Pilgram => Settings.Instance.pilgramSpawnProbability,
				DifficultyLevel.Voyager => Settings.Instance.voyagerSpawnProbability,
				DifficultyLevel.Stalker => Settings.Instance.stalkerSpawnProbability,
				DifficultyLevel.Interloper => Settings.Instance.interloperSpawnProbability,
				DifficultyLevel.Challenge => Settings.Instance.challengeSpawnProbability,
				DifficultyLevel.Storymode => Settings.Instance.storySpawnProbability,
				_ => 0f,
			};
		}
	}
}
