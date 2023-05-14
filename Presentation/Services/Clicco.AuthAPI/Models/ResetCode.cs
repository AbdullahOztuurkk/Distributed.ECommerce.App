using Clicco.AuthAPI.Models.Common;

namespace Clicco.AuthServiceAPI.Models
{
    public class ResetCode : BaseEntity
    {
        public int UserId { get; set; }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsAvailable()
        {
            return (Code != null) && IsActive && (ExpirationDate >= DateTime.Now);
        }
    }
}
