namespace Panda.Services
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<string> GetAllUserNames();
    }
}
