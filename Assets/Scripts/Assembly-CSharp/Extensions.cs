using UnityEngine;

public static class Extensions
{
	public static Vector3 WithY(this Vector3 a, float y)
	{
		return new Vector3(a.x, y, a.z);
	}
}
