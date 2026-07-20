using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions.Must;
using UnityEditor.ShaderGraph.Internal;

public class PlayerBullet : Bullet
{
    [field: HideInInspector] public AimingReticle aimingReticle;

    [field: SerializeField] public ExplosionStats explosionStats { get; protected set; }

    public SpellData[] spellData;

    public override void ResetBaseStats(BulletStats stats)
    {
        base.ResetBaseStats(stats);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(this.gameObject.activeSelf)
        {
            ApplyUpdateEffects();
        }
    }

    public void ApplyUpdateEffects()
    {
        foreach (SpellData spell in spellData)
        {
            ApplyUpdateEffects(spell.ID, spell.mult);
        }
    }

    public void ApplyUpdateEffects(SpellData.SpellID effectID, float mult)
    {
        switch (effectID)
        {
            default:
                break;
        }
    }

    public void ApplyOnFireEffect()
    {
        Debug.Log($"Looking at Spells");
        Debug.Log($" Bullet Spell Length :{spellData.Length}");
        foreach (SpellData spell in spellData)
        {
            Debug.Log($"Spell: {spell.ID}");
            ApplyOnFireEffect(spell.ID, spell.mult);
        }
    }

    public void ApplyOnHitEffects()
    {
        foreach (SpellData spell in spellData)
        {
            ApplyHitEffects(spell.ID, spell.mult);
        }
    }

    public void ApplyOnFireEffect(SpellData.SpellID effectID, float mult)
    {
        switch (effectID)
        {
            case SpellData.SpellID.Explosion:
                Debug.Log("Spell Has Explosion");
                float explosionMult = mult * explosionStats.explosionForce;
                float radiusMult = mult * explosionStats.radius;
                explosionStats.CreateExplosion(this.transform.position, explosionStats.lifetime, explosionMult, radiusMult);
                //Exploding
                break;
            default:
                break;
        }
    }
    
    public void ApplyHitEffects(SpellData.SpellID effectID, float mult)
    {
        switch (effectID)
        {
            case SpellData.SpellID.Explosion:
                float explosionMult = mult * explosionStats.explosionForce;
                float radiusMult = mult * explosionStats.radius;
                explosionStats.CreateExplosion(this.transform.position, explosionStats.lifetime, explosionMult, radiusMult);
                //Exploding
                break;
            default:
                break;
        }
    }


    public override void ShootBullet()
    {
        spawnPos = aimingReticle.transform.position;
        target = aimingReticle.transform.position;
        base.ShootBullet();
    }

    public override void onFire()
    {
        Debug.Log("On Fire");
        base.onFire();
        ApplyOnFireEffect();
    }

    public void ExplodingEffect()
    {
        //On hit create a "knockback wave"
    }
}
