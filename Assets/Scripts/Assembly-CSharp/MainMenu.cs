using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void moveToChapterSelect()
	{
		SceneManager.LoadSceneAsync(1);
	}

	public void moveToStats()
	{
		SceneManager.LoadSceneAsync(2);
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
