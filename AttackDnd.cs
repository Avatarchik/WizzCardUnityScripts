using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackDnd : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
	public static string attackCardMessage = "wc:attackCard:";
	public static Vector3 startPosition;
	public static string attackCardDamage = "wc:attackHero:";
	public static GameObject attackObj;



	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
	 if(!Listener.enemyTurn && gameObject.GetComponent<Card>().canUse) {
		startPosition = transform.position;
		attackObj = gameObject;
		var cardAttribute = gameObject.GetComponentsInChildren<Text> ();
		attackCardMessage += cardAttribute [1].text + ":";
		attackCardDamage += cardAttribute [1].text + ":";
		attackCardMessage += cardAttribute [2].text + ":";
		attackCardMessage += attackObj.name + ":";
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}
	#endregion

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		if (!Listener.enemyTurn && gameObject.GetComponent<Card>().canUse ) {
			transform.position = Input.mousePosition;
		}
	}
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData)
	{
		if (!Listener.enemyTurn && attackObj != null) {
			attackObj = null;
			transform.position = startPosition;
			attackCardMessage = "wc:attackCard:";
			attackCardDamage = "wc:attackHero:";
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		}
	}
	#endregion
}
