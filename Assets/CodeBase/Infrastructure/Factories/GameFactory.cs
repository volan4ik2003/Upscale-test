using Code.Runtime.Infrastructure.AssetManagement;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private IAssetProvider _assetProvider;
    public GameFactory(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public void SpawnHero(Vector3 at)
    {
        _assetProvider.Instantiate(AssetPath.HeroPath, at, Quaternion.identity,null);
    }

    public void SpawnMap(Vector3 at)
    {
        _assetProvider.Instantiate(AssetPath.MazePath, at, Quaternion.identity, null);
    }

    public void SpawnUI()
    {
        _assetProvider.Instantiate(AssetPath.UIPath);
    }

    public void Cleanup()
    {
    
    }
}