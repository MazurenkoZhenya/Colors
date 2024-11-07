using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
	[SerializeField]
	private ColorsDatabaseScriptableObject colorsDatabase;

	[SerializeField]
	private ColorObjectsSpawner colorObjectsSpawner;

	[SerializeField]
	private HudController hudController;

	[SerializeField]
	private int gameplayObjectsAmount = 3;

	private GameRound gameRound;

	void Awake()
	{
		Debug.Assert(colorsDatabase != null, "ColorsDatabase is null, setup colors database for proper gameplay workflow");
		Debug.Assert(colorObjectsSpawner != null, "ColorObjectsSpawner is null, setup objects spawner for proper gameplay workflow");
		Debug.Assert(hudController != null, "HudController is null, setup hud controller for proper gameplay workflow");

		Debug.Assert(gameplayObjectsAmount > 0, $"Invalid gameplayObjectsAmount: { gameplayObjectsAmount }, value should be more then 0");
		Debug.Assert(colorsDatabase.GetColorsCount() >= gameplayObjectsAmount, 
			$"Invalid Gameplay data, available colors: { colorsDatabase.GetColorsCount() } less than objects to generate: { gameplayObjectsAmount }, fix the setup otherwise gameplay might be broken");
	}
	void Start()
	{
		StartNewRound();
	}

	void StartNewRound()
	{
		if (gameRound != null)
		{
			gameRound.RoundCompletedEvent -= OnRoundCompleted;
			gameRound.IncorrectActionEvent -= OnRoundIncorrectAction;
			gameRound.Release();
		}

		gameRound = new GameRound(colorObjectsSpawner, colorsDatabase, gameplayObjectsAmount);
		gameRound.Init();
		gameRound.RoundCompletedEvent += OnRoundCompleted;
		gameRound.IncorrectActionEvent += OnRoundIncorrectAction;

		RefreshUI();
	}

	void OnRoundCompleted()
	{
		//Debug.Log("Target color match, round completed");

		StartNewRound();
	}

	void OnRoundIncorrectAction() 
	{
		//Debug.LogWarning("Wrong color");

		if (hudController != null)
		{
			hudController.ShowIncorrectColorMessage();
		}
	}
	void RefreshUI() 
	{
		if (hudController != null && gameRound != null)
		{
			hudController.SetTargetColorText(gameRound.GetTargetColor().colorName);
		}
	}
}
