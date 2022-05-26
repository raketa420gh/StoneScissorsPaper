using UnityEngine;
using Zenject;

public class SoundPlayer : MonoBehaviour
{
    private GameFactory _factory;

    [Inject]
    public void Construct(GameFactory factory) => 
        _factory = factory;
    
    public void CreateBackgroundMusic(Vector3 position)
    {
        var audioSource = _factory.CreateSFX(position, AssetPath.BackgroundMusic);
        audioSource.PlayOneShot(audioSource.clip);
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void CreateSfxUnitDeath(Vector3 position)
    {
        var audioSource = _factory.CreateSFX(position, AssetPath.SfxUnitDeath);
        audioSource.PlayOneShot(audioSource.clip);
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    
    public void CreateSfxUnitCreation(Vector3 position)
    {
        var audioSource = _factory.CreateSFX(position, AssetPath.SfxUnitCreation);
        audioSource.PlayOneShot(audioSource.clip);
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}
