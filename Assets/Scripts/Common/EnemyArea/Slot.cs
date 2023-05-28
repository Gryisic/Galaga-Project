using System;
using Common.Ships;
using UnityEngine;

namespace Common.EnemyArea
{
    public class Slot
    {
        public bool IsAvailable => ShipInSlot == null;
        public Ship ShipInSlot { get; private set; }
        public Vector2 Position { get; }
        
        public Slot(Vector2 position)
        {
            Position = position;
        }

        public void AddShip(Ship ship)
        {
            if (IsAvailable == false)
                throw new InvalidOperationException("Trying to add a ship in occupied slot");

            ShipInSlot = ship;
        }

        public void RemoveShip() => ShipInSlot = null;
    }
}