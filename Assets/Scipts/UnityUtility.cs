using UnityEngine;

public static class UnityUtility
{
	/// <summary>
	/// Проверяет наличие компонента на игровом объекте
	/// </summary>
	/// <typeparam Тип компонента="T"></typeparam>
	/// <param Игровой объект на котором проверяется наличие компонента="obj"></param>
	/// <returns>true/false</returns>
	public static bool HasComponent<T>(this GameObject obj) where T : Component
	{
		return obj.GetComponent<T>() != null;
	}
	/// <summary>
	/// Метод получает ищет объект на сцене с определенным тэгом, получает его компонент Transform и возвращает его
	/// </summary>
	/// <param name="tagName">Тэг искомого объекта на сцене</param>
	/// <returns></returns>
	public static Transform FindGameObjectTransformWithTag(string tagName)
    {
		Debug.Log("!!!");
		Transform transform = GameObject.FindGameObjectWithTag(tagName)?.transform;
		return transform;
	}
}


