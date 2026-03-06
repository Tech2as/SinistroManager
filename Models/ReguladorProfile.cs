namespace SinistroManager.Models;

public class ReguladorProfile
{
     public Guid Id { get; private set; }
     public Guid UserId { get; private set; }
     public string Cpf { get; private set; }

   public ReguladorProfile(Guid userId, string cpf)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Cpf = cpf;
    }
}