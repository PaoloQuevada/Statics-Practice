using UnityEngine;

public class LineDemo : MonoBehaviour
{
	public LineFactory lineFactory;

	private Vector2 start;

	private Line drawnLine;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			drawnLine = lineFactory.GetLine(vector, vector, 0.02f, Color.black);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			drawnLine = null;
		}
		if (drawnLine != null)
		{
			drawnLine.end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

	public void Clear()
	{
		foreach (Line item in lineFactory.GetActive())
		{
			item.gameObject.SetActive(value: false);
		}
	}
}
