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

        /// <summary>
        /// Gets or sets user private identifier
        /// </summary>
        public string Pin { get; set; }
        /// <summary>
        /// Gets or sets user firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets user lastname
        /// </summary>
        public string LastName { get; set; }
    }
}