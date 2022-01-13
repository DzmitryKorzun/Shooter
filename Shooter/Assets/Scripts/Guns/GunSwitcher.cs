using UnityEngine;
using System;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private Gun[] guns;

    private string pressedButton;
    private int idGun;

    public Gun[] Guns => guns;

    private void Start()
    {
        guns[0].GunActive();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            pressedButton = Input.inputString;
            int.TryParse(pressedButton, out idGun);
            if (idGun <= guns.Length && idGun != 0)
            {
                for (int i = 0; i < guns.Length; i++)
                {
                    if (idGun != (i + 1))
                    {
                        guns[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        guns[i].gameObject.SetActive(true);
                        guns[i].GunActive();
                    }
                }
            }
        }
    }
}

