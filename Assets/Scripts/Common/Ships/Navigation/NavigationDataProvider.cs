using System;
using Common.Splines;
using Infrastructure.Utils;
using UnityEngine;

namespace Common.Ships.Navigation
{
    [Serializable]
    public class NavigationDataProvider
    {
        [SerializeField] private Enums.NavigationType _type;

        [SerializeField] private Spline _spline;

        public Enums.NavigationType Type => _type;
        
        public Spline Spline => _spline;
    }
}