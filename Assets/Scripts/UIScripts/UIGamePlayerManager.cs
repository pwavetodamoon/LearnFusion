using deVoid.UIFramework;
using UnityEngine;

public sealed class LoadingManager : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameGamePlay;

    private void Awake()
    {
        _uIFrameGamePlay = _defaultUISetting.CreateUIInstance();

    }

    private void Start()
    {
        // _uIFrameGamePlay.OpenWindow(ScreenIds.UILoadingScene);
    }

}