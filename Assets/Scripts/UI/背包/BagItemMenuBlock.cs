using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//鼠标悬浮变色
//文字动态，鼠标点击触发特定函数
public class BagItemMenuBlock : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDeselectHandler
{
    public BagItemMenu bagItemMenu;
    public Image image;
    public TextMeshProUGUI textGUI;
    public Color hoverColor;
    public Color normalColor;
    public Action OnClick;

    public void Init(ItemMenuActionInfo info)
    {
        textGUI.text = info.text;
        OnClick += info.action;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name}OnPClick了");
        OnClick.Invoke();
        bagItemMenu.Destroy();
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
