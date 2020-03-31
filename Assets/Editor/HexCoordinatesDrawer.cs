using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof(HexCoordConvert))]
public class HexCoordinatesDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		HexCoordConvert coordinates = new HexCoordConvert(
			property.FindPropertyRelative("x").intValue,
			property.FindPropertyRelative("z").intValue
		);

		position = EditorGUI.PrefixLabel(position, label);
		GUI.Label(position, coordinates.ToString());
	}
}
