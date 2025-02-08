using MediatR;

namespace ChatMePls.DefaultServices.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
    
}