using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class HeroAttackSlot : MonoBehaviour, IDropHandler {
	public static string enemyHP = "";
	Regex reg = new Regex (@"hp:(\d+):");
	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{

		if (AttackDnd.attackObj != null && AttackDnd.attackObj.GetComponent<Card>().canUse && !useCardFromHand.beginDrag) {
			string message = "wc:attackHero:";
			message += AttackDnd.attackObj.GetComponent<Card>().cardAttack + ":" + loadHeroImgOnBattlefield.hero + ":";
			enemyHP = Client.SendMessage(message);
			while( !enemyHP.Contains("hp:")){
				enemyHP = Client.ReadMessage();
			}
			Debug.Log(enemyHP);
			Match matcher = reg.Match(enemyHP);
			GameObject.Find("enemyHP").GetComponent<Text>().text = matcher.Groups[1].Value;
			if (Int32.Parse(matcher.Groups[1].Value) == 0) {
				endGame ();
			}
			message = "wc:attackHero:";
			AttackDnd.attackObj.GetComponent<Card>().canUse = false;


		}
	}
	#endregion

	private void endGame(){
		for(int i = loadCardsInBattle.endGame.Count - 1; i >= 0; i--) {
			var g = loadCardsInBattle.endGame[i] as GameObject;
			if (g.name == "endWin"){
				var gameObj = Instantiate(g);
				gameObj.transform.SetParent(GameObject.Find("mainPanel").transform);
				gameObj.transform.position = new Vector3(Screen.width/2,Screen.height/2,0);
				loadCardsInBattle.endGame.Clear();
			}
		}
	}


}
