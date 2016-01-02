using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class getData : MonoBehaviour {
	public static string netString;
	private string login = "";
	private string password = "";

	public void gatText(){
		netString = "wc:1:";
		login = GameObject.Find("Login").GetComponent<InputField>().text;
		password = GameObject.Find ("Password").GetComponent<InputField> ().text;
		netString += login + ":" + password+":";

	}

	public string getNetString(){
		return netString;
	}
}
