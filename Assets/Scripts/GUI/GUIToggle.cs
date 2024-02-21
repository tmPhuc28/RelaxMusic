using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIToggle : MonoBehaviour
{
    [SerializeField] private Toggle tg;
    [SerializeField] private GameObject nonCheck;

    private void Start()
    {
        tg.onValueChanged.AddListener(ToggleSetting);
    }

    private void ToggleSetting(bool isOn)
    {
        if(tg.isOn)
            nonCheck.SetActive(false);
        else
            nonCheck.SetActive(true);
    }
}
