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

		private List<GameObject> _sceneObjects = new List<GameObject>();
		private GameObject _inspectedObject = null;
		private List<Transform> _inspectedObjChildren = new List<Transform>();
		private List<Component> _inspectedObjComponents = new List<Component>();

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
			RefreshSceneTree();
		}

		void RefreshSceneTree()
		{
			_sceneObjects = GameObjectUtil.FindSceneRoots();
			_sceneObjects.OrderBy(obj => obj.name).ToList();

			ModLogger.Log($"Found {_sceneObjects.Count} roots in the game scene");
		}

		void SelectInspectObject(GameObject obj)
		{
			_inspectedObject = obj;
			
			_inspectedObjChildren.Clear();
			_inspectedObjComponents.Clear();

			for (int i = 0; i < _inspectedObject.transform.childCount; i++)
			{
				_inspectedObjChildren.Add(_inspectedObject.transform.GetChild(i));
			}

			_inspectedObjComponents = _inspectedObject.GetComponents<Component>().ToList();
		}

		Rect _generalWindowRect = new Rect(20, 20, 200, 120);
		Rect _sceneWindowRect = new Rect(200, 100, 300, 500);
		Rect _inspectChildrenWindowRect = new Rect(550, 100, 300, 500);
		Rect _inspectComponentsWindowRect = new Rect(1050, 100, 300, 500);
		
		void OnGUI()
		{
			_generalWindowRect = GUI.Window(0, _generalWindowRect, RenderGeneralWindow, "SeppahBaws' Test Mod");
			_sceneWindowRect = GUI.Window(1, _sceneWindowRect, RenderSceneWindow, "Scene view");

			if (_inspectedObject != null)
			{
				_inspectChildrenWindowRect = GUI.Window(2, _inspectChildrenWindowRect, RenderInspectChildrenWindow,
					"Object Children view");
				_inspectComponentsWindowRect = GUI.Window(3, _inspectComponentsWindowRect,
					RenderInspectComponentsWindow, "Object Components view");
			}
		}

		void RenderGeneralWindow(int id)
		{
			if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
			{
				ModLogger.Log("{Mod Manager} Hello World");
			}

			if (GUI.Button(new Rect(10, 50, 100, 20), "Main menu"))
			{
				_gameRoot.MainMenu.ExitSession();
			}

			if (GUI.Button(new Rect(10, 80, 100, 20), "Quit Game"))
			{
				_gameRoot.MainMenu.ExitGame();

				ModLogger.Log("Quit Game!");
			}

			GUI.DragWindow();
		}

		Vector2 sceneScrollPos = Vector2.zero;
		void RenderSceneWindow(int id)
		{
			const float Y_OFFSET = 20.0f;

			GUI.Label(new Rect(20, 20, 300, 20), $"{_sceneObjects.Count} roots in scene");

			if (GUI.Button(new Rect(150, 20, 100, 20), "Refresh scene"))
			{
				RefreshSceneTree();
			}

			sceneScrollPos = GUI.BeginScrollView(new Rect(10, 50, 280, 450), sceneScrollPos, new Rect(0, 0, 280, _sceneObjects.Count * Y_OFFSET));
			for (int i = 0; i < _sceneObjects.Count; i++)
			{
				float yPos = i * Y_OFFSET;

				if (_sceneObjects[i])
				{
					// GUILayout.BeginHorizontal();
					if (GUI.Button(new Rect(0, yPos, 20, 20), "+"))
					{
						SelectInspectObject(_sceneObjects[i]);
						ModLogger.Log($"ModManager - Selecting {_sceneObjects[i].name}");
					}
					GUI.Label(new Rect(30, yPos, 300, 20), $"{_sceneObjects[i].name}");
					// GUILayout.EndHorizontal();
				}
			}
			GUI.EndScrollView(true);

			GUI.DragWindow();
		}

		Vector2 inspectChildrenScrollPos = Vector2.zero;
		void RenderInspectChildrenWindow(int id)
		{
			GUI.Label(new Rect(20, 20, 300, 20), $"{_inspectedObjChildren.Count} children in {_inspectedObject.name}");

			if (GUI.Button(new Rect(20, 45, 100, 20), "Dump Object"))
			{
				const string path = @"E:/GoodCompanyDump.txt";
				SceneDumper.Dump(_inspectedObject, path);
				ModLogger.Log($"Object {_inspectedObject} dumped to {path}");
			}

			if (_inspectedObject.transform.parent)
			{
				if (GUI.Button(new Rect(130, 45, 100, 20), "View parent"))
				{
					SelectInspectObject(_inspectedObject.transform.parent.gameObject);
				}
			}

			const float Y_OFFSET = 20.0f;

			inspectChildrenScrollPos = GUI.BeginScrollView(new Rect(10, 70, 280, 450), inspectChildrenScrollPos, new Rect(0, 0, 260, _inspectedObjChildren.Count * Y_OFFSET));
			for (int i = 0; i < _inspectedObjChildren.Count; i++)
			{
				float yPos = i * Y_OFFSET;

				if (GUI.Button(new Rect(0, yPos, 20, 20), "+"))
				{
					SelectInspectObject(_inspectedObjChildren[i].gameObject);
				}

				if (GUI.Button(new Rect(25, yPos, 20, 20), "x"))
				{
					GameObject.Destroy(_inspectedObjChildren[i].gameObject);
					SelectInspectObject(_inspectedObject);
				}

				GUI.Label(new Rect(50, yPos, 300, 20), _inspectedObjChildren[i].gameObject.name);
			}
			GUI.EndScrollView(true);

			GUI.DragWindow();
		}

		Vector2 inspectComponentsScrollPos = Vector2.zero;
		void RenderInspectComponentsWindow(int id)
		{
			GUI.Label(new Rect(20, 20, 300, 20), $"{_inspectedObjComponents.Count} components on {_inspectedObject.name}");

			const float Y_OFFSET = 20.0f;

			inspectComponentsScrollPos = GUI.BeginScrollView(new Rect(10, 70, 280, 450), inspectComponentsScrollPos, new Rect(0, 0, 260, _inspectedObjComponents.Count * Y_OFFSET));
			for (int i = 0; i < _inspectedObjComponents.Count; i++)
			{
				float yPos = i * Y_OFFSET;

				GUI.Label(new Rect(10, yPos, 300, 20), _inspectedObjComponents[i].GetType().ToString());
			}
			GUI.EndScrollView(true);

			GUI.DragWindow();
		}
	}
}