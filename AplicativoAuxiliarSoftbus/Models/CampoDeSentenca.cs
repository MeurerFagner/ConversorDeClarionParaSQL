using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicativoAuxiliarSoftbus.Models
{
    public class CampoDeSentenca : BindableBase
    {
        public enum TiposVariavelCalrion
        {
            Real,
            Long,
            String,
            Data,
            Hora,
        }
        private string _nomeVariavel;
        public string NomeVariavel
        {
            get { return _nomeVariavel; }
            set { SetProperty(ref _nomeVariavel, value); }
        }
        private TiposVariavelCalrion _tipo;
        public TiposVariavelCalrion Tipo
        {
            get { return _tipo; }
            set { SetProperty(ref _tipo, value); }
        }

        //INICIALMENTE SERÁ UMA STRING, MAIS A FRENTE DEVE SER ADAPITADO PARA USAR UM TIPO VARIAVEL
        private string _valor;
        public string Valor
        {
            get { return _valor; }
            set { SetProperty(ref _valor, value); }
        }
    }
}
