using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    private int _blocksRemain;

    private SceneLoaderScript _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoaderScript>();
    }

    public void IncreaseBlockQuantity()
    {
        _blocksRemain++;
    }

    public void DecreaseBlockQuantity()
    {
        _blocksRemain--;
        if (_blocksRemain == 0) _sceneLoader.LoadNextScene();
    }


}
