using System;
using System.Reflection;

using HarmonyLib;
using BepInEx;
using BepInEx.Harmony;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoodCompany;
using GoodCompany.GUI;

namespace GoodCompanyTestMod
{
	[HarmonyPatch(typeof(GameRoot))]
	[HarmonyPatch("Awake")]
	class GameRoot_Awake_Patch
	{
		private static GameObject _modManager;
		
		static void Postfix(GameRoot __instance)
		{
			_modManager = UnityEngine.Object.Instantiate<GameObject>(new GameObject("SeppahBaws_TestModManager"));
			ModManager manager = _modManager.AddComponent<ModManager>();
			manager.SetGameRoot(__instance);
			
			ModLogger.Log("Game Root Awake!");
		}
	}

	[HarmonyPatch(typeof(GameRoot))]
	[HarmonyPatch("GetVersion")]
	class GameRoot_GetVersion_Patch
	{
		static void Postfix(GameRoot __instance, ref string __result)
		{
			__result += " Modded";
		}
	}
}