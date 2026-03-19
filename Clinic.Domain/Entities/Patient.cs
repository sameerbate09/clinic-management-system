using System.Net;

namespace Clinic.Domain.Entities;

public class Patient
{
    public Guid PatientId { get; set; }
    public string Name { get; private set; }
    public string Mobile { get; private set; }
    public int Age { get; private set; }
    public string Gender { get; private set; }
    public string Concern { get; private set; }
    public string? BloodGroup { get; private set; }
    public Address? Address { get; set; }

    public Patient(Guid patientId, string name, string mobile, int age, string gender, string concern, string bloodGroup, Address? address)
    {
        PatientId = patientId;
        Name = name;
        Mobile = mobile;
        Age = age;
        Gender = gender;
        Concern = concern;
        BloodGroup = bloodGroup;
        Address = address;
    }

    public Patient(string name, string mobile, int age, string gender, string concern, string bloodGroup, Address? address)
    {
        Name = name;
        Mobile = mobile;
        Age = age;
        Gender = gender;
        Concern = concern;
        BloodGroup = bloodGroup;
        Address = address;
    }
}