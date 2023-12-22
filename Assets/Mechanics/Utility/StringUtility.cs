public static class StringUtility
{
    // Format the money value for display, e.g., convert 1000 to "1k"
    public static string FormatMoney(int moneyValue)
    {
        if (moneyValue >= 1000 || moneyValue<= -1000)
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
