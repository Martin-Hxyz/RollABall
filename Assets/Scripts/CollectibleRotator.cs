using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRotator : MonoBehaviour
{
    private void Update()
    {
        Transform collectibleTransform = this.transform;
        collectibleTransform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        
        float yPos = Mathf.PingPong(Time.time, 1) * 0.25f;
        collectibleTransform.position = new Vector3(transform.position.x, yPos + 0.5f, collectibleTransform.position.z);
    }
}