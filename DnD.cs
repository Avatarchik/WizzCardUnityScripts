using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class DnD : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static string nameOfCard;
	public static int cardCost;
	public static GameObject game_obj;
	private Vector3 startPosition;
	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		game_obj = gameObject;
		var text_game_obj = gameObject.GetComponentsInChildren<Text> ();
		cardCost = Convert.ToInt32(text_game_obj [0].text);
		nameOfCard = text_game_obj [3].text;
		startPosition = transform.position;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;

	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		nameOfCard = null;
		transform.position = startPosition;
	}

	#endregion



}
