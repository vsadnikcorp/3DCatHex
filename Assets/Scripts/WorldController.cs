using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
	public HexCell cellPrefab;
	public byte activeTerrain;
	public Text cellLabelPrefab;
	public HexCursor hexCursorPrefab;
	
	public int MapType { get; protected set; }
	public short NumberColumns { get; protected set; }
	public short NumberRows { get; protected set; }

	HexCell[] cells;
	Canvas gridCanvas;
	HexMesh hexMesh;
	HexCursor hexCursor;

	public void CreateMap(int mapType, byte defaultterraintype, short numbercolumns, short numberrows)
	{
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();
		HexCursor hexCursor = Instantiate<HexCursor>(hexCursorPrefab);
		this.MapType = mapType;
		this.NumberColumns = numbercolumns;
		this.NumberRows = numberrows;
		HexMetrics.Init(mapType);
		hexCursor.Init(mapType, hexCursor);
		Camera.main.orthographicSize = 50.0f;

		cells = new HexCell[numberrows * numbercolumns];

		for (int z = 0, i = 0; z < numberrows; z++)
		{
			for (int x = 0; x < numbercolumns; x++)
			{
				CreateCell(mapType, defaultterraintype, x, z, i++);
			}
		}
		hexMesh.Triangulate(cells);
		//TODO:  CENTER CAMERA ON MAP--LINE BELOW KIND OF WORKS, BUT MATH LOOKS OFF
		//Camera.main.transform.position = new Vector2(numbercolumns * (int)HexMetrics.innerRadius, numberrows * (int)HexMetrics.innerRadius);
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
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(maptype, x, z);
		
		int y = (-x - z);
		
		Text label = Instantiate<Text>(cellLabelPrefab);
		label.tag = "HexLabel";
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		
		//TODO:  GUI LABELS AXIAL VS OFFSET
		label.text = cell.coordinates.ToStringOnSeparateLines(); //FOR HEX LABEL IN AXIAL COORDS
		cell.name = cell.coordinates.ToString(); //FOR CELL NAME IN AXIAL COORDS
		//label.text = (x + ", " + z); //FOR HEX LABEL IN OFFSET COORDS
		//cell.name = (x + ", " + y + ". " + z); //FOR CELL NAME IN OFFSET COORDS

		label.name = "Label " + cell.coordinates.ToString();

		cell.terraintype = defaultterraintype;
	}

	//public void GetCellAt(Vector3 position, byte activeterraintype)
	public void GetCellAt(Vector3 position)
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
				index = coordinates.X + coordinates.Z * NumberColumns + coordinates.Z / 2;
				break;
			case 1: //FLAT TOP HEX
				index = (coordinates.Z + coordinates.X / 2) * NumberColumns + coordinates.X;
				break;
			default:
				index = 0;
				break;
		}
		HexCell cell = cells[index];

		////TO COLORIZE CELLS
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			cell.terraintype = EditorController.GetActiveTerrainType();
			hexMesh.Triangulate(cells);
		}
	}
}
