using System;
using Core.GameModes;

namespace Infrastructure.Interfaces
{
    public interface IGameMode
    {
        public event Action<GameModeArgs> Finished;

        public void Activate(GameModeArgs args);
    }
}