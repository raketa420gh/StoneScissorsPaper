using UnityEngine;

public class Pointer : MonoBehaviour
{
    private CreationZone _creationZone;
    private PlayerData _playerData;
    private bool _canCreate;

    public bool CanCreate => _canCreate;

    private void OnTriggerEnter(Collider other)
    {
        var creationZone = other.GetComponent<CreationZone>();

        if (creationZone)
        {
            gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
            _canCreate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var creationZone = other.GetComponent<CreationZone>();
        
        if (creationZone)
        {
            gameObject.transform.localScale = Vector3.one;
            _canCreate = false;
        }
    }
}