using System;
using Infrastructure.Interfaces;

namespace Infrastructure.Extensions
{
    public static class GameModeExtensions
    {
        public static void Deactivate(this IGameMode mode)
        {
            if (mode is IDeactivatable deactivatable)
                deactivatable.Deactivate();
        }

        public static void Reset(this IGameMode mode)
        {
            if (mode is IResetable resetable)
                resetable.Reset();
        }

        public static void Dispose(this IGameMode mode)
        {
            if (mode is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
