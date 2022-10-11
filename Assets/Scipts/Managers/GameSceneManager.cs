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

	//// ����� �������� �� ��������� �������
	//public void GoToNext()
	//{
	//	// ���������, ��������� �� ��������� �������.
	//	if (curLevel < maxLevel)
	//	{
	//		curLevel++;
	//		string name = "Level_" + curLevel;
	//		Debug.Log("Loading " + name);
	//		// ������� �������� �����
	//		SceneManager.LoadScene(name);
	//	}
	//	else
	//	{
	//		Debug.Log("Last level");
	//		Messenger.Broadcast(GameEvent.GAME_COMPLETE);
	//	}
	//}

	//// ����� ��� ������� ���������� ������
	//public void ReachObjective()
	//{
	//	// ����� ����� ���� ��� ��������� ���������� �����
	//	Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
	//}

	//// ����� ������������ ������ ������
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
