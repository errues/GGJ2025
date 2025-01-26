using System.Threading.Tasks;
using UnityEngine;

public class VfxTrigger : MonoBehaviour
{
    private VfxLibrary _library;
    public Transform SpawningPoint;

    private void Awake()
    {
        _library = Resources.Load<VfxLibrary>("VfxLibrary");
    }

    public async void PlayVFX(string id)
    {
        GameObject vfx = _library.GetVfxById(id);
        if (vfx == null) return;

        GameObject instance = Instantiate(vfx, SpawningPoint.position, SpawningPoint.rotation);
        await Task.Delay(1000);
        if (instance != null)
        {
            DestroyImmediate(instance);
        }
    }
}

