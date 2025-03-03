using System.ComponentModel.DataAnnotations;
using ChatMePls.DefaultServices.CQRS;
using MediatR;

namespace ChatMePls.User.Api.Authentication.UpdatePersonalData;


public record UpdatePersonalDataCommand(
    [Required] string Name,
    [Required] string Surname,
    [Required] string Gender,
    DateTime DateOfBirth
) : ICommand;

public class UpdatePersonalDataHandler : ICommandHandler<UpdatePersonalDataCommand>
{
    public Task<Unit> Handle(UpdatePersonalDataCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}