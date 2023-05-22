using System.Collections.Generic;

namespace Domain.Common
{
    public class ValueObjectString : ValueObject
    {
        public string Value { get; private set; }

        protected int Lenght { get; set; } = 10;

        protected ValueObjectString()
        {

        }

        protected ValueObjectString(string value)
        {
            Value = value;
        }



        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
