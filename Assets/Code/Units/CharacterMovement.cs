using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour, ICharacterMovement
{
    private CharacterController _controller;

    private void Awake() => 
        _controller = GetComponent<CharacterController>();

    public void Move(Vector3 direction) => 
        _controller.Move(direction * Time.fixedDeltaTime);
}