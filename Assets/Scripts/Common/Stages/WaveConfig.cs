using System.Collections.Generic;
using Common.Ships.Navigation;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Stages
{
    [CreateAssetMenu(menuName = "Gameplay / Configs / Wave", fileName = "Wave")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private List<NavigationDataProvider> _navigationProviders;
        [SerializeField] private List<Enums.ShipType> _ships;
        [SerializeField] private int _shipsAmount;
        [SerializeField] private Enums.NextWaveConditionType _conditionType;

        [Space] [SerializeField] private float _delay;
        
        public List<NavigationDataProvider> NavigationProviders => _navigationProviders;
        public List<Enums.ShipType> Ships => _ships;
        public int ShipsAmount => _shipsAmount;
        public Enums.NextWaveConditionType ConditionType => _conditionType;
        
        public float Delay => _delay;
    }
}