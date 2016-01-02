using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadPresets : MonoBehaviour {

	public static ArrayList common = new ArrayList();
	public static ArrayList necro = new ArrayList();
	public static ArrayList warrior = new ArrayList();
	public static ArrayList mage = new ArrayList();
	public static ArrayList hunt = new ArrayList();
	public static int counter = 1;
	private Vector3 scale;




	public void loadCommon() {
		try {clearPanel();
			}
		catch(UnityException e){ Debug.LogError(e.Data);}
		GameObject[] gos = Resources.LoadAll<GameObject> ("forAll") ;
		foreach (GameObject g in gos) {
			common.Add(g);
		}
		addToPanel(common,10);
		
	}
	public void loadNecro() {
		try {clearPanel();}
		catch(UnityException e){Debug.LogError(e.Data);}
		GameObject[] gos = Resources.LoadAll<GameObject> ("Necro") ;
		foreach (GameObject g in gos) {
			necro.Add(g);
		}
		addToPanel(necro,5);
	}
	public void loadWarrior() {
		try {clearPanel();
		}
		catch(UnityException e){Debug.LogError(e.Data);}
		GameObject[] gos = Resources.LoadAll<GameObject> ("Warrior") ;
		foreach (GameObject g in gos) {
			warrior.Add(g);
		}
		addToPanel(warrior,5);
	}
	public void loadHunt() {
		try {clearPanel();
		}
		catch(UnityException e){Debug.LogError(e.Data);}
		GameObject[] gos = Resources.LoadAll<GameObject> ("Hunt") ;
		foreach (GameObject g in gos) {
			hunt.Add(g);
		}
		addToPanel(hunt,5);
	}
	public void loadMage() {
		try {clearPanel();
		}
		catch(UnityException e){Debug.LogError(e.Data);}
		GameObject[] gos = Resources.LoadAll<GameObject> ("Mage") ;
		foreach (GameObject g in gos) {
			mage.Add(g);
		}
		addToPanel(mage,5);
	}

	private void addToPanel(ArrayList arrayList, int max_Counter){
		if (counter <= max_Counter){
		var cardPanel = GameObject.Find ("CardPanel");
		foreach (GameObject g in arrayList) {
			GameObject m = Instantiate (g);
			m.transform.SetParent(cardPanel.transform);
			m.transform.localScale = new Vector3(1f,1f,1f);
			m.transform.rotation = new Quaternion(0f,0f,0f,0f);
			counter++;
			}

		}
	}

	private void clearPanel(){
		counter = 1;
		common.Clear ();
		necro.Clear ();
		mage.Clear ();
		warrior.Clear ();
		hunt.Clear ();
		GameObject[] toDestoy = GameObject.FindGameObjectsWithTag ("itsCard");
		foreach (GameObject destoy in toDestoy) {
			Destroy (destoy);
			}
		}
}	
