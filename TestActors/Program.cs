using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestActors.Actors;
using TestActors.Messages;

namespace TestActors
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("batching");

            var batcher = system.ActorOf<BatchSupervisor>("batch");

            Console.WriteLine("Running - type a currency to batch, anything to exit");

            var s = Console.ReadLine();

            while (s != string.Empty)
            {
                batcher.Tell(new BatchMessage(s));

                s = Console.ReadLine();
            }
        }
    }
}
