using System;
using UnityEngine;
using System.Collections;
using TMPro;
using NUnit.Framework;

public abstract class Bullet : MonoBehaviour
{

    protected Vector3 spawnPos;
    protected Vector3 target;
    public static event Action<Bullet> onFireEvent;
    public static event Action<Bullet> onHitEvent;
    public static event Action<Bullet> onUpdateEvent;

    public BulletStats baseStats;
    public float currentSpeed = 0;
    public float currentDamage = 0;
    public float currentlifetime = 0;
    public float currentGravity = 0;
    public Sprite currentSprite;

    protected bool fired = false;

    public void ResetBaseStats(BulletStats stats)
    {
        currentSpeed = stats.speed;
        currentDamage = stats.damage;
        currentlifetime = stats.lifetime;
        currentGravity = stats.gravity;
        currentSprite = stats.sprite;
    }

    /// <summary>
    /// Called when the bullet is fired from the source.
    /// Combo effects should be place here.
    /// </summary>
    public virtual void onFire()
    {
        ResetBaseStats(baseStats);
        //Apply the effects that get applied on fire

        if (onFireEvent != null)
        {
            onFireEvent?.Invoke(this);
        }
    }

    public virtual void onUpdate()
    {
        //Apply the effects that get applied on fire

        if (onUpdateEvent != null)
        {
            onUpdateEvent?.Invoke(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onHit(collision);
    }

    /// <summary>
    /// Called when the bullet hits and object
    /// </summary>
    /// <param name="collision"></param>
    /// Object that colloded with bullet
    public virtual void onHit(Collision collision)
    {
        //Apply all the effects that happen on hit
        if (onHitEvent != null)
        {
            onHitEvent?.Invoke(this);
        }
        StopAllCoroutines();
    }
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetBaseStats(baseStats);
    }

    public virtual void ShootBullet()
    {
        Vector2 aimPosition = target;
        Vector2 direction = (aimPosition - (Vector2)transform.position).normalized;

        this.transform.position = spawnPos;
        this.transform.gameObject.SetActive(true);

        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.AddForce(direction * currentSpeed, ForceMode.Impulse);
        fired = true;
        onFire();
        StartCoroutine(EndAfterLifetime(currentlifetime));
    }

    IEnumerator EndAfterLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
        StopAllCoroutines();
    }

}
