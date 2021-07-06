using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    private UnityAction<Category> m_OnButtonClicked;
    private Button m_ButtonComponent;

    public E_CategoryType CategoryType;

    public enum E_CategoryType
    {
        crystals,
        coins,
        boosters
    }
    public void Awake()
    {
        m_ButtonComponent = GetComponent<Button>();
        m_ButtonComponent.onClick.AddListener(OnButtonClicked);
    }

    public void Initialize(UnityAction<Category> onCallbackElementClicked)
    {
        m_OnButtonClicked = onCallbackElementClicked;
    }

    protected void OnButtonClicked()
    {
        if (m_OnButtonClicked != null)
        {
            m_OnButtonClicked.Invoke(this);
        }
    }

}
