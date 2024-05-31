using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogChoiceBlock : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDeselectHandler
{
    public DialogChoiceMenu dialogChoiceMenu;
    public Image image;
    public TextMeshProUGUI textGUI;
    public Color hoverColor;
    public Color normalColor;
    public Color abandonColor;
    public Action OnClick;
    public DialogChoiceActionInfo info;
    public bool abandon = false;
    public bool stay = false;
    public bool Abandon
    {
        get
        {
            return abandon;
        }
        set
        {
            abandon = value;
            if (Abandon)
            {
                image.color = abandonColor;
            }
            else
            {
                if (stay)
                    image.color = hoverColor;
                else
                    image.color = normalColor;
            }
        }
    }
    public void Init(DialogChoiceActionInfo info)
    {
        this.info = info;
        Choice choice = info.choice;
        if (choice.locked)
        {
            Abandon = true;
            textGUI.text = choice.LockText;
        }
        else
        {
            textGUI.text = info.text;
        }
        OnClick += info.action;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Abandon)
        {

        }
        else
        {
            Debug.Log($"{gameObject.name}OnPClick了");
            OnClick.Invoke();
            dialogChoiceMenu.Destroy();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        stay = true;
        if (Abandon)
        {

        }
        else
        {
            Debug.Log($"{gameObject.name}进入了");
            image.color = hoverColor;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        stay = false;
        if (Abandon)
        {

        }
        else
        {
            image.color = normalColor;
        }
    }
    public void Destroy()
    {
        OnClick = null;
        Destroy(this.gameObject);
    }
    public void OnDeselect(BaseEventData eventData)
    {
    }
}
