using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class Patient
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Mobile { get; private set; }
    public int Age { get; private set; }
    public string Gender { get; private set; }
    public string Concern { get; private set; }

    public Patient(int id, string name, string mobile, int age, string gender, string concern)
    {
        Id = id;
        Name = name;
        Mobile = mobile;
        Age = age;
        Gender = gender;
        Concern = concern;
    }

    public Patient(string name, string mobile, int age, string gender, string concern)
    {
        Name = name;
        Mobile = mobile;
        Age = age;
        Gender = gender;
        Concern = concern;
    }
}

