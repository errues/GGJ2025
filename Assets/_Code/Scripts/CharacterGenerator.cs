using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [Header("Body Parts")]
    [SerializeField] private List<Mesh> arms;
    [SerializeField] private List<Mesh> ears;
    [SerializeField] private List<Mesh> eyebrows;
    [SerializeField] private List<Mesh> eyeL;
    [SerializeField] private List<Mesh> eyeR;
    [SerializeField] private List<Mesh> feet;
    [SerializeField] private List<Mesh> head;
    [SerializeField] private List<Mesh> head1;
    [SerializeField] private List<Mesh> legs;
    [SerializeField] private List<Mesh> mouth;
    [SerializeField] private List<Mesh> nose;
    [SerializeField] private List<Mesh> pants;
    [SerializeField] private List<Mesh> shirt3;

    [Header("Base Model")]
    [SerializeField] private SkinnedMeshRenderer armsRenderer;
    [SerializeField] private SkinnedMeshRenderer earsRenderer;
    [SerializeField] private SkinnedMeshRenderer eyebrowsRenderer;
    [SerializeField] private SkinnedMeshRenderer eyeLRenderer;
    [SerializeField] private SkinnedMeshRenderer eyeRRenderer;
    [SerializeField] private SkinnedMeshRenderer feetRenderer;
    [SerializeField] private SkinnedMeshRenderer headRenderer;
    [SerializeField] private SkinnedMeshRenderer head1Renderer;
    [SerializeField] private SkinnedMeshRenderer mouthRenderer;
    [SerializeField] private SkinnedMeshRenderer legsRenderer;
    [SerializeField] private SkinnedMeshRenderer noseRenderer;
    [SerializeField] private SkinnedMeshRenderer pantsRenderer;
    [SerializeField] private SkinnedMeshRenderer shirt3Renderer;

    private void Start()
    {
        GenerateRandomCharacters(10);
    }

    public void GenerateRandomCharacters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GenerateRandomCharacter(i);
        }
    }

    private void GenerateRandomCharacter(int index)
    {
        // Create a new GameObject for the random character
        GameObject character = new GameObject($"RandomCharacter_{index}");
        character.transform.position = new Vector3(index * 2.0f, 0, 0); // Offset characters for visibility

        // Assign random meshes to each body part
        SetRandomMesh(character, armsRenderer, arms);
        SetRandomMesh(character, earsRenderer, ears);
        SetRandomMesh(character, eyebrowsRenderer, eyebrows);
        SetRandomMesh(character, eyeLRenderer, eyeL);
        SetRandomMesh(character, eyeRRenderer, eyeR);
        SetRandomMesh(character, feetRenderer, feet);
        SetRandomMesh(character, headRenderer, head);
        SetRandomMesh(character, head1Renderer, head1);
        SetRandomMesh(character, legsRenderer, legs);
        SetRandomMesh(character, mouthRenderer, mouth);
        SetRandomMesh(character, noseRenderer, nose);
        SetRandomMesh(character, pantsRenderer, pants);
        SetRandomMesh(character, shirt3Renderer, shirt3);
    }

    private void SetRandomMesh(GameObject character, SkinnedMeshRenderer renderer, List<Mesh> meshList)
    {
        if (meshList.Count == 0) return;

        // Create a new GameObject for the mesh
        GameObject part = new GameObject(renderer.name);
        part.transform.parent = character.transform;

        // Add a SkinnedMeshRenderer to the new part
        SkinnedMeshRenderer skinnedMeshRenderer = part.AddComponent<SkinnedMeshRenderer>();

        // Assign a random mesh from the list
        Mesh randomMesh = meshList[Random.Range(0, meshList.Count)];
        skinnedMeshRenderer.sharedMesh = randomMesh;

        // Copy the materials, bones, and root bone from the original renderer
        skinnedMeshRenderer.materials = renderer.materials;
        skinnedMeshRenderer.bones = renderer.bones;
        skinnedMeshRenderer.rootBone = renderer.rootBone;
    }

}
