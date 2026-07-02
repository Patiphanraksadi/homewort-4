# 📄 Text/CSV Viewer (FileProcessing) - Homework Submission

## 📌 Overview
This project is a Text/CSV Viewer written in C#. It is developed as part of the FileProcessing homework assignment to handle large datasets efficiently.

**Student Name:** นายปฏิภาณ รักษาดี
**Student ID:** 6810301015

---

## 🚀 Features Implemented (Part A)
โปรแกรมนี้ได้รับการพัฒนาและปรับปรุงให้ตรงตาม Requirement ดังนี้:
- ✅ **Large Data Handling:** รองรับการโหลดข้อมูลจาก MalwareBazaar (~1 ล้าน records) โดยใช้ `StreamReader` เพื่อป้องกันปัญหา Memory เต็มและโปรแกรมค้าง (Program doesn't crash)
- ✅ **Partial Loading (m–n):** ผู้ใช้สามารถระบุช่วงบรรทัดที่ต้องการเริ่มต้น (m) และสิ้นสุด (n) เพื่อแสดงผลเฉพาะช่วงที่ต้องการได้
- ✅ **Filtering:** รองรับการคัดกรองข้อมูลตามประเภทไฟล์ (เช่น `exe`, `dll`, `txt`)
- ✅ **Combined Conditions (Bonus):** สามารถใช้งานการโหลดแบบระบุช่วง (m-n) ร่วมกับการคัดกรองข้อมูล (Filter) ได้ในเวลาเดียวกัน

---

## 🧪 Testing & Quality Assurance (Part B)
โปรเจกต์นี้มาพร้อมกับการทดสอบระบบ (Software Testing) ตามหลักการวิศวกรรมซอฟต์แวร์:
- มีการออกแบบ Test Case ครอบคลุมทั้ง **Normal case, Edge case และ Error case** (เช่น การใส่ค่า m > n)
- รายงานผลการทดสอบ (Test Report) ถูกบันทึกและสรุปผล Pass/Fail ไว้ในไฟล์ `Basev100.xlsx` ที่แนบมากับ Repository นี้แล้ว

---

## ▶️ How to Run
1. Open the project in **Visual Studio** or compatible IDE.
2. Build the solution.
3. ตรวจสอบให้แน่ใจว่าไฟล์ Dataset (เช่น `malware_dataset.csv`) อยู่ในโฟลเดอร์ที่ถูกต้อง
4. Run the program และทำตามคำแนะนำบนหน้าจอ:
   - `Enter start row (m):` ใส่เลขบรรทัดเริ่มต้น
   - `Enter end row (n):` ใส่เลขบรรทัดสิ้นสุด
   - `Enter file type filter:` ใส่คำที่ต้องการค้นหา (เช่น exe) หรือกด Enter เพื่อข้าม

---

## 📊 Data Source & ⚠️ Ethical Use Awareness
This project uses malware metadata from MalwareBazaar (https://bazaar.abuse.ch/), operated by abuse.ch. 
This data is used for **educational purposes only** to practice handling large CSV files. 

- As responsible software developers, this knowledge is meant to understand and defend, not to exploit.
- The original dataset structure and attribution remain unchanged where applicable.

---

## 📜 License
This project is licensed under the **MIT License**.