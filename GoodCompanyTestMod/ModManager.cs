using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using HarmonyLib;
using BepInEx;
using BepInEx.Harmony;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoodCompany;
using GoodCompany.GUI;
using GoodCompany.Utilities;

namespace GoodCompanyTestMod
{
	public class ModManager : MonoBehaviour
	{
		public static ModManager Instance { get; private set; }

		// public ControllerInputData ControllerInput { get; set; }
		
		private GameRoot _gameRoot;

		private Dictionary<string, BaseModWindow> _windows = new Dictionary<string, BaseModWindow>
		{
			{ "Scene Viewer", new SceneViewer(false) },
			{ "Buildings Test Mod Window", new BuildingsTestModWindow(false) },
			{ "Asset Bundle Test", new AssetBundleTest(false) }
		};

		public void SetGameRoot(GameRoot gameRoot)
		{
			_gameRoot = gameRoot;
			// GameSettings.SetBool("console", true);
			ModLogger.Log("{Mod Manager} " + $"found {GameSettings.Entries.Count} GameSettings entries");
			foreach (var pair in GameSettings.Entries)
			{
				ModLogger.Log("{Mod Manager} Setting \"" + pair.Key + "\" : " + pair.Value);
			}
			ModLogger.Log("{Mod Manager} Console activation key: " + gameRoot.ConsolePrefab.ActivationKey);
		}
		
		void Awake()
		{
			ModLogger.Log("{Mod Manager} Awake()");
			Instance = this;
		}

		void Start()
		{
			foreach (var pair in _windows)
			{
				pair.Value.OnStart();
			}
		}

		void Update()
		{
			// if (Input.GetKeyDown(KeyCode.Keypad1))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)1);
			// else if (Input.GetKeyDown(KeyCode.Keypad2))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)2);
			// else if (Input.GetKeyDown(KeyCode.Keypad3))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)3);
			// else if (Input.GetKeyDown(KeyCode.Keypad4))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)4);
			// else if (Input.GetKeyDown(KeyCode.Keypad5))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)5);
			// else if (Input.GetKeyDown(KeyCode.Keypad6))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)6);
			// else if (Input.GetKeyDown(KeyCode.Keypad7))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)7);
			// else if (Input.GetKeyDown(KeyCode.Keypad8))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)8);
			// else if (Input.GetKeyDown(KeyCode.Keypad9))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)9);
			// else if (Input.GetKeyDown(KeyCode.Tab))
			// 	SessionManagerStatic.Instance.MsgSender.SendFastForwardMessage((byte)100);
		}

		Rect _generalWindowRect = new Rect(20, 20, 200, 200);
		
		void OnGUI()
		{
			_generalWindowRect = GUI.Window(0, _generalWindowRect, RenderGeneralWindow, "SeppahBaws' Test Mod");

			foreach (var pair in _windows)
			{
				if (pair.Value.Activated)
					pair.Value.OnRender();
			}
		}

		void RenderGeneralWindow(int id)
		{
			// using (var scope = new GUILayout.VerticalScope("box"))
			// {
				if (GUILayout.Button("Hello World"))
				{
					ModLogger.Log("{Mod Manager} Hello World");
				}

				if (GUILayout.Button("Main menu"))
				{
					_gameRoot.MainMenu.ExitSession();
				}

				if (GUILayout.Button("Quit Game"))
				{
					_gameRoot.MainMenu.ExitGame();

					ModLogger.Log("Quit Game!");
				}

				foreach (var pair in _windows)
				{
					_windows[pair.Key].Activated = GUILayout.Toggle(pair.Value.Activated, pair.Key);
				}
			// }

			GUI.DragWindow();
		}
	}
}