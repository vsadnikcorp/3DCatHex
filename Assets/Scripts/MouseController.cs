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
			world.GetCellAt(hit.point);
			UpdateCursorPosition(hit.point);
		}
	}

	void UpdateCursorPosition(Vector3 vposition)
	{
		Vector3 position = transform.InverseTransformPoint(vposition);
		Vector3 correctPosition = new Vector3(position.x, position.z, position.y);
		HexCoordinates coordinates = HexCoordinates.FromPosition(world.MapType, correctPosition);
		Vector2 V2position = HexCoordinates.FromHexToScreen(world.MapType, coordinates);
		world.hexCursor.transform.position = V2position;
	}
		
}
