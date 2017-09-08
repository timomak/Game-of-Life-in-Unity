using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

	private int numCols = 8;
	private int numRows = 5;
	private float cellSideLenght = 1;
	private float margin = 0.5f;
	private Cell[,] cells;
	private float evolutionPeriod = 0.5f;
	private float evolutionTimer = 0.5f;

	private void Evolve() {

		for (int col = 0; col < cells.GetLength(0); ++col) {
			for (int row = 0; row < cells.GetLength(1); ++row) {

				int numAliveNeighbors = GetNumAliveNeighbors(col,row);

				Cell cell = cells[col,row];

				if (cell.IsAlive()) {

					if (numAliveNeighbors < 2 || numAliveNeighbors > 3) {
						cell.isAliveNext = false;
					} else {
						cell.isAliveNext = true;
					}

				} else if (!cell.IsAlive() && numAliveNeighbors == 3) {
					cell.isAliveNext = true;
				}
			}
		}

		foreach (Cell cell in cells) {
			cell.UpdateIsAlive();
		}
	}
	private int GetNumAliveNeighbors(int colCenter, int rowCenter) {

		int numAliveNeighbors = 0;

		for (int dCol = -1; dCol <= 1; ++dCol) {
			for (int dRow = -1; dRow <= 1; ++dRow) {

				if (dCol == 0 && dRow == 0) {continue;}

				int col = colCenter + dCol;
				int row = rowCenter + dRow;

				if (col < 0) {col = cells.GetLength(0) - 1;}
				if (col >= cells.GetLength(0)) {col = 0;}

				if (row < 0) {row = cells.GetLength(1) - 1;}
				if (row >= cells.GetLength(1)) {row = 0;}

				if (cells[col,row].IsAlive()) {
					++numAliveNeighbors;
				}
			}
		}

		return numAliveNeighbors;
	}
	// Use this for initialization
	void Start ()
	{
		cells = new Cell[numCols, numRows];
		for (int col = 0; col < numCols; ++col) {
			for (int row = 0; row < numRows; ++row) {
				Cell cell = Utilities.GetNewCell ();
				float x = (col + 0.5f - numCols * 0.5f) * (cellSideLenght + margin);
				float y = (row + 0.5f - numRows * 0.5f) * (cellSideLenght + margin);
				cell.transform.localPosition = new Vector2 (x, y);
				cells [col, row] = cell;
			}
		}	
		cells [2, 3].isAliveNext = true;
		cells [3, 2].isAliveNext = true;
		cells [4, 4].isAliveNext = true;		
		cells [4, 2].isAliveNext = true;
		cells [4, 3].isAliveNext = true;
	}
    
	// Update is called once per fram
	void Update ()
	{
		evolutionTimer -= Time.deltaTime;
		if (evolutionTimer < 0) {
			evolutionTimer = evolutionPeriod;
			Evolve ();
		}
	}

	public void PlayOrPause ()
	{
		
	}

	public void Clear ()
	{

	}

	public void Randomize ()
	{
		
	}

	public int GetPopulationCount ()
	{
		return 0;
	}

	public int GetGenerationCount ()
	{
		return 0;
	}

	public void SetEvolutionPeriod (float sliderValue)
	{

	}
}
