using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinRotation : MonoBehaviour
{
    [SerializeField] private float amp;
    [SerializeField] private float speed = 1.0f;
    // Start is called before the first frame update

    private Vector3 originalRot;

    void Start()
    {
        originalRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(originalRot + Vector3.forward * Mathf.Sin(Time.time * speed) * amp);
    }
}
