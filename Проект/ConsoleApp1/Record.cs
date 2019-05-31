using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Person
    {
        public readonly string Name;
        public readonly string Polis;
        public readonly string PhoneNumber;
        public readonly string BirthDate;
        public readonly string Gender;

        public Person(string name, string polis, string phoneNumber, string birthDate, string gender)
        {
            Name = name;
            Polis = polis;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Gender = gender;
        }
    }

    class Record
    {
        public readonly int Id;
        public readonly int DoctorID;
        public readonly DateTime Date;
        public readonly string Time;
        public readonly Person Patient;

        public Record(int id, int doctorID, DateTime date, string time, Person patient)
        {
            Id = id;
            DoctorID = doctorID;
            Date = date;
            Time = time;
            Patient = patient;
        }

        public override bool Equals(object obj)
        {
            return obj is Record record && Id == record.Id && record.DoctorID == DoctorID;
        }

        public override int GetHashCode()
        {
            return (Id + 71) * Patient.Name.GetHashCode() + DoctorID * 173;
        }
    }
}