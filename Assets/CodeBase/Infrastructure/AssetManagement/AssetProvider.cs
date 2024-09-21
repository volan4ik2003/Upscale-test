using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.AssetManagement
{
   
    public class AssetProvider : IAssetProvider
    {
        private readonly DiContainer _container;
      
        public AssetProvider(DiContainer container)
        {
            _container = container;
        }
        public GameObject Instantiate(string path, Vector3 at, Quaternion rotation, Transform parent = null)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefab(prefab, at, rotation, parent);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
           return _container.InstantiatePrefab(prefab);
        }
    }
    
}