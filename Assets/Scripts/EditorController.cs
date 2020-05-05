using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using System.IO;

public class EditorController : MonoBehaviour
 {
	public byte[] terraintypes;
	public WorldController world;
	public UnityEngine.UI.ToggleGroup toggleGroup;
	int brushSize;
	public byte ActiveTerrainType { get; protected set; }
	public byte DefaultTerrainType { get; protected set; }
	public int NumberColumns { get; protected set; }
	public int NumberRows { get; protected set; }
	public int MapType { get; protected set; }
	public int ChunkColumns { get; protected set; }
	public int ChunkRows { get; protected set; }
	public int ChunkSize { get; protected set; }

	public static byte GetDefaultTerrainType()
	{
		byte defaultTerrainType;
		//TODO: DEFAULT TERRAIN MUST BE ADDED TO GUI
		defaultTerrainType = 0;
		EditorController editor = FindObjectOfType<EditorController>();
		editor.DefaultTerrainType = defaultTerrainType;
		return defaultTerrainType;
	}

	public static byte GetActiveTerrainType()
	{
		EditorController editor = FindObjectOfType<EditorController>();
		//TODO:  REPLACE THIS WHEN SOMETHING OTHER THAN TOGGLES USED TO SET ACTIVE TERRAIN (NOTE: CANNOT USE STATIC "SETTERRAINGFX" IN INSPECTOR TO GET ACTIVE TERRAIN TYPE.
		byte terraintype = 0;
		ToggleGroup toggleGroup;
		toggleGroup = editor.GetComponentInChildren<ToggleGroup>();
		Toggle activetoggle = toggleGroup.ActiveToggles().FirstOrDefault();
		switch (activetoggle.name.ToString())
		{
			case "OpenToggle":
				terraintype = (byte)HexMetrics.TerrainType.Open;
				break;
			case "ForestToggle":
				terraintype = (byte)HexMetrics.TerrainType.Forest;
				break;
			case "MountainToggle":
				terraintype = (byte)HexMetrics.TerrainType.Mountain;
				break;
			case "SeaToggle":
				terraintype = (byte)HexMetrics.TerrainType.Sea;
				break;
			default:
				Debug.Log("No terrain type assigned!");
				break;
		}
		editor.ActiveTerrainType = terraintype;
		return terraintype;
	}

	//public static Color SetTerrainGFX(HexCell cell, byte terraintype)
	//{
	//	Color terrainGFX;
	//	switch (terraintype)
	//	{
	//		case 0:
	//			terrainGFX = Color.yellow;
	//			break;
	//		case 1:
	//			terrainGFX = Color.green;
	//			break;
	//		case 2:
	//			terrainGFX = Color.grey;
	//			break;
	//		case 3:
	//			terrainGFX = Color.blue;
	//			break;
	//		default:
	//			terrainGFX = Color.white;
	//			break;
	//	}
	//	return terrainGFX;
	//}

	public void CreateMapButton()
	{
		//if (world != null) ClearMap();  //
		MapType = GameObject.Find("MapTypeDropdown").GetComponent<TMP_Dropdown>().value;
		NumberColumns = Convert.ToInt16(GameObject.Find("MapXText").GetComponent<TMP_InputField>().text);
		NumberRows = Convert.ToInt16(GameObject.Find("MapYText").GetComponent<TMP_InputField>().text);
		ChunkSize = Convert.ToInt32(GameObject.Find("ChunkSize").GetComponent<TMP_InputField>().text);
		//ChunkColumns = Convert.ToInt32(GameObject.Find("ChunkColumns").GetComponent<TMP_InputField>().text);
		//ChunkRows = Convert.ToInt32(GameObject.Find("ChunkRows").GetComponent<TMP_InputField>().text);
		DefaultTerrainType = GetDefaultTerrainType();
		//world.CreateMap(MapType, DefaultTerrainType, ChunkSize, ChunkColumns, ChunkRows);
		world.CreateMap(MapType, DefaultTerrainType, ChunkSize, NumberColumns, NumberRows);
	}

	//public void ClearMap()
	//{
		
	//	foreach (GameObject go in GameObject.FindGameObjectsWithTag("Chunk"))
	//	{
	//		Destroy(go);
	//	}
	//	foreach (GameObject go in GameObject.FindGameObjectsWithTag("HexCell"))
	//	{
	//		Destroy(go);
	//	}
	//	foreach (GameObject go in GameObject.FindGameObjectsWithTag("HexLabel"))
	//	{
	//		Destroy(go);
	//	}

		//CATLIKE TO REMOVE OLD MAP, MOVED TO WC
		//if (chunks != null)
		//{
		//	for (int i = 0; i < chunks.Length; i++)
		//	{
		//		Destroy(chunks[i].gameObject);
		//	}
		//}
	//}

	public void EditCell(HexCell hexcell)
	{
		if (hexcell)
		{
			hexcell.TerrainType = EditorController.GetActiveTerrainType();
		}
	}

	public void EditCells(HexCell centerhexcell)
	{
		int centerX = centerhexcell.coordinates.X;
		int centerZ = centerhexcell.coordinates.Z;

		for (int r = 0, z = centerZ - brushSize; z <= centerZ; z++, r++)
		{
			for (int x = centerX - r; x <= centerX + brushSize; x++)
			{
				EditCell(world.GetHexCellFromCoords(new HexCoordinates(x, z)));
			}
		}
		for (int r = 0, z = centerZ + brushSize; z > centerZ; z--, r++)
		{
			for (int x = centerX - brushSize; x <= centerX + r; x++)
			{
				EditCell(world.GetHexCellFromCoords(new HexCoordinates(x, z)));
			}
		}
	}

	public void SetBrushSize(float brushsize)
	{
		brushSize = (int)brushsize;
	}
	public void ShowUI(bool visible)
	{
		world.ShowUI(visible);
	}
	public void SaveMap()
	{
		//Debug.Log(Application.persistentDataPath);
		string path = Path.Combine(Application.persistentDataPath, "test.map");
		using (BinaryWriter binWriter = new BinaryWriter(File.Open(path, FileMode.Create)))
		{
			binWriter.Write(0);
			world.SaveWorld(binWriter);
		}
	}

	public void LoadMap()
	{
		string path = Path.Combine(Application.persistentDataPath, "test.map");
		using (BinaryReader binReader = new BinaryReader(File.OpenRead(path)))
		{
			int fileHeader = binReader.ReadInt32();
			if (fileHeader == 0)
			{
				//binReader.ReadInt32();
				world.LoadWorld(binReader);
			}
			else
			{
				Debug.LogWarning("Unknown map format" + fileHeader);
			}
		}
	}
}
