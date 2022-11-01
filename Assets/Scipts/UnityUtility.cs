using UnityEngine;

public static class UnityUtility
{
	/// <summary>
	/// ��������� ������� ���������� �� ������� �������
	/// </summary>
	/// <typeparam ��� ����������="T"></typeparam>
	/// <param ������� ������ �� ������� ����������� ������� ����������="obj"></param>
	/// <returns>true/false</returns>
	public static bool HasComponent<T>(this GameObject obj) where T : Component
	{
		return obj.GetComponent<T>() != null;
	}
	/// <summary>
	/// ����� �������� ���� ������ �� ����� � ������������ �����, �������� ��� ��������� Transform � ���������� ���
	/// </summary>
	/// <param name="tagName">��� �������� ������� �� �����</param>
	/// <returns></returns>
	public static Transform FindGameObjectTransformWithTag(string tagName)
    {
		Debug.Log("!!!");
		Transform transform = GameObject.FindGameObjectWithTag(tagName)?.transform;
		return transform;
	}
}


