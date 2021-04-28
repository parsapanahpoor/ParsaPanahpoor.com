using DataAccees.Interfaces;
using DataAccees.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.IoC
{
    public class DependencyContainer
    {

        public static void RegisterServices(IServiceCollection service)
        {

            service.AddTransient<IUserService, UserService>();


        }
    }
}
