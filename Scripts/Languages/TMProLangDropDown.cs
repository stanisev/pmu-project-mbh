using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TMProLangDropDown : MonoBehaviour
{
	[SerializeField] string[] languages;
	public TMP_Dropdown dropdown;
	int index;

	void Awake()
	{

		dropdown = this.GetComponent<TMP_Dropdown>();
		int v = PlayerPrefs.GetInt("_language_index", 0);
		dropdown.value = v;

		int newIndex = PlayerPrefs.GetInt("_titleScreen_index");
		UIController.instance.ChangeTitleScreen(newIndex);
		UIController.instance.ChangeButtonsTitleScreenText(newIndex);

		dropdown.onValueChanged.AddListener(delegate
		{
			index = dropdown.value;
			PlayerPrefs.SetInt("_language_index", index);
			UIController.instance.ChangeTitleScreen(index);
			PlayerPrefs.SetInt("_titleScreen_index", index);
			PlayerPrefs.SetString("_language", languages[index]);
			ApplyLanguageChanges();
		});
	}

	void ApplyLanguageChanges()
	{
		SceneManager.LoadScene(0);
	}

	void OnDestroy()
	{
		dropdown.onValueChanged.RemoveAllListeners();
	}

}
