using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Scriptable Objects/BulletStats")]
public class BulletStats : ScriptableObject
{
    [field: SerializeField] public float speed { get; protected set; }
    [field: SerializeField] public float damage { get; protected set; }
    [field: SerializeField] public float lifetime { get; protected set; }
    [field: SerializeField] public float gravity { get; protected set; }
    [field: SerializeField] public Sprite sprite { get; protected set; }
}
