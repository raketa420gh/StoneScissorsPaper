using UnityEngine;

public class ParticleFX : MonoBehaviour
{
    [SerializeField] private GameObject _fxObject;

    public void Play()
    {
        var particleSystem = Instantiate(_fxObject, transform.position, Quaternion.identity)
            .GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
        Destroy(particleSystem, 1f);
    }
}