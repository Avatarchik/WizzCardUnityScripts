using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class slotForUseCardFH : MonoBehaviour, IDropHandler {
	#region IDropHandler implementation
	public static ArrayList cardsOnBattlefield = new ArrayList();

	public void OnDrop (PointerEventData eventData)	{
		if (!AttackDnd.attackObj && useCardFromHand.beginDrag != null) {
			cardsOnBattlefield.Add(useCardFromHand.beginDrag);
			useCardFromHand.beginDrag.transform.SetParent (transform);
			useCardFromHand.beginDrag.transform.localScale = new Vector3 (7f, 12f, 1f);
			Destroy (useCardFromHand.beginDrag.GetComponent<useCardFromHand> ());
			useCardFromHand.beginDrag.AddComponent<AttackDnd> ();
			useCardFromHand.beginDrag.GetComponent<AttackDnd>().enabled = false;
			useCardFromHand.beginDrag.GetComponent<CanvasGroup> ().blocksRaycasts = false;
			Client.SendSimpleMessage (useCardFromHand.message);
			loadHeroImgOnBattlefield.gemsCount -= useCardFromHand.beginDrag.GetComponent<Card>().cardCost;
			Client.SendSimpleMessage("enemyGems:" + loadHeroImgOnBattlefield.gemsCount + ":");
			setGems(loadHeroImgOnBattlefield.gemsCount);
			useCardFromHand.beginDrag = null;
			useCardFromHand.message = "wc:useCard:";
		}
	}

	#endregion

	private void setGems(int gemsCount){
		GameObject.Find ("heroGems").GetComponent<Text> ().text = gemsCount + "";
	}

}
