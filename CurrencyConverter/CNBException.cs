namespace CurrencyConverter
{
    public class CNBException : Exception
    {
        public CNBException() : base() { }
        public CNBException(string message) : base(message) { }
        public CNBException(string message, Exception innerException) : base(message, innerException) { }
    }
}
