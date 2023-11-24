using Clicco.AuthAPI.Models.Response;

namespace Clicco.AuthAPI.Models.Extensions
{
    public static class ViewModelMapper
    {
        public static RegisteredUserDto AsViewModel(this User user)
        {
            return new RegisteredUserDto
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
