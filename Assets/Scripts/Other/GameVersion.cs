using UnityEngine;
using TMPro;

public class GameVersion : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Version {Application.version}";
    }
}
