using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using TipoVariaveis = AplicativoAuxiliarSoftbus.Models.CampoDeSentenca.TiposVariavelCalrion;

namespace AplicativoAuxiliarSoftbus.Models
{
    public class ConversorDeSentenca
    {
        public static ObservableCollection<CampoDeSentenca> ListaCamposDaSentencaEmClarion(string sentenca)
        {
            ObservableCollection<CampoDeSentenca> camposDeSentenca = new ObservableCollection<CampoDeSentenca>();
            // BUSCA VARIAVEIS LONG
            Regex rgx = new Regex(@"(?<=((format)\())[A-Z\:\\_\\d]+(?=,)", RegexOptions.IgnoreCase);
            MatchCollection matchsVariaves = rgx.Matches(sentenca);
            foreach (Match item in matchsVariaves)
            {
                if (camposDeSentenca.Count(p => p.NomeVariavel == item.Value) == 0)
                {
                    CampoDeSentenca campoSentenca = new CampoDeSentenca
                    {
                        NomeVariavel = item.Value,
                        Tipo = TipoVariaveis.Long
                    };
                    if (campoSentenca.NomeVariavel.ToLower().Contains("data"))
                        campoSentenca.Tipo = TipoVariaveis.Data;
                    else if (campoSentenca.NomeVariavel.ToLower().Contains("hora"))
                        campoSentenca.Tipo = TipoVariaveis.Hora;

                    camposDeSentenca.Add(campoSentenca);
                }
            }

            // BUSCA STRINGS
            rgx = new Regex(@"(?<=(('''\s{0,}&\s{0,}clip\()))[A-Z-\:\\_\d]+(?=(\)\s{0,}&\s?'''))", RegexOptions.IgnoreCase);
            foreach (Match item in rgx.Matches(sentenca))
            {
                if (camposDeSentenca.Count(p => p.NomeVariavel == item.Value) == 0)
                {
                    var campoSentenca = new CampoDeSentenca
                    {
                        NomeVariavel = item.Value,
                        Tipo = TipoVariaveis.String
                    };

                    camposDeSentenca.Add(campoSentenca);
                }

            }

            //BUSCA NÚMEROS REAIS
            rgx = new Regex(@"(?<=(&\s{0,}(?!(format\(|clip\())))[A-Z-\:-\\_\d]{1,}", RegexOptions.IgnoreCase);
            foreach (Match item in rgx.Matches(sentenca))
            {
                if (camposDeSentenca.Count(p => p.NomeVariavel == item.Value) == 0)
                {
                    var campoSentenca = new CampoDeSentenca
                    {
                        NomeVariavel = item.Value,
                        Tipo = TipoVariaveis.Real
                    };

                    camposDeSentenca.Add(campoSentenca);
                }

            }

            return camposDeSentenca;
        }

        public static string ConverteSentencaClarionParaSQL(string sentencaClarion)
        {
            throw new NotImplementedException();
        }

    }
}
