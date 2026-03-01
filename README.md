# 🌟Mini Capstone Project  

**Grade 11 ICT - Starapple Group 5**  

A VB.NET HR Attendance & Login Desktop App using QR scanning and Microsoft Access.  
This project is our **mini capstone** for ICT, demonstrating how to use **Windows Forms, database connectivity, and QR scanning** in a simple HR system.  

---

## 🧾 Overview

This Human Resource application is a **Windows Forms (WinForms) desktop app** developed using **Visual Basic .NET (VB.NET)** within **Microsoft Visual Studio**. The app is designed to streamline attendance tracking and user authentication in a simple and user-friendly interface.

Key functionality includes:  
- **Webcam QR Code Scanning** – Users can scan their unique QR code to quickly retrieve their username, reducing manual input and errors.  
- **Credential Verification** – After scanning, the system prompts for a password to ensure secure login.  
- **Automated Attendance Logging** – Successfully verified users are recorded for the day’s attendance (Time-In), and duplicate entries for the same day are prevented.  
- **Database Management** – All user credentials and attendance data are stored securely in a **Microsoft Access database (`Login.accdb`)**, accessed through **OleDb** for reliable data handling.  

📌 **Key goals for this project:**  
- Gain hands-on experience with **VB.NET GUI development** using Windows Forms.  
- Learn to work with databases, including **Microsoft Access** and **OleDb connections**.  
- Implement **QR code scanning functionality** to simplify login and attendance processes.  
- Build a **basic HR system prototype** that can be extended or adapted for real-world use.  

This project serves as both a learning exercise for VB.NET programming and a practical demonstration of how QR-based attendance systems can be implemented in small-scale HR applications.

---

## 🧰 Features

This application is designed to simplify user authentication and attendance tracking while providing an intuitive Windows interface. Key features include:

- **✨ Scan QR Codes to Get Username**  
  Users can quickly scan a QR code to automatically retrieve their username, reducing manual entry and errors.  

- **🔒 Prompt for Password Verification**  
  After scanning the QR code, the system prompts the user to enter their password to confirm identity and ensure secure access.  

- **🕘 Record Attendance (Time-In)**  
  The application automatically logs attendance for the day when the user successfully verifies their credentials. It prevents duplicate entries for the same day to maintain accurate records.  

- **📊 Store Data in an Access Database (`Login.accdb`)**  
  All user credentials, attendance records, and related information are securely stored in a Microsoft Access database, enabling easy data management and retrieval.  

- **🪩 Navigate Between Multiple Forms (Form1 → Form8)**  
  Users can smoothly navigate through different sections of the application using multiple forms, each designed for specific tasks, improving workflow and usability.

---

## 🛠 Built With

This project was developed using a combination of programming languages, frameworks, tools, and libraries that enable a fully functional Windows application with database connectivity and QR scanning capabilities.

- **Visual Basic .NET (VB.NET)** – The main programming language used for building the application logic and interface.  
- **Windows Forms (WinForms)** – The GUI framework for designing and managing the application's graphical user interface.  
- **Microsoft Visual Studio** – The integrated development environment (IDE) used to write, debug, and compile the project.  
- **Microsoft Access Database (`.accdb`)** – The database system used to store user credentials and related data.  
- **AForge.Video / AForge.Video.DirectShow** – Libraries for handling video input, particularly for accessing and controlling the computer’s camera.  
- **ZXing / ZXing.Windows.Compatibility** – Libraries for reading and decoding QR codes through the camera input.  
- **OleDb (Object Linking and Embedding Database)** – The .NET data provider used to connect and interact with the Access database.  

These tools and libraries together provide the foundation for a robust, database-driven Windows application with camera and QR scanning functionality.

---

## 🚀 Getting Started  

### 📥 Clone the Project  
```git clone https://github.com/TonieC/LastOfSigbeans.git```


### 📂 Open in Visual Studio  

1. Open **Visual Studio**  
2. Go to **File → Open → Project/Solution**  
3. Select the `.slnx` file in the root folder  

### 📌 Set the Database Path  

Update the connection string in `Form1.vb` if your `Login.accdb` is in a different location:  
```"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=PATH_TO_Login.accdb"```

Replace `PATH_TO_Login.accdb` with your actual folder path.

---

## 📋 How to Use  

1. Run the project in Visual Studio (F5 or Start)  
2. Scan a QR code with the webcam  
3. Enter the correct password when prompted  
4. Attendance will be recorded if not already logged  

---

## 🛠 Extending the Project  

For future improvements:  
📍 Add Employee Management (CRUD forms)  
📍 Attendance Reports  
📍 Time-out Tracking  
📍 User Roles (Admin / Staff)  
📍 UI/UX improvements  

---

## 🤝 Contributors  

**Starapple Group 5 - Grade 11 ICT**  

- Kirsten Jules Espiritu (Project Manager)  
- Wenses Agpalza Cabucana  (Software Analyst)
- Jesstonie Cadizal  (Programmer)
- Kheith Zoleta  (Technical Documentation Analyst)
- Nayzie Justiniano  (Quality Assurance)
- Reiven Joyce Suratos  (Technical Documentation Analyst)
- Sedrick Bulado (Programmer)   

---

## 📄 License

This project is open-source and licensed under the MIT License.

You are free to:
- Use the project for any purpose, including personal, educational, or commercial use.
- Modify the project to suit your needs.
- Distribute the original or modified versions of the project.
- Sublicense your modifications under the same or compatible terms.

Conditions:
- You must include the original MIT License notice and copyright in any copy or substantial portion of the project.
- The project is provided "as is", without any warranty. The authors are not liable for any damages or issues arising from its use.

For full details, see the [MIT License text](https://opensource.org/licenses/MIT).
