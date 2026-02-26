namespace SinistroManager.Domain.Entities;
public abstract class User
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string CPF { get; private set; }

    protected User(string name, string email, string cpf)
    {
        Name = name;
        Email = email;
        CPF = cpf;
    }
}