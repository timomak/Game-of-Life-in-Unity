using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	private bool isAlive;
	float scaleFactor;
    // Use this for initializatio
    void Start() {

    }
    
    // Update is called once per fram
    void Update() {
		if (isAlive == true) {
			scaleFactor = 1;
			Utilities.ChangeCellColor (this, Color.green);
		} else {
			scaleFactor = 0.5f;
			Utilities.ChangeCellColor (this, Color.red);
		}
    }
}
