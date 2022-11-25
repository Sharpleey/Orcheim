using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour, IGameManager
{
	[SerializeField] private LoadingScreenController _loadingScreen;

	public ManagerStatus Status { get; private set; }
	/// <summary>
	/// Свойство используется в LoadingScreenController для отображения значения прогресса
	/// </summary>
	public AsyncOperation AsyncOperationLoadingScene => _asyncOperationLoadingScene;

	private AsyncOperation _asyncOperationLoadingScene;

	private bool _isGamePaused;
	private bool _isLoadSceneGameModeOrccheim;

	public static class Event
	{
		#region Events for manager
		/// <summary>
		/// Сменить/загрузить сцену
		/// </summary>
		public const string SWITCH_TO_SCENE = "SWITCH_TO_SCENE";
		/// <summary>
		/// Поставить/снять игру с паузы
		/// </summary>
		public const string PAUSE_GAME = "PAUSE_GAME";
		#endregion

		#region Events broadcast by manager
		/// <summary>
		/// Начата загрузка сцены
		/// </summary>
		public const string STARTED_LOADING_SCENE = "STARTED_LOADING_SCENE";
		/// <summary>
		/// Cцена загружена
		/// </summary>
		public const string SCENE_LOADING_COMPLETE = "SCENE_LOADING_COMPLETE";
		/// <summary>
		/// Сцена загружена
		/// </summary>
		public const string SCENE_STARTED = "SCENE_STARTED";
		#endregion
	}

	/// <summary>
	/// Класс с константными значениями названий сцен
	/// </summary>
	public static class Scene
	{
		/// <summary>
		/// Название сцены главного меню
		/// </summary>
		public const string MAIN_MENU = "MainMenu";

		/// <summary>
		/// Сцена теста механики волн
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
	/// Метод смены сцены
	/// </summary>
	/// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
	private void SwitchToScene(string sceneName)
    {
		StartCoroutine(LoadAsyncScene(sceneName));
	}

	/// <summary>
	/// Метод ставит игру на паузу
	/// </summary>
	/// <param name="isPaused">Поставить игру на паузу или нет</param>
	private void PauseGame(bool isPaused)
    {
		_isGamePaused = isPaused;

		Time.timeScale = isPaused ? 0 : 1;
	}

	/// <summary>
	/// Метод для аминхронной загрузки сцены
	/// </summary>
	/// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
	/// <returns></returns>
	private IEnumerator LoadAsyncScene(string sceneName)
	{
		// Показываем анимацию появления экрана загрузки
        _loadingScreen.Show();

		// Останавливаем дальнейшее выполнение кода пока не окончена анимация показа экрана загрузки
        while (_loadingScreen.IsShow)
        {
			yield return null;
		}

		// Загружаем сцену в асинхронном режиме
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(sceneName);

		// Останавливаем дальнейшее выполнение кода, пока идет загрузка сцены
		while (!_asyncOperationLoadingScene.isDone)
		{
			yield return null;
		}
		//
		// После загрузки происходит автоматический переход на сцену
		//

		//TODO
		// Если сцена загружена в режиме Orccheim 
		if (_isLoadSceneGameModeOrccheim)
        {
			Messenger.Broadcast(GameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM);
			Messenger<SoundType, float>.Broadcast(AudioManager.Event.PLAY_RANDOM_SOUND, SoundType.AmbientMusic, 0);

			_isLoadSceneGameModeOrccheim = false;
		}

		// Снимаем игру с паузы, если она была на паузе
		if (_isGamePaused)
			PauseGame(false);

		// Плавное скрываем экрна загрузки
		_loadingScreen.Hide();

		// Обнуляем данные операции загрузки сцены
		_asyncOperationLoadingScene = null;
	}
}
