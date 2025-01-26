using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlurredBufferMultiObjectOutlineRendererFeature : ScriptableRendererFeature
{
    private static readonly int SpreadId = Shader.PropertyToID("_Spread");

    [SerializeField] private RenderPassEvent renderEvent = RenderPassEvent.AfterRenderingTransparents;
    [Space, SerializeField] private Material dilationMaterial;
    [SerializeField] private Material outlineMaterial;
    [SerializeField, Range(1, 60)] private int spread = 10;
    [SerializeField] private Color outlineColor = Color.cyan;

    private BlurredBufferMultiObjectOutlinePass _outlinePass;

    private List<Renderer> _targetRenderers;

    public void AddRenderers(List<Renderer> targetRenderers)
    {
        if (_targetRenderers == null)
            _targetRenderers = new List<Renderer>();

        _targetRenderers.AddRange(targetRenderers);

        if (_outlinePass != null)
            _outlinePass.Renderers = _targetRenderers;
    }

    public void RemoveRenderers(List<Renderer> targetRenderers)
    {
        if (_targetRenderers != null)
        {
            foreach (Renderer renderer in targetRenderers)
                _targetRenderers.Remove(renderer);

            if (_outlinePass != null)
                _outlinePass.Renderers = _targetRenderers;
        }
    }

    public void SetRenderers(List<Renderer> targetRenderers)
    {
        _targetRenderers = targetRenderers;

        if (_outlinePass != null)
            _outlinePass.Renderers = _targetRenderers;
    }

    public override void Create()
    {
        name = "Outliner";

        // Pass in constructor variables which don't/shouldn't need to be updated every frame.
        _outlinePass = new BlurredBufferMultiObjectOutlinePass();
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (_outlinePass == null)
            return;

        if (!dilationMaterial ||
            !outlineMaterial ||
            _targetRenderers == null ||
            _targetRenderers.Count == 0)
        {
            // Don't render the effect if there's nothing to render
            return;
        }

        // Any variables you may want to update every frame should be set here.
        _outlinePass.RenderEvent = renderEvent;
        _outlinePass.DilationMaterial = dilationMaterial;
        dilationMaterial.SetInteger("_Spread", spread);
        _outlinePass.OutlineMaterial = outlineMaterial;
        outlineMaterial.SetColor("_BaseColor", outlineColor);
        _outlinePass.Renderers = _targetRenderers;

        renderer.EnqueuePass(_outlinePass);
    }
}