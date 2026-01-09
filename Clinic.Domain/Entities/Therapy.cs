using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

public class Therapy
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Therapy(string name)
    {
        Name = name;
    }

    public Therapy(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

