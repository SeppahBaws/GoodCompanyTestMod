using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using HarmonyLib;
using BepInEx;
using BepInEx.Harmony;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoodCompany;
using GoodCompany.GUI;
using UnityEngine.Events;

namespace GoodCompany.GUI
{
	static class MainMenu_Extensions
	{
		public static void OnGUI(this MainMenu menu)
		{
			GoodCompanyTestMod.GUIHandler.DrawGUI();
		}
	}
}

namespace GoodCompanyTestMod
{
	static class GUIHandler
	{
		public static void DrawGUI()
		{
			ModLogger.Log("DrawGUI handler is being called!");
			
			if (GUI.Button(new Rect(10, 10, 150, 100), "Hello World"))
			{
				ModLogger.Log("Hello World from custom button!");
			}
		}
	}

	[HarmonyPatch(typeof(MainMenu))]
	[HarmonyPatch("Initialize")]
	class MainMenu_Initialize_Patch
	{
		static void Postfix(MainMenu __instance, ref GUIButtonMainmenu ____quitGameBtn,
			MainMenu.IControl ____control, ref TMP_Text ____version)
		{
			____version.color = Color.magenta;
			____version.margin = Vector4.one;
			____version.transform.position = new Vector3(-10, 5, 1);

			// Don't show the disclaimer when going to the main menu
			typeof(MainMenu).GetMethod("ShowDisclaimer", BindingFlags.NonPublic | BindingFlags.Instance)
				.Invoke(__instance, new object[] { false });


			____quitGameBtn.OnClick.RemoveAllListeners();
			____quitGameBtn.OnClick.AddListener(new UnityAction(____control.QuitToDesktop));


			// Instantiate the text
			// TMP_Text modInfoText = UnityEngine.Object.Instantiate<TMP_Text>(new TMP_Text(), new Vector3(500, 200, 0), Quaternion.identity);
			// // TMP_Text modInfoText = new TMP_Text();
			// modInfoText.text = "Hello World";
			// modInfoText.color = new Color(200, 200, 200);

			// GUIButtonSimple btn = new GUIButtonSimple();

			ModLogger.Log("Main menu initialized! version text should be magenta now.");
		}
	}

	[HarmonyPatch(typeof(MainMenu))]
	[HarmonyPatch("ToggleSettings")]
	class MainMenu_ToggleSettings_Patch
	{
		static void Postfix(MainMenu __instance, TMP_Text ____version)
		{
			____version.color = Color.magenta;
			// ____version.text = "Test Test";
			ModLogger.Log("Toggled settings menu!");
		}
	}

	// /// <summary>
	// /// Shows all the buttons on the main menu
	// /// Note: this fills the entire screen, so I don't recommend using this.
	// /// </summary>
	// [HarmonyPatch(typeof(MainMenu))]
	// [HarmonyPatch("UpdateButtonStates")]
	// class MainMenu_UpdateButtonStates_Patch
	// {
	// 	static void Postfix(
	// 		MainMenu __instance,
	// 		ref GUIButtonMainmenu ____singleBtn,
	// 		ref GUIButtonMainmenu ____headstartBtn,
	// 		ref GUIButtonMainmenu ____levelsBtn,
	// 		ref GUIButtonMainmenu ____multiBtn,
	// 		ref GUIButtonMainmenu ____saveBtn,
	// 		ref GUIButtonMainmenu ____saveAsBtn,
	// 		ref GUIButtonMainmenu ____loadBtn,
	// 		ref GUIButtonMainmenu ____joinBtn,
	// 		ref GUIButtonMainmenu ____settingsBtn,
	// 		ref GUIButtonMainmenu ____aboutBtn,
	// 		ref GUIButtonMainmenu ____backToMapBtn,
	// 		ref GUIButtonMainmenu ____quitSessionBtn,
	// 		ref GUIButtonMainmenu ____quitGameBtn,
	// 		ref GUIButtonMainmenu ____resumeBtn)
	// 	{
	// 		____singleBtn.gameObject.SetActive(true);
	// 		____headstartBtn.gameObject.SetActive(true);
	// 		____levelsBtn.gameObject.SetActive(true);
	// 		____multiBtn.gameObject.SetActive(true);
	// 		____saveBtn.gameObject.SetActive(true);
	// 		____saveAsBtn.gameObject.SetActive(true);
	// 		____loadBtn.gameObject.SetActive(true);
	// 		____joinBtn.gameObject.SetActive(true);
	// 		____settingsBtn.gameObject.SetActive(true);
	// 		____aboutBtn.gameObject.SetActive(true);
	// 		____backToMapBtn.gameObject.SetActive(true);
	// 		____quitSessionBtn.gameObject.SetActive(true);
	// 		____quitGameBtn.gameObject.SetActive(true);
	// 		____resumeBtn.gameObject.SetActive(true);
	// 	}
	// }
}
