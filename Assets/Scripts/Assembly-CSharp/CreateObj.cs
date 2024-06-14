using UnityEngine;
using UnityEngine.UI;

public class CreateObj : MonoBehaviour
{
	public Image PP;

	public Sprite[] temp;

	public GameObject parent;

	private void Start()
	{
		for (int i = 0; i < temp.Length; i++)
		{
			Image image = Object.Instantiate(PP);
			image.transform.SetParent(parent.transform, worldPositionStays: false);
			image.sprite = temp[i];
			image.gameObject.SetActive(value: true);
		}
	}

	private void Update()
	{
	}
}
