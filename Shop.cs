using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    List<Item> m_ItemList = new List<Item>();

    [SerializeField]
    List<Category> m_CategoryList = new List<Category>();

    [SerializeField]
    List<Row> m_RowList = new List<Row>();

    [SerializeField]
    TextMeshProUGUI m_CrystalsCounter;

    [SerializeField]
    TextMeshProUGUI m_CoinsCounter;
    
    [SerializeField]
    int m_CrystalsLimit, m_CoinsLimit;

    private int m_Crystals, m_Coins;

    void Start()
    {
        Initialize();

        m_Crystals = ReadValue(m_CrystalsCounter);
        m_Coins = ReadValue(m_CoinsCounter);
    }

    private void Initialize()
    {
        foreach (Item item in m_ItemList)
        {
            item.Initialize(OnItemClicked);
        }

        foreach (Category category in m_CategoryList)
        {
            category.Initialize(OnCategoryClicked);
        }
    }

    private void OnCategoryClicked(Category category)
    {
        ShowAllRows();

        foreach (Item item in m_ItemList)
        {
            if (SelectItemsToShow(item, category))
            {
                item.Show();
            }
            else
            {
                item.Hide();
            }
        }

        ShowSelectedRows();
    }

    private void ShowAllRows()
    {
        foreach (Row row in m_RowList)
        {
            if (!row.m_IsDummy)
            {
                row.Show();
            }
            else
            {
                row.Hide();
            }
        }
    }

    private void ShowSelectedRows()
    {
        foreach (Row row in m_RowList)
        {
            if (!row.IsEmpty() || row.m_IsDummy)
            {
                row.Show();
            }
            else
            {
                row.Hide();
            }
        }
    }

    private bool SelectItemsToShow(Item item, Category category)
    {
        switch (category.CategoryType)
        {
            case Category.E_CategoryType.crystals:
                return (item.ItemType == Item.E_ItemType.crystals || item.ItemType == Item.E_ItemType.empty);

            case Category.E_CategoryType.coins:
                return (item.ItemType == Item.E_ItemType.coins || item.ItemType == Item.E_ItemType.empty);

            case Category.E_CategoryType.boosters:
                return (item.ItemType == Item.E_ItemType.booster || item.ItemType == Item.E_ItemType.boosterPremium);

            default:
                return false;
        }
    }

    private int ReadValue(TextMeshProUGUI text)
    {
        if (Int32.TryParse(text.text, out int number))
        {
            return number;
        }

        return 0;
    }

    private void OnItemClicked(Item item)
    {
        switch (item.ItemType)
        {
            case Item.E_ItemType.crystals:
                {
                    m_Crystals += PurchaseCurrency(m_Crystals, item.GetValue(), m_CrystalsLimit);
                    m_CrystalsCounter.text = m_Crystals.ToString();
                    break;
                }
            case Item.E_ItemType.coins:
                {
                    m_Coins += PurchaseCurrency(m_Coins, item.GetValue(), m_CoinsLimit);
                    m_CoinsCounter.text = m_Coins.ToString();
                    break;
                }
            case Item.E_ItemType.booster:
                {
                    m_Coins -= PurchaseBooster(m_Coins, item.GetPrice());
                    m_CoinsCounter.text = m_Coins.ToString();
                    break;
                }
            case Item.E_ItemType.boosterPremium:
                {
                    m_Crystals -= PurchaseBooster(m_Crystals, item.GetPrice());
                    m_CrystalsCounter.text = m_Crystals.ToString();
                    break;
                }
        }
    }

    private int PurchaseCurrency(int ownedCurrency, int value, int limit)
    {
        if (ownedCurrency + value > limit)
        {
            Debug.Log("Limit reached");
            return 0;
        }
        else
        {
            return value;
        }
    }

    private int PurchaseBooster(int ownedCurrency, int price)
    {
        if (ownedCurrency - price < 0)
        {
            Debug.Log("Insufficient funds");
            return 0;
        }
        else
        {
            return price;
        }
    }

}
