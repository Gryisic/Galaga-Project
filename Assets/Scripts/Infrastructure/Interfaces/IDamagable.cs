using System;

namespace Infrastructure.Interfaces
{
    public interface IDamagable
    {
        event Action DamageTaken;
        
        void ApplyDamage(IDamageDealer dealer);
    }
}