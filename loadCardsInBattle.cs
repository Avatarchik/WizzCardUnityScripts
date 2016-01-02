using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class loadCardsInBattle : MonoBehaviour {
	private ArrayList allObject = new ArrayList();
	private ArrayList playerCardsPull = new ArrayList();
	private static ArrayList enemyCardsPull = new ArrayList();
	public static int cardGenerator;
	private static string parsePattern = @"(\w+):(\d+)";
	private static bool isButtonPressed = false;
	private int gemsCount = 1;
	public static ArrayList endGame = new ArrayList();
	// Use this for initialization
	void Start () {
		loadResurses ();
		foreach (string s in client.cardsDeck) {
			findObjWithName (s,playerCardsPull);
		}
		foreach(string s in client.enemyCardDeck){
			findObjWithName(s, enemyCardsPull);
		}

		for (int i = 0; i < 3; i++) {
			int random = Random.Range(0,playerCardsPull.Count);
			GameObject randomGameObj = playerCardsPull[random] as GameObject;
			playerCardsPull.RemoveAt(random);
			//Debug.Log("Random Game Obj name is " + randomGameObj.name);
			var game  = Instantiate(randomGameObj);
			game.name = randomGameObj.name +" "+ cardGenerator;
			cardGenerator++;
			game.GetComponent<DnD>().enabled = false;
			game.AddComponent<useCardFromHand>();
			game.transform.parent = GameObject.Find ("handCardPanel").transform;
			game.transform.localScale = new Vector3(8f,12f,1f);

		}

	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] gObj = Resources.LoadAll<GameObject>("endGame");
		foreach (GameObject g in gObj) {
			endGame.Add(g);
		}
	}
	public void loadEndGame(){

	}
	public static void enemyUseCard(string cardName){
		foreach (GameObject g in enemyCardsPull) {
			Regex reg = new Regex(parsePattern);
			Match match = reg.Match(cardName);
			string card = match.Groups[1].Value;
			string number = match.Groups[2].Value;
			if(g.name == card){
				var obj = Instantiate(g);
				obj.name = g.name +" "+ number;
				obj.transform.parent = GameObject.Find ("usingEnemyCardPanel").transform;
				obj.transform.localScale = new Vector3 (7f, 12f, 1f);
				Destroy(obj.GetComponent<DnD>());
				obj.AddComponent<AttackSlot>();
				enemyCardsPull.Remove(g);
				break;
			}
		}
	}


	private void addToArrayList(GameObject[] g){
		foreach (GameObject gObj in g) {
			allObject.Add (gObj);
		}
	}

	private void loadResurses(){
		GameObject[] gObj = Resources.LoadAll<GameObject> ("forAll");
		addToArrayList (gObj);
		gObj = Resources.LoadAll<GameObject> ("Hunt");
		addToArrayList (gObj);
		gObj = Resources.LoadAll<GameObject> ("Necro");
		addToArrayList (gObj);
		gObj = Resources.LoadAll<GameObject> ("Warrior");
		addToArrayList (gObj);
		gObj = Resources.LoadAll<GameObject> ("Mage");
		addToArrayList (gObj);
	}

	private void findObjWithName(string str, ArrayList listToSave){
			foreach (GameObject g in allObject) {
			if (g.name ==(str)){
				listToSave.Add (g);
				}
			}
		}

	public void getCard(){

		if (playerCardsPull.Count > 0) {
			int random = Random.Range (0, playerCardsPull.Count - 1);
			GameObject randomGameObj = playerCardsPull [random] as GameObject;
			playerCardsPull.RemoveAt (random);
			var game = Instantiate (randomGameObj);
			game.name = randomGameObj.name + " " + cardGenerator;
			cardGenerator++;
			game.transform.parent = GameObject.Find ("handCardPanel").transform;
			game.transform.localScale = new Vector3 (8f, 12f, 1f);
			game.GetComponent<DnD> ().enabled = false;
			game.AddComponent<useCardFromHand> ();
		} else
			this.name = "noCards";
}

	public void nextTurn (){
		if (!isButtonPressed) {
			if (gemsCount < 5){
				gemsCount++;
			}
			var gems = GameObject.Find("heroGems").GetComponent<Text>().text = gemsCount + "";
			Client.SendSimpleMessage("enemyGems:" + gems + ":");
			getCard();
			Listener.enemyTurn = true;
			Client.SendSimpleMessage ("Next turn");
			var endTurnButton = GameObject.Find ("endTurnKey");
			endTurnButton.GetComponentInChildren<Text> ().text = "Чужой ход";
			endTurnButton.GetComponent<Button>().enabled = false;
			Listener.fuckingStart ();
			isButtonPressed = true;
		}
	}

	public static void setUrTurn(){
		Listener.listener.Abort ();
		var endTurnButton = GameObject.Find ("endTurnKey");
		endTurnButton.GetComponentInChildren<Text> ().text = "Закончить ход";
		endTurnButton.GetComponent<Button>().enabled = true;
		isButtonPressed = false;
		for(int i = slotForUseCardFH.cardsOnBattlefield.Count-1; i >= 0; i--){
			var g = slotForUseCardFH.cardsOnBattlefield[i] as GameObject;
			if(g != null){
			g.GetComponent<AttackDnd>().enabled = true;
			g.GetComponent<CanvasGroup> ().blocksRaycasts = true;
			g.GetComponent<Card>().canUse = true;
			//slotForUseCardFH.cardsOnBattlefield.Remove(g);
			}
		}
	}

}