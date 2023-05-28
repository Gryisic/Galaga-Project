using System;
using Common.Ships;
using Infrastructure.Interfaces;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Factories
{
    public class ShipsFactory : IFactory<Ship>
    {
        private Ship _prefab;
        
        public void Load() => _prefab = Resources.Load<Ship>(Constants.PathToShipPrefab);

        public void Create(Vector2 at, Transform parent, out Ship ship)
        {
            if (_prefab == null)
                throw new NullReferenceException("Ship prefab isn't loaded");

            _prefab.enabled = false;

            ship = GameObject.Instantiate(_prefab, at, Quaternion.identity, parent);

            _prefab.enabled = true;
        }
    }
}