using Akka.Actor;
using TestActors.Messages;

namespace TestActors.Actors
{
    public class BatchSupervisor : ReceiveActor
    {
        public BatchSupervisor()
        {
            this.Receive<BatchMessage>(msg =>
            {
                var childRef = Context.Child(msg.Currency);

                if (childRef == ActorRefs.Nobody)
                {
                    var ctor = Props.Create<BatchActor>(msg.Currency);
                    childRef = Context.ActorOf(ctor, msg.Currency);
                }

                childRef.Forward(msg);
            });
        }
    }
}
