namespace SinistroManager.Models;

public class OficinaProfile
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Cnpj { get; private set; }
    public string Address { get; private set; }
    public string AddressNumber { get; private set; }
    public string Cep { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string PhoneNumber { get; private set; }

    private OficinaProfile() { }
    public OficinaProfile(Guid userId, string cnpj, string address, string addressNumber,
        string cep, string city, string state, string phoneNumber)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Cnpj = cnpj;
        Address = address;
        AddressNumber = addressNumber;
        Cep = cep;
        City = city;
        State = state;
        PhoneNumber = phoneNumber;
    }
}