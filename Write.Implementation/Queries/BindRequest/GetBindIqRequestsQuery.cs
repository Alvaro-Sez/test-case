using Write.Contacts.Queries;

namespace Write.Implementation.Queries.BindRequest;

public class GetBindIqRequestsQuery 
{
    public GetBindIqRequestsQuery(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}