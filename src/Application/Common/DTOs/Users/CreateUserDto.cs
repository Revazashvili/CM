namespace Application.Common.DTOs.Users
{
    public class CreateUserDto
    {
        public CreateUserDto(string pin, string firstName, string lastName)
        {
            Pin = pin;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Pin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}