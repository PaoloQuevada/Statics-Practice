using System;
using UnityEngine;
using UnityEngine.UI;

public class Level6 : MonoBehaviour, IDataPersistence
{
	public GameObject arrow1;

	public GameObject arrow2;

	public GameObject winMenu;

	public ArrowRenderer arrow1Length;

	public ArrowRenderer arrow2Length;

	public Text details;

	public Text wrong;

	public Text horizontal;

	public Text vertical;

	public InputField magnitude;

	public InputField alpha;

	public InputField beta;

	public InputField gamma;

	private double magnitudeAnswer;

	private double alphaAnswer;

	private double betaAnswer;

	private double gammaAnswer;

	private double[] angle1Degrees;

	private double[] angle2Degrees;

	private int arrowState;

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
		horizontal.text = "X";
		vertical.text = "Y";
		angle1Degrees = new double[2]
		{
			Math.Round(UnityEngine.Random.Range(1f, 360.01f), 1),
			Math.Round(UnityEngine.Random.Range(1f, 360.01f), 1)
		};
		angle2Degrees = new double[2]
		{
			Math.Round(UnityEngine.Random.Range(1f, 360.01f), 1),
			Math.Round(UnityEngine.Random.Range(1f, 360.01f), 1)
		};
		double[] array = new double[2]
		{
			ConvertToRadians(angle1Degrees[0]),
			ConvertToRadians(angle1Degrees[1])
		};
		double[] array2 = new double[2]
		{
			ConvertToRadians(angle2Degrees[0]),
			ConvertToRadians(angle2Degrees[1])
		};
		double num = Math.Round(UnityEngine.Random.Range(1f, 10f), 1);
		double num2 = Math.Round(UnityEngine.Random.Range(1f, 10f), 1);
		Debug.Log(angle1Degrees[0] + ", " + angle1Degrees[1]);
		arrow1Length.stemLength = (float)num;
		arrow2Length.stemLength = (float)num2;
		num *= 100.0;
		num2 *= 100.0;
		Debug.Log(num + ", " + num2);
		double num3 = Math.Round(num * Math.Sin(array[1]), 1);
		double num4 = Math.Round(num * Math.Cos(array[1]), 1);
		double num5 = Math.Round(num4 * Math.Cos(array[0]), 1);
		double num6 = Math.Round(num4 * Math.Sin(array[0]), 1);
		double num7 = Math.Round(num * Math.Sin(array2[1]), 1);
		double num8 = Math.Round(num * Math.Cos(array2[1]), 1);
		double num9 = Math.Round(num8 * Math.Cos(array2[0]), 1);
		double num10 = Math.Round(num8 * Math.Sin(array2[0]), 1);
		double num11 = Math.Round(ConvertToDegrees(Math.Acos(num5 / num)), 1);
		double num12 = Math.Round(ConvertToDegrees(Math.Acos(num6 / num)), 1);
		double num13 = Math.Round(ConvertToDegrees(Math.Acos(num3 / num)), 1);
		double num14 = Math.Round(ConvertToDegrees(Math.Acos(num9 / num2)), 1);
		double num15 = Math.Round(ConvertToDegrees(Math.Acos(num10 / num2)), 1);
		double num16 = Math.Round(ConvertToDegrees(Math.Acos(num7 / num2)), 1);
		details.text = "The first vector has a magnitude of " + num + "N and has the following alpha, beta and gamma angles: " + num11 + "°, " + num12 + "°, " + num13 + "°. \nThe second vector has a magnitude of " + num2 + "N and has the following alpha, beta and gamma angles: " + num14 + "°, " + num15 + "°, " + num16 + "°. ";
		double num17 = num5 + num9;
		double num18 = num6 + num10;
		double num19 = num3 + num7;
		magnitudeAnswer = Math.Round(Math.Sqrt(Math.Pow(num17, 2.0) + Math.Pow(num18, 2.0) + Math.Pow(num19, 2.0)));
		Debug.Log(num17 + ", " + num18 + ", " + num19 + ", " + magnitudeAnswer);
		alphaAnswer = Math.Round(ConvertToDegrees(Math.Acos(num17 / magnitudeAnswer)), 1);
		betaAnswer = Math.Round(ConvertToDegrees(Math.Acos(num18 / magnitudeAnswer)), 1);
		gammaAnswer = Math.Round(ConvertToDegrees(Math.Acos(num19 / magnitudeAnswer)), 1);
		Debug.Log(alphaAnswer + ", " + betaAnswer + ", " + gammaAnswer);
	}

	public void rotateGraph()
	{
		if (arrowState == 0)
		{
			arrow1.transform.rotation = Quaternion.Euler(0f, 0f, (float)angle1Degrees[1]);
			arrow2.transform.rotation = Quaternion.Euler(0f, 0f, (float)angle2Degrees[1]);
			arrowState = 1;
			vertical.text = "Z";
		}
		else if (arrowState == 1)
		{
			arrowState = 2;
			horizontal.text = "Y";
		}
		else if (arrowState == 2)
		{
			arrow1.transform.rotation = Quaternion.Euler(0f, 0f, (float)angle1Degrees[0]);
			arrow2.transform.rotation = Quaternion.Euler(0f, 0f, (float)angle2Degrees[0]);
			arrowState = 0;
			horizontal.text = "X";
			vertical.text = "Y";
		}
	}

	public void checkAnswer()
	{
		if (string.IsNullOrWhiteSpace(magnitude.text) | string.IsNullOrWhiteSpace(alpha.text) | string.IsNullOrWhiteSpace(beta.text) | string.IsNullOrWhiteSpace(gamma.text))
		{
			wrong.text = "Please input an answer!";
		}
		else if ((Convert.ToDouble(magnitude.text) == magnitudeAnswer) & (Convert.ToDouble(alpha.text) == alphaAnswer) & (Convert.ToDouble(beta.text) == betaAnswer) & (Convert.ToDouble(gamma.text) == gammaAnswer))
		{
			attempts++;
			winCount++;
			time = Time.timeSinceLevelLoad;
			winMenu.SetActive(value: true);
		}
		else if ((Convert.ToDouble(magnitude.text) != magnitudeAnswer) | (Convert.ToDouble(alpha.text) != alphaAnswer) | (Convert.ToDouble(beta.text) != betaAnswer) | (Convert.ToDouble(gamma.text) != gammaAnswer))
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
