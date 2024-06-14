using System;
using UnityEngine;
using UnityEngine.UI;

public class Level3 : MonoBehaviour, IDataPersistence
{
	public GameObject arrow;

	public GameObject winMenu;

	public ArrowRenderer arrowLength;

	public Text details;

	public InputField vectorX;

	public InputField vectorY;

	public Text wrong;

	private double answerX;

	private double answerY;

	private int playCount;

	private int winCount;

	private int attempts;

	private double avgAttempts;

	private double time;

	private double bestTime;

	public void LoadData(GameData data)
	{
		Debug.Log(data.winCount);
		winCount = data.winCount;
		playCount = data.playCount + 1;
		avgAttempts = data.avgAttempts;
		bestTime = data.bestTimes[0];
	}

	public void SaveData(ref GameData data)
	{
		Debug.Log(winCount);
		data.winCount = winCount;
		data.playCount = playCount;
		if (attempts > 0)
		{
			if (avgAttempts > 0.0)
			{
				data.avgAttempts = ((double)attempts + avgAttempts) / 2.0;
			}
			else
			{
				data.avgAttempts = attempts;
			}
		}
		if (((time < bestTime) | (bestTime == 0.0)) & (time != 0.0))
		{
			data.bestTimes[0] = Math.Round(time, 2);
		}
	}

	private void Awake()
	{
		int num = UnityEngine.Random.Range(1, 91);
		double num2 = ConvertToRadians(num);
		double num3 = Math.Round(UnityEngine.Random.Range(2.4f, 24.01f), 1);
		arrow.transform.rotation = Quaternion.Euler(0f, 0f, num);
		arrowLength.stemLength = (float)num3;
		num3 *= 10.0;
		details.text = num + "°, " + num3 + "N";
		answerX = Math.Round(num3 * Math.Sin(num2));
		answerY = Math.Round(num3 * Math.Cos(num2));
		Debug.Log(answerX + " " + answerY);
	}

	public void checkAnswer()
	{
		if (string.IsNullOrWhiteSpace(vectorX.text) | string.IsNullOrWhiteSpace(vectorY.text))
		{
			wrong.text = "Please input an answer!";
		}
		else if ((Convert.ToDouble(vectorX.text) == answerX) & (Convert.ToDouble(vectorY.text) == answerY))
		{
			attempts++;
			winCount++;
			time = Time.timeSinceLevelLoad;
			winMenu.SetActive(value: true);
		}
		else if ((Convert.ToDouble(vectorX.text) != answerX) | (Convert.ToDouble(vectorY.text) != answerY))
		{
			attempts++;
			Debug.Log(attempts);
			wrong.text = "Try again!";
		}
	}

	public double ConvertToRadians(double angle)
	{
		return Math.PI / 180.0 * angle;
	}

	public double ConvertToDegrees(double angle)
	{
		return 180.0 / Math.PI * angle;
	}
}
