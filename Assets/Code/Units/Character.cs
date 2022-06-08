using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]

public class Character : Unit
{
    protected CharacterMovement _movement;

    private void Awake() => 
        _movement = GetComponent<CharacterMovement>();
}