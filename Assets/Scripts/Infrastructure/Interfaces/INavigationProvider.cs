using Common.Ships.Navigation;

namespace Infrastructure.Interfaces
{
    public interface INavigationProvider
    {
        Navigation Navigation { get; }
    }
}