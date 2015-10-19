using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestActors.Messages;

namespace TestActors.Actors
{
    public class BatchActor : ReceiveActor
    {
        public BatchActor(string currency)
        {
            _currency = currency;

            this.Receive<BatchMessage>(b =>
            {
                // Process the message here...
                Console.WriteLine("Processing message for '{0}'", _currency);
                // Then work out if I'm done...
                if (++_processed >= _maxSize)
                {
                    _batchTimeout.Cancel();
                    Context.Stop(Context.Self);
                }

                return true;
            });

            this.Receive<BatchTimeoutMessage>(t =>
            {
                Console.WriteLine("Timeout reached");
                Context.Stop(Context.Self);
            });
        }

        protected override void PreStart()
        {
            Console.WriteLine("{0} Actor Started - can read batch duration and size here", _currency);
            _maxSize = 5;
            _batchTimeout = Context.System.Scheduler.ScheduleTellOnceCancelable(5000, Context.Self, new BatchTimeoutMessage(), Context.Self);
        }

        protected override void PostStop()
        {
            Console.WriteLine("{0} Actor stopped", _currency);
        }

        private string _currency;
        private int _maxSize;
        private int _processed;
        private ICancelable _batchTimeout;
    }
}
