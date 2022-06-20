using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InvisibleWallScript : MonoBehaviour
{
    [SerializeField] float _increaseSpeedAfterBounce = 5f;
    [SerializeField] float _speedBoostDurationInSecs = 0.5f;


    private void OnCollisionExit2D(Collision2D collision)
    {
        IncreaseBallVelocity(collision);

    }

    private async void IncreaseBallVelocity(Collision2D collision)
    {
        var rigidBody2D = collision.gameObject.GetComponent<Rigidbody2D>();
        float angleRads = Mathf.Atan2(rigidBody2D.velocity.y, rigidBody2D.velocity.x);
        Vector2 newVelocity = new Vector2(Mathf.Cos(angleRads), Mathf.Sin(angleRads));
        rigidBody2D.velocity = newVelocity * (rigidBody2D.velocity.magnitude + _increaseSpeedAfterBounce);

        await Task.Delay(TimeSpan.FromSeconds(_speedBoostDurationInSecs));

        try
        {
            angleRads = Mathf.Atan2(rigidBody2D.velocity.y, rigidBody2D.velocity.x);
            newVelocity = new Vector2(Mathf.Cos(angleRads), Mathf.Sin(angleRads));
            rigidBody2D.velocity = newVelocity * (rigidBody2D.velocity.magnitude - _increaseSpeedAfterBounce);
        }
        catch
        {
            return;
        }
    }
}
