using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseColliderScript : MonoBehaviour
{
    private async void OnTriggerEnter2D(Collider2D collision)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        SceneManager.LoadScene("GameOver");
    }

}
