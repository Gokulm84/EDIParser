using System;
using System.Collections.Generic;
using System.Linq;

class EdiParser
{
    public static void Main()
    {
        string ediData = "Your eligibility EDI data here";
        EligibilityTransaction eligibilityTransaction = ParseEligibilityEdi(ediData);

        Console.WriteLine($"Transaction Type: {eligibilityTransaction.TransactionType}");
        Console.WriteLine($"Subscriber Information: {eligibilityTransaction.SubscriberInformation}");
        // Add more properties as needed based on the specific structure of your eligibility EDI data.
    }

    public static EligibilityTransaction ParseEligibilityEdi(string ediData)
    {
        EligibilityTransaction eligibilityTransaction = new EligibilityTransaction();

        string[] segmentStrings = ediData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var segmentString in segmentStrings)
        {
            string[] elements = segmentString.Split('*');
            string segmentId = elements[0];

            switch (segmentId)
            {
                case "ISA":
                    eligibilityTransaction.InterchangeControlHeader = new ISASegment(elements.Skip(1).ToList());
                    break;
                case "GS":
                    eligibilityTransaction.FunctionalGroupHeader = new GSSegment(elements.Skip(1).ToList());
                    break;
                case "ST":
                    eligibilityTransaction.TransactionSetHeader = new STSegment(elements.Skip(1).ToList());
                    break;
                case "BHT":
                    eligibilityTransaction.BeginningOfHierarchicalTransaction = new BHTSegment(elements.Skip(1).ToList());
                    break;
                // Add more cases for other segments as needed.
            }
        }

        return eligibilityTransaction;
    }
}

class ISASegment
{
    public List<string> Elements { get; }

    public ISASegment(List<string> elements)
    {
        Elements = elements;
    }
}

class GSSegment
{
    public List<string> Elements { get; }

    public GSSegment(List<string> elements)
    {
        Elements = elements;
    }
}

class STSegment
{
    public List<string> Elements { get; }

    public STSegment(List<string> elements)
    {
        Elements = elements;
    }
}

class BHTSegment
{
    public List<string> Elements { get; }

    public BHTSegment(List<string> elements)
    {
        Elements = elements;
    }
}

class EligibilityTransaction
{
    public ISASegment InterchangeControlHeader { get; set; }
    public GSSegment FunctionalGroupHeader { get; set; }
    public STSegment TransactionSetHeader { get; set; }
    public BHTSegment BeginningOfHierarchicalTransaction { get; set; }
    // Add more properties as needed based on the specific structure of your eligibility EDI data.
}
