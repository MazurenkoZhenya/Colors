using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ColorDataScriptableObject", order = 1)]
public class ColorDataScriptableObject : ScriptableObject
{
	[SerializeField]
	public string colorName;
	
	[SerializeField]
	public Color color;
}
