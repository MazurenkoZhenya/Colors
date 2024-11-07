using System;
using UnityEngine;

public class ColorInteractiveObject : MonoBehaviour
{
	[SerializeField]
	private string shaderColorProperty = "_Color";

	public ColorDataScriptableObject ColorData { get; private set; }
	public event Action<ColorInteractiveObject> OnInteract;

	private Material material;

	void Awake()
    {
		var renderer = GetComponent<Renderer>();
		if (renderer)
		{
			material = renderer.material;
		}
		else 
		{
			Debug.LogError($"InteractiveObject: { gameObject.name } have no Renderer, fix the object");
		}
	}

	public void ChangeColor(ColorDataScriptableObject colorData)
	{
		ColorData = colorData;
		if (material)
		{
			material.SetColor(shaderColorProperty, colorData.color);
		}
	}

	private void OnMouseDown()
	{
		OnInteract?.Invoke(this);
	}
}
