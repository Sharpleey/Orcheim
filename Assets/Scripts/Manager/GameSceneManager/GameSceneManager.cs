using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Менеджер отвечает за переключение сцен, перезагрузку сцен, паузу игры
/// </summary>
public class GameSceneManager : MonoBehaviour
{
	[SerializeField] private GameSceneManagerConfig _config;
	[SerializeField] private LoadingScreenController _loadingScreen;

	#region Private fields
	private AsyncOperation _asyncOperationLoadingScene;
	private string _currentScene;
	private bool _isGamePaused;
    #endregion

	#region Private methods

	/// <summary>
	/// Метод для аминхронной загрузки сцены
	/// </summary>
	/// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
	/// <returns></returns>
	private async Task LoadAsyncScene(string scene)
	{
		// Показываем анимацию появления экрана загрузки
		_loadingScreen.Show();

		// Останавливаем дальнейшее выполнение кода пока не окончена анимация показа экрана загрузки
		while (_loadingScreen.IsShowing)
		{
			await Task.Yield();
		}

		// Загружаем сцену в асинхронном режиме
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(scene);

		// Останавливаем дальнейшее выполнение кода, пока идет загрузка сцены
		while (!_asyncOperationLoadingScene.isDone)
		{
			_loadingScreen.SetValueProgressBar(_asyncOperationLoadingScene.progress);

			await Task.Yield();
		}

		//
		// После загрузки происходит автоматический переход на сцену
		//

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
	public async void SwitchToScene(string newScene)
    {
		_currentScene = newScene;

		await LoadAsyncScene(newScene);
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
