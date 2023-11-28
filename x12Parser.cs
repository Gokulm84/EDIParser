using System;
using System.Collections.Generic;
using System.Linq;

class EdiParser
{
    public static void Main()
    {
        string ediData = "Your EDI data here";
        List<EdiSegment> segments = ParseEdi(ediData);

        foreach (var segment in segments)
        {
            Console.WriteLine($"{segment.Id}: {string.Join("|", segment.Elements)}");
        }
    }

    public static List<EdiSegment> ParseEdi(string ediData)
    {
        List<EdiSegment> segments = new List<EdiSegment>();

        string[] segmentStrings = ediData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var segmentString in segmentStrings)
        {
            string[] elements = segmentString.Split('*');
            string segmentId = elements[0];

            List<string> segmentElements = elements.Skip(1).ToList();

            EdiSegment segment = new EdiSegment(segmentId, segmentElements);
            segments.Add(segment);
        }

        return segments;
    }
}

class EdiSegment
{
    public string Id { get; }
    public List<string> Elements { get; }

    public EdiSegment(string id, List<string> elements)
    {
        Id = id;
        Elements = elements;
    }
}
