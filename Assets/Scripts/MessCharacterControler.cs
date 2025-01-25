using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class MessCharacterControler : MonoBehaviour
{
    [SerializeField]
    private float m_MinDirtyLevelToClean = 50f; // Speed at which dirtyLevel increases or decreases per second
    [SerializeField]
    private float m_IncrementSpeed = 10f; // Speed at which dirtyLevel increases or decreases per second
    private Material mCubeMaterial; // Material of the cube
    private float mDirtyLevel = 0f; // Current dirty level of the cube (0 to 100)

    private bool mCleaningDirtyNPC;

    void Start()
    {
        // Get the material of the cube
        mCubeMaterial = GetComponent<Renderer>().material;
        if (mCubeMaterial != null)
        {
            MessNPC();
        }
    }

    private void MessNPC()
    {
        if (mCleaningDirtyNPC)
            return;

        StartCoroutine(_MessCharacter());
    }

    // Coroutine to gradually increase the dirty level
    private IEnumerator _MessCharacter()
    {

        while (mDirtyLevel < 100f)
        {
            // Increment dirtyLevel by a fixed amount per frame
            mDirtyLevel += m_IncrementSpeed * Time.deltaTime;

            // Clamp dirtyLevel to ensure it stays within the 0-100 range
            mDirtyLevel = Mathf.Clamp(mDirtyLevel, 0f, 100f);

            // Update the cube's color based on the dirty level
            UpdateCubeColor(mDirtyLevel);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final dirty level is exactly 100
        mDirtyLevel = 100f;
        UpdateCubeColor(mDirtyLevel);
    }

    public bool CanCleanDirtyCharacter()
    {
        return mDirtyLevel >= m_MinDirtyLevelToClean;
    }

    // Coroutine to gradually decrease the dirty level
    public void CleanDirtyCharacter()
    {
        if(mDirtyLevel <= 0)
        {
            if (mCleaningDirtyNPC)
                mCleaningDirtyNPC = false;

            mDirtyLevel = 0;
            return;
        }


        mCleaningDirtyNPC = true;
        // Decrement dirtyLevel by a fixed amount per frame
        mDirtyLevel -= m_IncrementSpeed * Time.deltaTime;

        // Clamp dirtyLevel to ensure it stays within the 0-100 range
        mDirtyLevel = Mathf.Clamp(mDirtyLevel, 0f, 100f);

        // Update the cube's color based on the dirty level
        UpdateCubeColor(mDirtyLevel);
    }

    public void CancelCleanDirtyCharacter()
    {
        mCleaningDirtyNPC = false;
    }

    
    // Updates the cube's material color based on the dirty level
    private void UpdateCubeColor(float dirtyLevel)
    {
        Color color;

        if (dirtyLevel <= 50f)
        {
            // Transition from white to yellow (first half: 0 to 50)
            color = Color.Lerp(Color.white, Color.yellow, dirtyLevel / 50f);
        }
        else
        {
            // Transition from yellow to brown through green (second half: 50 to 100)
            float secondaryProgress = (dirtyLevel - 50f) / 50f;
            Color greenToBrown = Color.Lerp(Color.green, new Color(0.6f, 0.3f, 0f), secondaryProgress);
            color = Color.Lerp(Color.yellow, greenToBrown, secondaryProgress);
        }

        // Apply the calculated color to the cube's material
        mCubeMaterial.color = color;
    }
}
