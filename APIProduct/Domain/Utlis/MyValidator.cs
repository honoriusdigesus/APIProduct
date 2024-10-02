using System.Text.RegularExpressions;

namespace APIProduct.Domain.Utlis
{
    public class MyValidator
    {
        private static readonly Regex _regexNumber = new Regex(@"^(?!0)\d{6,10}$");
        private static readonly Regex _regexLetters = new Regex(@"^[A-Za-z]{3,20}$");
        private static readonly Regex _regexPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,16}$");


        public bool IsEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }

        public bool IsIdentityDocument(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false; // Retorna falso si la cadena es nula o vacía
            }

            return _regexNumber.IsMatch(input); // Retorna verdadero si la cadena cumple con el patrón
        }

        public bool IsString(string input) {
            if (string.IsNullOrEmpty(input))
            {
                return false; // Retorna falso si la cadena es nula o vacía
            }

            return _regexLetters.IsMatch(input); // Retorna verdadero si la cadena cumple con el patrón
        }

        internal bool IsPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false; // Retorna falso si la cadena es nula o vacía
            }

            return _regexPassword.IsMatch(password); // Retorna verdadero si la cadena cumple con el patrón
        }
    }
}
