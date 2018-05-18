using Akka.Actor;
using BVirtual.FaTi.Business.Actors.User;

namespace BVirtual.FaTi.Business.Providers
{
    public class UserManagerActorProvider
    {
        private IActorRef UserManagerActorInstance { get; }

        public UserManagerActorProvider(ActorSystem actorSystem)
        {
            UserManagerActorInstance = actorSystem.ActorOf(UserManagerActor.Props(), "userManager");
        }

        public IActorRef Get()
        {
            return UserManagerActorInstance;
        }

    }
}
