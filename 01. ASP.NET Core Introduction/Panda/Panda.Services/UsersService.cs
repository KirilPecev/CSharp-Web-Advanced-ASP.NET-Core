namespace Panda.Services
{
    using Panda.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersService : IUsersService
    {
        private readonly PandaDbContext db;

        public UsersService(PandaDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<string> GetAllUserNames()
        {
            return this.db.Users.Select(x => x.UserName).AsEnumerable<string>();
        }
    }
}
