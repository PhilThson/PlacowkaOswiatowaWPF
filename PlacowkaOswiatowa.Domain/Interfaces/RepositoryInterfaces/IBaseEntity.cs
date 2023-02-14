namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
