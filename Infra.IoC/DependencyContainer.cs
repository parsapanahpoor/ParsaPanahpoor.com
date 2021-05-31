using DataAccees.Interfaces;
using DataAccees.Services;
using DataAccees.UnitOfWork;
using DataContext;
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


            service.AddTransient<UnitOfWork<ParsaPanahpoorDBContext>>();


        }
    }
}
