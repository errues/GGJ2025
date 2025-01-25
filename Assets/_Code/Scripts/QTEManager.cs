using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For the UI if you want to use text to indicate the buttons

public class QTEManager : MonoBehaviour
{
    [Header("QTE Settings")]
    public List<KeyCode> possibleButtons; // List of possible buttons (can be configured in the editor)
    public int sequenceLength = 5; // Length of the sequence
    public float buttonPressTime = 1.5f; // Time to press each button
    public float totalSequenceTime = 10f; // Total time to complete the QTE

    [Header("UI Elements")]
    public Text qteDisplay; // Text to display the sequence (optional)
    public Text timerDisplay; // Text to display the timer (optional)

    private List<KeyCode> qteSequence = new List<KeyCode>(); // Generated sequence
    private int currentStep = 0; // Index of the current step in the sequence
    private bool isQTEActive = false; // Flag to check if the QTE is active
    private float buttonTimer = 0f; // Timer for the current button
    private float sequenceTimer = 0f; // Timer for the entire sequence

    void Start()
    {
        // Start the QTE
        StartQTE();
    }

    void Update()
    {
        if (isQTEActive)
        {
            // Update the timers
            buttonTimer -= Time.deltaTime;
            sequenceTimer -= Time.deltaTime;

            // Update the UI (optional)
            if (timerDisplay != null)
            {
                timerDisplay.text = $"Time: {sequenceTimer:F2}";
            }

            // Check if the time for the current button has run out
            if (buttonTimer <= 0)
            {
                FailQTE();
                return;
            }

            // Check if the total sequence time has run out
            if (sequenceTimer <= 0)
            {
                FailQTE();
                return;
            }

            // Check the player's input
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in possibleButtons)
                {
                    if (Input.GetKeyDown(key))
                    {
                        if (key == qteSequence[currentStep])
                        {
                            // Correct button press
                            OnCorrectButtonPress();
                        }
                        else
                        {
                            // Incorrect button press
                            FailQTE();
                        }
                        return;
                    }
                }
            }
        }
    }

    void StartQTE()
    {
        // Generate the sequence
        qteSequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            qteSequence.Add(possibleButtons[Random.Range(0, possibleButtons.Count)]);
        }

        // Reset variables
        currentStep = 0;
        isQTEActive = true;
        buttonTimer = buttonPressTime;
        sequenceTimer = totalSequenceTime;

        // Display the sequence in the UI (optional)
        UnityEngine.Debug.Log(SequenceToString());
        if (qteDisplay != null)
        {
            qteDisplay.text = SequenceToString();
        }
    }

    void OnCorrectButtonPress()
    {
        currentStep++;

        if (currentStep >= qteSequence.Count)
        {
            // Sequence completed
            CompleteQTE();
        }
        else
        {
            // Reset the timer for the next button
            buttonTimer = buttonPressTime;
        }
    }

    void CompleteQTE()
    {
        isQTEActive = false;
        Debug.Log("QTE Successfully Completed!");
        // Here you can add logic for when the player completes the QTE
    }

    void FailQTE()
    {
        isQTEActive = false;
        Debug.Log("QTE Failed!");
        // Here you can add logic for when the player fails the QTE
    }

    string SequenceToString()
    {
        string sequenceString = "";
        foreach (KeyCode key in qteSequence)
        {
            sequenceString += key.ToString() + " ";
        }
       
        return sequenceString.Trim();
    }
}
