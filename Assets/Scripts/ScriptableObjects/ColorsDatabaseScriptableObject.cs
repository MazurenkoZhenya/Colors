using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ColorsDatabaseScriptableObject", order = 1)]
public class ColorsDatabaseScriptableObject : ScriptableObject
{
	[SerializeField]
	private List<ColorDataScriptableObject> colors;

	public int GetColorsCount()
	{
		return colors.Count;
	}
	public ColorDataScriptableObject GetColor(int index)
	{
		if (index < colors.Count)
		{
			return colors[index];
		}

		return null;
	}

	public int FindColorIndex(ColorDataScriptableObject colorData)
	{
		return colors.IndexOf(colorData);
	}
}
