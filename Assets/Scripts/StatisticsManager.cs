using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    [SerializeField] Text coinsText;

    private int coins = 0;

    public void AddCoin()
    {
        coins++;

        if (coinsText == null)
            Debug.LogWarning("No coins number display attached");
        else
            coinsText.text = coins.ToString();
    }
}
