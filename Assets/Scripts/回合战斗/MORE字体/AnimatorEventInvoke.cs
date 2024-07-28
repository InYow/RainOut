using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventInvoke : MonoBehaviour
{
    public UnityEvent unityEvent;

    public void EventInvoke()
    {
        unityEvent.Invoke();
    }
}
