using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _description;

    private void OnEnable()
    {
        _description.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _description.SetActive(false);
    }
}
