namespace SinistroManager.Domain;

public class User
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string CPF { get; private set; }

    public User(string name, string email, string cpf)
    {
        Name = name;
        CPF = cpf; 
        Email = email;
    }
}