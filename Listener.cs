using UnityEngine;
using System.Collections;
using System.Threading;
using System.Text.RegularExpressions;

public class Listener : MonoBehaviour {
	public static bool enemyTurn = true;
	private static string parsePattern = @"card:(\w+):(\d+):"; 
	public static string str = "";
	public static ArrayList cardsList = new ArrayList();
	public static ArrayList cardsChange = new ArrayList();
	public static string turnTriger = "";
	public static string myHP = "";
	public static string enemyGems = "";
	public static Thread listener;


	// Use this for initialization
	void Start () {
		listener = new Thread (listen);
	}

	public static void fuckingStart(){
		listener = new Thread (listen);
		listener.Start ();
	//	Debug.Log ("Is started");
	}
	
	static void listen(){
		while (enemyTurn) {
			string message  = Client.ReadMessage();
			if(message.Contains("card:")){
				Regex reg = new Regex(parsePattern);
				Match match = reg.Match(message);
				str = match.Groups[1].Value + ":" + match.Groups[2].Value;
			//	Debug.Log(str);
				cardsList.Add(str);
			}
			else if(message.Contains("result:")){
				cardsChange.Add(message);
				Debug.Log(message);
			}
			else if (message.Contains("hp:")){
				myHP = message;
			}

			else if (message.Contains("Next turn")){
				Listener.enemyTurn = false;
				turnTriger = "Next turn"; 
			}
			else if (message.Contains("enemyGems:")){
				enemyGems = message;
			}

		}

	}
}