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

        public class PaginationFilter
        {
            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public PaginationFilter() : this(1,10)
            {

            }

            public PaginationFilter(int pageNumber, int pageSize)
            {
                PageNumber = pageNumber < 1 ? 1 : pageNumber;
                PageSize = pageSize < 10 ? 10 : pageSize;
            }
        }
    }
}
