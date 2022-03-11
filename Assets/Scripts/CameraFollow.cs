using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _offset;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _offset = transform.position - _player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}