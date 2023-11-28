using System;
using System.Collections.Generic;
using System.Linq;

class EdiParser
{
    public static void Main()
    {
        string ediData = "Your eligibility EDI data here";
        CoverageInformation coverageInformation = GetCoverageInformation(ediData);

        Console.WriteLine($"Coverage Code: {coverageInformation.CoverageCode}");
        Console.WriteLine($"Coverage Begin Date: {coverageInformation.CoverageBeginDate}");
        Console.WriteLine($"Coverage End Date: {coverageInformation.CoverageEndDate}");
        // Add more properties as needed based on the specific structure of your EDI data.
    }

    public static CoverageInformation GetCoverageInformation(string ediData)
    {
        CoverageInformation coverageInformation = new CoverageInformation();

        string[] segmentStrings = ediData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var segmentString in segmentStrings)
        {
            string[] elements = segmentString.Split('*');
            string segmentId = elements[0];

            switch (segmentId)
            {
                case "HI":
                    if (elements.Length > 2 && elements[1] == "ABK")
                    {
                        coverageInformation.CoverageCode = elements[2];
                    }
                    break;
                case "DTP":
                    if (elements.Length > 2 && elements[1] == "348")
                    {
                        coverageInformation.CoverageBeginDate = elements[2];
                    }
                    else if (elements.Length > 2 && elements[1] == "349")
                    {
                        coverageInformation.CoverageEndDate = elements[2];
                    }
                    break;
                // Add more cases for other segments as needed.
            }
        }

        return coverageInformation;
    }
}

class CoverageInformation
{
    public string CoverageCode { get; set; }
    public string CoverageBeginDate { get; set; }
    public string CoverageEndDate { get; set; }
    // Add more properties as needed based on the specific structure of your EDI data.
}
