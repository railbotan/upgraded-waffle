using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Doctor
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Speciality;
        public readonly int CabinetNumber;
        public Dictionary<DateTime, List<string>> WorkHours = new Dictionary<DateTime, List<string>>(); 

        public Doctor(int id, string name, string speciality, int cabinetNumber)
        {
            Id = id;
            Name = name;
            Speciality = speciality;
            CabinetNumber = cabinetNumber;
        }

        public override bool Equals(object obj)
        {
            return obj is Doctor doctor && Name.Equals(doctor.Name) && Id == doctor.Id;
        }

        public override int GetHashCode()
        {
            return (Id + 11) * Name.GetHashCode();
        }
    }
}
