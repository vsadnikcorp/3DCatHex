using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
 {
	public byte[] terraintypes;
	public WorldController world;
	public UnityEngine.UI.ToggleGroup toggleGroup;
	public byte ActiveTerrainType { get; protected set; }
	public byte DefaultTerrainType { get; protected set; }
	public short NumberColumns { get; protected set; }
	public short NumberRows { get; protected set; }
	public int MapType { get; protected set; }

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
				terraintype = 0;
				break;
			case "ForestToggle":
				terraintype = 1;
				break;
			case "MountainToggle":
				terraintype = 2;
				break;
			case "SeaToggle":
				terraintype = 3;
				break;
			default:
				Debug.Log("superdafuq");
				break;
		}
		editor.ActiveTerrainType = terraintype;
		return terraintype;
	}

	public static Color SetTerrainGFX(HexCell cell, byte terraintype)
	{
		Color terrainGFX;
		switch (terraintype)
		{
			case 0:
				terrainGFX = Color.yellow;
				break;
			case 1:
				terrainGFX = Color.green;
				break;
			case 2:
				terrainGFX = Color.grey;
				break;
			case 3:
				terrainGFX = Color.blue;
				break;
			default:
				terrainGFX = Color.white;
				break;
		}
		return terrainGFX;
	}

	public void CreateMapButton()
	{
		if (world != null) ClearMap();
		MapType = GameObject.Find("MapTypeDropdown").GetComponent<TMP_Dropdown>().value;
		NumberColumns = Convert.ToInt16(GameObject.Find("MapXText").GetComponent<TMP_InputField>().text);
		NumberRows = Convert.ToInt16(GameObject.Find("MapYText").GetComponent<TMP_InputField>().text);
		DefaultTerrainType = GetDefaultTerrainType();
		world.CreateMap(MapType, DefaultTerrainType, NumberColumns, NumberRows);
	}

	public void ClearMap()
	{
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("HexCell"))
		{
			Destroy(go);
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("HexLabel"))
		{
			Destroy(go);
		}
	}
}
