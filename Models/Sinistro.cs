using SinistroManager.Enums;

namespace SinistroManager.Models;

public class Sinistro
{
    public Guid Id { get; set; }
    public Guid OficinaId { get; set; }
    public Guid ReguladorId { get; set; }
    public string Chassi { get; set; } = "";
    public decimal ValorReparo { get; set; }
    public bool Salvado { get; set; }
    public SinistroStatus Status { get; set; }
    public DateTime DataSinistro { get; set; }

}
