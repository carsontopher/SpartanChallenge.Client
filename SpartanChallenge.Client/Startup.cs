using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Newtonsoft;

[assembly: OwinStartup(typeof(SpartanChallenge.Client.Startup))]

namespace SpartanChallenge.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
