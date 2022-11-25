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

	public static class Event
	{
		#region Events for manager
		/// <summary>
		/// �������/��������� �����
		/// </summary>
		public const string SWITCH_TO_SCENE = "SWITCH_TO_SCENE";
		/// <summary>
		/// ���������/����� ���� � �����
		/// </summary>
		public const string PAUSE_GAME = "PAUSE_GAME";
		#endregion

		#region Events broadcast by manager
		/// <summary>
		/// ������ �������� �����
		/// </summary>
		public const string STARTED_LOADING_SCENE = "STARTED_LOADING_SCENE";
		/// <summary>
		/// C���� ���������
		/// </summary>
		public const string SCENE_LOADING_COMPLETE = "SCENE_LOADING_COMPLETE";
		/// <summary>
		/// ����� ���������
		/// </summary>
		public const string SCENE_STARTED = "SCENE_STARTED";
		#endregion
	}

	/// <summary>
	/// ����� � ������������ ���������� �������� ����
	/// </summary>
	public static class Scene
	{
		/// <summary>
		/// �������� ����� �������� ����
		/// </summary>
		public const string MAIN_MENU = "MainMenu";

		/// <summary>
		/// ����� ����� �������� ����
		/// </summary>
		public const string TEST_AI = "TestSceneController";
	}

	private void Awake()
	{
		Messenger<bool>.AddListener(GameSceneManager.Event.PAUSE_GAME, PauseGame);

		Messenger.AddListener(GameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim);

		Messenger<string>.AddListener(GameSceneManager.Event.SWITCH_TO_SCENE, SwitchToScene);
	}
	private void OnDestroy()
	{
		Messenger<bool>.RemoveListener(GameSceneManager.Event.PAUSE_GAME, PauseGame);

		Messenger.RemoveListener(GameEvent.NEW_GAME_MODE_ORCCHEIM, NewGameModeOrccheim);

		Messenger<string>.RemoveListener(GameSceneManager.Event.SWITCH_TO_SCENE, SwitchToScene);
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

		Time.timeScale = isPaused ? 0 : 1;
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

		//TODO
		// ���� ����� ��������� � ������ Orccheim 
		if (_isLoadSceneGameModeOrccheim)
        {
			Messenger.Broadcast(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM);
			Messenger<SoundType, float>.Broadcast(AudioManager.Event.PLAY_RANDOM_SOUND, SoundType.AmbientMusic, 0);

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
