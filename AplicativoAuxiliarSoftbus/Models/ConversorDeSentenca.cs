using AplicativoAuxiliarSoftbus.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
//using TipoDeVariavel = AplicativoAuxiliarSoftbus.Models.VariavelClarion.TipoDeVariavel;

namespace AplicativoAuxiliarSoftbus.Models
{
    public class ConversorDeSentenca
    {
        public static ObservableCollection<VariavelClarion> ExtrairVariaveisCalrionDeSentenca(string sentenca)
        {
            ObservableCollection<VariavelClarion> colecaoDeVariaveisCalrion = new ObservableCollection<VariavelClarion>();
            if (string.IsNullOrEmpty(sentenca))
                return colecaoDeVariaveisCalrion;
            // BUSCA VARIAVEIS LONG
            Regex expressaoRegular = new Regex(@"(?<=((format)\())[A-Z\:\\_\\d]+(?=,)", RegexOptions.IgnoreCase);
            MatchCollection colecaoDeOcorrenciasDaExpressaoRegular = expressaoRegular.Matches(sentenca);
            foreach (Match ocorrenciaDeVariavelLong in colecaoDeOcorrenciasDaExpressaoRegular)
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel == ocorrenciaDeVariavelLong.Value) == 0)
                {
                    VariavelClarion variavelClarion = new VariavelClarion
                    {
                        NomeVariavel = ocorrenciaDeVariavelLong.Value,
                        Tipo = TipoDeVariavel.Long
                    };
                    if (variavelClarion.NomeVariavel.ToLower().Contains("data"))
                        variavelClarion.Tipo = TipoDeVariavel.Data;
                    else if (variavelClarion.NomeVariavel.ToLower().Contains("hora"))
                        variavelClarion.Tipo = TipoDeVariavel.Hora;

                    colecaoDeVariaveisCalrion.Add(variavelClarion);
                }
            }

            // BUSCA STRINGS
            expressaoRegular = new Regex(@"(?<=(('''\s{0,}&\s{0,}clip\()))[A-Z-\:\\_\d]+(?=(\)\s{0,}&\s?'''))", RegexOptions.IgnoreCase);
            foreach (Match ocorrenciaDeVariavelString in expressaoRegular.Matches(sentenca))
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel == ocorrenciaDeVariavelString.Value) == 0)
                {
                    VariavelClarion variavelCalrion = new VariavelClarion
                    {
                        NomeVariavel = ocorrenciaDeVariavelString.Value,
                        Tipo = TipoDeVariavel.String
                    };

                    colecaoDeVariaveisCalrion.Add(variavelCalrion);
                }

            }

            //BUSCA NÚMEROS REAIS
            expressaoRegular = new Regex(@"(?<=(&\s{0,}(?!(format\(|clip\())))[A-Z-\:-\\_\d]{1,}", RegexOptions.IgnoreCase);
            foreach (Match ocorrenciaDeVAriavelReal in expressaoRegular.Matches(sentenca))
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel == ocorrenciaDeVAriavelReal.Value) == 0)
                {
                    VariavelClarion variavelCalrion = new VariavelClarion
                    {
                        NomeVariavel = ocorrenciaDeVAriavelReal.Value,
                        Tipo = TipoDeVariavel.Real
                    };

                    colecaoDeVariaveisCalrion.Add(variavelCalrion);
                }

            }

            return colecaoDeVariaveisCalrion;
        }

        public static string ConverteSentencaClarionParaSQL(string sentencaClarion, ObservableCollection<VariavelClarion> variaveisCalrions)
        {
            string resultado = sentencaClarion;

            resultado = resultado.Replace("\n", "");
            resultado = Regex.Replace(resultado, @"'?\s?&\s?\|", "\n");

            var expressao = @"('?)(&\s{0,}(((format)|(clip))\()?)[A-Z-\:-_\d]{1,}\,?(\s?@n_10)?\)?\s?\&?'?";
            var rgx = new Regex(expressao, RegexOptions.IgnoreCase);
            foreach (Match item in rgx.Matches(resultado))
            {
                var campoDeSentenca = variaveisCalrions.FirstOrDefault(p => item.Value.Contains(p.NomeVariavel));
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
