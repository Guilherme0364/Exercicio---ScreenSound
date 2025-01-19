namespace ScreenSound.Shared.Modelos.Modelos;

public class Musica
{
    public Musica(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; } // A interrogação indica que pode ser nula
    public virtual Artista? Artista { get; set; } // Irá relacionar o modelo (tabela) com o artista, passando uma variavel do tipo artista que contem o objeto artista relacionado à música

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");

    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";
    }
}