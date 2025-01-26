using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class OutlineRenderer : MonoBehaviour
{
    [SerializeField] private BlurredBufferMultiObjectOutlineRendererFeature outlineRendererFeature;
    [SerializeField] private List<Renderer> outlineRenderers;

    [ContextMenu("Reassign Renderers Now")]
    private void OnValidate()
    {
        if (outlineRenderers == null || outlineRenderers.Count == 0)
            outlineRenderers = GetComponentsInChildren<Renderer>(true).ToList();
    }

    private void OnEnable()
    {
        outlineRendererFeature?.AddRenderers(outlineRenderers);
    }

    private void OnDisable()
    {
        outlineRendererFeature?.RemoveRenderers(outlineRenderers);
    }
}
