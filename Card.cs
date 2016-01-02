using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {
	public int cardCost = 1;
	public int cardAttack = 2;
	public int cardHP = 3;
	public bool canUse = false;


	// Use this for initialization
	void Start () {
		change ();
	}

	void change()
	{
		var game_obj = gameObject.GetComponentsInChildren<Text> ();
		game_obj [0].text = "" + cardCost;
		game_obj [1].text = "" + cardAttack;
		game_obj [2].text = "" + cardHP;
	}
	public void setUse(bool g){
		canUse = g;
	}
}
	
