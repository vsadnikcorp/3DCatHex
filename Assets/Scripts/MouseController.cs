using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
	public WorldController world;
	public HexCursor hexCursor;

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
			HexCell hexcell = world.GetHexCell(hit.point);
			UpdateCursorPosition(hit.point);

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				hexcell.TerrainType = EditorController.GetActiveTerrainType();
			}

		}
	}

	void UpdateCursorPosition(Vector3 vposition)
	{
		//TODO:  FIX WEIRDNESS WITH V2POSITION/V3POSITION:  IF Z NOT SET TO -0.01, TILE CURSOR
		//OCCLUDED BY PARTS OF MAP ABOVE ROW 16. NO IDEA WHY.
		Vector3 position = transform.InverseTransformPoint(vposition);
		Vector3 correctPosition = new Vector3(position.x, position.z, position.y);
		HexCoordinates coordinates = HexCoordinates.FromPosition(world.MapType, correctPosition);
		Vector2 V2position = HexCoordinates.FromHexToScreen(world.MapType, coordinates);
		Vector3 V3position = new Vector3(V2position.x, V2position.y, -0.01f); //Z SET TO -0.01 OR ELSE PARTS OF MAP OCCLUDE CURSOR
		world.hexCursor.transform.position = V3position;
	}
		
}
