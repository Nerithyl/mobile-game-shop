using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private UnityAction<Item> m_OnItemClicked;

    [SerializeField]
    private Button m_Button;

    [SerializeField]
    private TextMeshProUGUI m_Value;

    [SerializeField]
    private TextMeshProUGUI m_Price;

    private Button m_ButtonComponent;

    public E_ItemType ItemType;

    public enum E_ItemType
    {
        crystals,
        coins,
        booster,
        boosterPremium,
        empty
    }

    public void Awake()
    {
        m_ButtonComponent = m_Button.GetComponent<Button>();
        m_ButtonComponent.onClick.AddListener(OnButtonClicked);
    }

    public void Initialize(UnityAction<Item> onCallbackElementClicked)
    {
        m_OnItemClicked = onCallbackElementClicked;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void OnButtonClicked()
    {
        if (m_OnItemClicked != null)
        {
            m_OnItemClicked.Invoke(this);
        }
    }

    public int GetValue()
    {
        if (ItemType == E_ItemType.booster || ItemType == E_ItemType.boosterPremium)
        {
            return 1;
        }
        else if (Int32.TryParse(m_Value.text, out int number))
        {
            return number;
        }

        return 0;
    }

    public int GetPrice()
    {
        if ((ItemType == E_ItemType.booster || ItemType == E_ItemType.boosterPremium) 
            && Int32.TryParse(m_Price.text, out int number))
        {
            return number;
        }

        return 0;
    }
}
