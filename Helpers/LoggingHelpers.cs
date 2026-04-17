namespace BlazorCRUD.Helpers
{
    internal static class LoggingHelpers
    {
        /// <summary>
        /// Sanitizes a user-provided string value before including it in a log message to prevent log forging.
        /// Removes carriage return and newline characters that could inject fake log entries.
        /// </summary>
        internal static string? SanitizeLogValue(string? value)
            => value?.Replace("\r", "").Replace("\n", "");
    }
}
