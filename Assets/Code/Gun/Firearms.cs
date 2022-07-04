using System;
using System.Threading;
using System.Threading.Tasks;
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

    public async Task StartShooting(ITargetable target, CancellationToken cancellationToken)
    {
        _isShooting = true;

        while (_isShooting)
        {
            await Task.Delay(TimeSpan.FromSeconds(_shootingSpeed), cancellationToken);
            Shoot(target);
        }
    }

    public void StopShooting(CancellationTokenSource cancellationTokenSource)
    {
        cancellationTokenSource.Cancel();
        _isShooting = false;
    }

    private void Shoot(ITargetable target)
    {
        IProjectile projectile = _factory.CreateShell(_muzzleTransform.position);
        var targetTransform = target.GetTransform();
        projectile.SetTarget(targetTransform);
    }
}