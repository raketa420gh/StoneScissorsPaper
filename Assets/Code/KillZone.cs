using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<Unit>();

        if (unit)
            unit.DestroyUnit();
    }
}
