using System.ComponentModel;

namespace CommerceService.Domain.Constant;

public enum TransactionStatus
{
    [Description("Failed")]
    Failed = 0,

    [Description("Pending")]
    Pending = 1,

    [Description("Success")]
    Success = 2,
}
