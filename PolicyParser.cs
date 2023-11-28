using System;
using System.Collections.Generic;
using System.Linq;

class EdiParser
{
    public static void Main()
    {
        string ediData = "Your eligibility EDI data here";
        PolicyInformation policyInformation = GetPolicyInformation(ediData);

        Console.WriteLine($"Policy Code: {policyInformation.PolicyCode}");
        Console.WriteLine($"Policy Number: {policyInformation.PolicyNumber}");
        Console.WriteLine($"Policy Start Date: {policyInformation.PolicyStartDate}");
        // Add more properties as needed based on the specific structure of your EDI data.
    }

    public static PolicyInformation GetPolicyInformation(string ediData)
    {
        PolicyInformation policyInformation = new PolicyInformation();

        string[] segmentStrings = ediData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var segmentString in segmentStrings)
        {
            string[] elements = segmentString.Split('*');
            string segmentId = elements[0];

            switch (segmentId)
            {
                case "HI":
                    if (elements.Length > 2 && elements[1] == "BK")
                    {
                        policyInformation.PolicyCode = elements[2];
                    }
                    break;
                case "REF":
                    if (elements.Length > 2 && elements[1] == "1L")
                    {
                        policyInformation.PolicyNumber = elements[2];
                    }
                    break;
                case "DTP":
                    if (elements.Length > 2 && elements[1] == "356")
                    {
                        policyInformation.PolicyStartDate = elements[2];
                    }
                    break;
                // Add more cases for other segments as needed.
            }
        }

        return policyInformation;
    }
}

class PolicyInformation
{
    public string PolicyCode { get; set; }
    public string PolicyNumber { get; set; }
    public string PolicyStartDate { get; set; }
    // Add more properties as needed based on the specific structure of your EDI data.
}
