using Akka.Actor;

namespace BVirtual.FaTi.Business.Actors.User
{
    public class UserManagerActor : ReceiveActor
    {
        public UserManagerActor()
        {
            //ReceiveAny();
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new UserManagerActor());
        }
    }
}
