using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
	private bool isAlive;
	public bool isAliveNext;
	private float scaleFactor;
	public bool IsAlive() {
		return isAlive;
	}

	public void UpdateIsAlive() {
		isAlive = isAliveNext;
	}
    // Use this for initializatio
    void Start() {
		
    }
    
    // Update is called once per fram
    void Update() {
		if (isAlive == true) {
			scaleFactor = 1;
			transform.localScale = Vector3.one * scaleFactor;
			Utilities.ChangeCellColor (this, Color.green);
		} else {
			scaleFactor = 0.5f;
			transform.localScale = Vector3.one * scaleFactor;
			Utilities.ChangeCellColor (this, Color.red);
		}
    }
}
