using UnityEngine;
using System.Collections.Generic;

public class AwardsMenuController : MonoBehaviour
{
    [SerializeField] private GameMenuCanvasController _pauseMenuCanvasController;

    [SerializeField] private List<ButtonAwardController> _buttonAwardControllers;

    private List<Award> _awards = new List<Award>();

    private void OnEnable()
    {
        _awards = LootManager.Instance?.GetListRandomAwards(3);

        if (_awards != null && _buttonAwardControllers != null)
        {
            for (int i = 0; i < _awards.Count; i++)
            {
                _buttonAwardControllers[i].SetAward(_awards[i]);
            }
        }
       
    }
}
