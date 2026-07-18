using UnityEngine;

public class PlayerBullet : Bullet
{
    [field: HideInInspector] public AimingReticle aimingReticle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ShootBullet()
    {
        spawnPos = aimingReticle.transform.position;
        target = aimingReticle.transform.position;
        base.ShootBullet();
    }

    public override void onFire()
    {
        base.onFire();

    }


}
