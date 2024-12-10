namespace PanelWay_Backend.Repository.Interfaces
{
	public interface IGenericRepositoryFactory
	{
		IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
	}
}
