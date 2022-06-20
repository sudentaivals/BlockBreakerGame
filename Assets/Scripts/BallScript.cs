using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] PaddleScript paddle1;
    [SerializeField] Vector2 _velocityOnLaunch;
    [SerializeField] List<AudioClip> _audioEffects;
    [SerializeField] float _angleDegreeDispersion = 10.0f;

    private int _lastPlayedSoundEffectIndex = -1;

    private Vector3 _paddleToBallVector;

    private bool _isBallLauched = false;

    private AudioSource MyAudioSource => GetComponent<AudioSource>();

    private Rigidbody2D _rigidBodyComponent;

    void Start()
    {
        _paddleToBallVector = transform.position - paddle1.transform.position;
        _rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!_isBallLauched)
        {
            SetDefaultBallPosition();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButton(1))
        {
            _isBallLauched = true;
            var rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = _velocityOnLaunch;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isBallLauched)
        {
            ChangeBallDirection();
            PlayRandomSoundEffect();
        }
    }


    private void ChangeBallDirection()
    {
        float oneDegreeAsRads = Mathf.PI / 180f;
        float oldAngle = Mathf.Atan2(_rigidBodyComponent.velocity.y, _rigidBodyComponent.velocity.x);
        float newAngle = UnityEngine.Random.Range(oldAngle - (_angleDegreeDispersion / 2f * oneDegreeAsRads), oldAngle + (_angleDegreeDispersion / 2 * oneDegreeAsRads));
        Vector2 newVelocity = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle)) * _rigidBodyComponent.velocity.magnitude;
        _rigidBodyComponent.velocity = newVelocity;
    }


    private void PlayRandomSoundEffect()
    {

        if (_audioEffects == null || _audioEffects.Count == 0 || !_isBallLauched) return;

        if (_audioEffects.Count == 1)
        {
            MyAudioSource.clip = _audioEffects.First();
        }
        else
        {
            while (true)
            {
                int randomIndex = UnityEngine.Random.Range(0, _audioEffects.Count);
                if (randomIndex == _lastPlayedSoundEffectIndex)
                {
                    continue;
                }
                else
                {
                    MyAudioSource.clip = _audioEffects[randomIndex];
                    _lastPlayedSoundEffectIndex = randomIndex;
                    break;
                }
            }
        }
        MyAudioSource.PlayOneShot(MyAudioSource.clip);
    }

    private void PlayRandomSoundEffectClever()
    {
        if (_isBallLauched)
        {
            var clip = _audioEffects[UnityEngine.Random.Range(0, _audioEffects.Count)];
            MyAudioSource.PlayOneShot(clip);

        }
    }

    private void SetDefaultBallPosition()
    {
        transform.position = paddle1.transform.position + _paddleToBallVector;
    }
}
