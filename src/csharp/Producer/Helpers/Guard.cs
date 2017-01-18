namespace Producer.Helpers
{
    using System;

    public static class Guard
    {
        public static void AgainstNullOrEmptyString(
            string value,
            string propertyName)
        {
            if (value == null)
                throw new ArgumentNullException(propertyName);

            if (Equals(value, string.Empty))
                throw new ArgumentException(propertyName);
        }
    }
}