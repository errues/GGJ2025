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
    [SerializeField] private List<Mesh> foto;
    [SerializeField] private List<Mesh> head;
    [SerializeField] private List<Mesh> head1;
    [SerializeField] private List<Mesh> legs;
    [SerializeField] private List<Mesh> mouth;
    [SerializeField] private List<Mesh> nose;
    [SerializeField] private List<Mesh> pants;
    [SerializeField] private List<Mesh> shirt3;

    [Header("Face Textures")]
    [SerializeField] private List<Texture> faceTextures;

    [Header("Base Model")]
    [SerializeField] private GameObject baseModel;

    [SerializeField]
    private List<GameObject> CharactersPool;

    private void Start()
    {
        CharactersPool = new List<GameObject>();
        GenerateCharactersWithUniqueTextures();
    }

    public void GenerateCharactersWithUniqueTextures()
    {
        // Ensure there are enough textures to generate characters
        int characterCount = faceTextures.Count;
        List<Texture> availableTextures = new List<Texture>(faceTextures);

        for (int i = 0; i < characterCount; i++)
        {
            GenerateCharacterWithTexture(i, availableTextures[i]);
        }
    }

    private void GenerateCharacterWithTexture(int index, Texture faceTexture)
    {
        // Create a unique parent GameObject for the character
        GameObject characterParent = new GameObject($"CharacterParent_{index}");
        characterParent.transform.position = Vector3.zero;
        characterParent.transform.parent = this.gameObject.transform;


        // Instantiate the base model as a child of the parent
        GameObject character = Instantiate(baseModel, characterParent.transform);
        character.name = $"Character_{index}";

        // Assign random meshes to each body part using the child renderers
        SetRandomMesh(character, "character:arms", arms);
        SetRandomMesh(character, "character:ears", ears);
        SetRandomMesh(character, "character:eyebrows", eyebrows);
        SetRandomMesh(character, "character:eyeL", eyeL);
        SetRandomMesh(character, "character:eyeR", eyeR);
        SetRandomMesh(character, "character:feet", feet);
        SetRandomMesh(character, "character:foto", foto, true, faceTexture);
        SetRandomMesh(character, "character:head", head);
        SetRandomMesh(character, "character:head1", head1);
        SetRandomMesh(character, "character:legs", legs);
        SetRandomMesh(character, "character:mouth", mouth);
        SetRandomMesh(character, "character:nose", nose);
        SetRandomMesh(character, "character:pants", pants);
        SetRandomMesh(character, "character:shirt3", shirt3);

        // Add the character parent to the pool
        CharactersPool.Add(characterParent);
    }

    private void SetRandomMesh(GameObject character, string childName, List<Mesh> meshList, bool isHead = false, Texture faceTexture = null)
    {
        if (meshList.Count == 0) return;

        // Find the child renderer by name
        Transform childTransform = character.transform.Find("character:Geometry/character:basechar/"+childName);
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
