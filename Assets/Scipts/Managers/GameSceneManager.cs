using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IGameManager
{
	[SerializeField] private LoadingScreenController _loadingScreen;

	public ManagerStatus Status { get; private set; }
	/// <summary>
	/// �������� ������������ � LoadingScreenController ��� ����������� �������� ���������
	/// </summary>
	public AsyncOperation AsyncOperationLoadingScene => _asyncOperationLoadingScene;

	private AsyncOperation _asyncOperationLoadingScene;

	private bool _isGamePaused;
	private bool _isLoadSceneGameModeOrccheim;

	private void Awake()
	{
		Messenger<bool>.AddListener(GameSceneManagerEvent.PAUSE_GAME, PauseGame);

		Messenger.AddListener(GlobalGameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim);

		Messenger<string>.AddListener(GameSceneManagerEvent.SWITCH_TO_SCENE, SwitchToScene);
	}
	private void OnDestroy()
	{
		Messenger<bool>.RemoveListener(GameSceneManagerEvent.PAUSE_GAME, PauseGame);

		Messenger.RemoveListener(GlobalGameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim);

		Messenger<string>.RemoveListener(GameSceneManagerEvent.SWITCH_TO_SCENE, SwitchToScene);
	}

	public void Startup()
	{
		Debug.Log("Game Scene manager starting...");

		_isGamePaused = false;
		_isLoadSceneGameModeOrccheim = false;

		// any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
		Status = ManagerStatus.Started;
	}

	private void NewGameModeOrccheim()
	{
		_isLoadSceneGameModeOrccheim = true;
	}

	/// <summary>
	/// ����� ����� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	private void SwitchToScene(string sceneName)
    {
		StartCoroutine(LoadAsyncScene(sceneName));
	}

	/// <summary>
	/// ����� ������ ���� �� �����
	/// </summary>
	/// <param name="isPaused">��������� ���� �� ����� ��� ���</param>
	private void PauseGame(bool isPaused)
    {
		_isGamePaused = isPaused;

		//TODO 
		if (_isGamePaused)
		{
			Time.timeScale = 0;
			return;
		}
		
		Time.timeScale = 1;
	}

	/// <summary>
	/// ����� ��� ����������� �������� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	/// <returns></returns>
	private IEnumerator LoadAsyncScene(string sceneName)
	{
		// ���������� �������� ��������� ������ ��������
        _loadingScreen.Show();

		// ������������� ���������� ���������� ���� ���� �� �������� �������� ������ ������ ��������
        while (_loadingScreen.IsShow)
        {
			yield return null;
		}

		// ��������� ����� � ����������� ������
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(sceneName);

		// ������������� ���������� ���������� ����, ���� ���� �������� �����
		while (!_asyncOperationLoadingScene.isDone)
		{
			yield return null;
		}
		//
		// ����� �������� ���������� �������������� ������� �� �����
		//

		// ���� ����� ��������� � ������ Orccheim
		if (_isLoadSceneGameModeOrccheim)
        {
			Messenger.Broadcast(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM);

			_isLoadSceneGameModeOrccheim = false;
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
