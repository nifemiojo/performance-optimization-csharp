namespace Memory.ParseFromString;
internal record struct Point3D(double X, double Y, double Z)
{
    public static Point3D ParseBaseline(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input[0] != '(' || input[^1] != ')')
        {
            throw new FormatException($"Invalid input format: {input}");
        }

        // Split and substring both create new strings allocated in the heap
        input = input.Substring(1, input.Length - 2);
        string[] parts = input.Split(',');

        if (parts.Length != 3)
        {
            throw new FormatException($"Invalid input format: {input}");
        }

        double x = double.Parse(parts[0].Trim());
        double y = double.Parse(parts[1].Trim());
        double z = double.Parse(parts[2].Trim());

        return new Point3D(x, y, z);
    }

    public static Point3D ParseOptimized(string input)
    {
        // Span<char> does not allocate a new string in the heap,
        // instead it points to the original string
        ReadOnlySpan<char> span = input;

        if (span[0] != '(' || span[^1] != ')')
        {
            throw new FormatException($"Invalid input format: {input}");
        }

        int firstComma = span.IndexOf(',');
        int secondComma = span.LastIndexOf(',');

        if (firstComma == -1 || secondComma == -1)
        {
            throw new FormatException($"Invalid input format: {input}");
        }

        double x = double.Parse(span.Slice(1, firstComma - 1));
        double y = double.Parse(span.Slice(firstComma + 1, secondComma - firstComma - 1));
        double z = double.Parse(span.Slice(secondComma + 1, span.Length - secondComma - 2));

        return new Point3D(x, y, z);
    }

    public override string ToString() => $"({X}, {Y}, {Z})";
}
