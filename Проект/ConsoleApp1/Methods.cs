using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ConsoleApp1
{
    internal static class Methods
    {
        public static HashSet<Doctor> GetDoctors()
        {
            var doctors = new HashSet<Doctor>();
            var document = new XmlDocument();
            document.Load("Врачи.xml");
            var root = document.DocumentElement;
            foreach (XmlNode doc in root)
            {
                var att = doc.Attributes;
                var id = int.Parse(att.GetNamedItem("id").Value);
                var name = att.GetNamedItem("name").Value;
                var speciality = att.GetNamedItem("speciality").Value;
                var cabinet = int.Parse(att.GetNamedItem("cabinet").Value);
                var workHours = new Dictionary<DateTime, List<string>>();
                foreach (XmlNode workTime in doc.ChildNodes)
                {
                    var date = DateTime.Parse(workTime.Attributes.GetNamedItem("date").Value);
                    if (!workHours.ContainsKey(date))
                        workHours[date] = new List<string>();
                    foreach (XmlNode time in workTime.ChildNodes)
                    {
                        workHours[date].Add(time.InnerText);
                    }
                }
                var doctor = new Doctor(id, name, speciality, cabinet)
                {
                    WorkHours = workHours
                };
                doctors.Add(doctor);
            }
            return doctors;
        }

        public static void AddDoctor(Doctor doctor)
        {
            var document = new XmlDocument();
            document.Load("Врачи.xml");
            var root = document.DocumentElement;
            var xmlDoctor = document.CreateElement("doctor");

            var id = document.CreateAttribute("id");
            var name = document.CreateAttribute("name");
            var cabinetNumber = document.CreateAttribute("cabinet");
            var speciality = document.CreateAttribute("speciality");

            id.Value = doctor.Id.ToString();
            name.Value = doctor.Name;
            cabinetNumber.Value = doctor.CabinetNumber.ToString();
            speciality.Value = doctor.Speciality;

            xmlDoctor.Attributes.Append(id);
            xmlDoctor.Attributes.Append(name);
            xmlDoctor.Attributes.Append(cabinetNumber);
            xmlDoctor.Attributes.Append(speciality);
            
            foreach (var date in doctor.WorkHours.Keys)
            {
                var workHours = document.CreateElement("workHours");
                var dateAtt = document.CreateAttribute("date");
                dateAtt.Value = date.ToLongDateString();
                workHours.Attributes.Append(dateAtt);
                foreach (var time in doctor.WorkHours[date])
                {
                    var xmlTime = document.CreateElement("time");
                    var textTime = document.CreateTextNode(time);
                    xmlTime.AppendChild(textTime);
                    workHours.AppendChild(xmlTime);
                }
                xmlDoctor.AppendChild(workHours);
            }

            root.AppendChild(xmlDoctor);
            document.Save("Врачи.xml");
        }

        public static HashSet<Record> GetRecords()
        {
            var records = new HashSet<Record>();
            var document = new XmlDocument();
            document.Load("Записи.xml");
            var root = document.DocumentElement;
            foreach (XmlNode rec in root)
            {
                var id = int.Parse(rec.Attributes.GetNamedItem("id").Value);
                var doctorID = int.Parse(rec.SelectSingleNode("doctorID").InnerText);
                var date = DateTime.Parse(rec.SelectSingleNode("date").InnerText);
                var time = rec.SelectSingleNode("time").InnerText;
                var patient = GetPatient(rec.SelectSingleNode("patient"));
                records.Add(new Record(id, doctorID, date, time, patient));
            }
            return records;
        }

        private static Person GetPatient(XmlNode pacient)
        {
            var name = pacient.Attributes.GetNamedItem("name").Value;
            var polis = pacient.SelectSingleNode("polis").InnerText;
            var phone = pacient.SelectSingleNode("phone").InnerText;
            var birthDate = pacient.SelectSingleNode("birthDate").InnerText;
            var gender = pacient.SelectSingleNode("gender").InnerText;
            return new Person(name, polis, phone, birthDate, gender);
        }

        public static void AddRecord(Record record)
        {
            var document = new XmlDocument();
            document.Load("Записи.xml");
            var root = document.DocumentElement;
            var xmlRecord = document.CreateElement("record");

            var id = document.CreateAttribute("id");
            id.Value = record.Id.ToString();
            xmlRecord.Attributes.Append(id);

            var doctorID = document.CreateElement("doctorID");
            var textDoctorID = document.CreateTextNode(record.DoctorID.ToString());
            doctorID.AppendChild(textDoctorID);
            xmlRecord.AppendChild(doctorID);

            var date = document.CreateElement("date");
            var textDate = document.CreateTextNode(record.Date.ToLongDateString());
            date.AppendChild(textDate);
            xmlRecord.AppendChild(date);

            var time = document.CreateElement("time");
            var textTime = document.CreateTextNode(record.Time);
            time.AppendChild(textTime);
            xmlRecord.AppendChild(time);

            //Добавим данные пациента

            var patient = document.CreateElement("patient");

            var name = document.CreateAttribute("name");
            name.Value = record.Patient.Name;
            patient.Attributes.Append(name);

            var polis = document.CreateElement("polis");
            var textPolis = document.CreateTextNode(record.Patient.Polis);
            polis.AppendChild(textPolis);
            patient.AppendChild(polis);

            var phone = document.CreateElement("phone");
            var textPhone = document.CreateTextNode(record.Patient.PhoneNumber);
            phone.AppendChild(textPhone);
            patient.AppendChild(phone);

            var birthDate = document.CreateElement("birthDate");
            var textBirthDate = document.CreateTextNode(record.Patient.BirthDate);
            birthDate.AppendChild(textBirthDate);
            patient.AppendChild(birthDate);

            var gender = document.CreateElement("gender");
            var textGender = document.CreateTextNode(record.Patient.Gender);
            gender.AppendChild(textGender);
            patient.AppendChild(gender);

            xmlRecord.AppendChild(patient);

            root.AppendChild(xmlRecord);
            document.Save("Записи.xml");
        }

        public static void UpdataDoctors(HashSet<Doctor> doctors)
        {
            var document = new XmlDocument();
            document.Load("Врачи.xml");
            var root = document.DocumentElement;
            root.RemoveAll();
            document.Save("Врачи.xml");
            foreach (var doctor in doctors)
            {
                AddDoctor(doctor);
            }
        }

        public static void UpdataRecords(HashSet<Record> records)
        {
            var document = new XmlDocument();
            document.Load("Записи.xml");
            var root = document.DocumentElement;
            root.RemoveAll();
            document.Save("Записи.xml");
            foreach (var record in records)
            {
                AddRecord(record);
            }
        }
    }
}
