using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
  	public int width = 0;
	public int height = 0;
	public HexCell cellPrefab;
	//public Color defaultColor = Color.white;
	public byte defaultTerrain = 0;
	public byte activeTerrain;
	public Text cellLabelPrefab;
	public static byte mapType;
	HexCell[] cells;
	Canvas gridCanvas;
	HexMesh hexMesh;

	public int MapType { get { return mapType; } }

	void Awake()
	{
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		mapType = 0;
		HexMetrics.Init(mapType);
		Camera.main.orthographicSize = 50.0f;

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				CreateCell(x, z, i++);
			}
		}

	}

	void Start()
	{
		hexMesh.Triangulate(cells);
	}

	void CreateCell(int x, int z, int i)
	{
		Vector3 position;
		switch (mapType)
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
		cell.coordinates = HexCoordConvert.FromOffsetCoordinates(x, z);
		
		int y = (-x - z);
		cell.name = cell.coordinates.ToString(); //FOR CELL NAME IN AXIAL COORDS
		//cell.name = (x + ", " + y + ". " + z); //FOR CELL NAME IN OFFSET COORDS

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
		label.name = "Label " + cell.coordinates.ToString();

		cell.terraintype = defaultTerrain;
	}

	public void GetCellAt(Vector3 position, byte activeterraintype)
	{
		int index;
		position = transform.InverseTransformPoint(position);
		HexCoordConvert coordinates = HexCoordConvert.FromPosition(position);
		Debug.Log("touched at " + coordinates.ToString());

		////TO COLORIZE CELLS
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			switch (mapType)
			{
				case 0:  //POINTY-TOP HEX
					index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
					break;
				case 1: //FLAT TOP HEX
					index = (coordinates.Z + coordinates.X / 2) * width + coordinates.X;
					break;
				default:
					index = 0;
					break;
			}
			HexCell cell = cells[index];
			cell.terraintype = activeterraintype;
			cell.terraingfx = hexMesh.SetTerrainGFX(activeterraintype);
			hexMesh.Triangulate(cells);
		}
	}
}
