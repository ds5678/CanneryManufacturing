using ModComponentMapper;
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
                    return Settings.options.pilgramSpawnProbability;
                case DifficultyLevel.Voyager:
                    return Settings.options.voyagerSpawnProbability;
                case DifficultyLevel.Stalker:
                    return Settings.options.stalkerSpawnProbability;
                case DifficultyLevel.Interloper:
                    return Settings.options.interloperSpawnProbability;
                case DifficultyLevel.Challenge:
                    return Settings.options.challengeSpawnProbability;
                case DifficultyLevel.Storymode:
                    return Settings.options.storySpawnProbability;
                default:
                    return 0f;
            }
        }
    }
}
