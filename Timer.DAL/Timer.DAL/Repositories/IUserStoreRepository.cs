﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Timer.DAL.Repositories
{
    interface IUserStoreRepository : IUserEmailStore<User, int>, IRepository<User>, IQueryableUserStore<User, int>, IUserPasswordStore<User, int>
    {

    }
}
