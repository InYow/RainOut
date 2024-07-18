using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StateChecker : Checker
{
    public State state;

    public TextMeshProUGUI stackCountGUI;

    private void OnValidate()
    {
        state = GetComponent<State>();
    }
}
