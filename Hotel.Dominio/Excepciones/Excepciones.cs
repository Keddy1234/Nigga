namespace Hotel.Dominio.Excepciones;

public class ReglaNegocioException : Exception
{
    public ReglaNegocioException(string mensaje) : base(mensaje) { }
}

public class NoEncontradoException : Exception
{
    public NoEncontradoException(string mensaje) : base(mensaje) { }
}

public class ConflictoException : Exception
{
    public ConflictoException(string mensaje) : base(mensaje) { }
}
