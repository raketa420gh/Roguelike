using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Firearms : MonoBehaviour, IFirearms
{
    [SerializeField] private Transform _muzzleTransform;
    private float _shootingSpeed = 0.5f;
    private bool _isShooting;
    private IFactory _factory;

    [Inject]
    public void Construct(IFactory factory) => 
        _factory = factory;

    public async Task StartShooting(ITargetable target)
    {
        _isShooting = true;
        
        while (_isShooting)
        {
            Shoot(target);
            await UniTask.Delay(TimeSpan.FromSeconds(_shootingSpeed));
        }
    }

    public void StopShooting() => 
        _isShooting = false;

    private void Shoot(ITargetable target)
    {
        IProjectile projectile = _factory.CreateShell(_muzzleTransform.position);
        projectile.SetTarget(target.GetTransform());
    }
}