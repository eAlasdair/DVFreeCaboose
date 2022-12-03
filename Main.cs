using UnityModManagerNet;
using HarmonyLib;
using DV;

namespace FreeCabooseMod
{
    public static class Main
    {
        public static UnityModManager.ModEntry entry;

        /**
         * Mod entrypoint
         * Returns true if load is successful
         */
        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            entry = modEntry;

            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();

            modEntry.OnUnload = OnUnload;

            return true;
        }

        /**
         * Unpatch the mod (hopefully) safely
         */
        private static bool OnUnload(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.UnpatchAll(modEntry.Info.Id);
            return true;
        }

        /**
         * Set the summon price of all crew vehicles to 0
         */
        [HarmonyPatch(typeof(CommsRadioCrewVehicle), "SummonPrice", MethodType.Getter)]
        public static class SummonPricePatch
        {
            static void Postfix(ref float __result)
            {
                __result = 0f;
            }
        }
    }
}
