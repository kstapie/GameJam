using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour {
	
	void OnGUI () {

		GUI.Box (new Rect (10, 585, 110, 25), "Droplet Strength");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(5,5,80,20), "Main Menu")) {
			Application.LoadLevel("menu");
		}
	}
}