SocialContractMonitoringSystem for the Pamantasan ng Lungsod ng Valenzuela

MVVM Wpf architecture

Requirements to run:

Install the MYSQL Connector:
https://dev.mysql.com/downloads/connector/net/8.0.html

Add to the project as reference (You may need to remove the initial reference then re-add it before it works)

To let uploading work, on the server you set at startup, 
create the following folders:
PDFFolder
SocialContractsFolder 
SocialContractFolder (without an s) - On this specific folder, place the blank soocial contract form with the name SocialContractPDF

After creating, setup advanced sharing for the local network
Guide on how to Advance Share: 
https://www.digitalcitizen.life/share-libraries-or-folders-using-advanced-sharing/

On the Advanced Sharing window, click on "Permissions" then check all on the boxes below "Allow"

Before you start anything, make sure that network sharing is turned on for both (or all) pcs who's going to use the application (and the server, ofc) and turn off passwordsharing. if you know how to specifically do network sharing with a bunch of security, feel free, just make sure the other pc's can actually access the folders shared (instructions below). 

User Manual:
On the MainServer side, you need to install xampp to access the database directly (and import it), then it's up to you whether you want to install the apache and mysql service or just start it up with xampp. 
Create a User account. This UA will be the one to be used to access the database.
import the database, name it as socialcontractdb. 

Create three folders, ff are tha names:
PDFFolder
SocialContractsFolder
SocialcontractFolder (without an s, put the blank socialcontract pdf here).
Share these folders via the Share button and let others access it via Advanced Sharing. Adjust the permissions on both sharing methods so that the other pcs can access them. 


On the PC's connected to the server where you start up the application, do the following beforehand.
Go to cmd, type ipconfig, and remember the ipaddress of whatever connection you're going to use to connect to the mainserver (via LAN, or wireless, idk up to you, it works both ways from what I tested). 

Once you start up a fresh copy of the application, it will put up an error message and send you to a window that asks you to input ipaddress, dbname, username, and password.
The ipaddress needs the ipaddress you got earlier from cmd. Type it in. 
The dbname is socialcontractdb.
The username and password would be the user account you made earlier in the xampp server. Using Root doesn't work so don't try it. 
And you're all set.

The username and password for the application itself is below:
admin
12345678

There's only one account so don't forget it. You can add more if you want but you need to edit the database file itself, not to mention that the password itself is hashed. An idea to doing that would be creating a user account with the password you want, copying it via the database file and you're set. 

