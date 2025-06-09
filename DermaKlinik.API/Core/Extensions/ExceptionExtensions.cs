namespace DermaKlinik.API.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<string> Messages(this Exception ex)
        {
            if (ex != null)
            {
                yield return ex.Message;
                IEnumerable<Exception> innerExceptions = Enumerable.Empty<Exception>();
                if (ex is AggregateException && (ex as AggregateException).InnerExceptions.Any())
                    innerExceptions = (ex as AggregateException).InnerExceptions;
                else if (ex.InnerException != null)
                    innerExceptions = (new Exception[1] { ex.InnerException });
                foreach (Exception innerEx in innerExceptions)
                {
                    foreach (string msg in innerEx.Messages())
                        yield return msg;
                }
            }
        }

        public static string MessagesStr(this Exception ex) => string.Join("\n", ex.Messages());
    }
}
