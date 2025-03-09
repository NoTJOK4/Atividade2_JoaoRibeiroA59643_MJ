using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIhandle : MonoBehaviour
{
    private VisualElement m_Healthbar;
    public static UIhandle instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        if (uiDocument != null)
        {
            m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
            if (m_Healthbar != null)
            {
                SetHealthValue(1.0f);
            }
            else
            {
                Debug.LogError("HealthBar VisualElement not found in UIDocument.");
            }
        }
        else
        {
            Debug.LogError("UIDocument component missing on " + gameObject.name);
        }
    }

    public void SetHealthValue(float percentage)
    {
        if (m_Healthbar != null)
        {
            m_Healthbar.style.width = Length.Percent(100 * percentage);
        }
        else
        {
            Debug.LogError("HealthBar VisualElement is null.");
        }
    }
}