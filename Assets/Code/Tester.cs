using System;
using UnityEngine;
using Zenject;

public class Tester : MonoBehaviour
{
    private IFactory _factory;
    private IInputService _input;
    
    
    [Inject]
    public void Construct(IFactory factory, IInputService input)
    {
        _factory = factory;
        _input = input;
    }

    private void Start()
    {
        var startCharacterPosition = new Vector3(0, 1, -5);
        _factory.CreateHero(startCharacterPosition);
    }
}