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

	public void Startup()
    {
        Debug.Log("Game Scene manager starting...");


        // any long-running startup tasks go here, and set status to 'Initializing' until those tasks are complete
        Status = ManagerStatus.Started;
    }

	/// <summary>
	/// ����� ����� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	public void SwitchToScene(string sceneName)
    {
		StartCoroutine(LoadAsyncScene(sceneName));
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
		// ����� �������� ���������� �������������� ������� �� �����
		// ������� �������� ����� ��������
		_loadingScreen.Hide();
		// �������� ������ �������� �� �����
		_asyncOperationLoadingScene = null;
	}
}
