using UnityEngine;
using Zenject;

public class Factory : IFactory
{
    private AssetProvider _assetProvider;

    [Inject]
    public void Construct(AssetProvider assetProvider) =>
        _assetProvider = assetProvider;

    public ParticleSystem CreateVFX(Vector3 position, string path = AssetPath.VFX, Transform parent = null)
    {
        GameObject obj = _assetProvider.Instantiate(path, position);
        obj.transform.SetParent(parent);
        var particleSystem = obj.GetComponentInChildren<ParticleSystem>();
        return particleSystem;
    }

    public AudioSource CreateSFX(Vector3 position, string path = AssetPath.SFX, Transform parent = null)
    {
        GameObject obj = _assetProvider.Instantiate(path, position);
        obj.transform.SetParent(parent);
        var audioSource = obj.GetComponentInChildren<AudioSource>();
        return audioSource;
    }
}