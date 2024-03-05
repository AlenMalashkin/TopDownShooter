using UnityEngine;

namespace Code.Services.AssetProvider
{
    public interface IAssetProvider : IService
    {
        GameObject LoadAsset(string assetPath);
        T LoadAsset<T>(string assetPath) where T : Object;
    }
}