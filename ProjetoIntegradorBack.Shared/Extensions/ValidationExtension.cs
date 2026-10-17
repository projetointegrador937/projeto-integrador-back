using ProjetoIntegradorBack.Shared.Pattern;
using System.Text.RegularExpressions;

namespace ProjetoIntegradorBack.Shared.Extensions
{
    public static class ValidationExtension
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        public static Failure? EnsureNotEmpty(this string? value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new Failure(400, $"{fieldName} is required");
            return null;
        }
        public static Failure? EnsureNotNegative(this decimal value, string fieldName)
        {
            if (value < 0)
                return new Failure(400, $"{fieldName} cannot be negative");
            return null;
        }
        public static Failure? EnsureEmail(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new Failure(400, "Email is required");

            return !EmailRegex.IsMatch(value)
                ? new Failure(400, "Invalid email format")
                : null;
        }

    }
       
}
