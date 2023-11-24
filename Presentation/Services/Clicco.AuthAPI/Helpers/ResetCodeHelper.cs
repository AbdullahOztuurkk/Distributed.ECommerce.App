using System.Text;

namespace Clicco.AuthServiceAPI.Helpers
{
    public static class ResetCodeHelper
    {
        private const string Characters = "0123456789";
        private const int LinkLength = 12;

        public static string GenerateResetCode()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < LinkLength; i++)
            {
                int index = random.Next(Characters.Length);
                sb.Append(Characters[index]);
            }

            return sb.ToString();
        }
    }
}
