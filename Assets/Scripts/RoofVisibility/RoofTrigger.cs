using UnityEngine;

public class RoofTrigger : MonoBehaviour
{
    private RoofVisibility _roof;

    private void Awake() => _roof = GetComponentInParent<RoofVisibility>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _roof.SetHideByPlayer(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _roof.SetHideByPlayer(false);
    }
}
