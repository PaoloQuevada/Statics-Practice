using UnityEngine;

public class SpriteStretch : MonoBehaviour
{
	public bool KeepAspectRatio;

	private void Start()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		float num = vector.x * 2f;
		float num2 = vector.y * 2f;
		Vector3 size = base.gameObject.GetComponent<SpriteRenderer>().bounds.size;
		float num3 = num / size.x;
		float num4 = num2 / size.y;
		if (KeepAspectRatio)
		{
			if (num3 > num4)
			{
				num4 = num3;
			}
			else
			{
				num3 = num4;
			}
		}
		base.gameObject.transform.localScale = new Vector3(num3, num4, 1f);
	}
}
