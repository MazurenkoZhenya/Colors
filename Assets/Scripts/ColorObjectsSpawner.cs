using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorObjectsSpawner : MonoBehaviour
{
	[SerializeField]
	private List<ColorInteractiveObject> objects;

    [SerializeField]
    private GameObject parent;

	[SerializeField]
	private Vector3 spawnRange;

	void Awake()
	{
		Debug.Assert(parent != null, "Parent is null, setup container for spawned objects");
	}
	public List<ColorInteractiveObject> SpawnObjects(int amount)
    {
		List<ColorInteractiveObject> spawnedObjects = null;

		Debug.Assert(amount > 0, "Trying to spawn invalid amount of ColorInteractiveObject: " + amount);
		if (amount > 0)
		{
			spawnedObjects = new List<ColorInteractiveObject>(amount);

			for (int i = 0; i < amount; i++)
			{
				int randomIndex = Random.Range(0, objects.Count);

				Vector3 randomSpawnPosition = new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y), Random.Range(-spawnRange.z, spawnRange.z));
				ColorInteractiveObject spawnedObject = Instantiate(objects.First(), randomSpawnPosition, Quaternion.identity, parent.transform);
				spawnedObjects.Add(spawnedObject);
			}
		}
		return spawnedObjects;
	}
}
