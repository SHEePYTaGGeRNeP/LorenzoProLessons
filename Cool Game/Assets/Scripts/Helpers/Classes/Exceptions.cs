using System;

namespace Helpers.Classes
{
    public class NegativeInputException : Exception
    {
        private readonly string _parameter;
        public NegativeInputException(string parameter)
        {
            this._parameter = parameter;
        }

        public override string ToString()
        {
            return $"parameter: {this._parameter} was negative.\n{base.ToString()}";
        }
    }
}