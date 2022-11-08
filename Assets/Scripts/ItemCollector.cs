using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private AudioSource source;
    public AudioClip coinSound;
    public AudioClip fallSound;
    public AudioClip keySound;
    public AudioClip extraLiveSound;
    public AudioClip finnishSound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTouchFinish()
    {
        // Level 2 has no keys
        //if (GameManager.instance.KeysCompleted)
        //{
            GameManager.instance.SetGameState(GameState.GS_LEVEL_COMPLETED);
        //}
        //else
        //{
        //    Debug.Log($"go collect more keys");
        //}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            StatisticsManager.Instance.AddCoin();
            source.PlayOneShot(coinSound, AudioListener.volume);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Finish"))
        {
            source.PlayOneShot(finnishSound, AudioListener.volume);
            OnTouchFinish();
        }
        else if (other.CompareTag("Key"))
        {
            GameManager.instance.AddKey();
            source.PlayOneShot(keySound, AudioListener.volume);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("ExtraLive"))
        {
            Debug.Log($"You have an extra live now, good luck wasting both");
            GameManager.instance.AddLive();
            source.PlayOneShot(extraLiveSound, AudioListener.volume);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("FallLevel"))
        {
            source.PlayOneShot(fallSound, AudioListener.volume);
            GameManager.instance.GameOver();
        }
    }
}
