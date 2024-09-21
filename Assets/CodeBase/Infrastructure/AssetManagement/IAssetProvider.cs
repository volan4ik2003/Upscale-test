using UnityEngine;

        public interface IAssetProvider
        {
            GameObject Instantiate(string path, Vector3 at, Quaternion rotation, Transform parent = null);
            GameObject Instantiate(string path);
        }
    
