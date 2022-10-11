using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IGameManager
{
    public ManagerStatus Status { get; private set; }

    public void Startup()
    {
        Debug.Log("GameScene manager starting...");

        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

	public void StartMainMenu()
    {
        SceneManager.LoadScene(Scenes.MAIN_MENU);
	}

	public void StartTestScene()
    {
		SceneManager.LoadScene(Scenes.TEST_AI);
	}

	//// Метод перехода на следующий уровень
	//public void GoToNext()
	//{
	//	// Проверяем, достигнут ли последний уровень.
	//	if (curLevel < maxLevel)
	//	{
	//		curLevel++;
	//		string name = "Level_" + curLevel;
	//		Debug.Log("Loading " + name);
	//		// Команда загрузки сцены
	//		SceneManager.LoadScene(name);
	//	}
	//	else
	//	{
	//		Debug.Log("Last level");
	//		Messenger.Broadcast(GameEvent.GAME_COMPLETE);
	//	}
	//}

	//// Метод для тригера завершения уровня
	//public void ReachObjective()
	//{
	//	// здесь может быть код обработки нескольких целей
	//	Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
	//}

	//// Метод перезагрузки текщег уровня
	//public void RestartCurrent()
	//{
	//	string name = "Level_" + curLevel;
	//	Debug.Log("Loading " + name);
	//	SceneManager.LoadScene(name);
	//}

	//public void UpdateData(int curLevel, int maxLevel)
	//{
	//	this.curLevel = curLevel;
	//	this.maxLevel = maxLevel;
	//}
}
