using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Renderer))]
public class RoofVisibility : MonoBehaviour
{
    private Renderer _renderer;
    private int _hideVotes = 0;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    
    public void SetHideByCursor(bool shouldHide)
    {
        UpdateHideVotes(shouldHide);
    }

    public void SetHideByPlayer(bool shouldHide)
    {
        UpdateHideVotes(shouldHide);
    }
    
    private void UpdateHideVotes(bool shouldHide)
    {
        _hideVotes += shouldHide ? 1 : -1;
        Apply();
    }

    private void Apply()
    {
        _renderer.shadowCastingMode = _hideVotes == 0 ? ShadowCastingMode.On : ShadowCastingMode.ShadowsOnly;
    }
}
