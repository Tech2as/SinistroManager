namespace SinistroManager.Domain.Entities;
using SinistroManager.Domain.Enums;
public class Sinistro
{
     public Guid Id { get; private set; }
     public Guid OficinaId { get; private set; }
     public Guid ReguladorId { get; private set; }
     public string Chassi { get; private set; }
     public double ValorReparo { get; private set; }
     public bool Salvado { get; private set; }
     public SinistroStatus Status { get; private set; }
     public DateTime DataSinistro { get; private set; }

    public Sinistro(
        Guid oficinaId,
        Guid reguladorId,
        string chassi,
        double valorReparo,
        bool salvado,
        SinistroStatus status)
    {
        Id = Guid.NewGuid();
        OficinaId = oficinaId;
        ReguladorId = reguladorId;
        Chassi = chassi;
        ValorReparo = valorReparo;
        Salvado = salvado;
        Status = status;
        DataSinistro = DateTime.Now;
    }

}