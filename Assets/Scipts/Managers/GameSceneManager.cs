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

		//TODO 
		if (_isGamePaused)
		{
			Time.timeScale = 0;
			return;
		}
		
		Time.timeScale = 1;
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

		// Если сцена загружена в режиме Orccheim
		if (_isLoadSceneGameModeOrccheim)
        {
			Messenger.Broadcast(GlobalGameEvent.STARTING_NEW_GAME_MODE_ORCCHEIM);

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
