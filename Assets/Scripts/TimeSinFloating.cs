using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSinFloating : MonoBehaviour
{
    [SerializeField] private float ampHor;
    [SerializeField] private float ampVer;
    [SerializeField] private float speedHor=1.0f;
    [SerializeField] private float speedVer=1.0f;
    // Start is called before the first frame update

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originalPos + Vector3.up * Mathf.Sin(Time.time * speedVer) * ampVer + Vector3.right * Mathf.Sin(Time.time * speedHor) * ampHor;
    }
}
