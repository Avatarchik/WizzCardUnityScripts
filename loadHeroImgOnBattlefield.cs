using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class loadHeroImgOnBattlefield : MonoBehaviour {
	private string enemyHeroName;
	private Sprite heroSprite;
	private static Regex reg = new Regex (@"result:(\d):(\d):(\w+) (\d+):(\w+) (\d+):");
	private static Regex attackHero = new Regex (@"hp:(\d+):");
	private static Regex gemsChange = new Regex (@"enemyGems:(\d):");
	public static string attackCardName = "";
	public static string defCardName = "";
	public static string defCardHP = "";
	public static string attackCardHP = "";
	public static string hero = "";
	private GameObject gems;
	private GameObject enemyGems;
	public static int gemsCount;

	// Use this for initialization
	void Start () {

		loadHeroImg (chooseHeroScript.heroName,gameObject);
		enemyHeroName = Client.ReadMessage ();
		GameObject enemyImg = GameObject.FindWithTag ("enemyHeroImg");
		loadHeroImg (enemyHeroName, enemyImg);
	//	Debug.Log (enemyHeroName);
		Client.loadEnemyCardList (client.enemyCardDeck);
		string turn = Client.ReadMessage ();
		if (turn.Contains ("U r first")) {
			loadCardsInBattle.cardGenerator = 0;
			hero = "firstPlayer";
			Listener.enemyTurn = false;
			Debug.Log (turn);
		} else if (turn.Contains ("U r second")) {
			hero = "secondPlayer";
			var endTurnButton = GameObject.Find ("endTurnKey");
			endTurnButton.GetComponentInChildren<Text> ().text = "Чужой ход";
			endTurnButton.GetComponent<Button>().enabled = false;
			loadCardsInBattle.cardGenerator = 16;
			Listener.enemyTurn = true;
			Debug.Log (turn);
		}
		Listener.fuckingStart ();
		gems = GameObject.Find("heroGems");
		enemyGems = GameObject.Find ("enemyGems");
		gemsCount = Int32.Parse(gems.GetComponent<Text>().text);
	}

	void Update(){

		if (Listener.cardsList.Count > 0) {
			for (int i = Listener.cardsList.Count - 1; i >= 0; i--){
				string g = Listener.cardsList[i] as String;
				loadCardsInBattle.enemyUseCard (g);
				Listener.cardsList.Remove(g);
			}
		} 
		else if (Listener.cardsChange.Count > 0){
			for (int i = Listener.cardsChange.Count - 1; i >= 0; i--){
				string str = Listener.cardsChange[i] as String;
				Match match = reg.Match(str);
				attackCardHP = match.Groups[1].Value;
				defCardHP = match.Groups[2].Value;
				attackCardName = match.Groups[3].Value + " " + match.Groups[4];
				defCardName = match.Groups[5].Value + " " + match.Groups[6];
				GameObject attackCard = GameObject.Find(attackCardName);
				GameObject defCard = GameObject.Find (defCardName);
				setCardHP(attackCard, defCard, attackCardHP, defCardHP);
				Debug.Log(attackCardHP + " " + defCardHP  + " " +  attackCardName  + " " + defCardName);
				Listener.cardsChange.Remove(str);
			}
		}
		else if (Listener.myHP.Contains("hp:")){
			Match matcher = attackHero.Match (Listener.myHP);
			setMyHP(matcher.Groups[1].Value);
			if (Int32.Parse(matcher.Groups[1].Value) == 0){
				endGame();
			}
			Listener.myHP = "";
		}
		else if (Listener.enemyGems.Contains("enemyGems:")){
			Match matcher = gemsChange.Match(Listener.enemyGems);
			setEnemyGems(matcher.Groups[1].Value);
			Listener.enemyGems = "";
		}
		if(Listener.turnTriger.Contains("Next turn")){
			gemsCount = Int32.Parse(gems.GetComponent<Text>().text);
			loadCardsInBattle.setUrTurn();
			Listener.turnTriger = "";
		}

	}
	private void setCardHP(GameObject attackCard, GameObject defCard, string hp, string hp2){
		if (attackCard != null && defCard != null) {
			int firstCard = 1;
			int secondHp = 1;
			var cardAttr = attackCard.GetComponentsInChildren<Text> ();
			cardAttr [2].text = hp;
			var cardAttribute = defCard.GetComponentsInChildren<Text> ();
			cardAttribute [2].text = hp2;
			if ((firstCard = Int32.Parse (hp)) <= 0)
				Destroy (attackCard);
			else if ((secondHp = Int32.Parse (hp2)) <= 0)
				Destroy (defCard);
		}
	}

	private void endGame(){
		for(int i = loadCardsInBattle.endGame.Count - 1; i >= 0; i--) {
			var g = loadCardsInBattle.endGame[i] as GameObject;
			if (g.name == "endLose"){
				var gameObj = Instantiate(g);
				gameObj.transform.SetParent(GameObject.Find("mainPanel").transform);
				gameObj.transform.position = new Vector3(Screen.width/2,Screen.height/2,0);
				loadCardsInBattle.endGame.Clear();
			}
		}
	}
	
	private void setEnemyGems(string gamesCount){
		//Debug.Log ("Is called");
		enemyGems.GetComponent<Text> ().text = gamesCount;
	}

	private void setMyHP(string hp){

		GameObject.Find ("heroHp").GetComponent<Text> ().text = hp;
	}

	private void loadHeroImg(string heroName, GameObject heroImg){
		if (heroName.Contains ("Hunt")) {
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/Hunt"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
		}
		else if (heroName.Contains ("Mage")) {
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/Mage"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
		} 
		else if (heroName.Contains ("Warrior")) {
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/warrior"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
		}
		else if (heroName.Contains ("Necromancer")) {
			heroSprite = Sprite.Create (Resources.Load<Texture2D> ("hero/necro"), new Rect (0, 0, 200, 250), new Vector2 (0.5f, 0.5f), 25f);
		}
		else
			return;
		heroImg.GetComponent<Image>().sprite = heroSprite;
	}
}