namespace Clicco.Domain.Shared
{
    public class Global
    {
        public class QueueNames
        {
            public const string UpdatedTransactionQueue = "updated-transaction-queue";
            public const string DeletedTransactionQueue = "deleted-transaction-queue";        
        }

        public class ExcludeAttribute : Attribute
        {

        }

        public class CustomElementAttribute : Attribute
        {

        }

        public class DisplayElementAttribute : Attribute
        {
            public string ParameterName { get; private set; }
            public DisplayElementAttribute(string parameterName)
            {
                ParameterName = parameterName;
            }
        }
    }
}
