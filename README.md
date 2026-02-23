# 🌟 LastOfSigbeans - Mini Capstone Project  

**Grade 11 ICT - Starapple Group 5**  

A VB.NET HR Attendance & Login Desktop App using QR scanning and Microsoft Access.  
This project is our **mini capstone** for ICT, demonstrating how to use **Windows Forms, database connectivity, and QR scanning** in a simple HR system.  

---

## 🧾 Overview  

LastOfSigbeans is a **Windows Forms application** built with **Visual Basic .NET** in **Visual Studio**.  
It uses a webcam to scan **QR codes**, checks credentials against an **Access database**, and records attendance automatically.  

📌 Key goals for this project:  
- Learn VB.NET GUI development  
- Work with databases (Access + OleDb)  
- Implement QR code scanning for login and attendance  
- Create a simple HR system prototype  

---

## 🧰 Features  

✨ Scan QR codes to get username  
🔒 Prompt for password verification  
🕘 Record attendance (Time-In) if not already logged today  
📊 Store data in an Access database (`Login.accdb`)  
🪩 Navigate between multiple forms (Form1 → Form8)  

---

## 🛠 Built With  

- Visual Basic .NET (VB.NET)  
- Windows Forms (WinForms)  
- Microsoft Visual Studio  
- Microsoft Access Database (`.accdb`)  
- AForge.Video / AForge.Video.DirectShow (camera)  
- ZXing / ZXing.Windows.Compatibility (QR scanning)  
- OleDb (database connectivity)  

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

## 🗂 Database Structure  

**login table**  
- username (Text)  
- password (Text)  
- FullName (Text)  

**attendance table**  
- Username (Text)  
- FullName (Text)  
- LoginDate (Date)  
- LoginTime (Date/Time)  
- Status (Text)  

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

This project is open-source under the **MIT License**.
