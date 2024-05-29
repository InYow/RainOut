using System;
using TMPro;
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
    public Action OnClick;
    public void Init(DialogChoiceActionInfo info)
    {
        //        Debug.Log("Init DialogChoiceBlock");
        textGUI.text = info.text;
        OnClick += info.action;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name}OnPClick了");
        OnClick.Invoke();
        dialogChoiceMenu.Destroy();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name}进入了");
        image.color = hoverColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
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
