using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string stringInput)
        {
            var input = stringInput ?? throw new ArgumentNullException(nameof(stringInput));

            return Encoding.UTF8.GetBytes(input);
        }

        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsValidRsaId(this string input)
        {
            if (input.IsNullOrEmpty() || input.Length != 13)
            {
                return false;
            }

            var year = input.Substring(0, 2);
            var month = input.Substring(2, 2);
            var day = input.Substring(4, 2);

            var validDate = DateTime.ParseExact($"{year}-{month}-{day}", "yy-MM-dd", null);

            if (validDate == DateTime.MinValue)
            {
                return false;
            }

            var intOddTot = 0;

            for (var i = 0; i < 12; i++)
            {
                var c = input[i].ToString();
                intOddTot = intOddTot + Int32.Parse(c);
                i = i + 1;
            }

            var intEvenTot = 0;
            var intEvenResult = 0;

            for (var i = 1; i <= 12; i++)
            {
                var c = input[i].ToString();
                intEvenResult = Int32.Parse(c) * 2;

                if (intEvenResult < 10)
                {
                    intEvenTot = intEvenTot + intEvenResult;
                }
                else
                {
                    intEvenTot = intEvenTot + (int)Math.Floor((decimal)(intEvenResult / 10)) + intEvenResult % 10;
                }

                i = i + 1;
            }

            var sum = intEvenTot + intOddTot;
            var strchk = 0;

            if ((sum % 10) > 0)
            {
                strchk = 10 - (sum % 10);
            }
            else
            {
                strchk = 0;
            }

            var lstDigit = long.Parse(input) % 10;

            if (strchk == lstDigit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidStaffNumber(this string input)
        {
            if (input.IsNullOrEmpty() || input.Length != 8)
            {
                return false;
            }

            var prefix = input.Substring(0, 2);
            var numberString = input.Substring(2, 6);

            var prefixValid = prefix.ToLowerInvariant().Equals("nb") || prefix.ToLowerInvariant().Equals("cc");
            var numberValid = int.TryParse(numberString, out int number);

            return prefixValid && numberValid;
        }

        public static string Reverse(this string stringInput)
        {
            var input = stringInput ?? throw new ArgumentNullException(nameof(stringInput));

            var charArray = input.ToCharArray();

            Array.Reverse(charArray);

            return new string(charArray);
        }

        public static string ToBase64(this string stringInput)
        {
            var input = stringInput ?? throw new ArgumentNullException(nameof(stringInput));

            return Convert.ToBase64String(input.GetBytes());
        }

        public static byte[] ToBytesFromBase64(this string stringInput)
        {
            var input = stringInput ?? throw new ArgumentNullException(nameof(stringInput));

            var data = Convert.FromBase64String(input);
            return data;
        }

        public static string ToCamelCase(this string stringInput)
        {
            var input = stringInput.IsNullOrEmpty() ? throw new ArgumentNullException(nameof(stringInput)) : stringInput;

            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }

        public static string ToCleanPostalCode(this string code)
        {
            if (!code.IsNullOrEmpty())
            {
                if (code.Length > 4)
                {
                    return code.Substring(code.Length - 4, 4);
                }
                else
                {
                    return code;
                }
            }
            else
            {
                return null;
            }
        }

        public static Guid ToGuidFromShortGuid(this string shortGuid)
        {
            string base64 = shortGuid.Replace('-', '+').Replace('_', '/') + "==";

            var bytes = Convert.FromBase64String(base64);

            return new Guid(bytes);
        }

        public static string ToInternationalPhone(this string number)
        {
            if (!String.IsNullOrWhiteSpace(number))
            {
                number = number.Replace(" ", "").Replace("(", "").Replace(")", "");

                if (!number.StartsWith("+"))
                {
                    if (number.StartsWith("0"))
                    {
                        return "+27" + number.TrimStart('0');
                    }
                    else if (number.Length > 9) //likely in international format already 10 number digits would have been leading zero, just missing +
                    {
                        return "+" + number;
                    }
                    else //default to RSA country code
                    {
                        return "+27" + number.TrimStart('0');
                    }
                }
                else
                    return number;
            }
            else
                return number;
        }

        public static string ToMasked(this string sourceValue, int RemainChars = 2, int Position = 0, char maskCharacter = 'X')
        {
            string result = "";
            if (sourceValue.IsNullOrEmpty()) return "";
            if (sourceValue.Length <= 2) return sourceValue;

            //seed
            for (int i = 0; i < sourceValue.Length - RemainChars; i++)
            {
                result = $"{result}{maskCharacter}";
            }

            switch (Position)
            {
                case 0:
                    result = result + sourceValue.Substring(sourceValue.Length - RemainChars, RemainChars);
                    break;

                case 1:
                    result = sourceValue.Substring(0, RemainChars) + result;
                    break;

                default:
                    break;
            }

            return result;
        }

        public static string ToMaskedEmail(this string sourceValue, int RemainChars = 1, int Position = 1, char maskCharacter = '*')
        {
            if (sourceValue.Length > 1)
            {
                var indexOfAt = sourceValue.IndexOf('@');
                var indexOfFirstDot = sourceValue.IndexOf('.', indexOfAt);

                var toMaskLocalPart = sourceValue.Substring(0, indexOfAt);
                var domain = sourceValue.Substring(indexOfAt + 1, (indexOfFirstDot - (indexOfAt + 1)));
                var toMaskDomainExtention = sourceValue.Substring(indexOfFirstDot);

                var result = $"{toMaskLocalPart.ToMasked(RemainChars, Position, maskCharacter)}" +
                    $"{domain}" +
                    $"{toMaskDomainExtention.ToMasked(0, Position, maskCharacter)}";

                return result;
            }
            return sourceValue.ToMasked();
        }

        public static string ToMaskedPhone(this string sourceValue, int RemainChars = 4, int Position = 0, char maskCharacter = 'X')
        {
            var result = sourceValue.ToMasked(RemainChars, Position, maskCharacter);

            if (!string.IsNullOrWhiteSpace(result) && result.Length == 10)
            {
                result = $"{result.Substring(0, 3)} {result.Substring(3, 3)} {result.Substring(6)}";
            }

            return result;
        }

        public static string ToNonCharacterName(this string name)
        {
            if (!name.IsNullOrEmpty())
            {
                return name
                    .Replace("-", " ")
                    .Replace("@", " ")
                    .Replace("!", " ")
                    .Replace("#", " ")
                    .Replace("$", " ")
                    .Replace("^", " ")
                    .Replace("%", " ")
                    .Replace("&", " ")
                    .Replace("*", " ")
                    .Replace("(", " ")
                    .Replace(")", " ")
                    .Replace("+", " ")
                    .Replace("=", " ")
                    .Replace("-", " ")
                    .Replace("[", " ")
                    .Replace("header", " ")
                    .Replace("'", " ")
                    .Replace(",", " ")
                    .Replace(".", " ")
                    .Replace("{", " ")
                    .Replace("}", " ")
                    .Replace("|", " ")
                    .Replace(":", " ")
                    .Replace("<", " ")
                    .Replace(">", " ")
                    .Replace("?", " ");
            }
            else
            {
                return null;
            }
        }

        public static string ToPhoneBody(this string number)
        {
            if (!number.IsNullOrEmpty())
            {
                number = number.Replace(" ", "");

                if (!number.IsNullOrEmpty())
                {
                    if (number.Contains("+"))
                    {
                        var dialingCode = number.Substring(0, 3);
                        var withoutDialing = number.Replace(dialingCode, "0");
                        return withoutDialing.Substring(3, withoutDialing.Length - 3);
                    }
                    else
                    {
                        return number.Substring(3, number.Length - 3);
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string ToPhoneCode(this string number)
        {
            if (!number.IsNullOrEmpty())
            {
                number = number.Replace(" ", "");

                if (!number.IsNullOrEmpty())
                {
                    if (number.Contains("+"))
                    {
                        var dialingCode = number.Substring(0, 3);
                        number = number.Replace(dialingCode, "0");
                        return number.Substring(0, 3).Trim();
                    }
                    else
                    {
                        return number.Substring(0, 3).Trim();
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static int ToRsaIdAge(this string input)
        {
            var today = DateTime.Now;

            if (!int.TryParse(input.Substring(0, 2), out int year))
            {
                return 0;
            }

            if (!int.TryParse(input.Substring(2, 2), out int month))
            {
                return 0;
            }

            if (!int.TryParse(input.Substring(4, 2), out int day))
            {
                return 0;
            }

            var cutoff = today.Year - 2000;
            var dob = (year > cutoff ? "19" : "20") + (year.ToString().Length > 1 && year.ToString().Length < 4 ? year.ToString() : "0" + year.ToString()) + '/' + month + '/' + day;
            year = Int32.Parse(dob.Substring(0, 4));

            DateTime validDate;
            try
            {
                validDate = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                return 0;
            }

            if (validDate == DateTime.MinValue)
            {
                return 0;
            }

            var birthDate = Convert.ToDateTime(dob);
            var age = today.Year - birthDate.Year;
            var m = today.Month - birthDate.Month;
            if (m < 0 || (m == 0 && today < birthDate))
            {
                age--;
            }

            return age;
        }

        public static string ToRsaIdGendorCode(this string input)
        {
            if (input.IsNullOrEmpty())
            {
                return null;
            }

            try
            {
                if (input.Length > 11 && int.TryParse(input.Substring(6, 4), out int value))
                {
                    return value < 5000 ? "F" : "M";
                }
            }
            catch
            {
                // Do nothing
            }

            return null;
        }

        public static string ToSanatizedValue(this string input)
        {
            return !input.IsNullOrEmpty() ? input.ToLower().Replace(" ", "").Replace("%20", "") : "";
        }

        public static string ToStringFromBase64(this string stringInput)
        {
            var input = stringInput ?? throw new ArgumentNullException(nameof(stringInput));

            var data = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(data);
        }

        public static string ToTitleCase(this string input)
        {
            if (input.IsNullOrEmpty())
            {
                return input;
            }

            var inputWords = input.Trim().Split(' ');
            var resultWord = new StringBuilder();
            foreach (var word in inputWords)
            {
                resultWord.Append(char.ToUpperInvariant(word[0]));
                if (word.Length > 1)
                {
                    resultWord.Append(word.ToLowerInvariant().Substring(1));
                }

                resultWord.Append(" ");
            }

            return resultWord.ToString().Trim();
        }

        public static bool ValidIdentityNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length != 13)
            {
                return false;
            }

            var year = input.Substring(0, 2);
            var month = input.Substring(2, 2);
            var day = input.Substring(4, 2);

            var validDate = DateTime.ParseExact($"{year}-{month}-{day}", "yy-MM-dd", null);

            if (validDate == DateTime.MinValue)
            {
                return false;
            }

            var intOddTot = 0;

            for (var i = 0; i < 12; i++)
            {
                var c = input[i].ToString();
                intOddTot = intOddTot + Int32.Parse(c);
                i = i + 1;
            }

            var intEvenTot = 0;
            var intEvenResult = 0;

            for (var i = 1; i <= 12; i++)
            {
                var c = input[i].ToString();
                intEvenResult = Int32.Parse(c) * 2;

                if (intEvenResult < 10)
                {
                    intEvenTot = intEvenTot + intEvenResult;
                }
                else
                {
                    intEvenTot = intEvenTot + (int)Math.Floor((decimal)(intEvenResult / 10)) + intEvenResult % 10;
                }

                i = i + 1;
            }

            var sum = intEvenTot + intOddTot;
            var strchk = 0;

            if ((sum % 10) > 0)
            {
                strchk = 10 - (sum % 10);
            }
            else
            {
                strchk = 0;
            }

            var lstDigit = long.Parse(input) % 10;

            if (strchk == lstDigit)
            {
                var age = ToRsaIdAge(input);
                if (age >= 18)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }


    }
}