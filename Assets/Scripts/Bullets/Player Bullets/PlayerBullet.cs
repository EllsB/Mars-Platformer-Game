using UnityEngine;

public class PlayerBullet : Bullet
{
    [field: SerializeField] protected AimingReticle aimingReticle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void onFire()
    {
        base.onFire();
        spawnPos = aimingReticle.transform.position;
        target = aimingReticle.transform.position;
        ShootBullet();
    }


}
