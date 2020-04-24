using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
	public WorldController world;
	//public GameObject hexCursor;
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

			//HexCursor hexCursor = GameObject.Find("HexCursor").GetComponent<HexCursor>(); 
			//HexCoordinates cursorPosition = HexCoordinates.FromPosition(0, hit.point);
			//int cursorX = cursorPosition.Z;
			//int cursorY = cursorPosition.Y;
			//int cursorZ = cursorPosition.Z;
			//hexCursor.transform.position = new Vector3(cursorX, cursorY, cursorZ);
			//Debug.Log(hexCursor == null);
			//Debug.Log(hexCursor.transform.position);
		}
	}
}
