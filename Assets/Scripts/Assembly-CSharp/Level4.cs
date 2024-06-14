using System;
using UnityEngine;
using UnityEngine.UI;

public class Level4 : MonoBehaviour, IDataPersistence
{
	public GameObject arrow1;

	public GameObject arrow2;

	public GameObject arrow3;

	public GameObject winMenu;

	public ArrowRenderer arrow1Length;

	public ArrowRenderer arrow2Length;

	public ArrowRenderer arrow3Length;

	public Text details;

	public InputField magnitude;

	public InputField direction;

	public Text wrong;

	private double answerMagnitude;

	private double answerDirection;

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
		int num = UnityEngine.Random.Range(0, 361);
		int num2 = UnityEngine.Random.Range(0, 361);
		int num3 = UnityEngine.Random.Range(0, 361);
		double num4 = Math.Round(UnityEngine.Random.Range(1f, 10f), 1);
		double num5 = Math.Round(UnityEngine.Random.Range(1f, 10f), 1);
		double num6 = Math.Round(UnityEngine.Random.Range(1f, 10f), 1);
		arrow1.transform.rotation = Quaternion.Euler(0f, 0f, num);
		arrow2.transform.rotation = Quaternion.Euler(0f, 0f, num2);
		arrow3.transform.rotation = Quaternion.Euler(0f, 0f, num3);
		arrow1Length.stemLength = (float)num4;
		arrow2Length.stemLength = (float)num5;
		arrow3Length.stemLength = (float)num6;
		num4 *= 100.0;
		num5 *= 100.0;
		num6 *= 100.0;
		details.text = "The vector with the normal head has a magnitude and direction of " + num4 + "N and " + num + " respectively.\nThe vector with the long head has a magnitude and direction of " + num5 + "N and " + num2 + " respectively.\nThe vector with the wide head has a magnitude and direction of " + num6 + "N and " + num3 + " respectively.\n";
		double num7 = Math.Round(num4 * solveXComponent(num) + num5 * solveXComponent(num2) + num6 * solveXComponent(num3), 1);
		double num8 = Math.Round(num4 * solveYComponent(num) + num5 * solveYComponent(num2) + num6 * solveYComponent(num3), 1);
		answerMagnitude = Math.Round(Math.Sqrt(Math.Pow(num7, 2.0) + Math.Pow(num8, 2.0)));
		answerDirection = solveDirection(num7, num8);
		if (answerDirection < 0.0)
		{
			answerDirection = 360.0 + answerDirection;
		}
		Debug.Log(num7 + ", " + num8);
		Debug.Log(answerMagnitude + ", " + answerDirection);
	}

	public double solveDirection(double sumX, double sumY)
	{
		double num = Math.Abs(Math.Round(ConvertToDegrees(Math.Atan(sumY / sumX)), 1));
		if (sumX >= 0.0 && sumY >= 0.0)
		{
			Debug.Log(num);
			return num;
		}
		if (sumX < 0.0 && sumY > 0.0)
		{
			Debug.Log(num);
			return num + 90.0;
		}
		if (sumX < 0.0 && sumY < 0.0)
		{
			Debug.Log(num);
			return num + 180.0;
		}
		if (sumX > 0.0 && sumY < 0.0)
		{
			Debug.Log(num);
			return num + 270.0;
		}
		return -1.0;
	}

	public double solveXComponent(int angle)
	{
		int closestAngle = getClosestAngle(angle);
		double num = ConvertToRadians(closestAngle);
		if (90 >= angle)
		{
			return Math.Cos(num);
		}
		if (180 >= angle && angle > 90)
		{
			return -1.0 * Math.Sin(num);
		}
		if (270 >= angle && angle > 180)
		{
			return -1.0 * Math.Cos(num);
		}
		if (360 >= angle && angle > 270)
		{
			return Math.Sin(num);
		}
		return -1.0;
	}

	public double solveYComponent(int angle)
	{
		int closestAngle = getClosestAngle(angle);
		double num = ConvertToRadians(closestAngle);
		if (90 >= angle)
		{
			return Math.Sin(num);
		}
		if (180 >= angle && angle > 90)
		{
			return Math.Cos(num);
		}
		if (270 >= angle && angle > 180)
		{
			return -1.0 * Math.Sin(num);
		}
		if (360 >= angle && angle > 270)
		{
			return -1.0 * Math.Cos(num);
		}
		return -1.0;
	}

	public int getClosestAngle(int angle)
	{
		while (angle > 90)
		{
			angle -= 90;
		}
		return angle;
	}

	public void checkAnswer()
	{
		if (string.IsNullOrWhiteSpace(magnitude.text) | string.IsNullOrWhiteSpace(direction.text))
		{
			wrong.text = "Please input an answer!";
		}
		else if ((Convert.ToDouble(magnitude.text) == answerMagnitude) & (Convert.ToDouble(direction.text) == answerDirection))
		{
			attempts++;
			winCount++;
			time = Time.timeSinceLevelLoad;
			winMenu.SetActive(value: true);
		}
		else if ((Convert.ToDouble(magnitude.text) != answerMagnitude) | (Convert.ToDouble(direction.text) != answerDirection))
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
