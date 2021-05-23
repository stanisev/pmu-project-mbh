using UnityEngine;
using TMPro;

public class ButtonTextLang : MonoBehaviour
{
	[SerializeField]
	string key;

	void Start()
	{
		GetComponent<TMP_Text>().text = GameMultiLang.GetTraduction(key);
	}

	private string ButtonTextUpdater()
	{
		string randomFactKey = "FACTS0";
		int randomInt = Random.Range(0, 4);

		switch (randomInt)
		{
			case 0: randomFactKey = "FACTS0"; break;
			case 1: randomFactKey = "FACTS1"; break;
			case 2: randomFactKey = "FACTS2"; break;
			case 3: randomFactKey = "FACTS3"; break;
			default: randomFactKey = "FACTS0"; break;

		}
		Debug.Log(randomFactKey);
		return randomFactKey;
	}
}
