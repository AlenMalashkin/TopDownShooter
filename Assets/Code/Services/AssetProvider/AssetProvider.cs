using UnityEngine;

namespace Code.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject LoadAsset(string assetPath)
            => (GameObject) Resources.Load(assetPath);

        public T LoadAsset<T>(string assetPath) where T : Object
            => Resources.Load<T>(assetPath);
    }
}