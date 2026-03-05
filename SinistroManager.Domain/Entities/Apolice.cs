namespace SinistroManager.Domain.Entities;

public class Apolice
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string NumeroApolice { get; private set; }
    public decimal ValorCobertura { get; private set; }
    public DateTime DataHistorico { get; private set; }

    public Apolice(
        Guid userId,
        string numeroApolice,
        decimal valorCobertura
       )
    {
        Id = Guid.NewGuid();
        UserId = userId;
        NumeroApolice = numeroApolice;
        ValorCobertura = valorCobertura;
        DataHistorico = DateTime.Now;

    }
}