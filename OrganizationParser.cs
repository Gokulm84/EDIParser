using System;
using System.Collections.Generic;
using System.Linq;

class EdiParser
{
    public static void Main()
    {
        string ediData = "Your eligibility EDI data here";
        OrganizationInformation organizationInformation = GetOrganizationInformation(ediData);

        Console.WriteLine($"Organization Name: {organizationInformation.OrganizationName}");
        Console.WriteLine($"Organization ID: {organizationInformation.OrganizationId}");
        Console.WriteLine($"Organization Address: {organizationInformation.OrganizationAddress}");
        Console.WriteLine($"Organization City: {organizationInformation.OrganizationCity}");
        // Add more properties as needed based on the specific structure of your EDI data.
    }

    public static OrganizationInformation GetOrganizationInformation(string ediData)
    {
        OrganizationInformation organizationInformation = new OrganizationInformation();

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
                        organizationInformation.OrganizationName = elements[3];
                        organizationInformation.OrganizationId = elements[9];
                    }
                    break;
                case "N3":
                    if (elements.Length > 1)
                    {
                        organizationInformation.OrganizationAddress = elements[1];
                    }
                    break;
                case "N4":
                    if (elements.Length > 2)
                    {
                        organizationInformation.OrganizationCity = elements[1];
                        organizationInformation.OrganizationState = elements[2];
                    }
                    break;
                // Add more cases for other segments as needed.
            }
        }

        return organizationInformation;
    }
}

class OrganizationInformation
{
    public string OrganizationName { get; set; }
    public string OrganizationId { get; set; }
    public string OrganizationAddress { get; set; }
    public string OrganizationCity { get; set; }
    public string OrganizationState { get; set; }
    // Add more properties as needed based on the specific structure of your EDI data.
}
