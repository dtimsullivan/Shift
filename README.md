# Shift
Shift is a simple command line application that will take a preformatted CSV file and create a functioning import file for a [Polycom](https://www.poly.com/us/en) Sidecar on the [Metaswitch](https://www.metaswitch.com/) VVX Endpoint Pack.

## About
Metaswitch uses what they call an "Endpoint Pack" for building configurations for IP phones. As of the creation of this application, there was no easy way of reorganizing the values in the Polycom VVX Sidecar once set. If a customer wanted a sidecar to display their values in a different order, or if they simply wanted one removed, it meant having to make major changes to the configuration. This was especially tedious for long lists as each Sidecar can contain anywhere from 40 to 84 lines depending on the model. By making use of the Endpoint Pack's import function, the act of *shifting* a value in this list becomes much easier. This application will generate a compatible import file for use on the Endpoint Pack, hopefully allowing you to make all organization changes outside of the Endpoint Pack's GUI.

## How to Use
### Compatible Devices
This application supports the Polycom VVX 301, 311, 401, 411, 501, and 601. For the Sidecars (or "Expansion Module") you can choose between the [Paper](https://www.polycom.com/content/dam/polycom/common/documents/data-sheets/vvx-expansion-module-color-ds-enus.pdf) and the [Color](https://www.polycom.com/content/dam/polycom/common/documents/data-sheets/vvx-expansion-module-color-ds-enus.pdf) models of the Sidecar.

### Line States
You will have the choice between setting the line as a Speed Dial (SD) or Enhanced Monitored Extension (EME).

### Label
Each line will have a label that can be set. The application will ask you if you want to use the name you provided in the CSV as the label or not. It will also ask you if you want to clear out any existing labels. 

### CSV File Formatting
The Shift application expects that you will be providing a preformatted CSV file containing your contacts and phone numbers. The application assumes you are using a header to sort these values and will ignore the first line. 

Directory Number | Name
------------ | -------------
3075550106 | John Smith
3075550139 | Jane Doe

> **Note:** Make sure you don't have your CSV file open in any application before trying to use it in Shift.

### Output
The application will output the text file (with a name you defined) to the desktop of your device.
