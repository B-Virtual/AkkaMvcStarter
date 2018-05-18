using System.Threading.Tasks;
using Akka.Actor;
using BVirtual.FaTi.Business.Providers;
using Microsoft.AspNetCore.Mvc;

namespace BVirtual.FaTi.Business.Routes
{
    public class CreateUser
    {
        private IActorRef UserManager { get; }

        public CreateUser(UserManagerActorProvider provider)
        {
            UserManager = provider.Get();
        }

        public async Task<IActionResult> Execute()
        {
            return null;
        }
    }
}
