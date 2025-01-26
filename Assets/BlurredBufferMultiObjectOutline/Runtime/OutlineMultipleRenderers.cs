using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OutlineMultipleRenderers : MonoBehaviour
{
    [SerializeField] private BlurredBufferMultiObjectOutlineRendererFeature outlineRendererFeature;
    [SerializeField] private List<Renderer> selectedRenderers;

    [ContextMenu("Reassign Renderers Now")]
    private void OnValidate()
    {
        if (outlineRendererFeature)
            outlineRendererFeature.SetRenderers(selectedRenderers);
    }
}