using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Bullet : MonoBehaviour
{
    public static event Action<Bullet> onFireEvent;
    public static event Action<Bullet> onHitEvent;

    public BulletStats baseStats;
    public float currentSpeed = 0;
    public float currentDamage = 0;
    public float currentlifetime = 0;
    public float currentGravity = 0;
    public Sprite currentSprite;

    protected float lifetimeRemaining = 0;
    
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
        if (onFireEvent != null)
        {
            onFireEvent?.Invoke(this);
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
        if (onHitEvent != null)
        {
            onHitEvent?.Invoke(this);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetBaseStats(baseStats);
        lifetimeRemaining = currentlifetime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
