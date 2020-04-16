using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HexMapEditor : MonoBehaviour
{

	public byte[] terraintypes;
	public HexGrid hexGrid;
	//private Color activeColor;
	private byte activeTerrainType;

	void Awake()
	{
		SelectTerrain(0);
	}

	void Update()
	{
		if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			HandleInput();
		}
	}

	void HandleInput()
	{
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit))
		{
			hexGrid.GetCellAt(hit.point, activeTerrainType);
		}
	}

	public void SelectTerrain(int index)
	{
		Color terrainGFX = Color.white;
		activeTerrainType = terraintypes[index];
		switch (activeTerrainType)
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
		}
		//Debug.Log(terraintypes[index]);
	}
}
