using System;
using System.Collections.Generic;
using System.Linq;

class EdiParser
{
    public static void Main()
    {
        string ediData = "Your eligibility EDI data here";
        SubscriberInformation subscriberInformation = GetSubscriberInformation(ediData);

        Console.WriteLine($"Subscriber Name: {subscriberInformation.SubscriberName}");
        Console.WriteLine($"Subscriber ID: {subscriberInformation.SubscriberId}");
        Console.WriteLine($"Subscriber DOB: {subscriberInformation.SubscriberDOB}");
        // Add more properties as needed based on the specific structure of your EDI data.
    }

    public static SubscriberInformation GetSubscriberInformation(string ediData)
    {
        SubscriberInformation subscriberInformation = new SubscriberInformation();

        string[] segmentStrings = ediData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var segmentString in segmentStrings)
        {
            string[] elements = segmentString.Split('*');
            string segmentId = elements[0];

            switch (segmentId)
            {
                case "NM1":
                    if (elements.Length > 8 && elements[1] == "IL" && elements[8] == "MI")
                    {
                        subscriberInformation.SubscriberName = elements[3];
                        subscriberInformation.SubscriberId = elements[9];
                    }
                    break;
                case "DMG":
                    if (elements.Length > 2 && elements[1] == "D8")
                    {
                        subscriberInformation.SubscriberDOB = elements[2];
                    }
                    break;
                // Add more cases for other segments as needed.
            }
        }

        return subscriberInformation;
    }
}

class SubscriberInformation
{
    public string SubscriberName { get; set; }
    public string SubscriberId { get; set; }
    public string SubscriberDOB { get; set; }
    // Add more properties as needed based on the specific structure of your EDI data.
}
