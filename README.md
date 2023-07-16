# RFID Attendance Management System

![RFID Attendance Management System](readmeContent/cover.png)

## Introduction

The RFID Attendance Management System is a project designed to manage the attendance of students and teachers within an educational institution using RFID (Radio Frequency Identification) technology. The system consists of three fundamental subsystems:

1. **RFID System**: This system comprises RFID cards with tags and a reader-transmitter device that uses the RFID NFC Data Link protocol. It enables real-time attendance registration for both students and teachers in specific classrooms.

2. **Web System**: A web-based application that allows the management of attendance information stored in the database. It enables students to view their attendance records based on the class, and teachers can monitor real-time attendance in their classes and modify attendance records if necessary.

3. **Database**: The central storage for all student, teacher, classroom, attendance, and subject information, along with their interrelations.

## Sections

### Section 1: Implementing the RFID System

To reproduce the RFID System physically, follow these detailed steps:

1. Assemble the components as shown in FIGURE 1: ESP32 with Wi-Fi connectivity in Station mode and an RFID reader.
   ![RFID connections](images/connections.jpg)

2. Use the Arduino IDE to upload the RFID_Attendance.ino file to the ESP32, which facilitates the communication with the RFID reader.

3. Add images of the ESP32 connections and the RFID reader for better reference.

### Section 2: Installing and Configuring Web and Database Servers

For the installation and configuration of web and database servers, we recommend using XAMPP:

1. Download and install XAMPP from the official website.

2. Configure Apache and MySQL servers to run on the required ports.

3. Create a database using phpMyAdmin and import the provided "sistemaRFID.bak" file.

4. Elaborate on the installation process step-by-step, including relevant screenshots.

### Section 3: Creating a Local Network for All Subsystems

To connect all subsystems within the same network:

1. Obtain the IP and port of the Web Server and Database Server.

2. Update the RFID_Attendance.ino code with the obtained IP and port information for proper communication.

### Section 4: Installing the Normalized Database

To install the normalized database:

1. Explain in detail how to install the "sistemaRFID.bak" file using SQL Server Management Studio.

2. Provide guidance on verifying the successful installation of the database.

### Section 5: Installing the Web System

To install the Web System:

1. Create a guide to set up the API using Visual Studio for connecting to the database.

2. Detail the steps to deploy the dashboard using Visual Studio.

### Section 6: Initializing and Testing the Subsystems

Provide examples of usage:

1. How to add students and teachers to the database.

2. How to utilize the IoT Attendance Management System effectively.

3. Test scenarios and troubleshooting tips for potential issues.

## FAQ

1. **What hardware components are required for the RFID System?**
   The RFID System requires an ESP32 with Wi-Fi capability and an RFID reader.

2. **Can I use a different RFID reader for the system?**
   Yes, as long as the reader supports the RFID NFC Data Link protocol.

3. **How do I install the database backup file "sistemaRFID.bak"?**
   You can install it using SQL Server Management Studio. Detailed instructions can be found in Section 4.

4. **Can I deploy the Web System on a different web server?**
   Yes, as long as the server supports the required technologies (e.g., PHP, MySQL).

5. **How do I modify attendance records in the Web System?**
   Teachers can log in to the system and update attendance records for their classes.

6. **What happens if the Wi-Fi connection is lost?**
   The system may not be able to transmit real-time attendance data during the Wi-Fi outage. However, local attendance will still be recorded on the RFID reader.

7. **Is it possible to export attendance data from the Web System?**
   Yes, you can implement an export feature in the Web System to download attendance data in various formats.

8. **What security measures are in place to protect the system's data?**
   Ensure that you set strong passwords and employ secure network practices to safeguard the system's data.

9. **Can I use the system for multiple classrooms?**
   Yes, you can configure the system to work with multiple classrooms by expanding the database and web application.

## Conclusion

This README.md provides a comprehensive guide to setting up the IoT Attendance Management System with RFID technology. By following the step-by-step instructions in each section, users can successfully implement and test the system for efficient attendance management within an educational institution. If you encounter any issues or have further questions, please refer to the FAQ section or open an issue on the GitHub repository. Happy attendance management!
