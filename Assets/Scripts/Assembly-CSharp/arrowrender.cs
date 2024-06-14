using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class arrowrender : MonoBehaviour
{
	public float stemLength;

	public float stemWidth;

	public float tipLength;

	public float tipWidth;

	[NonSerialized]
	public List<Vector3> verticesList;

	[NonSerialized]
	public List<int> trianglesList;

	private Mesh mesh;

	private void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
	}

	private void Update()
	{
		GenerateArrow();
	}

	private void GenerateArrow()
	{
		verticesList = new List<Vector3>();
		trianglesList = new List<int>();
		Vector3 zero = Vector3.zero;
		float num = stemWidth / 2f;
		verticesList.Add(zero + num * Vector3.down);
		verticesList.Add(zero + num * Vector3.up);
		verticesList.Add(verticesList[0] + stemLength * Vector3.right);
		verticesList.Add(verticesList[1] + stemLength * Vector3.right);
		trianglesList.Add(0);
		trianglesList.Add(1);
		trianglesList.Add(3);
		trianglesList.Add(0);
		trianglesList.Add(3);
		trianglesList.Add(2);
		Vector3 vector = stemLength * Vector3.right;
		float num2 = tipWidth / 2f;
		verticesList.Add(vector + num2 * Vector3.up);
		verticesList.Add(vector + num2 * Vector3.down);
		verticesList.Add(vector + tipLength * Vector3.right);
		trianglesList.Add(4);
		trianglesList.Add(6);
		trianglesList.Add(5);
		mesh.vertices = verticesList.ToArray();
		mesh.triangles = trianglesList.ToArray();
	}
}
