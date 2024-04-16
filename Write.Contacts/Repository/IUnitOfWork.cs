namespace Write.Contacts.Repository;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}