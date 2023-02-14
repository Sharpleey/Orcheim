using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Менеджер отвечает за переключение сцен, перезагрузку сцен, паузу игры
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
	/// Свойство используется в LoadingScreenController для отображения значения прогресса
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
	private IEnumerator LoadAsyncScene(Scene scene)
	{
		// Показываем анимацию появления экрана загрузки
		_loadingScreen.Show();

		// Отправляем событие о начале загрузки новой сцены
		GameSceneEventManager.SceneLoadingStarted();

		// Останавливаем дальнейшее выполнение кода пока не окончена анимация показа экрана загрузки
		while (_loadingScreen.IsShow)
		{
			yield return null;
		}

		// Загружаем сцену в асинхронном режиме
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(scene.name);

		// Останавливаем дальнейшее выполнение кода, пока идет загрузка сцены
		while (!_asyncOperationLoadingScene.isDone)
		{
			yield return null;
		}

		//
		// После загрузки происходит автоматический переход на сцену
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

		// Снимаем игру с паузы, если она была на паузе
		if (_isGamePaused)
			PauseGame(false);

		// Плавное скрываем экрна загрузки
		_loadingScreen.Hide();

		// Обнуляем данные операции загрузки сцены
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
	/// Метод смены сцены
	/// </summary>
	/// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
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
	/// Метод перезапускает текущую запущенную сцену
	/// </summary>
	public void RestartScene()
    {
		StartCoroutine(LoadAsyncScene(_currentScene));
	}

	#endregion Public methods
}
