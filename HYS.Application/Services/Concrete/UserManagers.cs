using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Application.Services.Interfaces;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Interfaces;

namespace HYS.Application.Services.Concrete
{
    public class UserManagers:GenericManager<User>,IUserService
    {
        private readonly IDbConnection _dbConnection;
        public UserManagers(IGenericRepository<User> genericService, IDbConnection dbConnection) : base(genericService)
        {
            _dbConnection = dbConnection;
        }
    }
}
