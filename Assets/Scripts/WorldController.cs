﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
	public HexCell cellPrefab;
	public byte activeTerrain;
	public Text cellLabelPrefab;
	public GameObject hexCursor;

	public ChunkController chunkPrefab;
	ChunkController[] chunks;

	public int MapType { get; protected set; }
	public int NumberHexColumns { get; protected set; }
	public int NumberHexRows { get; protected set; }
	public int ChunkSize { get; protected set; }
	public int ChunkColumns { get; protected set; }
	public int ChunkRows { get; protected set; }

	HexCell[] cells;

	public void CreateMap(int mapType, byte defaultterraintype, int chunksize, int chunkcolumns, int chunkrows)
	{
		this.MapType = mapType;
		this.ChunkSize = chunksize;
		this.ChunkColumns = chunkcolumns;
		this.ChunkRows = chunkrows;

		HexMetrics.Init(mapType);
		hexCursor.SetActive(true);
		hexCursor.GetComponent<HexCursor>().Init(mapType);
		Camera.main.orthographicSize = 50.0f;

		this.NumberHexColumns = chunkcolumns * chunksize;
		this.NumberHexRows = chunkrows * chunksize;

		CreateChunks();
		CreateCells(mapType, defaultterraintype, this.NumberHexColumns, this.NumberHexRows);

		//TODO:  CENTER CAMERA ON MAP--LINE BELOW KIND OF WORKS, BUT MATH LOOKS OFF
		//Camera.main.transform.position = new Vector2(numbercolumns * (int)HexMetrics.innerRadius, numberrows * (int)HexMetrics.innerRadius);
	}

	void CreateChunks()
	{
		chunks = new ChunkController[ChunkColumns * ChunkRows];

		for (int z = 0, i = 0; z < ChunkRows; z++)
		{
			for (int x = 0; x < ChunkColumns; x++)
			{
				ChunkController chunk = chunks[i++] = Instantiate(chunkPrefab);
				chunk.Init(ChunkSize);
				chunk.transform.SetParent(transform);
				chunk.name = "chunk " + (i-1); //-1 MAKES CHUNK NAMES ZERO-BASED
				chunk.tag = "Chunk";
			}
		}
	}

	void CreateCells(int maptype, byte defaultterraintype, int numberhexcolumns, int numberhexrows)
	{
		cells = new HexCell[numberhexcolumns * numberhexrows];

		for (int z = 0, i = 0; z < numberhexrows; z++)
		{
			for (int x = 0; x < numberhexcolumns; x++)
			{
				CreateCell(maptype, defaultterraintype, x, z, i++);
			}
		}
	}

	void CreateCell(int maptype, byte defaultterraintype, int x, int z, int i)
	{
		Vector3 position;
		switch (maptype)
		{
			case 0: //POINTY-TOP HEX
				position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
				position.y = 0f;
				position.z = z * (HexMetrics.outerRadius * 1.5f);
				break;
			case 1: //FLAT-TOP HEX
				position.x = x * (HexMetrics.outerRadius * 1.5f);
				position.y = 0f;
				position.z = (z + x * 0.5f - x / 2) * (HexMetrics.innerRadius * 2f);
				break;
			default:
				position.x = 0f;
				position.y = 0f;
				position.z = 0f;
				break;
		}
		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(maptype, x, z);
		
		int y = (-x - z);
		
		Text label = Instantiate<Text>(cellLabelPrefab);
		label.tag = "HexLabel";
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		
		//TODO:  GUI LABELS AXIAL VS OFFSET
		label.text = cell.coordinates.ToStringOnSeparateLines(); //FOR HEX LABEL IN AXIAL COORDS
		cell.name = cell.coordinates.ToString(); //FOR CELL NAME IN AXIAL COORDS
		//label.text = (x + ", " + z); //FOR HEX LABEL IN OFFSET COORDS
		//cell.name = (x + ", " + y + ". " + z); //FOR CELL NAME IN OFFSET COORDS

		label.name = "Label " + cell.coordinates.ToString();
		cell.uiRect = label.rectTransform;
		cell.terraintype = defaultterraintype;
		AddCellToChunk(x, z, cell);
	}

	void AddCellToChunk(int x, int z, HexCell cell)
	{
		int chunkX = x / ChunkSize;
		int chunkZ = z / ChunkSize;

		ChunkController chunk = chunks[chunkX + chunkZ * ChunkColumns];

		int localX = x - chunkX * ChunkSize;
		int localZ = z - chunkZ * ChunkSize;
		chunk.AddCell(localX + localZ * ChunkSize, cell);
	}

	public HexCell GetHexCell(Vector3 position)
	{
		int index;
		int maptype = this.MapType;
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(maptype, position);
		Debug.Log("touched at " + coordinates.ToString());
		
		//GET CELL INDEX
		switch (maptype)
		{
			case 0:  //POINTY-TOP HEX
				index = coordinates.X + coordinates.Z * NumberHexColumns + coordinates.Z / 2;
				break;
			case 1: //FLAT TOP HEX
				index = (coordinates.Z + coordinates.X / 2) * NumberHexColumns + coordinates.X;
				break;
			default:
				index = 0;
				break;
		}
		HexCell cell = cells[index];
		
		////TO COLORIZE CELLS
		//if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		//{
		//	cell.terraintype = EditorController.GetActiveTerrainType();
		//	//hexMesh.Triangulate(cells); //CHUNKS
		//}

		return cell;
		
	}
	public void RefreshWorld()
	{
	}
}
