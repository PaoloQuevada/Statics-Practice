using UnityEngine;

namespace Kalkatos.DottedArrow
{
	public class Arrow : MonoBehaviour
	{
		[SerializeField]
		private float baseHeight;

		[SerializeField]
		private RectTransform baseRect;

		[SerializeField]
		private Transform origin;

		[SerializeField]
		private bool startsActive;

		private RectTransform myRect;

		private Canvas canvas;

		private Camera mainCamera;

		private bool isActive;

		public Transform Origin
		{
			get
			{
				return origin;
			}
			set
			{
				origin = value;
			}
		}

		private void Awake()
		{
			myRect = (RectTransform)base.transform;
			canvas = GetComponentInParent<Canvas>();
			mainCamera = Camera.main;
			SetActive(startsActive);
		}

		private void Update()
		{
			if (isActive)
			{
				Setup();
			}
		}

		private void Setup()
		{
			if (!(origin == null))
			{
				Vector2 vector = mainCamera.WorldToScreenPoint(origin.position);
				myRect.anchoredPosition = new Vector2(vector.x - (float)(Screen.width / 2), vector.y - (float)(Screen.height / 2)) / canvas.scaleFactor;
				Vector2 vector2 = Input.mousePosition - (Vector3)vector;
				vector2.Scale(new Vector2(1f / myRect.localScale.x, 1f / myRect.localScale.y));
				base.transform.up = vector2;
				baseRect.anchorMax = new Vector2(baseRect.anchorMax.x, vector2.magnitude / canvas.scaleFactor / baseHeight);
			}
		}

		private void SetActive(bool b)
		{
			isActive = b;
			if (b)
			{
				Setup();
			}
			baseRect.gameObject.SetActive(b);
		}

		public void Activate()
		{
			SetActive(b: true);
		}

		public void Deactivate()
		{
			SetActive(b: false);
		}

		public void SetupAndActivate(Transform origin)
		{
			Origin = origin;
			Activate();
		}
	}
}
