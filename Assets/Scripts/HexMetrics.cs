using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMetrics
{
	public const float outerRadius = 10f;
	public const float innerRadius = outerRadius * 0.8660254037844f;
	static byte mapType = HexGrid.mapType;
	public static Vector3[] corners = new Vector3[7];

	public static void Init(byte mapType)
	{
		switch (mapType)
		{
			case 0:  //POINTY-TOP HEX
				{
					corners[0] = new Vector3(0f, 0f, outerRadius);
					corners[1] = new Vector3(innerRadius, 0f, 0.5f * outerRadius);
					corners[2] = new Vector3(innerRadius, 0f, -0.5f * outerRadius);
					corners[3] =new Vector3(0f, 0f, -outerRadius);
					corners[4] = new Vector3(-innerRadius, 0f, -0.5f * outerRadius);
					corners[5] = new Vector3(-innerRadius, 0f, 0.5f * outerRadius);
					corners[6] = new Vector3(0f, 0f, outerRadius);
				}
				break;
			case 1:  //FLAT-TOP HEX
				{
					corners[0] = new Vector3(0.5f * outerRadius, 0f, innerRadius);
					corners[1] = new Vector3(outerRadius, 0f, 0f);
					corners[2] = new Vector3(0.5f * outerRadius, 0f, -innerRadius);
					corners[3] = new Vector3(-0.5f * outerRadius, 0f, -innerRadius);
					corners[4] = new Vector3(-outerRadius, 0f, 0f);
					corners[5] = new Vector3(-0.5f * outerRadius, 0f, innerRadius);
					corners[6] = new Vector3(0.5f * outerRadius, 0f, innerRadius);
				}
				break;
		}
	}
}
