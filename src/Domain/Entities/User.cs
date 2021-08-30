using Domain.Common;

namespace Domain.Entities
{
    public class User : Entity
    {
        public User() { }

        public User(string pin, string firstName, string lastName) =>
            (Pin, FirstName, LastName) = (pin, firstName, lastName);
        
        /// <summary>
        /// Gets or sets user private identifier
        /// </summary>
        public string Pin { get; }
        /// <summary>
        /// Gets or sets user firstname
        /// </summary>
        public string FirstName { get; }
        /// <summary>
        /// Gets or sets user lastname
        /// </summary>
        public string LastName { get; }
    }
}