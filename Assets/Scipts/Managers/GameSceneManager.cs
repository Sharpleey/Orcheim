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
	/// Свойство используется в LoadingScreenController для отображения значения прогресса
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

		StartCoroutine(LoadAsyncScene(scene));
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
}
