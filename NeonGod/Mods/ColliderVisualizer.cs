using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;


namespace NeonGod.Hacks
{
    [DisallowMultipleComponent]
    public class ColliderVisualizer : MonoBehaviour
    {
        public void Initialize(VisualizerColorType visualizerColor, string message, int fontSize)
        {
            Initialize(VisualizerColorDictionary[visualizerColor], message, fontSize);
        }

        public void Initialize(Color color, string message, int fontSize)
        {
            Collider component = GetComponent<Collider>();
            if (component is BoxCollider BoxCollider)
            {
                _visualizer = CreateVisualizer((PrimitiveType)3);
                SetVisualizerTransform(BoxCollider);

                Renderer renderer = component.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.enabled = false;
            }
            else if (component is SphereCollider sphereCollider)
            {
                _visualizer = CreateVisualizer(0);
                SetVisualizerTransform(sphereCollider);

                Renderer renderer = component.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.enabled = false;
            }
            else if (component is CapsuleCollider capsuleCollider)
            {
                _visualizer = CreateVisualizer((PrimitiveType)1);
                SetVisualizerTransform(capsuleCollider);

                Renderer renderer = component.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.enabled = false;
            }
            else return;

            Material material = _visualizer.GetComponent<Renderer>().material;
            material.shader = Shader.Find("Sprites/Default");
            material.color = color;
            CreateLabel(message, fontSize);
        }

        private static GameObject ColliderVisualizerCanvas
        {
            get
            {
                if (_colliderVisualizerCanvas == null)
                {
                    _colliderVisualizerCanvas = new GameObject("ColliderVisualizerCanvas");
                    _colliderVisualizerCanvas.AddComponent<Canvas>().renderMode = 0;
                    CanvasScaler canvasScaler = _colliderVisualizerCanvas.AddComponent<CanvasScaler>();
                    canvasScaler.uiScaleMode = (CanvasScaler.ScaleMode)1;
                    canvasScaler.referenceResolution = ReferenceResolution;
                    canvasScaler.matchWidthOrHeight = 1f;
                    _colliderVisualizerCanvas.AddComponent<GraphicRaycaster>();
                }
                return _colliderVisualizerCanvas;
            }
        }

        private static Font Font
        {
            get
            {
                Font font;
                if ((font = _font) == null)
                {
                    font = _font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                }
                return font;
            }
        }

        private void OnDestroy()
        {
            if (_label == null)
            {
                return;
            }
            Destroy(_label.gameObject);
            Destroy(_visualizer);
        }

        private GameObject CreateVisualizer(PrimitiveType primitiveType)
        {
            GameObject gameObject = GameObject.CreatePrimitive(primitiveType);
            gameObject.transform.SetParent(transform, false);
            Collider component = gameObject.GetComponent<Collider>();
            component.enabled = false;
            Destroy(component);
            return gameObject;
        }

        private void SetVisualizerTransform(BoxCollider boxCollider)
        {
            Transform transform = _visualizer.transform;
            transform.localPosition += boxCollider.center;
            transform.localScale = Vector3.Scale(transform.localScale, boxCollider.size);
        }

        private void SetVisualizerTransform(SphereCollider sphereCollider)
        {
            Transform transform = _visualizer.transform;
            transform.localPosition += sphereCollider.center;
            transform.localScale *= sphereCollider.radius * 2f;
        }

        private void SetVisualizerTransform(CapsuleCollider capsuleCollider)
        {
            Transform transform = _visualizer.transform;
            transform.localPosition += capsuleCollider.center;
            switch (capsuleCollider.direction)
            {
                case 0:
                    transform.Rotate(Vector3.forward * 90f);
                    break;
                case 1:
                    break;
                case 2:
                    transform.Rotate(Vector3.right * 90f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Vector3 localScale = transform.localScale;
            float radius = capsuleCollider.radius;
            float num = localScale.x * radius * 2f;
            float num2 = localScale.y * capsuleCollider.height * 0.5f;
            float num3 = localScale.z * radius * 2f;
            transform.localScale = new Vector3(num, num2, num3);
        }

        private void SetVisualizerTransform(MeshCollider meshCollider)
        {
            Transform transform = _visualizer.transform;
            MeshRenderer renderer = meshCollider.gameObject.GetComponent<MeshRenderer>();
            transform.localPosition = renderer.bounds.center;
            transform.localScale = new Vector3(10, 10, 10);
        }

        private void CreateLabel(string message, int fontSize)
        {
            GameObject gameObject = new GameObject("Label");
            gameObject.transform.SetParent(ColliderVisualizerCanvas.transform, false);
            _label = gameObject.AddComponent<Text>();
            _label.font = Font;
            _label.fontSize = fontSize;
            _label.alignment = (TextAnchor)4;
            _label.raycastTarget = false;
            _label.text = message;
            ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = (ContentSizeFitter.FitMode)2;
            contentSizeFitter.verticalFit = (ContentSizeFitter.FitMode)2;
        }

        private static readonly Vector2 ReferenceResolution = new Vector2(800f, 600f);

        private static readonly Dictionary<VisualizerColorType, Color> VisualizerColorDictionary = new Dictionary<VisualizerColorType, Color>(new VisualizerColorTypeComparer())
        {
        {
            VisualizerColorType.Red,
            new Color32(byte.MaxValue, 0, 0, 50)
        },
        {
            VisualizerColorType.Green,
            new Color32(0, 90, 0, 50)
        },
        {
            VisualizerColorType.Blue,
            new Color32(0, 0, byte.MaxValue, 50)
        }
    };

        private Text _label;

        private static GameObject _colliderVisualizerCanvas;

        private static Font _font;

        private GameObject _visualizer;

        public enum VisualizerColorType
        {
            Red,
            Green,
            Blue
        }

        public class VisualizerColorTypeComparer : IEqualityComparer<VisualizerColorType>
        {
            public bool Equals(VisualizerColorType x, VisualizerColorType y)
            {
                return x == y;
            }
            public int GetHashCode(VisualizerColorType obj)
            {
                return (int)obj;
            }
        }
    }
}