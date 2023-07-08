
namespace GMTK_2023.Behaviours
{
    public interface ISpawnable
    {
        public bool IsAlive { get; }

        public void Spawn();

        public void Kill();
    }
}
