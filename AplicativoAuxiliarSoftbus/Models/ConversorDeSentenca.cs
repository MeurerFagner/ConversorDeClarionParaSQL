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

        public static string ConverteSentencaClarionParaSQL(string sentencaClarion, ObservableCollection<CampoDeSentenca> campoDeSentencas)
        {
            string resultado = sentencaClarion;
            var expressao = @"('?)(&\s{0,}(((format)|(clip))\()?)[A-Z-\:-_\d]{1,}\,?(\s?@n_10)?\)?\s?\&?'?";
            var rgx = new Regex(expressao, RegexOptions.IgnoreCase);
            foreach (Match item in rgx.Matches(resultado))
            {
                var campoDeSentenca = campoDeSentencas.FirstOrDefault(p => item.Value.Contains(p.NomeVariavel));
                if (campoDeSentenca != null)
                    resultado = resultado.Replace(item.Value, campoDeSentenca.Valor);
            }
            resultado = Regex.Replace(resultado, @"'?\s?&\s?\|", "\n");
            resultado = Regex.Replace(resultado.Trim(), @"^\(", "");
            if (Regex.Matches(resultado, @"\(").Count < Regex.Matches(resultado, @"\)").Count)
                resultado = Regex.Replace(resultado.Trim(), @"\)$", "");

            resultado = resultado.Replace("''", "]").Replace("'", "").Replace("]", "'").Replace("&", "");
            return resultado;
        }

    }
}
