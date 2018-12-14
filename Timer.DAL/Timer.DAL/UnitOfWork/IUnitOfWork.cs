﻿using Microsoft.AspNet.Identity;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;

namespace Timer.DAL.Timer.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        UserManager<User,int> UserManager { get; set; }
        IRepository<Role> Roles { get; set; }

        void Save();
    }
}