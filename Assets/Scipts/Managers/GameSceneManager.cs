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
		StartCoroutine(LoadAsyncScene(sceneName));
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
		// После загрузки происходит автоматический переход на сцену
		// Плавное скрываем экрна загрузки
		_loadingScreen.Hide();
		// Обнуляем данные операции по сцене
		_asyncOperationLoadingScene = null;
	}
}
