using System;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Extensions
{
    public static class CommonExtensions
    {
        public static async void SafeFireAndForget(this Task task, Action<Exception>? onException = null, bool continueOnCapturedContext = false)
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
    }
}
