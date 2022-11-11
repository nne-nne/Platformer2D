using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{

    public TMP_Text qualityText;

    public void SetVolume(float vol)
    {
        AudioListener.volume = vol;
    }

    private void Awake()
    {
        qualityText.text = $"Quality: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";
    }

    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
        qualityText.text = $"Quality: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";
    }

    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
        qualityText.text = $"Quality: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
