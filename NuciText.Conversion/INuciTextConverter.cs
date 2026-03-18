namespace NuciText.Conversion
{
    /// <summary>
    /// Interface for converting text.
    /// </summary>
    public interface INuciTextConverter
    {
        /// <summary>
        /// Converts the given text to Windows-1252 encoding, replacing characters that are not supported in that encoding with their closest equivalents.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>The converted text.</returns>
        string ToWindows1252(string text);
    }
}
