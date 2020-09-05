using BepInEx;
using HarmonyLib;

namespace HS2_CustomMapsX
{
    [BepInPlugin(GUID, PluginName, Version)]
    [BepInProcess("HoneySelect2VR")]
    public class HS2_CustomMapsX : BaseUnityPlugin
    {
        public const string GUID = "kky.hs2.custommapsx";
        public const string PluginName = "HS2 Custom Maps Extended";
        public const string Version = "0.2.0";

        private void Awake()
        {
            var harmony = new Harmony(nameof(HS2_CustomMapsX));
            //harmony.PatchAll(typeof(HS2_CustomMapsX));

            var iteratorType = typeof(HS2VR.VRMapSelectUI).GetNestedType("<>c", AccessTools.all);
            var iteratorMethod = AccessTools.Method(iteratorType, "<InitList>b__10_1");
            var prefix = new HarmonyMethod(typeof(HS2_CustomMapsX), nameof(RemoveMapNoLimit_Patch));
            harmony.Patch(iteratorMethod, prefix);
        }

        internal static bool RemoveMapNoLimit_Patch(ref bool __result)
        {
            __result = true;
            return false;
        }
    }


}
