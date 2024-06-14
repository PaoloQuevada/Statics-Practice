using UnityEngine;
using UnityEngine.UI;

public class switch_scenes : MonoBehaviour
{
	public string sceneName = "";

	private void Start()
	{
		Button component = GetComponent<Button>();
		if (component != null && sceneName != "")
		{
			component.onClick.AddListener(delegate
			{
				Application.LoadLevel(sceneName);
			});
		}
	}

	private void Update()
	{
	}
}
