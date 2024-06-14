using UnityEngine;

public class StarController : MonoBehaviour, IDataPersistence
{
	public int level;

	public GameObject star1;

	public GameObject star2;

	public GameObject star3;

	private double bestTime;

	public void LoadData(GameData data)
	{
		bestTime = data.bestTimes[level - 1];
		setStars();
	}

	public void SaveData(ref GameData data)
	{
	}

	private void setStars()
	{
		if (!((bestTime == 0.0) | (bestTime > 300.0)))
		{
			if (bestTime <= 300.0)
			{
				star1.SetActive(value: true);
			}
			if (bestTime <= 180.0)
			{
				star2.SetActive(value: true);
			}
			if (bestTime <= 90.0)
			{
				star3.SetActive(value: true);
			}
		}
	}
}
