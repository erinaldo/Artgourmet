namespace Artebit.Restaurante.Global.Modelo.Enum
{
    public enum StatusConta
    {
        Aberta = 1,
        Fechada = 2,
        Bloqueada = 3,
        Transferida = 4,
        Cancelada = 5
    }

    public enum StatusMesa
    {
        SemStatus = 0,
        Ocupada = 1,
        Livre = 2,
        Reservada = 3,
        Bloqueada = 4
    }
}