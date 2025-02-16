using ChatMePls.DefaultServices.CQRS;
using ChatMePls.Lobby.Api.Enums;
using Marten;

namespace ChatMePls.Lobby.Api.House.CreateHouse;

public record CreateHouseCommand(
    string Title,
    string Description,
    IList<Guid> Admins,
    PrivacyLevel ReadingPrivacyLevel,
    PrivacyLevel PostingPrivacyLevel) : ICommand<CreateHouseResult>;

public record CreateHouseResult(Guid HouseUid);

public class CreateHouseHandler(IDocumentSession documentSession) : ICommandHandler<CreateHouseCommand, CreateHouseResult>
{
    public async Task<CreateHouseResult> Handle(CreateHouseCommand request, CancellationToken cancellationToken)
    {
        var model = new Models.House()
        {
            Uid = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Admins = request.Admins,
            ReadingLevel = request.ReadingPrivacyLevel,
            PostingLevel = request.PostingPrivacyLevel
        };
        
        documentSession.Store(model);
        await documentSession.SaveChangesAsync(cancellationToken);
        
        return  new CreateHouseResult(model.Uid);
    }
}