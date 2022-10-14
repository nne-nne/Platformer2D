using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] int maxKeyNumber = 3;
    [SerializeField] int keyNumber = 0;

    private void OnTouchFinish()
    {
        if (keyNumber >= maxKeyNumber)
        {
            Debug.Log($"finish");
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log($"go collect more keys");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            StatisticsManager.Instance.AddCoin();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Finish"))
        {
            OnTouchFinish();
        }
        else if (other.CompareTag("Key"))
        {
            GameManager.instance.AddKey();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("ExtraLive"))
        {
            Debug.Log($"You have an extra live now, good luck wasting both");
            other.gameObject.SetActive(false);
        }
    }
}
