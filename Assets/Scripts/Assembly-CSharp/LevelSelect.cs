using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour, IDataPersistence
{
	private double[] bestTimes;

	public void LoadData(GameData data)
	{
		bestTimes = data.bestTimes;
	}

	public void SaveData(ref GameData data)
	{
	}

	public void level1()
	{
		SceneManager.LoadSceneAsync(3);
	}

	public void level2()
	{
		SceneManager.LoadSceneAsync(4);
	}

	public void level3()
	{
		SceneManager.LoadSceneAsync(5);
	}

	public void level4()
	{
		SceneManager.LoadSceneAsync(6);
	}

	public void level5()
	{
		SceneManager.LoadSceneAsync(7);
	}

	public void level6()
	{
		SceneManager.LoadSceneAsync(8);
	}

	public void level7()
	{
		SceneManager.LoadSceneAsync(9);
	}

	public void level8()
	{
		SceneManager.LoadSceneAsync(10);
	}

	public void returnToMainMenu()
	{
		SceneManager.LoadSceneAsync(0);
	}
}
