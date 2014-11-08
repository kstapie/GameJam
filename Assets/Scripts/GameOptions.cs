using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour {
	
	void OnGUI () {
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(5,5,80,20), "Main Menu")) {
			Application.LoadLevel("menu");
		}
	}
}