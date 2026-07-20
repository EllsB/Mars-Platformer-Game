using System.Linq;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "ExplosionStats", menuName = "Scriptable Objects/ExplosionStats")]
public class ExplosionStats : ScriptableObject
{
    public float explosionForce = 10f;
    public float radius = 1f;
    public float lifetime = 1f;
    public GameObject explosionObj;

    public string[] ignoreTag;


    public void CreateExplosion(Vector3 spawnPos)
    {
        CreateExplosion(spawnPos, lifetime, explosionForce, radius);
    }

    public void CreateExplosion(Vector3 spawnPos, float lifetime, float explosionForce, float radius)
    {
        GameObject newExplosion = Instantiate(explosionObj, spawnPos, Quaternion.identity);
        //newExplosion.transform.localScale = explosionObj.transform.localScale * radius;
        //Destroy(newExplosion, lifetime);
        KnockBack(spawnPos, explosionForce, radius);
    }

    public void KnockBack(Vector3 spawnPos)
    {
        this.KnockBack(spawnPos, explosionForce, radius);
    }

    public void KnockBack(Vector3 spawnPos, float explosionForce, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnPos, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && !ignoreTag.Contains<string>(hit.gameObject.tag))
            {
                rb.AddExplosionForce(explosionForce, spawnPos, radius);
            }
        }
    }
}
