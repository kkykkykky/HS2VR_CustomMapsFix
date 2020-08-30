using BepInEx;
using HarmonyLib;
using HS2;
using Manager;
using System.Collections.Generic;
using System.Linq;

namespace HS2_CustomMapsX
{
    [BepInPlugin(GUID, PluginName, Version)]
    [BepInProcess("HoneySelect2VR")]
    public class HS2_CustomMapsX : BaseUnityPlugin
    {
        public const string GUID = "kky.hs2.custommapsx";
        public const string PluginName = "HS2 Custom Maps Extended";
        public const string Version = "1.0.0";

        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(HS2_CustomMapsX), nameof(HS2_CustomMapsX));
            Logger.LogDebug("mappatch awake");
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(HS2VR.VRMapSelectUI), "InitList")]
        private static bool ExpendMapNoRange_Patch(LobbyMapSelectInfoScrollController ___scrollCtrl)
        {
            List<MapInfo.Param> lst = (from map in BaseMap.infoTable.Values
                                       where map.Draw != -1
                                       //where map.No < 50 //remove map.No limit.
                                       select map).ToList<MapInfo.Param>();
            int[] array = (from map in BaseMap.infoTable
                           where map.Value.Draw != -1 && map.Value.Events.Contains(-1)
                           select map.Key).ToArray<int>();
            array = GlobalHS2Calc.ExcludeAchievementMap(array);
            array = GlobalHS2Calc.ExcludeFursRoomAchievementMap(array);
            ___scrollCtrl.SelectInfoClear();
            ___scrollCtrl.Init(lst, array);
            return false;
        }
        /*[HarmonyPatch(typeof(HS2VR.VRMapSelectUI), "<InitList>b__10_1")]
        internal static bool ExpendMapNoRange_Patch(ref bool __result)
        {
            __result = true;
            return false;
        }*/

    }


}
