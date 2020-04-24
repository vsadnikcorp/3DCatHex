using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
	Mesh hexMesh;
	List<Vector3> vertices;
	List<int> triangles;
	MeshCollider meshCollider;
	List<Color> terraingfx;
 	List<byte> terraintypes;

	void Awake()
	{
		GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
		meshCollider = gameObject.AddComponent<MeshCollider>();
		hexMesh.name = "Hex Mesh";

		//CHANGES MESH INDEX TO 32BIT, WHICH ALLOWS UP TO 4 BILLION VERTICES INSTEAD OF 65K
		hexMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
		vertices = new List<Vector3>();
		triangles = new List<int>();
		terraingfx = new List<Color>();
		terraintypes = new List<byte>();
	}

	public void Triangulate(HexCell[] cells)
	{
		hexMesh.Clear();
		vertices.Clear();
		triangles.Clear();
		terraingfx.Clear();

		terraintypes.Clear();

		for (int i = 0; i < cells.Length; i++)
		{
			Triangulate(cells[i]);
		}
		hexMesh.vertices = vertices.ToArray();
		hexMesh.triangles = triangles.ToArray();
		hexMesh.colors = terraingfx.ToArray();
		
		hexMesh.RecalculateNormals();
		meshCollider.sharedMesh = hexMesh;
	}

	/// <summary>
	/// CALLED FROM TRIANGULATE(CELLS)
	/// </summary>
	/// <param name="cell"></param>
	void Triangulate(HexCell cell)
	{
		Vector3 center = cell.transform.localPosition;
		for (int i = 0; i < 6; i++)
			{
				AddTriangle(
				center,
				center + HexMetrics.corners[i],
				center + HexMetrics.corners[i+1]
			);
			//Color terrainGFX = EditorController.SetTerrainGFX(cell.terraintype);
			//Color terrainGFX = EditorController.SetTerrainGFX(cell);
			byte terraintype = cell.terraintype;
			Color terrainGFX = EditorController.SetTerrainGFX(cell, terraintype);
			AddTriangleColor(terrainGFX); 
		}
	} 

	/// <summary>
	/// CALLED FROM TRIANGULATE(CELL), ADDS THREE VERTICES TO VERTICES AND TRIANGLE LISTS
	/// </summary>
	/// <param name="v1"></param>
	/// <param name="v2"></param>
	/// <param name="v3"></param>
	void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
	{
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
	}
	
	void AddTriangleColor(Color color)
	{
		terraingfx.Add(color);
		terraingfx.Add(color);
		terraingfx.Add(color);
	}
}
