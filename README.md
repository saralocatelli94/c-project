# c-project
Mandatory exe2

---------------------------
RUN/INSTALL THE APPLICATION
---------------------------

It is possible to run the project in two different ways:
1) Uploading the solution MandatoryProject2.sl in visual studio. IMPORTANT: select debug x86
2) Installing the UWP on the computer.
Fot the second option you can find the installer called "Add-AppDevPackage"
in MandatoryProject2/AppPackages/MandatoryProject2_1.0.5.0_Debug_Test. The installer 
is a windows script. In order to run it right click on the file and press "execute with
PowerShell". When the shell is open, select the "Y" option and wait until the 
application is installed.


--------------------
USE THE APPLICATION
--------------------
1) Find the file containing serial numbers.
To enter a submission you need to use a serial number generated by the application.
You can find the txt file containing the serial numbers in the local folder of the 
application packages
(C:\Users\'current user name'\AppData\Local\Packages\24df9725-518f-4e5e-bfd0-ba2bb6f72cca_pfvj52c5bwg9j\LocalState)
where 24df9725-518f-4e5e-bfd0-ba2bb6f72cca_pfvj52c5bwg9j is the package of the app.
Use one of these serial numbers for the submissions. If the file is deleted, the application 
will generate a new file.
In that folder the pictures of the submissions and the xml file containing the data are stored.

2) Add a submission
To add a submission some controls of the parameters are made:
-the name and the surname must contain only letters
-the phone number must contains at least 10 numbers
-the email must be valid
-the date of birth must be at least 10 years ago
-the picture can't be empty
-the serial number must be valid and used only onc

3) Username and password for see the submissions
To see the submissions you can use the username "admin" and the password "password"
or you can use the username used for one submission with the associated serial number as password.
In that page you will see 10 submissions per page.
