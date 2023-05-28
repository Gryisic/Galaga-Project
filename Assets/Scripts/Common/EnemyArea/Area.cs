using System.Collections.Generic;
using System.Linq;
using Common.Ships;
using UnityEngine;

namespace Common.EnemyArea
{
    public class Area : MonoBehaviour
    {
        private readonly List<Slot> _slots = new List<Slot>()
        {
            new Slot(new Vector2(-2, 4)), new Slot(new Vector2(-0.9f, 4)), new Slot(new Vector2(0.2f, 4)), new Slot(new Vector2(1.3f, 4)),
            new Slot(new Vector2(2, 3)), new Slot(new Vector2(0.9f, 3)), new Slot(new Vector2(-0.2f, 3)), new Slot(new Vector2(-1.3f, 3)),
            new Slot(new Vector2(-2, 2)), new Slot(new Vector2(-0.9f, 2)), new Slot(new Vector2(0.2f, 2)), new Slot(new Vector2(1.3f, 2)),
        };

        public bool IsEnoughFreeSlots(int slotsAmount) => _slots.Count(s => s.IsAvailable) >= slotsAmount;

        public void OccupySlots(IReadOnlyList<Ship> ships, out IReadOnlyDictionary<Ship, Vector2> shipPositionMap)
        {
            List<Slot> freeSlots = _slots.Where(s => s.IsAvailable).ToList();
            Dictionary<Ship, Vector2> map = new Dictionary<Ship, Vector2>();

            for (int i = 0; i < ships.Count; i++)
            {
                freeSlots[i].AddShip(ships[i]);
                map.Add(ships[i], freeSlots[i].Position);
            }

            shipPositionMap = map;
        }

        public void ReleaseSlot(Ship ship) => _slots.First(s => s.ShipInSlot == ship).RemoveShip();
    }
}