using System;
using UnityEngine;
using UnityEngine.UI;

public class Level5 : MonoBehaviour, IDataPersistence
{
	public GameObject arrow1;

	public GameObject arrow2;

	public GameObject winMenu;

	public ArrowRenderer arrow1Length;

	public ArrowRenderer arrow2Length;

	public Text angle1;

	public Text angle2;

	public Text length;

	public Text wrong;

	public InputField alpha;

	public InputField beta;

	public InputField gamma;

	private double alphaAnswer;

	private double betaAnswer;

	private double gammaAnswer;

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
		int num2 = UnityEngine.Random.Range(1, 91);
		double num3 = ConvertToRadians(num);
		double num4 = ConvertToRadians(num2);
		double num5 = Math.Round(UnityEngine.Random.Range(1f, 10f), 1);
		arrow1.transform.rotation = Quaternion.Euler(0f, 0f, num);
		arrow2.transform.rotation = Quaternion.Euler(0f, 0f, num2);
		arrow1Length.stemLength = (float)num5;
		arrow2Length.stemLength = (float)num5;
		num5 *= 10.0;
		Debug.Log(num5);
		angle1.text = num + "°";
		angle2.text = num2 + "°";
		length.text = "The vector has a magnitude of " + num5 + "N";
		double num6 = Math.Round(num5 * Math.Sin(num4), 1);
		double num7 = Math.Round(num5 * Math.Cos(num4), 1);
		double num8 = Math.Round(num7 * Math.Cos(num3), 1);
		double num9 = Math.Round(num7 * Math.Sin(num3), 1);
		Debug.Log(Math.Round(Math.Sqrt(Math.Pow(num8, 2.0) + Math.Pow(num9, 2.0) + Math.Pow(num6, 2.0))));
		alphaAnswer = Math.Round(ConvertToDegrees(Math.Acos(num8 / num5)), 1);
		betaAnswer = Math.Round(ConvertToDegrees(Math.Acos(num9 / num5)), 1);
		gammaAnswer = Math.Round(ConvertToDegrees(Math.Acos(num6 / num5)), 1);
		Debug.Log(alphaAnswer + ", " + betaAnswer + ", " + gammaAnswer);
	}

	public void checkAnswer()
	{
		if (string.IsNullOrWhiteSpace(alpha.text) | string.IsNullOrWhiteSpace(beta.text) | string.IsNullOrWhiteSpace(gamma.text))
		{
			wrong.text = "Please input an answer!";
		}
		else if ((Convert.ToDouble(alpha.text) == alphaAnswer) & (Convert.ToDouble(beta.text) == betaAnswer) & (Convert.ToDouble(gamma.text) == gammaAnswer))
		{
			attempts++;
			winCount++;
			time = Time.timeSinceLevelLoad;
			winMenu.SetActive(value: true);
		}
		else if ((Convert.ToDouble(alpha.text) != alphaAnswer) | (Convert.ToDouble(beta.text) != betaAnswer) | (Convert.ToDouble(gamma.text) != gammaAnswer))
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
