using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _worldWidthInUnits = 16f;
    [SerializeField] float _min = 0f;
    [SerializeField] float _max = 14f;

    private BallScript _ballScript;

    private GameStatusScript _gameStatus;
    void Start()
    {
        _gameStatus = FindObjectOfType<GameStatusScript>();
        _ballScript = FindObjectOfType<BallScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float paddleX = GetX();
        var paddleXClamp = Mathf.Clamp(paddleX, _min, _max);
        Vector2 newPaddlePosition = new Vector2(paddleXClamp, transform.position.y);
        transform.position = newPaddlePosition;
    }

    private float GetX()
    {
        if (_gameStatus.IsAutoplayEnabled)
        {
            return _ballScript.gameObject.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * _worldWidthInUnits;
        }
    }
}
