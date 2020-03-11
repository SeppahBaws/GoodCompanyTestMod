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

namespace GoodCompanyTestMod
{
	public class ModManager : MonoBehaviour
	{
		private GameRoot _gameRoot;

		private List<string> _sceneObjects = new List<string>();
		private GameObject _inspectedObject = null;
		private List<Transform> _inspectedObjChildren = new List<Transform>();

		public void SetGameRoot(GameRoot gameRoot)
		{
			_gameRoot = gameRoot;
		}
		
		void Awake()
		{
			ModLogger.Log("{Mod Manager} Awake()");
		}

		void Start()
		{
			Transform[] objects = FindObjectsOfType<Transform>(); // doesn't work for some reason...
			// UnityEngine.Object[] objects = FindObjectOfType<MonoBehaviour>(); // works
			
			ModLogger.Log($"Found {objects.Length} items in the game scene!");

			foreach (var obj in objects)
			{
				_sceneObjects.Add(obj.gameObject.name);
			}
		}

		void SelectInspectObject(GameObject obj)
		{
			_inspectedObject = obj;
			_inspectedObject.GetComponentsInChildren<Transform>(_inspectedObjChildren);
		}

		Rect _generalWindowRect = new Rect(20, 20, 200, 120);
		Rect _sceneWindowRect = new Rect(200, 100, 300, 500);
		Rect _inspectWindowRect = new Rect(550, 100, 300, 200);
		
		void OnGUI()
		{
			_generalWindowRect = GUI.Window(0, _generalWindowRect, RenderGeneralWindow, "SeppahBaws' Test Mod");
			_sceneWindowRect = GUI.Window(1, new Rect(_sceneWindowRect), RenderSceneWindow, "Scene view");
			// _inspectWindowRect = GUI.Window(2, new Rect(_inspectWindowRect), RenderInspectWindow, "Object view");
		}

		void RenderGeneralWindow(int id)
		{
			if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
			{
				ModLogger.Log("{Mod Manager} Hello World");
			}

			if (GUI.Button(new Rect(10, 50, 100, 20), "Quit Game"))
			{
				_gameRoot.MainMenu.ExitGame();

				ModLogger.Log("Quit Game!");
			}

			GUI.Label(new Rect(20, 80, 300, 20), $"{_sceneObjects.Count} objects in scene");

			GUI.DragWindow();
		}

		Vector2 sceneScrollPos = Vector2.zero;
		void RenderSceneWindow(int id)
		{
			const float Y_OFFSET = 20.0f;

			GUI.Label(new Rect(20, 20, 300, 20), $"{_sceneObjects.Count} objects in scene");

			sceneScrollPos = GUI.BeginScrollView(new Rect(10, 50, 280, 450), sceneScrollPos, new Rect(0, 0, 300, _sceneObjects.Count * Y_OFFSET));
			for (int i = 0; i < _sceneObjects.Count; i++)
			{
				float yPos = i * Y_OFFSET;

				GUILayout.BeginHorizontal();
				if (GUI.Button(new Rect(0, yPos, 20, 20), "+"))
				{
					// SelectInspectObject(_sceneObjects[i]);
				}
				GUI.Label(new Rect(30, yPos, 300, 20), _sceneObjects[i]);
				GUILayout.EndHorizontal();
			}
			GUI.EndScrollView();

			GUI.DragWindow();
		}

		// void RenderInspectWindow(int id)
		// {
		// 	if (_inspectedObject == null)
		// 		return;
		//
		// 	GUI.Label(new Rect(20, 20, 300, 20), $"{_inspectedObjChildren.Count} children in inspected object");
		//
		// 	const float Y_OFFSET = 20.0f;
		//
		// 	for (int i = 0; i < _inspectedObjChildren.Count; i++)
		// 	{
		// 		GUI.Label(new Rect(20, 50 + i * Y_OFFSET, 300, 20), _inspectedObjChildren[i].gameObject.name);
		// 	}
		//
		// 	GUI.DragWindow();
		// }
	}
}