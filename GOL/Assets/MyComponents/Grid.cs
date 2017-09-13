using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

	private int numCols = 8;
	private int numRows = 5;
	private float cellSideLenght = 1;
	private float margin = 0.5f;
	private Cell[,,] cells;
	private float evolutionPeriod = 0.5f;
	private float evolutionTimer = 0.5f;
	private bool isPlaying;
	private float evolutionPeriodMax = 1.5f;
	private float evolutionPeriodMin = 0.25f;
	public int generations = 0;
	public int population = 0;
	private int numLays = 5;

	private void Evolve() {
		foreach (Cell cell in cells) {
			cell.UpdateIsAlive();
		}
		if (isPlaying == true) {
			generations += 1;
			population = 0;
			for (int col = 0; col < cells.GetLength (0); ++col) {
				for (int row = 0; row < cells.GetLength (1); ++row) {
					for (int lays = 0; lays < cells.GetLength (1); ++lays) {

						int numAliveNeighbors = GetNumAliveNeighbors (col, row, lays);

						Cell cell = cells [col, row, lays];

						if (cell.IsAlive ()) {

							if (numAliveNeighbors < 2 || numAliveNeighbors > 3) {
								cell.isAliveNext = false;
							} else {
								cell.isAliveNext = true;
							}

						} else if (!cell.IsAlive () && numAliveNeighbors == 3) {
							cell.isAliveNext = true;
						}
					}
				}
				foreach (Cell cell in cells) {
					if (cell.isAliveNext == true) {
						population += 1;
					}
				}
				if (population == 0) {
					generations = 0;
					isPlaying = !isPlaying;
				}
			}
		}
	}
	private int GetNumAliveNeighbors(int colCenter, int rowCenter, int laysCenter) {

		int numAliveNeighbors = 0;

		for (int dCol = -1; dCol <= 1; ++dCol) {
			for (int dRow = -1; dRow <= 1; ++dRow) {
				for (int dLays = -1; dLays <= 1; ++dLays) {

					if (dCol == 0 && dRow == 0 && dLays == 0) {
						continue;
					}

					int col = colCenter + dCol;
					int row = rowCenter + dRow;
					int lays = laysCenter + dLays;

					if (col < 0) {
						col = cells.GetLength (0) - 1;
					}
					if (col >= cells.GetLength (0)) {
						col = 0;
					}

					if (row < 0) {
						row = cells.GetLength (1) - 1;
					}
					if (row >= cells.GetLength (1)) {
						row = 0;
					}

					if (lays < 0) {
						lays = cells.GetLength (1) - 1;
					}
					if (lays >= cells.GetLength (1)) {
						lays = 0;
					}

					if (cells [col, row, lays].IsAlive ()) {
						++numAliveNeighbors;
					}
				}
			}
		}

		return numAliveNeighbors;
	}
	// Use this for initialization
	void Start ()
	{
		cells = new Cell[numCols, numRows, numLays];
		for (int col = 0; col < numCols; ++col) {
			for (int row = 0; row < numRows; ++row) {
				for (int lays = 0; lays < numLays; ++lays) {
					Cell cell = Utilities.GetNewCell ();
					float x = (col + 0.5f - numCols * 0.5f) * (cellSideLenght + margin);
					float y = (row + 0.5f - numRows * 0.5f) * (cellSideLenght + margin);
					float z = (lays + 0.5f - numLays * 0.5f) * (cellSideLenght + margin);
					cell.transform.localPosition = new Vector3 (x, y,z);
					cells [col, row, lays] = cell;
				}
			}
		}	

		cells [4, 1, 1].isAliveNext = true;		
		cells [4, 2, 1].isAliveNext = true;
		cells [4, 3, 1].isAliveNext = true;
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
	private void ResetEvolutionTimer() {
		evolutionTimer = 0;
	}

	public void PlayOrPause ()
	{
		ResetEvolutionTimer();
		isPlaying = !isPlaying;
	}

	public void Clear ()
	{
		isPlaying = !isPlaying;
		generations = 0;
		ResetEvolutionTimer();
		foreach (Cell cell in cells) {
			cell.isAliveNext = false;
		}
	}

	public void Randomize ()
	{
		generations = 0;
		ResetEvolutionTimer();
		foreach (Cell cell in cells) {
			cell.isAliveNext = Random.value < 0.5f;
		}
	}

	public int GetPopulationCount ()
	{
		return population;
	}

	public int GetGenerationCount ()
	{
		return generations;
	}

	public void SetEvolutionPeriod (float sliderValue)
	{
		evolutionPeriod = Mathf.Lerp (evolutionPeriodMin, evolutionPeriodMax, 1.0f - sliderValue);
	}
}
