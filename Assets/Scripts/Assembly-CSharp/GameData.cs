using System;

[Serializable]
public class GameData
{
	public int playCount;

	public int winCount;

	public double avgAttempts;

	public double[] bestTimes;

	public GameData()
	{
		playCount = 0;
		winCount = 0;
		avgAttempts = 0.0;
		bestTimes = new double[8];
	}
}
