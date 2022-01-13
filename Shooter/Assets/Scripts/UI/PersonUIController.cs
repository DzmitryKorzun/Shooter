using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonUIController : MonoBehaviour
{
    [SerializeField] private Text healthTextStatus;
    [SerializeField] private PersonController personController;
    [SerializeField] private GunSwitcher gunSwitcher;
    [SerializeField] private Text reloadText;
    [SerializeField] private Text armoStatus;

    private Gun[] guns;
    private System.Text.StringBuilder sb = new System.Text.StringBuilder(5);

    void Start()
    {
        guns = gunSwitcher.Guns;
        healthTextStatus.text = personController.MaxHealth.ToString();
        foreach (Gun item in guns)
        {
            item.weaponStateChangeEvent += ChangeWeaponUI;
        }
    }

    private void ChangeWeaponUI(int currentArmo, int maxArmo, bool isReload)
    {
        currentArmo = Mathf.Clamp(currentArmo, 0, maxArmo); 
        sb.Clear();
        armoStatus.text = sb.Append($"{currentArmo} / {maxArmo}").ToString();
        if (isReload)
        {
            reloadText.gameObject.SetActive(true);
        }
        else
        {
            reloadText.gameObject.SetActive(false);
        }
    }

    public void ChangeHealthStatus(float value)
    {
        sb.Clear();
        healthTextStatus.text = sb.Append(value).ToString();
    }
}
