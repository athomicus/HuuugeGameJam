using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField]
    private float _MovingSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate( _MovingSpeed * Time.deltaTime * Vector3.left );
    }
}
