namespace Schedule.io.Models.ValueObjects
{
    public class PermissoesConvite
    {
        public bool ModificaEvento { get; private set; }
        public bool ConvidaUsuario { get; private set; }
        public bool VeListaDeConvidados { get; private set; }

        public void PodeModificarEvento()
        {
            ModificaEvento = true;
        }

        public void NaoPodeModificarEvento()
        {
            ModificaEvento = false;
        }

        public void PodeConvidar()
        {
            ConvidaUsuario = true;
        }

        public void NaoPodeConvidar()
        {
            ConvidaUsuario = false;
        }

        public void PodeVerListaDeConvidados()
        {
            VeListaDeConvidados = true;
        }

        public void NaoPodeVerListaDeConvidados()
        {
            VeListaDeConvidados = false;
        }

    }
}
