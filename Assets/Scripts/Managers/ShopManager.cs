using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopUI; // Ссылка на UI-магазина

    void Start()
    {
        if (shopUI == null)
        {
            Debug.LogError("Shop UI object is not assigned!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && shopUI != null)
        {
            // Переключение видимости магазина
            shopUI.SetActive(!shopUI.activeSelf);
        }
    }
}
