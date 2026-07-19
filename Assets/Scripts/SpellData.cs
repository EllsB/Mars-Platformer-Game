using System;
using UnityEngine;

[Serializable]
public class SpellData
{
    public SpellID ID = 0;
    public float mult = 1;

    public enum SpellID
    {
        NULL,
        Explosion
    }
}
