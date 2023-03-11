using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnMenuController : MonoBehaviour
{
    #region Private fields

    private MainMenuCanvasController _mainMenuCanvasController;

    #endregion Private fields

    #region Mono

    private void Start()
    {
        _mainMenuCanvasController = GetComponentInParent<MainMenuCanvasController>();
    }

    #endregion Mono

    #region Button event handlers

    public void OnClickBack()
    {
        _mainMenuCanvasController?.BackToMainMenu();
    }

    #endregion Button event handlers
}
