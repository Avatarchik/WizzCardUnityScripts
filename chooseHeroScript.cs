using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class chooseHeroScript : MonoBehaviour  {

	public static string heroName;


	// Use this for initialization
	void OnMouseDown(){
		heroName = gameObject.GetComponentInChildren<Text> ().text;
		Application.LoadLevel ("2");

	}


}
