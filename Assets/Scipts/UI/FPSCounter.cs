using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;

    private int _fps;

    private float _timer = 0;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.unscaledDeltaTime;
        if(_timer > 0.25f)
        {
            _fps = (int)(1f / Time.unscaledDeltaTime);
            SetFpsText(_fps.ToString());
            _timer = 0;
        }
    }

    private void SetFpsText(string fps)
    {
        _fpsText.text = "FPS: " + fps;
    }
}
