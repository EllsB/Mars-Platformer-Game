using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public SpellData[] equippedSpells = new SpellData[] {new SpellData(), new SpellData(), new SpellData() };
    public static SpellManager Instance
    {
        get { return s_instance; }
    }
    protected static SpellManager s_instance;

    [field: HideInInspector] public List<SpellData> currentSpellEffects;

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (Instance != this)
        {
            throw new UnityException($"There cannot be more than one {this.GetType().Name} script.  The instances are {this.name} and {Instance.name}.");
        }
    }
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpellEffects = new List<SpellData>();
        equippedSpells[0].ID = SpellData.SpellID.Explosion;
    }
}
