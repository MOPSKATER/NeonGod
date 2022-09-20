using UnityEngine;
using System.Linq;

namespace NeonGod.Hacks
{

    class CollisionVisualizer
    {
        public static void enable(bool colored)
        {
            foreach (Collider collider in from c in Object.FindObjectsOfType<Collider>()
                                          where !c.isTrigger && c.gameObject.name != "Player" && !c.GetComponent<BaseDamageable>()
                                          select c)
            {
                try
                {
                    //collider.gameObject.AddComponent<ColliderVisualizer>().Initialize(
                    //    (collider.GetType() == typeof(MeshCollider) && !((MeshCollider)collider).convex) ?
                    //    Color.red : Random.ColorHSV(0f, 1f, 4f, 4f, 1f, 1f), "", 12);

                    collider.gameObject.AddComponent<ColliderVisualizer>().Initialize(
                        collider.GetType() == typeof(MeshCollider) && !((MeshCollider)collider).convex ?
                        Color.red : colored ? Random.ColorHSV(0f, 1f, 4f, 4f, 1f, 1f) : Color.green, "", 12);
                }
                catch
                {
                }
            }
            BaseDamageable[] array = Object.FindObjectsOfType<BaseDamageable>();
            for (int i = 0; i < array.Length; i++)
            {
                array[i].gameObject.AddComponent<ColliderVisualizer>().Initialize(ColliderVisualizer.VisualizerColorType.Red, "", 12);
            }
        }
        public static void disable()
        {
            ColliderVisualizer[] visualizers = Object.FindObjectsOfType<ColliderVisualizer>();
            foreach (ColliderVisualizer visualizer in visualizers)
            {
                GameObject go = visualizer.gameObject;
                if (go != null)
                {
                    Renderer renderer = go.GetComponent<Renderer>();
                    if (renderer != null)
                        renderer.enabled = true;
                }
                Object.Destroy(visualizer);
            }
        }
    }
}