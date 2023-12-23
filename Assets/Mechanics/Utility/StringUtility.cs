/// <summary>
/// A utility class for formatting string values.
/// </summary>
public static class StringUtility
{
    /// <summary>
    /// Format the money value for display, e.g., convert 1000 to "1k".
    /// </summary>
    /// <param name="moneyValue">The money value to be formatted.</param>
    /// <returns>A string representation of the formatted money value.</returns>
    public static string FormatMoney(int moneyValue)
    {
        // Check if the value is 1000 or greater, or -1000 or smaller
        if (moneyValue >= 1000 || moneyValue <= -1000)
        {
            // If the value is 1000 or greater, display in "k" format
            return (moneyValue / 1000f).ToString("0") + "k";
        }
        else
        {
            // Otherwise, display the value as it is
            return moneyValue.ToString();
        }
    }
}
