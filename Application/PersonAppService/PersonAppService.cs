using ApplicationLayer.Base;
using ApplicationLayer.Base.Models;
using ApplicationLayer.PersonAppService.DTOs;
using ApplicationLayer.PersonAppService.Interfaces;
using ApplicationLayer.Resources;
using AutoMapper;
using DomainLayer.Entities.PersonModel;
using DomainLayer.Entities.PersonModel.Commands;
using PersistenceLayer.Base.NoSQLs.UnitOfWork.Interfaces;
using Support.ExceptionsManagement.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationLayer.PersonAppService
{
    public class PersonAppService : ApplicationServiceBase, IPersonAppService
    {
        public PersonAppService(IMongoUnitOfWorkFactory uowFactory, IMapper mapper)
            : base(uowFactory, mapper)
        {
        }

        public async Task<SuccessWithDataResponse<IEnumerable<PersonGetListResponse>>> GetListAsync()
        {
            using var uow = UowFactory.Create();
            var people = await uow.GetRepository<Person>().GetAllAsync().ConfigureAwait(false);

            return new SuccessWithDataResponse<IEnumerable<PersonGetListResponse>>(Mapper.Map<IEnumerable<PersonGetListResponse>>(people), ApplicationMessages.Success);
        }

        public async Task<SuccessWithDataResponse<PersonGetAsyncResponse>> GetAsync(string id)
        {
            using var uow = UowFactory.Create();
            var person = await uow.GetRepository<Person>().GetAsync(id).ConfigureAwait(false);

            if (person == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Person"));

            return new SuccessWithDataResponse<PersonGetAsyncResponse>(Mapper.Map<PersonGetAsyncResponse>(person), ApplicationMessages.Success);
        }

        public async Task<SuccessWithDataResponse<PersonGetAsyncResponse>> InsertAsync(PersonInsertRequest personInsertRequest)
        {
            using var uow = UowFactory.Create();
            var personDomainCreateUpdateCommand = Mapper.Map<PersonDomainCreateUpdateCommand>(personInsertRequest);

            var person = personDomainCreateUpdateCommand.Create();

            await uow.GetRepository<Person>().InsertAsync(person).ConfigureAwait(false);

            await uow.CommitAsync().ConfigureAwait(false);

            var result = await GetAsync(person.Id).ConfigureAwait(false);

            result.Message = ApplicationMessages.Success;

            return result;
        }

        public async Task<SuccessWithDataResponse<PersonGetAsyncResponse>> UpdateAsync(string id, PersonUpdateRequest personUpdateRequest)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (personUpdateRequest == null)
                throw new ArgumentNullException(nameof(personUpdateRequest));

            using var uow = UowFactory.Create();
            var person = await uow.GetRepository<Person>().GetAsync(id).ConfigureAwait(false);

            if (person == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Person"));

            var personDomainCreateUpdateCommand = Mapper.Map<PersonDomainCreateUpdateCommand>(personUpdateRequest);

            person.Update(personDomainCreateUpdateCommand);

            await uow.GetRepository<Person>().UpdateAsync(person).ConfigureAwait(false);

            await uow.CommitAsync().ConfigureAwait(false);

            var result = await GetAsync(person.Id).ConfigureAwait(false);

            result.Message = ApplicationMessages.Success;

            return result;
        }

        public async Task<SuccessResponse> DeleteAsync(string id)
        {
            using var uow = UowFactory.Create();
            var person = await uow.GetRepository<Person>().GetAsync(id).ConfigureAwait(false);

            if (person == null)
                throw new ApplicationLayerException(string.Format(ApplicationMessages.NotFound, "Person"));

            await uow.GetRepository<Person>().DeleteAsync(person).ConfigureAwait(false);
            await uow.CommitAsync().ConfigureAwait(false);

            person = await uow.GetRepository<Person>().GetAsync(id).ConfigureAwait(false);

            return new SuccessResponse { Message = ApplicationMessages.Success };
        }
    }
}
