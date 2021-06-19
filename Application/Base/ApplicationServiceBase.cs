using AutoMapper;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;

namespace ApplicationLayer.Base
{
    public abstract class ApplicationServiceBase
    {
        #region Construtores

        protected ApplicationServiceBase(IMongoUnitOfWorkFactory uowFactory, IMapper mapper)
        {
            UowFactory = uowFactory;
            Mapper = mapper;
        }

        #endregion

        #region Atributos

        protected IMongoUnitOfWorkFactory UowFactory { get; }
        protected IMapper Mapper { get; }

        #endregion
    }
}
