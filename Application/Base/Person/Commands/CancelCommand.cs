

using Dtos.Person;
using MediatR;
using Services.Base.Persons.Commands;

namespace Application.Base.Person.Commands
{

    public class CancelSessionCommand : IRequest<PersonCancelDto>
    {
     
        public PersonCancelDto PersonCancelDto { get; set; }


        public class CancelSessionCommandHandler : IRequestHandler<CancelSessionCommand, PersonCancelDto>
        {
            #region Private Fields
            private readonly IPersonCommandService _personCommandService;
            #endregion

            public CancelSessionCommandHandler(IPersonCommandService personCommandService)
            {
                _personCommandService = personCommandService;
            }
            public async Task<PersonCancelDto> Handle(CancelSessionCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _personCommandService.CancelPerson(request.PersonCancelDto, cancellationToken: cancellationToken);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
