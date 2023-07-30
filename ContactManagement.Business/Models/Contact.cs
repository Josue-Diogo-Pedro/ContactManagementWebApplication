namespace ContactManagement.Business.Models;

public class Contact
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public int EntityContact { get; set; }
    public string Email { get; set; }
}
