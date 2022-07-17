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
}


