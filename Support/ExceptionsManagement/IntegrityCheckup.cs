using Support.ExceptionsManagement.Exceptions;
using Support.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Support.ExceptionsManagement
{
    public class IntegrityCheckup
    {
        #region Members

        #endregion

        #region Constructors

        public IntegrityCheckup()
        {
            ExceptionsMessages = new List<string>();
        }

        #endregion

        public List<string> ExceptionsMessages { get; }

        #region Public Methods

        public void AddExceptionMessage(string message)
        {
            ExceptionsMessages.Add(message);
        }

        public void AddExceptionsMessages(List<string> messages)
        {
            ExceptionsMessages.AddRange(messages);
        }

        public void CheckCnpj(string cnpj, string message)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                AddExceptionMessage(message);
                return;
            }

            cnpj = cnpj.Trim().Replace("-", "").Replace("/", "");

            if (!cnpj.Length.Equals(14) || cnpj.Any(c => !int.TryParse(c.ToString(), out int characterConversion)))
            {
                AddExceptionMessage(message);
            }
            else
            {
                var multiplifier1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplifier2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                var cnpjPartial = cnpj.Substring(0, 12);

                var sum = cnpjPartial
                    .Select((character, index) => new { character, index })
                    .Sum(obj => int.Parse(obj.character.ToString()) * multiplifier1[obj.index]);

                var cnpjFirstDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);

                cnpjPartial = string.Format("{0}{1}", cnpjPartial, cnpjFirstDigit);

                sum = cnpjPartial
                    .Select((character, index) => new { character, index })
                    .Sum(obj => int.Parse(obj.character.ToString()) * multiplifier2[obj.index]);

                var cnpjSecondDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);

                var cnpjDigit = string.Format("{0}{1}", cnpjFirstDigit, cnpjSecondDigit);

                if (!cnpj.EndsWith(cnpjDigit))
                {
                    AddExceptionMessage(message);
                }
            }
        }

        public void CheckCpf(string cpf, string message)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                AddExceptionMessage(message);
                return;
            }

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (!cpf.Length.Equals(11) ||
                cpf.Any(c => !int.TryParse(c.ToString(), out int charecterConversion)))
            {
                AddExceptionMessage(message);
            }
            else
            {
                var multiplifier1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplifier2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                var cpfPartial = cpf.Substring(0, 9);

                var sum = cpfPartial
                    .Select((character, index) => new { character, index })
                    .Sum(obj => int.Parse(obj.character.ToString()) * multiplifier1[obj.index]);

                var cpfFirstDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);

                cpfPartial = string.Format("{0}{1}", cpfPartial, cpfFirstDigit);

                sum = cpfPartial
                    .Select((character, index) => new { character, index })
                    .Sum(obj => int.Parse(obj.character.ToString()) * multiplifier2[obj.index]);

                var cpfSecondDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);

                var cpfDigit = string.Format("{0}{1}", cpfFirstDigit, cpfSecondDigit);

                if (!cpf.EndsWith(cpfDigit))
                {
                    AddExceptionMessage(message);
                }
            }
        }

        public void CheckRequired<T>(T attribute, string message)
        {
            if (attribute.IsNull() || (attribute is string && (attribute as string).IsNullOrEmpty()) ||
                ((attribute is Enum || attribute is int) && Convert.ToInt16(attribute).Equals(0)) ||
                (attribute is DateTime && attribute.Equals(DateTime.MinValue)) ||
                (attribute is decimal && Convert.ToDecimal(attribute).Equals(0)))
            {
                AddExceptionMessage(message);
            }
        }

        public void CheckStringMaximumLimit(string attribute, int maximumLimit, string message)
        {
            if (attribute.Length > maximumLimit) AddExceptionMessage(message);
        }

        public void CheckUnicityInList<T>(IEnumerable<T> items, Func<T, bool> predicate, string message)
        {
            if (items == null) throw new ArgumentNullException("itens");

            if (items.Any(predicate)) AddExceptionMessage(message);
        }

        public T CheckItemExistenceInListBeforeRemoving<T>(IEnumerable<T> items,
            Func<T, bool> predicate,
            string message)
        {
            var enumerable = items as IList<T> ?? items.ToList();
            var itemToRemove = enumerable.SingleOrDefault(predicate);

            if (itemToRemove.IsNull()) AddExceptionMessage(message);

            return itemToRemove;
        }

        public void CheckFilledList<T>(IEnumerable<T> items, string message)
        {
            if (!items.Any()) AddExceptionMessage(message);
        }

        public void CheckIsTrue(bool isTrue, string message)
        {
            if (!isTrue) AddExceptionMessage(message);
        }

        public void ThrowExceptions()
        {
            if (ExceptionsMessages.Count == 0) return;

            var exceptionsWrapper = new ExceptionsWrapper();
            exceptionsWrapper.AddExceptionsMessages(ExceptionsMessages);

            throw exceptionsWrapper;
        }

        #endregion

    }
}
