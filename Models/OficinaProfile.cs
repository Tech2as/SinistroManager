namespace SinistroManager.Models;

public class OficinaProfile
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Cnpj { get; private set; }
    public string Address { get; private set; }
    public string NumberAddress { get; private set; }
    public string Cep { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PhoneNumber { get; private set; }

    public OficinaProfile(Guid userId, string cnpj, string address, string numberAddress,
        string cep, string city, string state, string phoneNumber)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Cnpj = cnpj;
        Address = address;
        NumberAddress = numberAddress;
        Cep = cep;
        City = city;
        State = state;
        PhoneNumber = phoneNumber;
    }
}