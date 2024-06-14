using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsPage : MonoBehaviour, IDataPersistence
{
	public Text stagesPlayed;

	public Text problemsSolved;

	public Text averageAttempts;

	public Text averageTime;

	public Text oneStars;

	public Text twoStars;

	public Text threeStars;

	public int playCount;

	public int winCount;

	public int star1;

	public int star2;

	public int star3;

	public double avgAttempts;

	public double[] bestTimes = new double[8];

	public double avgTime;

	public void LoadData(GameData data)
	{
		playCount = data.playCount;
		winCount = data.winCount;
		avgAttempts = Math.Round(data.avgAttempts, 2);
		bestTimes = data.bestTimes;
		setData();
	}

	public void SaveData(ref GameData data)
	{
		data.playCount = playCount;
		data.winCount = winCount;
		data.avgAttempts = avgAttempts;
		data.bestTimes = bestTimes;
	}

	public void setData()
	{
		double[] array = bestTimes;
		foreach (double num in array)
		{
			Debug.Log(num);
			if (avgTime == 0.0)
			{
				avgTime = num;
			}
			else
			{
				avgTime = num + avgTime / 2.0;
			}
			if (num > 0.0 && num <= 90.0)
			{
				star3++;
			}
			else if (num > 90.0 && num <= 180.0)
			{
				star2++;
			}
			else if (num > 150.0 && num <= 300.0)
			{
				star1++;
			}
		}
		Debug.Log(playCount);
		stagesPlayed.text = "Stages Played: " + playCount;
		problemsSolved.text = "Problems Solved: " + winCount;
		averageAttempts.text = "Average Tries per Attempt: " + avgAttempts;
		averageTime.text = "Average time to solve: " + avgTime;
		oneStars.text = "Number of one star levels: " + star1;
		twoStars.text = "Number of two star levels: " + star2;
		threeStars.text = "Number of three star levels: " + star3;
	}

	public void returnToMainMenu()
	{
		SceneManager.LoadSceneAsync(0);
	}
}
