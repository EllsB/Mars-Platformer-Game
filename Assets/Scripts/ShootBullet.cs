using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class ShootBullet : MonoBehaviour
{
    [field: SerializeField] protected AimingReticle aimingReticle;
    [field: SerializeField] public float shootCooldown { get; protected set; } = 1f;

    public ObjectPool bulletPool;
    private bool canShoot = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (canShoot)
        {
            GameObject bulletObj = bulletPool.GetPooledObject();
            PlayerBullet bullet = bulletObj.GetComponent<PlayerBullet>();
            if (bullet != null && aimingReticle.gameObject.activeSelf)
            {
                bullet.aimingReticle = aimingReticle;
                bullet.ShootBullet();
                StartCoroutine(InputCooldown(shootCooldown));
                canShoot = false;
            }
        }
    }

    public void OnSpell1(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (context.interaction is SlowTapInteraction)
                {
                    //Hold
                    Debug.Log("Hold Started");
                }
                break;
            case InputActionPhase.Performed:
                if (context.interaction is SlowTapInteraction)
                {
                    Debug.Log($"context.duration {context.duration}");
                    Debug.Log("Hold Release");
                    SpellData newSpell = new SpellData();
                    newSpell.ID = SpellManager.Instance.equippedSpells[0].ID;
                    newSpell.mult = SpellManager.Instance.equippedSpells[0].mult + (float)context.duration;
                    SpellManager.Instance.currentSpellEffects.Add(newSpell);
                    Debug.Log($"New Spell:\n ID {newSpell.ID} Mult: {newSpell.mult}");
                    //HoldRelease
                }
                else
                {
                    Debug.Log("Tap");
                    //Tap
                    SpellManager.Instance.currentSpellEffects.Add(SpellManager.Instance.equippedSpells[0]);
                }
                break;
            case InputActionPhase.Canceled:
                //HoldRelease
                break;
        }
    }

    IEnumerator InputCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
