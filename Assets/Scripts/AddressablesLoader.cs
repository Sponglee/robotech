using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesLoader : IDisposable
{
    private readonly List<AsyncOperationHandle> _loadedResources;

    public AddressablesLoader()
    {
        _loadedResources = new List<AsyncOperationHandle>();
    }

    public void Dispose()
    {
        ReleaseAll();
        _loadedResources.Clear();
    }

    public async Task<TResource> LoadResourceAsync<TResource>(string resourceId, CancellationToken token = default)
    {
        var handle = Addressables.LoadAssetAsync<TResource>(resourceId);

        while (handle.Status != AsyncOperationStatus.Succeeded)
        {
            await Task.Yield();

            if (token.IsCancellationRequested)
            {
                return default;
            }
        }

        var loadedResource = handle.Result;

        _loadedResources.Add(handle);

        return loadedResource;
    }

    public void ReleaseResource<TResource>(TResource resource)
    {
        Addressables.Release(resource);
    }

    public void ReleaseAll()
    {
        lock (_loadedResources)
        {
            for (var i = _loadedResources.Count - 1; i >= 0; i--)
            {
                var resource = _loadedResources[i];
                Addressables.Release(resource);
            }
        }
    }
}