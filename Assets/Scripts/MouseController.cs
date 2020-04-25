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

		//**TESTING FOR POINTS~~
		Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//HexCell hexUnderMouse = GetHexAtWorldCoordinate(currentMousePosition);
		//**~~~

		if (Physics.Raycast(inputRay, out hit))
		{
			world.GetCellAt(hit.point);

			////~~~~~~~~~~~~~~~~
			Vector3 position = transform.InverseTransformPoint(hit.point);
			//Debug.Log("mhit: " + hit.point); //SAME AS WORLD 
			Vector3 dafuq = new Vector3(position.x, position.z, position.y); //SAME AS WPOSITION
			
			//Debug.Log("mposition: " + dafuq.ToString());
			HexCoordinates coordinates = HexCoordinates.FromPosition(world.MapType, dafuq);//SAME AS WORLD COORDINATES
			////world.hexCursor.transform.position = dafuq;
			Debug.Log("dafuqed at: " + coordinates.ToString());

			//_+_+_+_+_
			Vector2 V2position = HexCoordinates.FromHexToScreen(world.MapType, coordinates);
			world.hexCursor.transform.position = V2position;
			//_+_+_+__+++_+

			//Debug.Log("mcursor: " +	coordinates.X + ", " + coordinates.Y + ", " + coordinates.Z);
			//Debug.Log(hexCursor.transform.position);
		}
	}
		
}
