
using Common.Enums;
using Common.Exceptions;
using Domain.Entity;
using Infrastructure.Base.Command.Interface;
using Infrastructure.Base.Query;
using AutoMapper;
using Dtos.General;

namespace Application.Services.General.Command
{
    public abstract class BaseCommandService<TModel> : IBaseCommandService<TModel> where TModel : class, IBaseEntity
    {
        #region Private Fields
        protected readonly IBaseCommandRepository<TModel> _commandRepository;
        protected readonly IBaseQueryRepository<TModel> _queryRepository;
        protected readonly IMapper _mapper;
      

        #endregion


        public BaseCommandService()
        {

        }
        public BaseCommandService(IBaseCommandRepository<TModel> commandRepository, IBaseQueryRepository<TModel> queryRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;

        }

        public BaseCommandService(IBaseCommandRepository<TModel> commandRepository, IBaseQueryRepository<TModel> queryRepository, IMapper mapper, IServiceProvider injector) : this(commandRepository, queryRepository, mapper)
        {

        }


        /// <summary>
        /// درج داده در دیتابیس همراه با عملیات ذخیره سازی
        /// نکته : در صورت نیاز به ذخیره چندین مدل در دیتابیس نیاز به پیاده سازی مجدد متد های پایه ای را دارید
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async virtual Task<T> AddAsync<T>(T model, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDto
        {
            try
            {

                UiValidationException validationExceptions = new UiValidationException(ResultType.Error);

                //map Dto to entity
                var _resultMap = _mapper.Map<TModel>(model);

                var _resultBeforeAdd = await BeforeAdd(_resultMap, cancellationToken);
                if (_resultBeforeAdd)
                {
                    var _resultAdd = await _commandRepository.AddAsync(_resultMap, cancellationToken, isSave);

                    model.Id = _resultMap.Id;
                    if (_resultAdd)
                    {
                        await AfterAdd();
                    }

                    return model;
                }
                else
                {
                    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.BeforeAddOperationNotComplate);
                }

                if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
                    throw validationExceptions;
                return null;

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ویرایش داده در دیتابیس همراه با عملیات ذخیره سازی
        /// نکته : در صورت نیاز به ذخیره چندین مدل در دیتابیس نیاز به پیاده سازی مجدد متد های پایه ای را دارید
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async virtual Task<T> EditAsync<T>(T model, bool isSave = true, bool ignoreFilter = true, CancellationToken cancellationToken = default) where T : BaseDto
        {
            try
            {
                UiValidationException validationExceptions = new UiValidationException(ResultType.Error);

                var entityResult = await _queryRepository.FindByIdAsync(model.Id, ignoreFilter, cancellationToken);

                if (entityResult != null)
                {
                    _mapper.Map(model, entityResult);

                    var _resultBeforeEdit = await BeforeEdit(entityResult, cancellationToken);
                    if (_resultBeforeEdit)
                    {
                        var _resultEdit = await _commandRepository.EditAsync(entityResult, cancellationToken, isSave);
                        if (_resultEdit)
                        {
                            await AfterEdit();
                        }
                        return model;
                    }
                    else
                    {
                        validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.BeforeEditOperationNotComplate);
                    }
                }
                else
                {
                    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.NotFound);
                }




                if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
                    throw validationExceptions;

                return null!;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// حذف داده در دیتابیس همراه با عملیات ذخیره سازی
        /// نکته : به صورت پیش فرض ذخیره سازی در دیتابیس انجام میشود در صورت نیاز پارامتر پیش فرض را تغییر دهید 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async virtual Task<bool> DeleteAsync(Guid id, bool isSave = true, bool ignoreFilter = true, CancellationToken cancellationToken = default)
        {
            try
            {
                UiValidationException validationExceptions = new UiValidationException(ResultType.Error);
                //عملیات پیش از حذف
                var _beforResult = await BeforeDelete(id, cancellationToken);
                if (_beforResult)
                {
                    var findResult = await _queryRepository.FindByIdAsync(id, ignoreFilter, cancellationToken: cancellationToken);
                    if (findResult != null)
                    {
                        return await _commandRepository.DeleteAsync(findResult, cancellationToken, isSave);
                    }
                    else
                    {
                        validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.NotFound);
                    }
                }
                else
                {
                    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.BeforeDeleteOperationNotComplate);
                }

                if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
                    throw validationExceptions;
                return true;

            }
            catch
            {
                throw;
            }



        }


        /// <summary>
        /// درج داده ها به صورت لیست در دیتابیس همراه با عملیات ذخیره سازی
        /// نکته : به صورت پیش فرض ذخیره سازی در دیتابیس انجام میشود در صورت نیاز پارامتر پیش فرض را تغییر دهید
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>


        public async Task<bool> AddRange<T>(IList<T> models, bool isSave = true, CancellationToken cancellationToken = default)
        {
            try
            {

                UiValidationException validationExceptions = new UiValidationException(ResultType.Error);
                //map Dto to entity
                var _resultMap = _mapper.Map<IList<TModel>>(models);
                var _resultBeforeAdd = await BeforeAddRange(_resultMap, cancellationToken);
                if (_resultBeforeAdd)
                {
                    var _resultAdd = await _commandRepository.AddRange(_resultMap, cancellationToken, isSave);
                    return _resultAdd;
                }
                else
                {
                    validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.BeforeAddRangeOperationNotComplate);
                }
                if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
                    throw validationExceptions;

                return true;
            }
            catch
            {
                throw;
            }
        }


      


        public async virtual Task<bool> BeforeAdd(TModel model, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);
        }
        public async virtual Task<bool> BeforeEdit(TModel model, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);
        }

        public async virtual Task<bool> BeforeDelete(Guid id, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);


        }



        public async virtual Task<bool> BeforeAddRange(IList<TModel> model, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);
        }

        public async virtual Task<bool> BeforeDeleteRange(IList<TModel> models, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);
        }

        public async virtual Task<bool> BeforeUpdateRange(IList<TModel> models, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);
        }


        public async virtual Task<bool> AfterAdd()
        {
            return await Task.FromResult(true);
        }

        public async virtual Task<bool> AfterEdit()
        {
            return await Task.FromResult(true);
        }
        public async virtual Task<bool> AfterDelete()
        {
            return await Task.FromResult(true);
        }


        public async virtual Task<bool> BeforeAdd(List<TModel> model, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);
        }


        public async virtual Task<bool> BeforeAddBulk(IEnumerable<TModel> model, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(true);
        }




    }

}
