using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class EntityArmor : MonoBehaviour
{
    public Vector2 offset = new(-2f, 0f);

    public int armor;

    public int Armor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
            if (value > 0)
            {
                Open();
            }
            else
            {
                Close();
            }
            textGUI.text = $"{value}";
        }
    }

    public TextMeshProUGUI textGUI;

    public Sprite sprite_Armored;

    [Header("非手动赋值")]
    public HealthBar healthBar;

    public Sprite sprite_Normal;

    private void OnValidate()
    {
        //记录值
        healthBar = transform.parent.GetComponentInChildren<HealthBar>();
        sprite_Normal = healthBar.image.sprite;

        //位置
        transform.localPosition = offset;
    }

    private void Start()
    {
        //
        Armor = Armor;
    }

    private void Open()
    {
        gameObject.SetActive(true);
        SpriteArmored();
    }

    private void Close()
    {
        gameObject.SetActive(false);
        SpriteNormal();
    }

    private void SpriteArmored()
    {
        healthBar.image.sprite = sprite_Armored;
    }

    private void SpriteNormal()
    {
        healthBar.image.sprite = sprite_Normal;
    }

    private void OnDestroy()
    {
        SpriteNormal();
    }
}
