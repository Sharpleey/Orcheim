using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IGameManager
{
	[SerializeField] private Scene[] _scenes;

	[SerializeField] private LoadingScreenController _loadingScreen;

	public ManagerStatus Status { get; private set; }

	/// <summary>
	/// �������� ������������ � LoadingScreenController ��� ����������� �������� ���������
	/// </summary>
	public AsyncOperation AsyncOperationLoadingScene => _asyncOperationLoadingScene;

	private AsyncOperation _asyncOperationLoadingScene;

	private bool _isGamePaused;

	private void Awake()
	{
		GlobalGameEventManager.OnPauseGame.AddListener(PauseGame);
	}

	public void Startup()
	{
		Debug.Log("Game Scene manager starting...");

		_isGamePaused = false;

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		Status = ManagerStatus.Started;
	}

	/// <summary>
	/// ����� ����� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	public void SwitchToScene(string sceneName)
    {
		Scene scene = Array.Find(_scenes, x => x.name == sceneName);

		if(scene == null)
        {
			Debug.Log("Scene " + sceneName + " not found!");
			return;
        }

		StartCoroutine(LoadAsyncScene(scene));
	}

	/// <summary>
	/// ����� ������ ���� �� �����
	/// </summary>
	/// <param name="isPaused">��������� ���� �� ����� ��� ���</param>
	private void PauseGame(bool isPaused)
    {
		_isGamePaused = isPaused;

		Time.timeScale = isPaused ? 0 : 1;
	}

	/// <summary>
	/// ����� ��� ����������� �������� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	/// <returns></returns>
	private IEnumerator LoadAsyncScene(Scene scene)
	{
		// ���������� �������� ��������� ������ ��������
        _loadingScreen.Show();

		// ���������� ������� � ������ �������� ����� �����
		GameSceneEventManager.SceneLoadingStarted();

		// ������������� ���������� ���������� ���� ���� �� �������� �������� ������ ������ ��������
        while (_loadingScreen.IsShow)
        {
			yield return null;
		}

		// ��������� ����� � ����������� ������
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(scene.name);

		// ������������� ���������� ���������� ����, ���� ���� �������� �����
		while (!_asyncOperationLoadingScene.isDone)
		{
			yield return null;
		}

		//
		// ����� �������� ���������� �������������� ������� �� �����
		//

		switch (scene.sceneType)
        {
			case SceneType.GameMap:
				GameSceneEventManager.GameMapStarted();
				break;
			default:
				GameSceneEventManager.SceneStarded();
				break;

		}

		// ������� ���� � �����, ���� ��� ���� �� �����
		if (_isGamePaused)
			PauseGame(false);

		// ������� �������� ����� ��������
		_loadingScreen.Hide();

		// �������� ������ �������� �������� �����
		_asyncOperationLoadingScene = null;
	}
}
