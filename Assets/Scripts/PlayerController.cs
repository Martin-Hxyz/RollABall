using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeedMultiplier = 2.0f;
    private Rigidbody _rigidbody;
    private float _movementX, _movementY;
    private int _score = 0;
    private int _totalCollectibles;
    private TextMeshProUGUI _countText;
    private TextMeshProUGUI _winText;
    private bool _ended = false;
    private float _dissolve = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _countText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        _winText = GameObject.FindWithTag("WinText").GetComponent<TextMeshProUGUI>();
        _winText.gameObject.SetActive(false);
        _totalCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        if (_ended) return;
        _rigidbody.AddForce(_movementX * moveSpeedMultiplier, 0, _movementY * moveSpeedMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collectible")) return;
        other.gameObject.SetActive(false);
        _score += 1;
//        _scaleMultiplier += 0.25f;

        Vector3 scale = transform.localScale;
        scale.Scale(new Vector3(1.1f, 1.1f, 1.1f));
        transform.localScale = scale;

        UpdateScoreText();

        if (_score >= _totalCollectibles)
        {
            EndGame();
        }
    }

    private void Update()
    {
        if (_ended)
        {
            float scale = 1 + Mathf.PingPong(Time.time, 1) * 0.25f;
            _winText.gameObject.transform.localScale = new Vector3(scale, scale, 1);

            if (_dissolve <= 1)
            {
                Material mat = gameObject.GetComponent<MeshRenderer>().material;
                mat.SetFloat("_Dissolve", _dissolve);
                _dissolve += (Time.deltaTime * 0.3f);
            }
        }
    }

    private void UpdateScoreText()
    {
        _countText.text = _score.ToString();
    }

    private void EndGame()
    {
        
        _ended = true;
        _winText.gameObject.SetActive(true);
    }
}