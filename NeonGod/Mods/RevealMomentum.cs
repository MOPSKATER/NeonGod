using UnityEngine;

namespace NeonGod.Mods
{
    internal class RevealMomentum : Mod
    {
        private float hVelocity = 0, vVelocity = 0;

        void Update()
        {
            Vector3 velocity = RM.drifter.Motor.BaseVelocity;
            hVelocity = new Vector2(velocity.x, velocity.z).magnitude;
            vVelocity = velocity.y;
        }

        void OnGUI()
        {
            if (!RM.mechController.GetIsAlive())
                return;

            Color hColor;
            Color vColor;

            if (hVelocity == 0)
                hColor = Color.white;
            else if (hVelocity < 19)
                hColor = Color.red;
            else
                hColor = Color.green;

            if (vVelocity < 0)
                vColor = Color.red;
            else if (vVelocity > 0)
                vColor = Color.green;
            else
                vColor = Color.white;

            GUIStyle hStyle = new()
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold
            };

            GUIStyle vStyle = new()
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold
            };

            hStyle.normal.textColor = hColor;
            vStyle.normal.textColor = vColor;

            GUI.Label(new Rect(10, 10, 100, 30), "HVelocity: " + hVelocity.Truncate(4), hStyle);
            GUI.Label(new Rect(10, 40, 100, 30), "VVelocity: " + vVelocity.Truncate(4), vStyle);
        }
    }
}