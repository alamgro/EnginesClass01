
namespace Collectables
{
	public interface ICollectable<T>
	{
		public T Value { get; }
		public void Collect();
	}

}
