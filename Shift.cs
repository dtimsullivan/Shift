using System;
using System.IO;
using System.Collections.Generic;

namespace Shift
{
    class Shift
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
  _____  _    _  _____  ______  _______  ________
 / ____|| |  | ||_   _||  ____||__   __||________|
| (___  | |__| |  | |  | |__      | |       ________
 \___ \ |  __  |  | |  |  __|     | |    >>|________|
 ____) || |  | | _| |_ | |        | |    ________
|_____/ |_|  |_||_____||_|        |_|   |________|                                                                       


Converts a CSV file to an import file for Polycom Sidecars in the Metaswitch VVX Endpoint Pack.

The output file will be saved to the user's desktop.

WARNING: This program assumes you have a header in your CSV file. The first line will be ignored!

View the README for more information.

-------------------------------------------------------------------------------------------------");

            // Get Polycom Model and Set Start Point
            string phoneType = "";
            bool phoneTypeAnswer = false;
            int expansionModuleStart = 0;
            Console.WriteLine("\nWhat is the model of Polycom phone? (301, 311, 401, 411, 501, 601)");
            while (phoneTypeAnswer == false)
            {
                string phoneTypeInput = Console.ReadLine();
                if (phoneTypeInput == "301")
                {
                    phoneType = "VVX_301";
                    expansionModuleStart = 17;
                    phoneTypeAnswer = true;
                }
                else if (phoneTypeInput == "311")
                {
                    phoneType = "VVX_311";
                    expansionModuleStart = 17;
                    phoneTypeAnswer = true;
                }
                else if (phoneTypeInput == "401")
                {
                    phoneType = "VVX_401";
                    expansionModuleStart = 23;
                    phoneTypeAnswer = true;
                }
                else if (phoneTypeInput == "411")
                {
                    phoneType = "VVX_411";
                    expansionModuleStart = 23;
                    phoneTypeAnswer = true;
                }
                else if (phoneTypeInput == "501")
                {
                    phoneType = "VVX_501";
                    expansionModuleStart = 23;
                    phoneTypeAnswer = true;
                }
                else if (phoneTypeInput == "601")
                {
                    phoneType = "VVX_601";
                    expansionModuleStart = 27;
                    phoneTypeAnswer = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid response, please try again.");
                }
            }
            // Get Expansion Module Type
            string expansionModuleType = "";
            bool expansionModuleAnswer = false;
            Console.WriteLine("\nIs the expansion module color? (Y or N)");
            while (expansionModuleAnswer == false)
            {
                string expansionModuleInput = Console.ReadLine();
                if (expansionModuleInput == "Y" || expansionModuleInput == "y" || expansionModuleInput == "Yes" || expansionModuleInput == "yes")
                {
                    expansionModuleType = "VVX_Color_EM";
                    expansionModuleAnswer = true;
                }
                else if (expansionModuleInput == "N" || expansionModuleInput == "n" || expansionModuleInput == "No" || expansionModuleInput == "no")
                {
                    expansionModuleType = "VVX_Paper_EM";
                    expansionModuleAnswer = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid response, please try again.");
                }
            }

            // Set Soft Key Action
            string softKeyAction = "";
            bool softKeyActionAnswer = false;
            Console.WriteLine("\nWhat is the soft key action? (SD or EME)");
            while (softKeyActionAnswer == false)
            {
                string softKeyActionInput = Console.ReadLine();
                if (softKeyActionInput == "SD" || softKeyActionInput == "sd")
                {
                    softKeyAction = "SpeedDial";
                    softKeyActionAnswer = true;
                }
                else if (softKeyActionInput == "EME" || softKeyActionInput == "eme")
                {
                    softKeyAction = "EnhancedMonitoredExtension";
                    softKeyActionAnswer = true;
                }
                else
                {
                    Console.WriteLine("\nInvalid response, please try again.");
                }
            }

            // Set Name As Label and/or Clear Existing Labels
            string nameAsLabel = "";
            string existingLabel = "";
            if (softKeyAction == "EnhancedMonitoredExtension")
            {
                bool nameAsLabelAnswer = false;
                Console.WriteLine("\nUse Name as Label? (Y or N)");
                while (nameAsLabelAnswer == false)
                {
                    string nameAsLabelInput = Console.ReadLine();
                    if (nameAsLabelInput == "Y" || nameAsLabelInput == "y" || nameAsLabelInput == "Yes" || nameAsLabelInput == "yes")
                    {
                        nameAsLabel = "Yes";
                        nameAsLabelAnswer = true;
                    }
                    else if (nameAsLabelInput == "N" || nameAsLabelInput == "n" || nameAsLabelInput == "No" || nameAsLabelInput == "no")
                    {
                        nameAsLabel = "No";
                        nameAsLabelAnswer = true;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid response, please try again.");
                    }
                }

                bool existingLabelAnswer = false;
                Console.WriteLine("\nAre there existing labels that need cleared? (Y or N)");
                while (existingLabelAnswer == false)
                {
                    string existingLabelInput = Console.ReadLine();
                    if (existingLabelInput == "Y" || existingLabelInput == "y" || existingLabelInput == "Yes" || existingLabelInput == "yes")
                    {
                        existingLabel = " ";
                        existingLabelAnswer = true;
                    }
                    else if (existingLabelInput == "N" || existingLabelInput == "n" || existingLabelInput == "No" || existingLabelInput == "no")
                    {
                        existingLabel = "";
                        existingLabelAnswer = true;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid response, please try again.");
                    }
                }
            }

            // Get CSV File Directory
            bool CSVDirectoryAnswer = false;
            Console.WriteLine("\nEnter path for the CSV file:");
            while (CSVDirectoryAnswer == false)
            {
                string CSVDirectory = Console.ReadLine();
                if (File.Exists(CSVDirectory))
                {
                    // Read CSV File
                    var metaNumber = new List<string>();
                    var metaName = new List<string>();
                    using (var reader = new StreamReader(CSVDirectory))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            metaNumber.Add(values[0]);
                            metaName.Add(values[1]);
                        }
                    }

                    // Set a Path to the Desktop
                    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    // Write configuration file
                    Console.WriteLine("\nExport File Name:");
                    string fileName = Console.ReadLine();
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName + ".txt")))
                    {
                        // Begin Configuration File Formatting
                        outputFile.WriteLine("<!--\nINFORMATION ABOUT THIS FILE\n\nThis exported file was created using the BluePrint GO Shift application in order to generate a configuration file for Polycom Expansion Modules. This file will be imported into an endpoint pack for the phone.\n-->\n\n<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        outputFile.WriteLine("<PhoneProfile xmlns=\"http://www.metaswitch.com/cp/clientxml\" Identity=\"" + phoneType + "\" SequenceNumber=\"1\" SIPPSVersion=\"2\">");
                        outputFile.WriteLine("  <ProfileSettings>");
                        outputFile.WriteLine("    <ProfileSetting Identity=\"MetaswitchSidecars\">" + expansionModuleType + "</ProfileSetting>");
                        for (var count = 1; count < metaNumber.Count; count++)
                        {

                            outputFile.WriteLine("    <ProfileSetting Identity=\"MetaswitchSoftKeyAction_" + expansionModuleStart + "\">" + softKeyAction + "</ProfileSetting>");
                            outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyLine_" + expansionModuleStart + "\">Line1</ProfileSetting>");
                            outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyXMLAppLine_" + expansionModuleStart + "\">1</ProfileSetting>");
                            if (softKeyAction == "SpeedDial")
                            {
                                outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyDial_" + expansionModuleStart + "\">" + metaNumber[count] + "</ProfileSetting>");
                            }
                            else
                            {
                                outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyDial_" + expansionModuleStart + "\"></ProfileSetting>");
                            }
                            outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyTransferDest_" + expansionModuleStart + "\"></ProfileSetting>");
                            outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyParkOrbit_" + expansionModuleStart + "\">MetaswitchNone</ProfileSetting>");
                            outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyUseSubscriberNameAsLabel_" + expansionModuleStart + "\">" + nameAsLabel + "</ProfileSetting>");
                            outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyXMLApp_" + expansionModuleStart + "\">MetaswitchNone</ProfileSetting>");
                            outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyMacro_" + expansionModuleStart + "\"></ProfileSetting>");
                            if (softKeyAction == "EnhancedMonitoredExtension")
                            {
                                outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyExtension_" + expansionModuleStart + "\">" + metaNumber[count] + "</ProfileSetting>");
                            }
                            else
                            {
                                outputFile.WriteLine("    <ProfileSetting Identity=\"SoftKeyExtension_" + expansionModuleStart + "\"></ProfileSetting>");
                            }
                            if (nameAsLabel == "Yes")
                            {
                                outputFile.WriteLine("    <ProfileSetting Identity=\"MetaswitchSoftKeyLabel_" + expansionModuleStart + "\">" + existingLabel + "</ProfileSetting>");
                            }
                            else
                            {
                                outputFile.WriteLine("    <ProfileSetting Identity=\"MetaswitchSoftKeyLabel_" + expansionModuleStart + "\">" + metaName[count] + "</ProfileSetting>");
                            }
                            expansionModuleStart = expansionModuleStart + 1;
                        }
                        outputFile.WriteLine("  </ProfileSettings>\n</PhoneProfile>");
                        // End Configuration File Formatting
                    }

                    Console.WriteLine("\nOutput Complete!");
                    return;
                }
                else
                {
                    Console.WriteLine("\nInvalid path entered or no file found, please try again.");
                }
            }
        }
    }
}
