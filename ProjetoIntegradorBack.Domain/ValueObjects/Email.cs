using ProjetoIntegradorBack.Shared.Extensions;
using ProjetoIntegradorBack.Shared.Pattern;

namespace ProjetoIntegradorBack.Domain.ValueObjects
{
    public class Email
    {      
        public string UserName { get; private set; }
        public string Domain { get; private set; }
        
        public Email() { }
        public static Result<Email> CreateEmail(string email)
        {
            var failure = email.EnsureEmail();
            if (failure != null)
                return Result<Email>.AsFailure(failure);

            var normalizedEmail = email.Trim().ToLowerInvariant();
            var splitEmail = normalizedEmail.Split("@", 2);
            var newEmail = new Email();
            newEmail.UserName = splitEmail[0];
            newEmail.Domain = splitEmail[1];
            return Result<Email>.AsSuccess(newEmail);   
        }
    }
}
