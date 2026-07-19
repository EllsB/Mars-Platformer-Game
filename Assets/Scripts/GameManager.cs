using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get { return s_instance; }
    }
    protected static GameManager s_instance;

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (Instance != this)
        {
            throw new UnityException($"There cannot be more than one {this.GetType().Name} script. The instances are {this.name} and {Instance.name}.");
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
}
