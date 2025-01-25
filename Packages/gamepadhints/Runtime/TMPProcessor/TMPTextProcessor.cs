using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class TMPTextProcessor : MonoBehaviour, ITextPreprocessor
{
    [SerializeField]
    private TMP_Text _tmpText;


    [SerializeField]
    private InputTypeChange _processorTrigger = new InputTypeChange();

    [SerializeField]
    private InputIconsProcess _textProcesses = new InputIconsProcess();

    //Regex pattern: Get text between "{" and "}"
    private const string _regexPattern = @"\{([^}]*)\}";
    private Regex _regex = new Regex(_regexPattern);
    private MatchCollection _regexMatches;

    private void OnValidate()
    {
        if (_tmpText == null)
        {
            _tmpText = GetComponent<TMP_Text>();
        }
    }

    private void OnEnable()
    {
        if (_tmpText != null)
        {
            _tmpText.textPreprocessor = this;

            if (Application.isPlaying)
            {
                if (_processorTrigger != null)
                {
                   _processorTrigger.SetTrigger(ForceTextprocess);
                }
            }
        }
    }

    private void OnDisable()
    {
        if (_tmpText != null)
        {
            if (_tmpText.textPreprocessor == (ITextPreprocessor)this)
                _tmpText.textPreprocessor = null;

            if (Application.isPlaying)
            {
                if (_processorTrigger != null)
                {
                    _processorTrigger.RemoveTrigger(ForceTextprocess);
                }
            }
        }
    }

    private void Start()
    {
        if (_tmpText != null)
        {
            _tmpText.textPreprocessor = this;
            ForceTextprocess();
        }
    }

    public void ForceTextprocess()
    {
        if (_tmpText != null)
        {
            _tmpText.havePropertiesChanged = true;
        }
    }


    public string PreprocessText(string text)
    {
        _regexMatches = _regex.Matches(text);

        if (_textProcesses != null)
        {
            text = _textProcesses.ProcessText(text, _regexMatches);
        }

        return text;
    }
}
