using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using BepInEx;
using BepInEx.Harmony;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoodCompany;
using GoodCompany.Assets;
using GoodCompany.GUI;

namespace GoodCompanyTestMod
{
	public class SessionManagerStatic
	{
		public static SessionManager Instance { get; set; }

		public static AssetBundle CustomAssetBundle { get; set; }
	}

	[HarmonyPatch(typeof(SessionManager))]
	[HarmonyPatch("Initialize")]
	public class SessionManager_Initialize_Patch
	{
		static void Prefix(ref AssetCollection assetCollection)
		{
			SessionManagerStatic.CustomAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "../CustomAssets/mytestbundle"));

			for (int i = 0; i < assetCollection.Models.Assets.Count; i++)
			{
				if (assetCollection.Models.Assets[i].AssetID == "workplaces/tables/bld_tinker_tier01")
				{
					SceneDumper.Dump(assetCollection.Models.Assets[i].Data, @"E:\GC_Test.txt");
					assetCollection.Models.Assets[i].Data =
						SessionManagerStatic.CustomAssetBundle.LoadAsset<GameObject>("Lathe-2");



					// Transform old = assetCollection.Models.Assets[i].Data.transform.Find("tinker_table3x1").transform;
					// Vector3 position = old.position;
					// Quaternion rotation = old.rotation;
					// Vector3 scale = old.localScale;
					//
					// GameObject.Destroy(old.gameObject);
					//
					// GameObject lathe =
					// 	UnityEngine.GameObject.Instantiate<GameObject>(
					// 		SessionManagerStatic.CustomAssetBundle.LoadAsset<GameObject>("Lathe"));
					//
					// lathe.transform.position = position;
					// lathe.transform.rotation = rotation;
					// lathe.transform.localScale = scale;
					//
					// lathe.transform.parent = assetCollection.Models.Assets[i].Data;

					break;
				}
			}
		}
	}

	[HarmonyPatch(typeof(SessionManager))]
	[HarmonyPatch("StartGame")]
	public class SessionManager_StartGame_Patch
	{
		static void Postfix(SessionManager __instance)
		{
			SessionManagerStatic.Instance = __instance;

			ModLogger.Log("{SessionManager} " + $"{SessionManagerStatic.Instance.AssetCollection.Models.Assets.Count} models found in AssetCollection!");

			string path = Path.Combine(Application.dataPath, "AssetCollection.json");
			
			using (StreamWriter writer = new StreamWriter(path, false))
			{
				string json = JsonUtility.ToJson(SessionManagerStatic.Instance.AssetCollection);
				writer.Write(json);
			}
		}
	}
}