using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using static MenuScreenPopup;

namespace NeonGod.Hacks
{
    internal class Help : Mod
    {
        private const string HELPTEXT =
            "Toggle Hitboxes: [CTRL] + b\n" +
            "Give Miracle Katana: k\n" +
            "Kill All: x\n" +
            "Toggle Noclip: n\n" +
            "Teleport:\n" +
            "-  Save: CTRL + num0-2\n" +
            "-  Load: [shift] + num0-2\n" +
            "Unlock Gate: u\n" +
            "Toggle Help: h";
        private bool _show = false;
        private GUIStyle _style;

        public Help()
        {
            _style = new GUIStyle
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold
            };
            _style.normal.textColor = Color.gray;
        }

        void Update()
        {
            if (Keyboard.current.hKey.wasPressedThisFrame)
                _show = !_show;
        }

        void OnGUI()
        {
            if (!(RM.mechController.GetIsAlive() && _show))
                return;

            GUI.Label(new Rect(Camera.main.pixelWidth - 250, 10, 200, 100), HELPTEXT, _style);
        }
    }
}
