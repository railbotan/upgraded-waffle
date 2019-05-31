using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var doctors = Methods.GetDoctors();
            var records = Methods.GetRecords(); 
            var doctor = new Doctor(5, "Иванов И. И.", "Окулист", 101);
            doctor.WorkHours.Add(new DateTime(2019, 06, 30), new List<string> { "10:00", "11:00", "12:00" });
            var record = new Record(2, 3, new DateTime(2019, 05, 30), "10:00", new Person("yf", "12", "12", "12.09.2000", "М"));
            doctors.Add(doctor);
            doctors.RemoveWhere(d => d.Id == 4);
            records.Add(record);
            Methods.AddDoctor(doctor);
            Methods.AddRecord(record);
            Methods.UpdataDoctors(doctors);
            Methods.UpdataRecords(records);
        }
    }
}
