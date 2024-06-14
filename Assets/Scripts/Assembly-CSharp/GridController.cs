using UnityEngine;

public class GridController : MonoBehaviour, IDataPersistence
{
	public GameObject arrowX;

	public GameObject arrowY;

	public GameObject arrowZ;

	public GameObject arrowRed;

	public GameObject arrowGreen;

	public GameObject arrowBlue;

	private double[] bestTimes;

	public void LoadData(GameData data)
	{
		bestTimes = data.bestTimes;
		loadArrows();
	}

	public void SaveData(ref GameData data)
	{
	}

	private void loadArrows()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		double[] array = bestTimes;
		foreach (double num4 in array)
		{
			if (num4 > 0.0 && num4 <= 90.0)
			{
				num3++;
			}
			if (num4 > 0.0 && num4 <= 180.0)
			{
				num2++;
			}
			if (num4 > 0.0 && num4 <= 300.0)
			{
				num++;
			}
		}
		Debug.Log(num + " " + num2 + " " + num3);
		if (num == 8)
		{
			arrowX.SetActive(value: false);
			arrowRed.SetActive(value: true);
		}
		if (num2 == 8)
		{
			arrowY.SetActive(value: false);
			arrowGreen.SetActive(value: true);
		}
		if (num3 == 8)
		{
			arrowZ.SetActive(value: false);
			arrowBlue.SetActive(value: true);
		}
	}
}
