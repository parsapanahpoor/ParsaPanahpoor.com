﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParsaPanahpoor.WebSite.ViewComponents
{
    public class ProjectsComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return await Task.FromResult((IViewComponentResult)View("ProjectsComponent"));

        }
    }
}
