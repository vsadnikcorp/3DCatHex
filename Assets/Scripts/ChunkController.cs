using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkController : MonoBehaviour
{
	HexCell[] cells;

	HexMesh hexMesh;
	Canvas gridCanvas;

	void Awake()
	{
	}

	public void Init(int chunksize)
	{
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();
		cells = new HexCell[chunksize * chunksize];
	}
		
	void LateUpdate()
	{
		hexMesh.Triangulate(cells);
		enabled = false;
	}

	public void AddCell(int index, HexCell cell)
	{
		cells[index] = cell;
		cell.transform.SetParent(transform, false);
		cell.uiRect.SetParent(gridCanvas.transform, false);
		cell.chunk = this;
	}

	public void RefreshChunks()
	{
		enabled = true;
	}
}
