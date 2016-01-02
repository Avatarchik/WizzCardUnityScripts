using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler {
	public  GameObject miniCardSlot;
	private int minCounter = 1;
	private const int MAX_COUNTER = 20;
	private Vector3 s;

	#region IDropHandler implementation

	void Start() {
		s = GameObject.Find ("card").transform.localScale;
	}

	public void OnDrop (PointerEventData eventData)
	{

		if (minCounter == 1) {
			var gameObj = gameObject.GetComponentsInChildren<Text> ();
			gameObj [1].text = "" + DnD.cardCost;
			gameObj [0].text = DnD.nameOfCard;
			minCounter++;
			string str = DnD.game_obj.name.Replace("(Clone)","");
			client.cardsDeck.Add (str);
		} else {
			if (minCounter <= 15){
				var gameObj = addCardSlot ().GetComponentsInChildren<Text> ();
				gameObj [1].text = "" + DnD.cardCost;
				gameObj [0].text = DnD.nameOfCard;
				string str = DnD.game_obj.name.Replace("(Clone)","");
				client.cardsDeck.Add (str);
				//Debug.Log(str);
				minCounter++;
			}
		}

	}

	#endregion


	public GameObject addCardSlot () {
		var obj = Instantiate (miniCardSlot);
		obj.transform.parent = GameObject.Find ("cardPanel").transform;
		obj.transform.localScale = s;
		return obj;
	}
	
}
