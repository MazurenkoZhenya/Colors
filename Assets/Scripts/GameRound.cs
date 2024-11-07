using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameRound
{
	public event Action RoundCompletedEvent;
	public event Action IncorrectActionEvent;

	private ColorsDatabaseScriptableObject colorsDatabase;
	private ColorObjectsSpawner objectsSpawner;
	private int gameplayObjectsAmount;

	private List<ColorInteractiveObject> spawnedObjects;
	private HashSet<int> roundColors = new HashSet<int>();
	private int targetColorIndex;

	public GameRound(ColorObjectsSpawner spawner, ColorsDatabaseScriptableObject database, int objectsAmount)
	{
		objectsSpawner = spawner;
		colorsDatabase = database;
		gameplayObjectsAmount = objectsAmount;
	}

	public void Init()
    {
		GenerateRoundColors();
		SpawnObjects();
	}

    public void Release()
    {
		DestroyObjects();
	}
	public ColorDataScriptableObject GetTargetColor()
	{
		return colorsDatabase.GetColor(targetColorIndex);
	}

	private void CompleteRound()
	{
		RoundCompletedEvent?.Invoke();
	}

	void GenerateRoundColors()
	{
		do
		{
			int randomIndex = UnityEngine.Random.Range(0, colorsDatabase.GetColorsCount());
			if (!roundColors.Contains(randomIndex))
			{
				roundColors.Add(randomIndex);
			}

		} while (roundColors.Count < gameplayObjectsAmount);

		targetColorIndex = roundColors.ElementAt(UnityEngine.Random.Range(0, roundColors.Count));
	}

	void SpawnObjects()
	{
		spawnedObjects = objectsSpawner.SpawnObjects(gameplayObjectsAmount);
		if (spawnedObjects != null)
		{
			for (int i = 0; i < spawnedObjects.Count; i++)
			{
				int colorIndex = roundColors.ElementAt(i);
				spawnedObjects[i].ChangeColor(colorsDatabase.GetColor(colorIndex));
			}
		}
		SubscribeToInteractions(spawnedObjects);
	}

	void DestroyObjects()
	{
		//Debug.Log("Destroy round objects");

		UnsubscribeFromInteractions(spawnedObjects);
		if (spawnedObjects != null)
		{
			foreach (ColorInteractiveObject obj in spawnedObjects)
			{
				GameObject.Destroy(obj.gameObject);
			}
		}
	}

	void SubscribeToInteractions(List<ColorInteractiveObject> interactiveObjects)
	{
		if (interactiveObjects != null)
		{
			foreach (ColorInteractiveObject interactiveObject in interactiveObjects)
			{
				interactiveObject.OnInteract += OnInteractedWithColorObject;
			}
		}
	}

	void UnsubscribeFromInteractions(List<ColorInteractiveObject> interactiveObjects)
	{
		if (interactiveObjects != null)
		{
			foreach (ColorInteractiveObject interactiveObject in interactiveObjects)
			{
				interactiveObject.OnInteract -= OnInteractedWithColorObject;
			}
		}
	}

	void OnInteractedWithColorObject(ColorInteractiveObject colorInteractiveObject)
	{
		//Debug.Log("On Interacted with color: " + colorInteractiveObject.ColorData.colorName);

		int interactedColor = colorsDatabase.FindColorIndex(colorInteractiveObject.ColorData);
		if (targetColorIndex == interactedColor)
		{
			CompleteRound();
		}
		else
		{
			IncorrectActionEvent?.Invoke();
		}
	}
}
