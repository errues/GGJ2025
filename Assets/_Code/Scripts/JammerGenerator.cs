using System.Collections.Generic;
using UnityEngine;

public class JammerGenerator : MonoBehaviour
{
    [Header("Body Parts")]
    [SerializeField] private List<Mesh> arms;
    [SerializeField] private List<Mesh> ears;
    [SerializeField] private List<Mesh> eyebrows;
    [SerializeField] private List<Mesh> eyeL;
    [SerializeField] private List<Mesh> eyeR;
    [SerializeField] private List<Mesh> feet;
    [SerializeField] private List<Mesh> foto;
    [SerializeField] private List<Mesh> head;
    [SerializeField] private List<Mesh> head1;
    [SerializeField] private List<Mesh> legs;
    [SerializeField] private List<Mesh> mouth;
    [SerializeField] private List<Mesh> nose;
    [SerializeField] private List<Mesh> pants;
    [SerializeField] private List<Mesh> shirt3;

    [Header("Face Textures")]
    public Texture FaceTexture;

    [Header("Base Model")]
    [SerializeField] private GameObject baseModel;

    //private void Start()
    //{
    //    // Assign random meshes to each body part using the child renderers
    //    Generate();
    //}

    public void Generate()
    {
        SetRandomMesh(baseModel, "character:arms", arms);
        SetRandomMesh(baseModel, "character:ears", ears);
        SetRandomMesh(baseModel, "character:eyebrows", eyebrows);
        SetRandomMesh(baseModel, "character:eyeL", eyeL);
        SetRandomMesh(baseModel, "character:eyeR", eyeR);
        SetRandomMesh(baseModel, "character:feet", feet);
        SetRandomMesh(baseModel, "character:foto", foto, true, FaceTexture);
        SetRandomMesh(baseModel, "character:head", head);
        SetRandomMesh(baseModel, "character:head1", head1);
        SetRandomMesh(baseModel, "character:legs", legs);
        SetRandomMesh(baseModel, "character:mouth", mouth);
        SetRandomMesh(baseModel, "character:nose", nose);
        SetRandomMesh(baseModel, "character:pants", pants);
        SetRandomMesh(baseModel, "character:shirt3", shirt3);
    }

    private void SetRandomMesh(GameObject character, string childName, List<Mesh> meshList, bool isHead = false, Texture faceTexture = null)
    {
        if (meshList.Count == 0) return;

        // Find the child renderer by name
        Transform childTransform = character.transform.Find("character:Geometry/character:basechar/" + childName);
        if (childTransform == null) return;

        SkinnedMeshRenderer skinnedMeshRenderer = childTransform.GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null) return;

        // Assign a random mesh from the list
        Mesh randomMesh = meshList[Random.Range(0, meshList.Count)];
        skinnedMeshRenderer.sharedMesh = randomMesh;

        // If the body part is the head, assign the specified texture
        if (isHead && faceTexture != null)
        {
            Material originalMaterial = skinnedMeshRenderer.material;
            Material newMaterial = new Material(originalMaterial);
            newMaterial.mainTexture = faceTexture;
            skinnedMeshRenderer.material = newMaterial;
        }
    }
}
