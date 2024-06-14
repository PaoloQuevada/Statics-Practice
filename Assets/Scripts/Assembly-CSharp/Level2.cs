using System;
using UnityEngine;
using UnityEngine.UI;

public class Level2 : MonoBehaviour, IDataPersistence
{
	public GameObject winMenu;

	public GameObject arrow1;

	public GameObject arrow2;

	public ArrowRenderer arrow1Length;

	public ArrowRenderer arrow2Length;

	public Text angle1;

	public Text angle2;

	public Text magnitudes;

	public Text wrong;

	public InputField magnitude;

	public InputField angle;

	private double degrees;

	private double length;

	private int playCount;

	private int winCount;

	private int attempts;

	private double avgAttempts;

	private double time;

	private double bestTime;

	public void LoadData(GameData data)
	{
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
		Debug.Log(playCount);
		int num = UnityEngine.Random.Range(1, 46);
		int num2 = UnityEngine.Random.Range(1, 46);
		double num3 = Math.Round(UnityEngine.Random.Range(2.4f, 24.01f), 1);
		double num4 = Math.Round(UnityEngine.Random.Range(2.4f, 24.01f), 1);
		arrow1.transform.rotation = Quaternion.Euler(0f, 0f, 90 - num);
		arrow2.transform.rotation = Quaternion.Euler(0f, 0f, num2);
		arrow1Length.stemLength = (float)num3;
		arrow2Length.stemLength = (float)num4;
		num3 *= 10.0;
		num4 *= 10.0;
		angle1.text = num + "°";
		angle2.text = num2 + "°";
		magnitudes.text = "The left hand vector has a magnitude of " + num3 + " while the right has a magnitude of " + num4;
		double num5 = ConvertToRadians((360 - 2 * (90 - num - num2)) / 2);
		length = Math.Round(Math.Sqrt(Math.Pow(num3, 2.0) + Math.Pow(num4, 2.0) - 2.0 * num3 * num4 * Math.Cos(num5)), 1);
		degrees = Math.Round(ConvertToDegrees(Math.Asin(num3 / length * Math.Sin(num5))), 1) + (double)num2;
		length = Math.Round(length);
		Debug.Log(length + ", " + degrees);
	}

	public void checkAnswer()
	{
		if (string.IsNullOrWhiteSpace(magnitude.text) | string.IsNullOrWhiteSpace(angle.text))
		{
			wrong.text = "Please input an answer!";
		}
		else if ((Convert.ToDouble(magnitude.text) == length) & (Convert.ToDouble(angle.text) == degrees))
		{
			attempts++;
			winCount++;
			time = Time.timeSinceLevelLoad;
			winMenu.SetActive(value: true);
		}
		else if ((Convert.ToDouble(magnitude.text) != length) | (Convert.ToDouble(angle.text) != degrees))
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
