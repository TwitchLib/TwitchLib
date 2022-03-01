using System;
using Xunit;

namespace TwitchLib.Api.Test.Helpers
{
    public static class AssertException
    {
        public static void Throws<TException>(string exceptionMessage, Action testCode) where TException : Exception
        {
            var exception = Assert.Throws<TException>(testCode);
            AssertParamNotNull(exception);
            Assert.Matches(exceptionMessage, exception.Message);
        }

        public static void Throws<TException>(string exceptionMessage, Func<object> testCode) where TException : Exception
        {
            var exception = Assert.Throws<TException>(testCode);
            AssertParamNotNull(exception);
            Assert.Matches(exceptionMessage, exception.Message);
        }

        private static void AssertParamNotNull(Exception exception)
        {
            if (exception is ArgumentException argumentException)
                Assert.NotNull(argumentException.ParamName);
        }

        public static void Throws<TArgumentNullException>(Func<object> testCode) where TArgumentNullException : ArgumentNullException
        {
            var exception = Assert.Throws<TArgumentNullException>(testCode);
            Assert.NotNull(exception.ParamName);
        }

        public static void Throws<TArgumentNullException>(Action testCode) where TArgumentNullException : ArgumentNullException
        {
            var exception = Assert.Throws<TArgumentNullException>(testCode);
            Assert.NotNull(exception.ParamName);
        }
    }
}