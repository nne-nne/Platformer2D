using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFloat : MonoBehaviour
{
    [SerializeField] private float amp;
    // Start is called before the first frame update

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originalPos + Vector3.up * Mathf.Sin(Time.time) * amp;
    }
}
