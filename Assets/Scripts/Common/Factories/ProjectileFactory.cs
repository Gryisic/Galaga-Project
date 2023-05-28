using System;
using Common.Projectiles;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Factories
{
    public class ProjectileFactory : IFactory<Projectile>
    {
        private Projectile _prefab;

        public void Load() => _prefab = Resources.Load<Projectile>(Constants.PathToProjectilePrefab);

        public void Create(Vector2 at, Transform parent, out Projectile projectile)
        {
            if (_prefab == null)
                throw new NullReferenceException("Projectile prefab isn't loaded");

            _prefab.enabled = false;

            projectile = GameObject.Instantiate(_prefab, at, Quaternion.identity, parent);

            _prefab.enabled = true;
        }
    }
}