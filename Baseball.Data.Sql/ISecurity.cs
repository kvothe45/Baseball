namespace Baseball.Data.Sql
{
    public interface ISecurity
    {

        Person Authenticate(string username, string password);

    }
}
