using System.Text;

namespace Services.PasswordGenerators
{
    public class PasswordGenerator
    {
        public string GeneratePassword()
        {
            const int length = 12;
            const string ValidChar = "abcdefghijklmnoprstuwxyzqABCDEFGHIJKLMNOPRSTUWXYZQ1234567890";
            
            return GenerateString(length, ValidChar);
        }

        private string GenerateString(int length, string validChar)
        {
            StringBuilder result = new StringBuilder();
            Random random = new Random();
            while (0 < length--)
            {
                result.Append(validChar[random.Next(validChar.Length)]);
            }

            return result.ToString();
        }
    }
}
