using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    private bool _fullScreen;

    private int _targetWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _fullScreen = true;
        _targetWidth = Screen.width * (292 / 240);
        Screen.SetResolution(_targetWidth, Screen.height, _fullScreen);
    }
}
