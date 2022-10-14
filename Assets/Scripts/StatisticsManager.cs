using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager Instance { get; private set; }

    [SerializeField] TMP_Text coinsText;

    private int coins = 0;

    public void AddCoin()
    {
        coins++;

        if (coinsText == null)
            Debug.LogWarning("No coins number display attached");
        else
            coinsText.text = coins.ToString();
    }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
