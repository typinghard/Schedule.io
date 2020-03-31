using System;
using System.Collections.Generic;
using System.Text;

namespace Agenda.Domain.ValueObjects
{
    public struct Email
    {
        public Email(string emailAddress)
        {
            //Assert(() => Regex.IsMatch(emailAddress ?? string.Empty, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"));
            EmailAddress = emailAddress;
        }

        public string EmailAddress { get; private set; }

        public static Email ToEmail(string emailAddress) => new Email(emailAddress);

        public override string ToString() => EmailAddress;
    }
}
