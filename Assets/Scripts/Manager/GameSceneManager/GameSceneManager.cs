using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Менеджер отвечает за переключение сцен, перезагрузку сцен, паузу игры
/// </summary>
public class GameSceneManager
{
	#region Private fields
	private GameSceneManagerConfig _config;
	private LoadingScreenController _loadingScreen;
	private AsyncOperation _asyncOperationLoadingScene;
	private Scene _currentScene;
	private bool _isGamePaused;
    #endregion

	public GameSceneManager(GameSceneManagerConfig config, LoadingScreenController loadingScreenController)
    {
		_config = config;
		_loadingScreen = loadingScreenController;
	}

	#region Private methods

	/// <summary>
	/// Метод для аминхронной загрузки сцены
	/// </summary>
	/// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
	/// <returns></returns>
	private async Task LoadAsyncScene(Scene scene)
	{
		// Показываем анимацию появления экрана загрузки
		_loadingScreen.Show();

		// Отправляем событие о начале загрузки новой сцены
		GameSceneEventManager.SceneLoadingStarted();

		// Останавливаем дальнейшее выполнение кода пока не окончена анимация показа экрана загрузки
		while (_loadingScreen.IsShowing)
		{
			await Task.Yield();
		}

		// Загружаем сцену в асинхронном режиме
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(scene.name);

		// Останавливаем дальнейшее выполнение кода, пока идет загрузка сцены
		while (!_asyncOperationLoadingScene.isDone)
		{
			_loadingScreen.SetValueProgressBar(_asyncOperationLoadingScene.progress);

			await Task.Yield();
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

	/// <summary>
	/// Метод ставит игру на паузу
	/// </summary>
	/// <param name="isPaused">Поставить игру на паузу или нет</param>
	public void PauseGame(bool isPaused)
	{
		_isGamePaused = isPaused;

		GameSceneEventManager.GamePause(_isGamePaused);

		Time.timeScale = isPaused ? 0 : 1;
	}

	/// <summary>
	/// Метод смены сцены
	/// </summary>
	/// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
	public async void SwitchToScene(string sceneName)
    {
		Scene scene = Array.Find(_config.Scenes, x => x.name == sceneName);

		if(scene == null)
        {
			Debug.Log("Scene " + sceneName + " not found!");
			return;
        }

		_currentScene = scene;

		await LoadAsyncScene(scene);
	}

	/// <summary>
	/// Метод перезапускает текущую запущенную сцену
	/// </summary>
	public async void RestartScene()
    {
		await LoadAsyncScene(_currentScene);
	}

    #endregion Public methods
}
