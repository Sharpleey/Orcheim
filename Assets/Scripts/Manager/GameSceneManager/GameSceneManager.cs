using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �������� �������� �� ������������ ����, ������������ ����, ����� ����
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
	/// ����� ��� ����������� �������� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	/// <returns></returns>
	private async Task LoadAsyncScene(string scene)
	{
		// ���������� �������� ��������� ������ ��������
		_loadingScreen.Show();

		// ������������� ���������� ���������� ���� ���� �� �������� �������� ������ ������ ��������
		while (_loadingScreen.IsShowing)
		{
			await Task.Yield();
		}

		// ��������� ����� � ����������� ������
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(scene);

		// ������������� ���������� ���������� ����, ���� ���� �������� �����
		while (!_asyncOperationLoadingScene.isDone)
		{
			_loadingScreen.SetValueProgressBar(_asyncOperationLoadingScene.progress);

			await Task.Yield();
		}

		//
		// ����� �������� ���������� �������������� ������� �� �����
		//

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
	public async void SwitchToScene(string newScene)
    {
		_currentScene = newScene;

		await LoadAsyncScene(newScene);
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
