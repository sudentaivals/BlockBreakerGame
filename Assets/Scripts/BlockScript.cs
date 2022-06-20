using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockScript : MonoBehaviour
{
    [SerializeField] AudioClip _breakingSoundEffect;

    [SerializeField] GameObject _particleEmitter;
    [SerializeField] int _maxHits = 2;
    [SerializeField] List<Sprite> _blockSprites;

    private LevelScript _level;

    private GameStatusScript _gameStatus;

    private int _timesHit = 0;

    private void Start()
    {
        _level = FindObjectOfType<LevelScript>();
        _gameStatus = FindObjectOfType<GameStatusScript>();
        if(tag != "Unbreakable")
        {
            _level.IncreaseBlockQuantity();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (tag)
        {
            case "Breakable":
                HandleHit();
                break;
            default:
                return;
        }
    }

    private void HandleHit()
    {
        _timesHit++;
        if (_timesHit >= _maxHits)
        {
            DestroyBlock();
        }
        else
        {
            SelectNextSprite();
        }
    }

    private void SelectNextSprite()
    {
        if (_blockSprites.Count == 0) return;
        GetComponent<SpriteRenderer>().sprite = GetNextSprite();
    }

    private Sprite GetNextSprite()
    {
        float maxDurabilityPercent = 1.0f;
        float blockDurabilityPercent = maxDurabilityPercent - (float)_timesHit / (float)_maxHits;
        float step = maxDurabilityPercent / (float)_blockSprites.Count;
        float minDurabilityPercent = maxDurabilityPercent - step;
        foreach (var sprite in _blockSprites)
        {
            if (blockDurabilityPercent > minDurabilityPercent && blockDurabilityPercent <= maxDurabilityPercent)
            {
                return sprite;
            }
            maxDurabilityPercent -= step;
            minDurabilityPercent = maxDurabilityPercent - step;
        }
        return _blockSprites.Last();
    }

    private void DestroyBlock()
    {
        _gameStatus.AddScoreAfterBlockIsDestroyed();
        AudioSource.PlayClipAtPoint(_breakingSoundEffect, Camera.main.transform.position);
        TriggerSpakleVFX();
        Destroy(gameObject);
        _level.DecreaseBlockQuantity();
    }

    private void TriggerSpakleVFX()
    {
        var sparkleEmitter = Object.Instantiate(_particleEmitter, transform.position, transform.rotation);
        var psSettings = sparkleEmitter.GetComponent<ParticleSystem>().main;
        psSettings.startColor = new ParticleSystem.MinMaxGradient(GetComponent<SpriteRenderer>().color);

    }
}
