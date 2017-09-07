using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	private int numCols = 8;
	private int numRows = 5;
	private float cellSideLenght = 1;
	private float margin = 0.5f;

	// Use this for initialization
	void Start() {
		for (int col = 0; col < numCols; ++col) {
			for (int row = 0; row < numRows; ++row) {
				Cell cell = Utilities.GetNewCell ();
				float x = (col + 0.5f - numCols * 0.5f) * (cellSideLenght + margin);
				float y = (row + 0.5f - numRows * 0.5f) * (cellSideLenght + margin);
				cell.transform.localPosition = new Vector2 (x, y);
			}
		}				
	}
    
	// Update is called once per fram
	void Update() {
		
	}

	public void PlayOrPause() {
		
	}

	public void Clear() {

	}

	public void Randomize() {
		
	}

	public int GetPopulationCount() {
        	return 0;
	}

	public int GetGenerationCount() {
        	return 0;
	}
	
	public void SetEvolutionPeriod(float sliderValue) {

	}
}
