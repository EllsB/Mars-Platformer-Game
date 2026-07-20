using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class ShootBullet : MonoBehaviour
{
    [field: SerializeField] protected AimingReticle aimingReticle;
    [field: SerializeField] public float shootCooldown { get; protected set; } = 1f;

    public ObjectPool bulletPool;
    private bool canShoot = true;

    protected int castingID = 10;

    protected Vector2 castingInput;


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
                Debug.Log($"Start Spell Data Length = {SpellManager.Instance.currentSpellEffects.Count}");
                List<SpellData> data = SpellManager.Instance.currentSpellEffects;
                bullet.spellData = data.ToArray();
                SpellManager.Instance.currentSpellEffects.Clear();
                Debug.Log($"Spell Data Length = {data.Count}");
                bullet.aimingReticle = aimingReticle;
                bullet.ShootBullet();
                StartCoroutine(InputCooldown(shootCooldown));
                canShoot = false;
            }
        }
    }

    protected void CastSpell(InputAction.CallbackContext context, int equippedSpell)
    {
        

    }

    protected int SetID(InputAction.CallbackContext context, Vector2 input)
    {
        int equippedID = -1;
        switch (input)
        {
            case Vector2 v when v.Equals(Vector2.up):
                equippedID = 0;
                break;
            case Vector2 v when v.Equals(Vector2.down):
                equippedID = 1;
                break;
            case Vector2 v when v.Equals(Vector2.left):
                equippedID = 2;
                break;
            case Vector2 v when v.Equals(Vector2.right):
                equippedID = 3;
                break;
            default:
                break;
        }
        return equippedID;
    }

    public void OnSpell1(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            castingInput = input;
        }

        switch (context.phase)
        {
            case InputActionPhase.Started:
            if (context.interaction is SlowTapInteraction)
            {
                //Hold
                Debug.Log("Hold Started");
                castingID = SetID(context, castingInput);
            }
            break;
        case InputActionPhase.Performed:
            if (context.interaction is SlowTapInteraction)
            {
                Debug.Log($"Casting ID {castingID}");
                Debug.Log($"context.duration {context.duration}");
                Debug.Log("Hold Release");
                SpellData newSpell = new SpellData();
                newSpell.ID = SpellManager.Instance.equippedSpells[castingID].ID;
                newSpell.mult = SpellManager.Instance.equippedSpells[castingID].mult + (float)context.duration;
                SpellManager.Instance.currentSpellEffects.Add(newSpell);
                Debug.Log($"New Spell:\n ID {newSpell.ID} Mult: {newSpell.mult}");
                //HoldRelease
            }
            else
            {
                Debug.Log("Tap");
                //Tap
                castingID = SetID(context, castingInput);
                Debug.Log($"Casting ID {castingID}");
                SpellManager.Instance.currentSpellEffects.Add(SpellManager.Instance.equippedSpells[castingID]);
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
