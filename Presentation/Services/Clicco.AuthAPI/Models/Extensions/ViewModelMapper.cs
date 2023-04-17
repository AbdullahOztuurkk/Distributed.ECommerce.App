using Clicco.AuthAPI.Models.Response;

namespace Clicco.AuthAPI.Models.Extensions
{
    public static class ViewModelMapper
    {
        public static NewUserViewModel AsViewModel(this User user)
        {
            return new NewUserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender == true ? "Male" : "Female",
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
