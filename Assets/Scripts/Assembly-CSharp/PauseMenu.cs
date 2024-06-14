using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject pauseMenu;

	public void Pause()
	{
		pauseMenu.SetActive(value: true);
	}

	public void Back()
	{
		SceneManager.LoadSceneAsync(1);
	}

	public void Retry()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	public void Resume()
	{
		pauseMenu.SetActive(value: false);
	}
}
