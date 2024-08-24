using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Commons
{
    //для конфигурирования приложения напрямую
    public class StaticHttpContextExensions
    {


        public static IApplicationBuilder UseStaticCustomHttpContext(IApplicationBuilder app)
        {
            IHttpContextAccessor httpAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();

            return app;
        }
    }
}
