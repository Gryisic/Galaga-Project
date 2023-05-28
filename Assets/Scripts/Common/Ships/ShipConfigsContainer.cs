using System;
using System.Collections.Generic;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Ships
{
    public class ShipConfigsContainer
    {
        private Dictionary<Enums.ShipType, ShipConfig> _configs = new Dictionary<Enums.ShipType, ShipConfig>();

        public void Initialize()
        {
            Enums.ShipType[] types = Enum.GetValues(typeof(Enums.ShipType)) as Enums.ShipType[];

            foreach (var shipType in types)
            {
                ShipConfig config = Resources.Load<ShipConfig>($"{Constants.PathToShipConfigs}/{shipType}");
                
                _configs.Add(shipType, config);
            }
        }
        
        public ShipConfig GetConfig(Enums.ShipType type)
        {
            if (_configs.ContainsKey(type) == false)
                throw new NullReferenceException($"Dictionary doesn't contain given key '{type}'");

            return _configs[type];
        }
    }
}