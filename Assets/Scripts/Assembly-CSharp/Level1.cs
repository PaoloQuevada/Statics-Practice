using System;
using UnitOf;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour, IDataPersistence
{
	[SerializeField]
	private GameObject winMenu;

	public Text text;

	public InputField answer;

	public Text wrong;

	public double unitB;

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
		Debug.Log(time + " " + bestTime);
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
		int num = UnityEngine.Random.Range(1, 4);
		int variable = UnityEngine.Random.Range(1, 4);
		int order = UnityEngine.Random.Range(1, 3);
		switch (num)
		{
		case 1:
			SpeedConvert(variable, order);
			break;
		case 2:
			DensityConvert(variable, order);
			break;
		case 3:
			PressureConvert(variable, order);
			break;
		}
	}

	public void question(int questionType, double unitA, string unitNameA, string unitNameB)
	{
		text.text = "Convert " + unitA + " " + unitNameA + " to " + unitNameB;
	}

	public void checkAnswer()
	{
		if (string.IsNullOrWhiteSpace(answer.text))
		{
			wrong.text = "Please input an answer!";
		}
		else if (Convert.ToDouble(answer.text) == unitB)
		{
			attempts++;
			winCount++;
			time = UnityEngine.Time.timeSinceLevelLoad;
			winMenu.SetActive(value: true);
		}
		else if (Convert.ToDouble(answer.text) != unitB)
		{
			attempts++;
			Debug.Log(attempts);
			wrong.text = "Try again!";
		}
	}

	public void SpeedConvert(int variable, int order)
	{
		Debug.Log("Question type is speed conversion!");
		Debug.Log("Excluded unit is " + variable + " and order is " + order);
		double num = 0.0;
		Speed speed = new Speed();
		num = UnityEngine.Random.Range(1f, 101f);
		num = Math.Round(num, 3);
		string unitNameA = "";
		string unitNameB = "";
		switch (variable)
		{
		case 1:
			Debug.Log("Case 1");
			unitNameA = "meters per second";
			unitNameB = "feet per second";
			if (order == 1)
			{
				speed = speed.FromMetersPerSecond(num);
				unitB = speed.ToFeetPerSecond();
			}
			else
			{
				speed = speed.FromFeetPerSecond(num);
				unitB = speed.ToMetersPerSecond();
			}
			break;
		case 2:
			Debug.Log("Case 2");
			unitNameA = "kilometers per hour";
			unitNameB = "feet per second";
			if (order == 1)
			{
				speed = speed.FromKilometersPerHour(num);
				unitB = speed.ToFeetPerSecond();
			}
			else
			{
				speed = speed.FromFeetPerSecond(num);
				unitB = speed.ToKilometersPerHour();
			}
			break;
		case 3:
			Debug.Log("Case 3");
			unitNameA = "kilometers per hour";
			unitNameB = "meters per second";
			if (order == 1)
			{
				speed = speed.FromKilometersPerHour(num);
				unitB = speed.ToMetersPerSecond();
			}
			else
			{
				speed = speed.FromMetersPerSecond(num);
				unitB = speed.ToKilometersPerHour();
			}
			break;
		}
		unitB = Math.Round(unitB, 3);
		Debug.Log("Convert this: " + num + "Answer should be this: " + unitB);
		question(1, num, unitNameA, unitNameB);
	}

	public void DensityConvert(int variable, int order)
	{
		Debug.Log("Question type is density conversion!");
		Debug.Log("Excluded unit is " + variable + " and order is " + order);
		double num = 0.0;
		double num2 = 0.0;
		double value = 0.0;
		double value2 = 0.0;
		Mass mass = new Mass();
		Volume volume = new Volume();
		num = UnityEngine.Random.Range(100f, 1001f);
		num = Math.Round(num, 3);
		num2 = UnityEngine.Random.Range(10f, 101f);
		num2 = Math.Round(num2, 3);
		string unitNameA = "";
		string unitNameB = "";
		Debug.Log("First mass is " + num + " and volume is " + num2);
		switch (variable)
		{
		case 1:
			Debug.Log("Case 1");
			unitNameA = "slugs per cubic cubic foot";
			unitNameB = "pounds per cubic foot";
			if (order == 1)
			{
				mass = mass.FromSlugs(num);
				volume = volume.FromCubicFeet(num2);
				value = mass.ToPounds();
				value2 = volume.ToCubicFeet();
			}
			else
			{
				mass = mass.FromPounds(num);
				volume = volume.FromCubicFeet(num2);
				value = mass.ToSlugs();
				value2 = volume.ToCubicFeet();
			}
			break;
		case 2:
			Debug.Log("Case 2");
			unitNameA = "kilograms per cubic meter";
			unitNameB = "pounds per cubic foot";
			if (order == 1)
			{
				mass = mass.FromKilograms(num);
				volume = volume.FromCubicMeters(num2);
				value = mass.ToPounds();
				value2 = volume.ToCubicFeet();
			}
			else
			{
				mass = mass.FromPounds(num);
				volume = volume.FromCubicFeet(num2);
				value = mass.ToKilograms();
				value2 = volume.ToCubicMeters();
			}
			break;
		case 3:
			Debug.Log("Case 3");
			unitNameA = "kilograms per cubic meter";
			unitNameB = "slugs per cubic cubic foot";
			if (order == 1)
			{
				mass = mass.FromKilograms(num);
				volume = volume.FromCubicMeters(num2);
				value = mass.ToSlugs();
				value2 = volume.ToCubicFeet();
			}
			else
			{
				mass = mass.FromSlugs(num);
				volume = volume.FromCubicFeet(num2);
				value = mass.ToKilograms();
				value2 = volume.ToCubicMeters();
			}
			break;
		}
		double value3 = num / num2;
		value3 = Math.Round(value3, 3);
		value = Math.Round(value, 3);
		value2 = Math.Round(value2, 3);
		unitB = value / value2;
		unitB = Math.Round(unitB, 3);
		Debug.Log("Second mass is " + value + " and volume is " + value2);
		Debug.Log("Convert this: " + value3 + "Answer should be this: " + unitB);
		question(2, value3, unitNameA, unitNameB);
	}

	public void PressureConvert(int variable, int order)
	{
		Debug.Log("Question type is pressure conversion!");
		Debug.Log("Excluded unit is " + variable + " and order is " + order);
		double num = 0.0;
		Pressure pressure = new Pressure();
		num = UnityEngine.Random.Range(1f, 101f);
		num = Math.Round(num, 3);
		string unitNameA = "";
		string unitNameB = "";
		switch (variable)
		{
		case 1:
			Debug.Log("Case 1");
			unitNameA = "bars";
			unitNameB = "PSI";
			if (order == 1)
			{
				pressure = pressure.FromBars(num);
				unitB = pressure.ToPSI();
			}
			else
			{
				pressure = pressure.FromPSI(num);
				unitB = pressure.ToBars();
			}
			break;
		case 2:
			Debug.Log("Case 2");
			unitNameA = "atmospheres";
			unitNameB = "PSI";
			if (order == 1)
			{
				pressure = pressure.FromStandardAtmospheres(num);
				unitB = pressure.ToPSI();
			}
			else
			{
				pressure = pressure.FromPSI(num);
				unitB = pressure.ToStandardAtmospheres();
			}
			break;
		case 3:
			Debug.Log("Case 3");
			unitNameA = "atmospheres";
			unitNameB = "bars";
			if (order == 1)
			{
				pressure = pressure.FromStandardAtmospheres(num);
				unitB = pressure.ToBars();
			}
			else
			{
				pressure = pressure.FromBars(num);
				unitB = pressure.ToStandardAtmospheres();
			}
			break;
		}
		unitB = Math.Round(unitB, 3);
		Debug.Log("Convert this: " + num + "Answer should be this: " + unitB);
		question(3, num, unitNameA, unitNameB);
	}
}
