using MediatR;

namespace ChatMePls.DefaultServices.CQRS;


public interface ICommand : ICommand<Unit>
{
    
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}