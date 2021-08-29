using Domain.Common;

namespace Domain.Entities
{
    public class User : Entity
    {
        public User() { }

        public User(string pin, string firstName, string lastName) =>
            (Pin, FirstName, LastName) = (pin, firstName, lastName);
        public string Pin { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}