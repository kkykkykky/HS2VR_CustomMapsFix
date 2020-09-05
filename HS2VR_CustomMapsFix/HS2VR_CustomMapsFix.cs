using BepInEx;
using HarmonyLib;

namespace HS2VR_CustomMapsFix
{
    [BepInPlugin(GUID, PluginName, Version)]
    [BepInProcess("HoneySelect2VR")]
    public class HS2VR_CustomMapsFix : BaseUnityPlugin
    {
        public const string GUID = "kky.hs2vr.custommapsfix";
        public const string PluginName = "HS2 VR Custom Maps Fix";
        public const string Version = "1.0.0";

        private void Awake()
        {
            var harmony = new Harmony(nameof(HS2VR_CustomMapsFix));
            harmony.PatchAll(typeof(HS2VR_CustomMapsFix));

            var iteratorType = typeof(HS2VR.VRMapSelectUI).GetNestedType("<>c", AccessTools.all);
            var iteratorMethod = AccessTools.Method(iteratorType, "<InitList>b__10_1");
            var prefix = new HarmonyMethod(typeof(HS2VR_CustomMapsFix), nameof(RemoveMapNoLimit_Patch));
            harmony.Patch(iteratorMethod, prefix);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(LobbyMapSelectInfoComponent), "SetData")]
        public static void ChangeThumbManifest_Patch(LobbyMapSelectInfoScrollController.ScrollData _info)
        {
            var tnManifestS = _info.info.ThumbnailManifest_S;
            if (tnManifestS != "" && tnManifestS != "abdata") _info.info.ThumbnailManifest_S = "abdata";
        }

        internal static bool RemoveMapNoLimit_Patch(ref bool __result)
        {
            __result = true;
            return false;
        }
    }


}
