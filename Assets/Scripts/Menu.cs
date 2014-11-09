using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{

	void OnGUI () {
		// Make a background box
        GUI.Box(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 25, 125, 100), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 80, 20), "Level 1"))
        {
			Application.LoadLevel("level_1");
		}
		
		// Make the second button.
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 25, 80, 20), "Level 2"))
        {
			Application.LoadLevel(2);
		}
	}
}