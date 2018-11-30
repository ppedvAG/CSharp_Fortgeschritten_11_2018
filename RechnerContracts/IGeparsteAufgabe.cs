namespace RechnerContracts
{
    public interface IGeparsteAufgabe
    {
        double Zahl1 { get; }
        double Zahl2 { get; }
        char Operator { get; }
    }
}