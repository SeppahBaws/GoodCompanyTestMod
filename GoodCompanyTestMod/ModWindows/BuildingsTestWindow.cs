using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace GoodCompanyTestMod
{
	public class BuildingsTestModWindow : BaseModWindow
	{
		public BuildingsTestModWindow(bool active = true)
		{
			Activated = active;
		}

		public override void OnStart()
		{
			base.OnStart();
		}

		Rect _windowRect = new Rect(50, 50, 140, 70);
		
		public override void OnRender()
		{
			base.OnRender();

			_windowRect = GUI.Window(4, _windowRect, RenderWindow, "test window");
		}

		string _text = "0";

		void RenderWindow(int id)
		{
			_text = GUI.TextField(new Rect(20, 20, 100, 20), _text);
			_text = Regex.Replace(_text, @"[^0-9]", "");
			if (_text == "")
				_text = "0";

			uint buildingId = UInt32.Parse(_text);
			
			if (GUI.Button(new Rect(20, 50, 100, 20), $"test unlock building {buildingId}"))
			{
				SessionManagerStatic.Instance.MsgSender.SendUnlockBuildingMessage(buildingId);
			}

			GUI.DragWindow();
		}
	}
}