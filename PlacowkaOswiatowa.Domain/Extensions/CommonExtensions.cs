using System.Threading.Tasks;
using System;

namespace PlacowkaOswiatowa.Domain.Extensions
{
    public static class CommonExtensions
    {
        public static async void SafeFireAndForget(this Task task, Action<Exception> onException = null, bool continueOnCapturedContext = false)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException.Invoke(ex);
            }
        }

        public static string ToLowerString(this object obj) =>
            obj == null ? string.Empty : obj.ToString().ToLower();

        public static string SafeToLower(object obj) =>
            obj == null ? string.Empty : obj.ToString().ToLower();
    }
}
