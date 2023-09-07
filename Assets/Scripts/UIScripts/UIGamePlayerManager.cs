using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;

public sealed class UIGamePlayManager : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameGamePlay;

    private void Awake()
    {
        _uIFrameGamePlay = _defaultUISetting.CreateUIInstance();
        AddListeners();
    }

    private void Start()
    {
    }
    private void AddListeners()
    {
        Signals.Get<ShowMovementSignal>().AddListener(ShowUIMovement);

    }

    private void RemoveAddListeners()
    {
        Signals.Get<ShowMovementSignal>().RemoveListener(ShowUIMovement);
    }
    private void OnDestroy()
    {
        RemoveAddListeners();
    }

    private void ShowUIMovement()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIMovement);

    }
}