using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �������� �������� �� ������������ ����, ������������ ����, ����� ����
/// </summary>
public class GameSceneManager : MonoBehaviour, IGameManager
{
	public static GameSceneManager Instance { get; private set; }

	#region Serialize fields

	[SerializeField] private Scene[] _scenes;

	[SerializeField] private LoadingScreenController _loadingScreen;

	#endregion Serialize fields

	#region Properties

	public ManagerStatus Status { get; private set; }

	#endregion Properties

	#region Private fields

	/// <summary>
	/// �������� ������������ � LoadingScreenController ��� ����������� �������� ���������
	/// </summary>
	public AsyncOperation AsyncOperationLoadingScene => _asyncOperationLoadingScene;

	private AsyncOperation _asyncOperationLoadingScene;

	private Scene _currentScene;

	private bool _isGamePaused;

    #endregion Private fields

    #region Mono

    private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);

		AddListeners();
	}

	#endregion Mono

	#region Private methods

	private void AddListeners()
	{
		GlobalGameEventManager.OnPauseGame.AddListener(PauseGame);
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

	#endregion Private methods

	#region Public methods

	public void Startup()
	{
		Debug.Log("Game Scene manager starting...");

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

		_currentScene = scene;

		StartCoroutine(LoadAsyncScene(scene));
	}

	/// <summary>
	/// ����� ������������� ������� ���������� �����
	/// </summary>
	public void RestartScene()
    {
		StartCoroutine(LoadAsyncScene(_currentScene));
	}

	#endregion Public methods
}
