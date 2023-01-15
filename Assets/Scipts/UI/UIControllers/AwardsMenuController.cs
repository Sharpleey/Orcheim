using UnityEngine;
using TMPro;

public class AwardsMenuController : MonoBehaviour
{
    [SerializeField] PauseMenuCanvasController _pauseMenuCanvasController;

    [SerializeField] TextMeshProUGUI _textButtonAward_1;
    [SerializeField] TextMeshProUGUI _textButtonAward_2;
    [SerializeField] TextMeshProUGUI _textButtonAward_3;

    private UpgratableParameter award_1;
    private UpgratableParameter award_2;
    private UpgratableParameter award_3;

    private void Awake()
    {
        _textButtonAward_1.text = "";
        _textButtonAward_2.text = "";
        _textButtonAward_3.text = "";
    }

    private void OnEnable()
    {
        GetAndShowAwards();
    }

    private void GetAndShowAwards()
    {
        if(!LootManager.Instance)
        {
            Debug.Log("LootManager is not created!");
            return;
        }

        //award_1 = LootManager.Instance.GetRandomUpgrade();
        //award_2 = LootManager.Instance.GetRandomUpgrade();
        //award_3 = LootManager.Instance.GetRandomUpgrade();

        //_textButtonAward_1.text = award_1.GetType().ToString();
        //_textButtonAward_2.text = award_2.GetType().ToString();
        //_textButtonAward_3.text = award_3.GetType().ToString();
    }

    public void OnClickAward_1()
    {
        if(award_1 != null)
            award_1.Upgrade();

        if (_pauseMenuCanvasController)
            _pauseMenuCanvasController.Pause();
    }

    public void OnClickAward_2()
    {
        if (award_2 != null)
            award_2.Upgrade();

        if (_pauseMenuCanvasController)
            _pauseMenuCanvasController.Pause();
    }

    public void OnClickAward_3()
    {
        if (award_3 != null)
            award_3.Upgrade();

        if (_pauseMenuCanvasController)
            _pauseMenuCanvasController.Pause();
    }
}
