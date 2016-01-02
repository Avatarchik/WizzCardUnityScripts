using UnityEngine;
using System.Collections;

public class goToScene3 : MonoBehaviour {
	private GameObject g;
	void Start(){

	}
	// Use this for initialization
	public void click () {

		if (client.cardsDeck.Count == 15) {
			string gameStatus = "";
			gameStatus = Client.SendMessage ("start game");
			Client.SendSimpleMessage (chooseHeroScript.heroName);
			Client.SendCardDeck ();
			if (gameStatus.Contains ("game is start")) {
				Application.LoadLevel (3);
			} else if (Client.ReadMessage ().Contains ("game is start")) {
		
				Application.LoadLevel (3);
			}
		}
	}

}
