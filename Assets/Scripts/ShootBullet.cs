using System.Collections;
using UnityEngine;

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

    public void onShoot()
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

    IEnumerator InputCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
