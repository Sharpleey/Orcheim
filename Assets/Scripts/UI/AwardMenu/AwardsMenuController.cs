using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class AwardsMenuController : MonoBehaviour
{
    [SerializeField] private GameMenuCanvasController _pauseMenuCanvasController;

    [SerializeField] private List<ButtonAwardController> _buttonAwardControllers;

    private List<Award> _awards = new List<Award>();

    private AudioManager _audioManager;
    private LootManager _lootManager;

    [Inject]
    private void Construct(AudioManager audioManager, LootManager lootManager)
    {
        _audioManager = audioManager;
        _lootManager = lootManager;
    }

    private void OnEnable()
    {
        _audioManager?.PlaySound(SoundType.Sfx, "level_up"); //TODO Вынести в константы

        _awards = _lootManager?.GetListRandomAwards(3);

        if (_awards != null && _buttonAwardControllers != null)
        {
            for (int i = 0; i < _awards.Count; i++)
            {
                _buttonAwardControllers[i].SetAward(_awards[i]);
            }
        }
       
    }
}
