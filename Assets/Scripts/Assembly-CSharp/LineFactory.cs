using System.Collections.Generic;
using UnityEngine;

public class LineFactory : MonoBehaviour
{
	public GameObject linePrefab;

	public int maxLines = 50;

	private Line[] pooledLines;

	private int currentIndex;

	private void Start()
	{
		pooledLines = new Line[maxLines];
		for (int i = 0; i < maxLines; i++)
		{
			GameObject gameObject = Object.Instantiate(linePrefab);
			gameObject.SetActive(value: false);
			gameObject.transform.SetParent(base.transform);
			pooledLines[i] = gameObject.GetComponent<Line>();
		}
	}

	public Line GetLine(Vector2 start, Vector2 end, float width, Color color)
	{
		Line obj = pooledLines[currentIndex];
		obj.Initialise(start, end, width, color);
		obj.gameObject.SetActive(value: true);
		currentIndex = (currentIndex + 1) % pooledLines.Length;
		return obj;
	}

	public List<Line> GetActive()
	{
		List<Line> list = new List<Line>();
		Line[] array = pooledLines;
		foreach (Line line in array)
		{
			if (line.gameObject.activeSelf)
			{
				list.Add(line);
			}
		}
		return list;
	}
}
