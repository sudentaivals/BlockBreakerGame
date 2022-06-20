using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatusScript : MonoBehaviour
{

    [Range(0.1f, 10f)][SerializeField] float _gameSpeed = 1f;


    [SerializeField] private int _score = 0;

    [SerializeField] private int _scorePerBlockDestroyed = 55;

    [SerializeField] TextMeshProUGUI _scoreText;

    [SerializeField] bool _autoplay = false;

    public bool IsAutoplayEnabled => _autoplay;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatusScript>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _scoreText.text = _score.ToString();
    }

    void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    public void DestroyCurrentGameStatus()
    {
        Destroy(gameObject);
    }

    public void AddScoreAfterBlockIsDestroyed()
    {
        _score += _scorePerBlockDestroyed;
        _scoreText.text = _score.ToString();
    }
}
