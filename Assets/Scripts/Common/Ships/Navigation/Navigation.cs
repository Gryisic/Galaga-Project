using System;
using Infrastructure.Interfaces;
using Infrastructure.Utils;

namespace Common.Ships.Navigation
{
    public class Navigation : IDisposable
    {
        public event Func<int, NavigationDataProvider> RequestNavigationProvider;
        public event Action<IMovable> Completed; 

        private readonly IMovable _movable;
        
        private INavigationStrategy _navigationStrategy;
        private int _navigationIndex;

        public Navigation(IMovable movable)
        {
            _movable = movable;
        }
        
        public void Dispose()
        {
            if (_navigationStrategy != null)
                _navigationStrategy.NavigationCompleted -= ChangeNavigationStrategy;
            
            _navigationStrategy?.Dispose();
        }

        public void Start()
        {
            ChangeNavigationStrategy();
            
            _movable.SetPosition(_navigationStrategy.InitialPosition);
            _navigationStrategy.StartMoving();
        }

        public void Stop()
        {
            _navigationStrategy.StopMoving();
            _navigationStrategy.NavigationCompleted -= ChangeNavigationStrategy;
        }
        
        private void ChangeNavigationStrategy()
        {
            NavigationDataProvider dataProvider = RequestNavigationProvider?.Invoke(_navigationIndex);

            if (dataProvider == null)
            {
                Completed?.Invoke(_movable);
                
                return;
            }
            
            if (_navigationStrategy != null)
            {
                _navigationStrategy.NavigationCompleted -= ChangeNavigationStrategy;
                _navigationStrategy.Dispose();
            }
            
            _navigationStrategy = DefineNavigationStrategy(dataProvider);
            _navigationStrategy.NavigationCompleted += ChangeNavigationStrategy;
            _navigationStrategy.StartMoving();
            
            _navigationIndex++;
        }

        private INavigationStrategy DefineNavigationStrategy(NavigationDataProvider dataProvider)
        {
            Enums.NavigationType type = dataProvider.Type;
            
            switch (type)
            {
                case Enums.NavigationType.AlongsideWithSpline:
                    return new AlongsideWithSpline(dataProvider.Spline, _movable);
            }

            return null;
        }
    }
}