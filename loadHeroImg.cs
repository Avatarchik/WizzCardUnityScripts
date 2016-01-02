using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loadHeroImg : MonoBehaviour {
	private Sprite heroSprite;
	// Use this for initialization
	void Start () {

		//chooseHeroScript.heroName
		if (chooseHeroScript.heroName.Contains ("Hunt")) {
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/Hunt"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
			GameObject.Find("mage").SetActive(false);
			GameObject.Find("necro").SetActive(false);
			GameObject.Find("warrior").SetActive(false);
		}
		 else if (chooseHeroScript.heroName.Contains ("Mage")) {
			GameObject.Find("hunt").SetActive(false);
			GameObject.Find("necro").SetActive(false);
			GameObject.Find("warrior").SetActive(false);
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/Mage"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
		} 
		else if (chooseHeroScript.heroName.Contains ("Warrior")) {
			GameObject.Find("hunt").SetActive(false);
			GameObject.Find("mage").SetActive(false);
			GameObject.Find("necro").SetActive(false);
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/warrior"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
		}
		else if (chooseHeroScript.heroName.Contains ("Necromancer")) {
			GameObject.Find("hunt").SetActive(false);
			GameObject.Find("mage").SetActive(false);
			GameObject.Find("warrior").SetActive(false);
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/necro"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
		}
		else
			return;
		gameObject.GetComponent<Image>().sprite = heroSprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
