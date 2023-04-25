using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities;

public class Class : BaseEntity
{
    public string Name { get; set; }

    public List<Student> Students { get; set; }

    [NotMapped]
    public int StudentCount { get; set; }
}
