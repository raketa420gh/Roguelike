using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Firearms : MonoBehaviour, IFirearms
{
    [SerializeField] private Transform _muzzleTransform;
    private float _shootingSpeed = 0.5f;
    private ITargetable _currentTarget;
    private IFactory _factory;

    [Inject]
    public void Construct(IFactory factory) => 
        _factory = factory;

    public async Task StartShooting()
    {
        while (_currentTarget != null)
        {
            Shoot(_currentTarget);
            await UniTask.Delay(TimeSpan.FromSeconds(_shootingSpeed));
        }
    }
    
    private void Shoot(ITargetable target)
    {
        IProjectile projectile = _factory.CreateShell(_muzzleTransform.position);
        projectile.SetDirection(target.GetDirectionRelativeTo(_muzzleTransform));
    }
}