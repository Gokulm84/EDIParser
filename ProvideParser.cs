using System;
using System.Collections.Generic;
using System.Linq;

class EdiParser
{
    public static void Main()
    {
        string ediData = "Your eligibility EDI data here";
        ProviderInformation providerInformation = GetProviderInformation(ediData);

        Console.WriteLine($"Provider Name: {providerInformation.ProviderName}");
        Console.WriteLine($"Provider Address: {providerInformation.ProviderAddress}");
        Console.WriteLine($"Provider City: {providerInformation.ProviderCity}");
        Console.WriteLine($"Provider State: {providerInformation.ProviderState}");
        // Add more properties as needed based on the specific structure of your EDI data.
    }

    public static ProviderInformation GetProviderInformation(string ediData)
    {
        ProviderInformation providerInformation = new ProviderInformation();

        string[] segmentStrings = ediData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var segmentString in segmentStrings)
        {
            string[] elements = segmentString.Split('*');
            string segmentId = elements[0];

            switch (segmentId)
            {
                case "NM1":
                    if (elements.Length > 8 && elements[1] == "1")
                    {
                        providerInformation.ProviderName = elements[3];
                    }
                    break;
                case "N3":
                    if (elements.Length > 1)
                    {
                        providerInformation.ProviderAddress = elements[1];
                    }
                    break;
                case "N4":
                    if (elements.Length > 2)
                    {
                        providerInformation.ProviderCity = elements[1];
                        providerInformation.ProviderState = elements[2];
                    }
                    break;
                // Add more cases for other segments as needed.
            }
        }

        return providerInformation;
    }
}

class ProviderInformation
{
    public string ProviderName { get; set; }
    public string ProviderAddress { get; set; }
    public string ProviderCity { get; set; }
    public string ProviderState { get; set; }
    // Add more properties as needed based on the specific structure of your EDI data.
}
