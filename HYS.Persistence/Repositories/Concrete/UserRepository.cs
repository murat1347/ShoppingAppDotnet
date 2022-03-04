using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Domain.Entities;

namespace HYS.Persistence.Repositories.Concrete
{
    internal class UserRepository:GenericRepository<User>
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
