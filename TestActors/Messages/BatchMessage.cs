namespace TestActors.Messages
{
    public class BatchMessage
    {
        public BatchMessage(string currency)
        {
            this.Currency = currency;
        }

        public string Currency { get; private set; }
    }
}
