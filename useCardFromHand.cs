using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System;

public class useCardFromHand : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler {
	#region IBeginDragHandler implementation
	private Vector3 startPosition;
	public static GameObject beginDrag;
	public static string message = "wc:useCard:";

	public void OnBeginDrag (PointerEventData eventData)
	{
		if (!Listener.enemyTurn && canUse()) {
			message += gameObject.name + ":";
			//Debug.Log (message);
			startPosition = transform.position;
			beginDrag = gameObject;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (!Listener.enemyTurn && canUse()) {
			transform.position = Input.mousePosition;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if (!Listener.enemyTurn && canUse()) {
			message = "wc:useCard:";
			beginDrag = null;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			transform.position = startPosition;
		}
	}

	private bool canUse(){
		if (loadHeroImgOnBattlefield.gemsCount >= gameObject.GetComponent<Card> ().cardCost)
			return true;
		else
			return false;
	}
	#endregion
}
