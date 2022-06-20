using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Door : MonoBehaviour, IDoor
{
    public event Action OnHeroTriggerEnter;
    
    [SerializeField] private GameObject _doorLeaf;
    private bool isOpened;

    private void OnTriggerEnter(Collider other)
    {
        var hero = other.GetComponent<Hero>();

        if (hero)
        {
            Close();
            OnHeroTriggerEnter?.Invoke();
        }
    }

    public void Open()
    {
        if (!isOpened)
        {
            _doorLeaf.gameObject.SetActive(false);
            isOpened = true;
        }
    }

    public void Close()
    {
        if (isOpened)
        {
            _doorLeaf.gameObject.SetActive(true);
            isOpened = false;
        }
    }
}