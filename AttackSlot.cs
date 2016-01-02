using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
public class AttackSlot : MonoBehaviour, IDropHandler {
	#region IDropHandler implementation


	public void OnDrop (PointerEventData eventData)
	{
		if (!useCardFromHand.beginDrag && AttackDnd.attackObj != null && AttackDnd.attackObj.GetComponent<Card>().canUse) {
			Regex reg = new Regex (@"result:(\d):(\d):.*");
			var cardAttribute = gameObject.GetComponentsInChildren<Text> ();
			AttackDnd.attackCardMessage += cardAttribute [1].text + ":";
			AttackDnd.attackCardMessage += cardAttribute [2].text + ":";
			AttackDnd.attackCardMessage += gameObject.name + ":";
			Debug.Log (AttackDnd.attackCardMessage);
			string result = Client.SendMessage (AttackDnd.attackCardMessage);
			while( !result.Contains("result")){
				result = Client.ReadMessage();
			}
			Debug.Log (result);
			Match match = reg.Match (result);
			if(match.Success){
				String hp1 = match.Groups [1].Value;
				string hp2 = match.Groups [2].Value;
				if(hp1 != "" && hp2 != ""){
					AttackDnd.attackObj.GetComponent<Card>().canUse = false;
					setCardHP (AttackDnd.attackObj, gameObject, hp1, hp2);
					AttackDnd.attackCardMessage = "wc:attackCard:";
				}
			}
		}
	}

	#endregion

	private void setCardHP(GameObject attackCard, GameObject defCard, string hp, string hp2){
		int firstCard = 1;
		int secondHp = 1;
		Debug.Log(hp + " " + hp2);
		var cardAttr = attackCard.GetComponentsInChildren<Text> ();
		var cardAttribute = defCard.GetComponentsInChildren<Text> ();
		cardAttr [2].text = hp;
		cardAttribute [2].text = hp2;
		if ((firstCard = Int32.Parse (hp)) <= 0) {
			Destroy (attackCard);
			slotForUseCardFH.cardsOnBattlefield.Remove (attackCard);
		}
		if ((secondHp = Int32.Parse(hp2)) <= 0)
			Destroy(defCard);
	}
}
