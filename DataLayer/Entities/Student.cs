namespace DataLayer.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public int ClassId { get; set; }
    public Class Class { get; set; }
    public List<Grade> Grades { get; set; }

    public string Address { get; set; }
}

