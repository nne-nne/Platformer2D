using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameShake : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float minIntensity = 0f, maxIntensity = 1f;
    [SerializeField] private float exp = 1;

    private SpriteRenderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float noiseVal = 1 - Mathf.PerlinNoise(Time.time * speed, Time.time * speed);
        noiseVal = Mathf.Pow(noiseVal, exp);
        noiseVal = Mathf.Lerp(minIntensity, maxIntensity, noiseVal);
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, noiseVal);
    }
}
