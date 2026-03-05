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

     // Teste logica
        public UserRole UserRole { get; set; }

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

// Lógica de negócios para aprovar sinistro (APRENDIZADO) 
    public void AprovarSinistro()
    {
        if (Status != SinistroStatus.EmAnalise)
            throw new InvalidOperationException("Somente sinistros em análise podem ser aprovados.");

        if(UserRole != UserRole.Regulador)
            throw new UnauthorizedAccessException("Apenas reguladores podem aprovar sinistros.");
        
        Status = SinistroStatus.Aprovado;
    }

}