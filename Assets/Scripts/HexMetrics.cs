using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexMetrics
{

	public const float outerRadius = 10f;
	public const float innerRadius = outerRadius * 0.866025404f;

	public static Vector3[] corners = {
		//POINTY TOP HEX
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(0f, 0f, outerRadius)

		////FLAT TOP HEX
		//new Vector3(0.5f * innerRadius, 0f, innerRadius), //1
		//new Vector3(outerRadius, 0f, 0f), //2
		//new Vector3(0.5f * innerRadius, 0f, -innerRadius), //3
		//new Vector3(-0.5f * innerRadius, 0f, -innerRadius), //4
		//new Vector3(-outerRadius, 0f, 0f), //5
		//new Vector3(-0.5f * innerRadius, 0f, innerRadius), //6
		//new Vector3(0.5f * innerRadius, 0f, innerRadius), //7
	};
}
