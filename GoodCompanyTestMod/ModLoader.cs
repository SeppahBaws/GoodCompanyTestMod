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
	[BepInPlugin("com.github.seppahbaws.gc-testmod", "Test Mod", "1.0.0.0")]
	public class ModLoader : BaseUnityPlugin
	{
		void Awake()
		{
			Harmony harmony = new Harmony("com.github.seppahbaws.gc-testmod");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
			ModLogger.Log("Loaded!");
		}
	}
}
