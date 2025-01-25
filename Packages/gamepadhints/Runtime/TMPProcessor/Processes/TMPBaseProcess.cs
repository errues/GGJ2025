using System;
using System.Text.RegularExpressions;

[System.Serializable]
public abstract class TMPBaseProcess
{
    public abstract string ProcessText(string textToProcess, MatchCollection textVariables);
}
