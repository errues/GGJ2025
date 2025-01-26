using System.Threading.Tasks;
using UnityEngine;

public class VfxTrigger : MonoBehaviour
{
    private VfxLibrary _library;
    private Transform _spawningPoint;

    private void Awake()
    {
        _library = Resources.Load<VfxLibrary>("VfxLibrary ");
    }

    public async void PlayVFX(string id)
    {
        GameObject vfx = _library.GetVfxById(id);
        if (vfx == null) return;

        GameObject instance = Instantiate(vfx, _spawningPoint.position, _spawningPoint.rotation);
        await Task.Delay(1000);
        DestroyImmediate(instance);
    }
}

