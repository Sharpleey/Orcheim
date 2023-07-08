using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// �������� �������� �� ������������ ����, ������������ ����, ����� ����
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
	/// ����� ��� ����������� �������� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	/// <returns></returns>
	private async Task LoadAsyncScene(Scene scene)
	{
		// ���������� �������� ��������� ������ ��������
		_loadingScreen.Show();

		// ���������� ������� � ������ �������� ����� �����
		GameSceneEventManager.SceneLoadingStarted();

		// ������������� ���������� ���������� ���� ���� �� �������� �������� ������ ������ ��������
		while (_loadingScreen.IsShowing)
		{
			await Task.Yield();
		}

		// ��������� ����� � ����������� ������
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(scene.name);

		// ������������� ���������� ���������� ����, ���� ���� �������� �����
		while (!_asyncOperationLoadingScene.isDone)
		{
			_loadingScreen.SetValueProgressBar(_asyncOperationLoadingScene.progress);

			await Task.Yield();
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

	/// <summary>
	/// ����� ������ ���� �� �����
	/// </summary>
	/// <param name="isPaused">��������� ���� �� ����� ��� ���</param>
	public void PauseGame(bool isPaused)
	{
		_isGamePaused = isPaused;

		GameSceneEventManager.GamePause(_isGamePaused);

		Time.timeScale = isPaused ? 0 : 1;
	}

	/// <summary>
	/// ����� ����� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
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
	/// ����� ������������� ������� ���������� �����
	/// </summary>
	public async void RestartScene()
    {
		await LoadAsyncScene(_currentScene);
	}

    #endregion Public methods
}
