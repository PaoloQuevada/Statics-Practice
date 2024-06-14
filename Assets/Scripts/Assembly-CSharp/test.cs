using System;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
	private Mesh mesh;

	public Vector3[] polygonPoints;

	public int[] polygonTriangles;

	public bool isFilled;

	public int polygonSides;

	public float polygonRadius;

	public float centerRadius;

	private void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
	}

	private void Update()
	{
		if (isFilled)
		{
			DrawFilled(polygonSides, polygonRadius);
		}
		else
		{
			DrawHollow(polygonSides, polygonRadius, centerRadius);
		}
	}

	private void DrawFilled(int sides, float radius)
	{
		polygonPoints = GetCircumferencePoints(sides, radius).ToArray();
		polygonTriangles = DrawFilledTriangles(polygonPoints);
		mesh.Clear();
		mesh.vertices = polygonPoints;
		mesh.triangles = polygonTriangles;
	}

	private void DrawHollow(int sides, float outerRadius, float innerRadius)
	{
		List<Vector3> list = new List<Vector3>();
		List<Vector3> circumferencePoints = GetCircumferencePoints(sides, outerRadius);
		list.AddRange(circumferencePoints);
		List<Vector3> circumferencePoints2 = GetCircumferencePoints(sides, innerRadius);
		list.AddRange(circumferencePoints2);
		polygonPoints = list.ToArray();
		polygonTriangles = DrawHollowTriangles(polygonPoints);
		mesh.Clear();
		mesh.vertices = polygonPoints;
		mesh.triangles = polygonTriangles;
	}

	private int[] DrawHollowTriangles(Vector3[] points)
	{
		int num = points.Length / 2;
		List<int> list = new List<int>();
		for (int i = 0; i < num; i++)
		{
			int num2 = i;
			int item = i + num;
			list.Add(num2);
			list.Add(item);
			list.Add((i + 1) % num);
			list.Add(num2);
			list.Add(num + (num + i - 1) % num);
			list.Add(num2 + num);
		}
		return list.ToArray();
	}

	private List<Vector3> GetCircumferencePoints(int sides, float radius)
	{
		List<Vector3> list = new List<Vector3>();
		float num = 1f / (float)sides;
		float num2 = (float)Math.PI * 2f;
		float num3 = num * num2;
		for (int i = 0; i < sides; i++)
		{
			float f = num3 * (float)i;
			list.Add(new Vector3(Mathf.Cos(f) * radius, Mathf.Sin(f) * radius, 0f));
		}
		return list;
	}

	private int[] DrawFilledTriangles(Vector3[] points)
	{
		int num = points.Length - 2;
		List<int> list = new List<int>();
		for (int i = 0; i < num; i++)
		{
			list.Add(0);
			list.Add(i + 2);
			list.Add(i + 1);
		}
		return list.ToArray();
	}
}
