namespace Application.Parametros
{
    public class ParametrosDePeticion
    {
        public int NumeroDePagina { get; set; }
        public int RegistrosXPagina { get; set; }
        
        // setea numero y registro si no mandas nada al objeto
        public ParametrosDePeticion()
        {
            this.NumeroDePagina = 1;
            this.RegistrosXPagina = 10;
        }

        // si mandas valores revisa que sean mayores a los de default y los asigna
        public ParametrosDePeticion(int numeroDePagina, int registrosXPagina)
        {
            this.NumeroDePagina = numeroDePagina < 1 ? 1 : numeroDePagina;
            this.RegistrosXPagina = registrosXPagina < 10 ? 10 : registrosXPagina;
        }

    }
}
