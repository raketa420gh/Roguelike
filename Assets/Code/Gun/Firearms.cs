using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Firearms : MonoBehaviour, IFirearms
{
    [SerializeField] private Transform _muzzleTransform;
    private float _shootingSpeed = 0.5f;
    private IFactory _factory;

    [Inject]
    public void Construct(IFactory factory) => 
        _factory = factory;

    public async Task StartShooting(ITargetable target)
    {
        while (true)
        {
            Shoot(target);
            await UniTask.Delay(TimeSpan.FromSeconds(_shootingSpeed));
        }
    }
    
    private void Shoot(ITargetable target)
    {
        IProjectile projectile = _factory.CreateShell(_muzzleTransform.position);
        projectile.SetDirection(target.GetDirectionRelativeTo(_muzzleTransform));
        Debug.Log("SHOOT");
    }
}