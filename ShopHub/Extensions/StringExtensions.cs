using System.Text.RegularExpressions;

namespace ShopHub.Extensions;

public static class StringExtensions
{
    public static bool IsValidEmail(
        this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return Regex.IsMatch(
            value,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public static string ToTitleCase(
        this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        string[] words = value
            .Trim()
            .ToLower()
            .Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < words.Length; i++)
        {
            words[i] =
                char.ToUpper(words[i][0]) +
                words[i].Substring(1);
        }

        return string.Join(" ", words);
    }

    public static bool IsNullOrEmpty(
        this string? value)
    {
        return string.IsNullOrEmpty(value);
    }
}