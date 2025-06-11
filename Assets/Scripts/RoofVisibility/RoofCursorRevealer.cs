using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Camera))]
public class RoofCursorRevealer : MonoBehaviour
{
    [SerializeField] private float _cursorRadius = 1.5f;
    [SerializeField] private LayerMask _roofLayer;
    
    readonly List<RoofVisibility> _lastFrame = new();
    private Camera _camera;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    
    private void LateUpdate()
    {
        if (RaycastGround(out var hitPoint) == false) return;
        
        // Find all roofs within the radius just once per frame:
        var hits = Physics.OverlapSphere(hitPoint, _cursorRadius, _roofLayer);
        HashSet<RoofVisibility> roofsThisFrame = new();
        
        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out RoofVisibility roof)) continue;
            
            roofsThisFrame.Add(roof);

            if (_lastFrame.Contains(roof) == false)
            {
                roof.SetHideByCursor(true);
            }
        }
        
        // Any roof hidden last frame but not this frame → show again
        foreach (var roof in _lastFrame.Where(roof => roofsThisFrame.Contains(roof) == false))
        {
            roof.SetHideByCursor(false);
        }

        _lastFrame.Clear();
        _lastFrame.AddRange(roofsThisFrame);
    }

    private bool RaycastGround(out Vector3 hitPoint)
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 500f))
        {
            hitPoint = hit.point;
            return true;
        }
        hitPoint = default;
        return false;
    }
}
