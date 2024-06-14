using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Line : MonoBehaviour
{
	private Vector2 startPosition;

	private Vector2 endPosition;

	private float lineWidth;

	private Color lineColor;

	private SpriteRenderer lineRenderer;

	public Vector2 start
	{
		get
		{
			return startPosition;
		}
		set
		{
			startPosition = value;
			UpdatePosition();
		}
	}

	public Vector2 end
	{
		get
		{
			return endPosition;
		}
		set
		{
			endPosition = value;
			UpdatePosition();
		}
	}

	public float width
	{
		get
		{
			return lineWidth;
		}
		set
		{
			lineWidth = value;
			UpdateWidth();
		}
	}

	public Color color
	{
		get
		{
			return lineColor;
		}
		set
		{
			lineColor = value;
			UpdateColor();
		}
	}

	private void Awake()
	{
		lineRenderer = GetComponent<SpriteRenderer>();
	}

	public void Initialise(Vector2 start, Vector2 end, float width, Color color)
	{
		startPosition = start;
		endPosition = end;
		lineWidth = width;
		lineColor = color;
		UpdatePosition();
		UpdateWidth();
		UpdateColor();
	}

	private void UpdatePosition()
	{
		Vector2 vector = endPosition - startPosition;
		float magnitude = vector.magnitude;
		Vector2 vector2 = vector / magnitude;
		Vector3 position = new Vector3(startPosition.x + endPosition.x, startPosition.y + endPosition.y) / 2f;
		lineRenderer.transform.position = position;
		float angle = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
		lineRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		float num = 2f;
		lineRenderer.transform.localScale = new Vector3(magnitude / num, width, lineRenderer.transform.localScale.z);
	}

	private void UpdateWidth()
	{
		lineRenderer.transform.localScale = lineRenderer.transform.localScale.WithY(lineWidth);
	}

	private void UpdateColor()
	{
		lineRenderer.color = lineColor;
	}
}
