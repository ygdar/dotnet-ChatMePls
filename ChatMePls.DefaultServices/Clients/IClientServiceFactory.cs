namespace ChatMePls.DefaultServices.Clients;

public interface IClientServiceFactory<out T>
    where T: Grpc.Core.ClientBase<T>
{
    T Create();
}