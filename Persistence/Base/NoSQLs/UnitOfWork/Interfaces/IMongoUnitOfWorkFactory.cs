namespace PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces
{
    public interface IMongoUnitOfWorkFactory
    {
        IMongoUnitOfWork Create();
    }
}
