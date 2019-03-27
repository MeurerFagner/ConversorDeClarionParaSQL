using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using TipoVariaveis = AplicativoAuxiliarSoftbus.Models.VariavelCalrion.TipoDeVariavel;

namespace AplicativoAuxiliarSoftbus.Models
{
    public class ConversorDeSentenca
    {
        public static ObservableCollection<VariavelCalrion> ExtrairVariaveisCalrionDeSentenca(string sentenca)
        {
            ObservableCollection<VariavelCalrion> colecaoDeVariaveisCalrion = new ObservableCollection<VariavelCalrion>();
            // BUSCA VARIAVEIS LONG
            Regex expressaoRegular = new Regex(@"(?<=((format)\())[A-Z\:\\_\\d]+(?=,)", RegexOptions.IgnoreCase);
            MatchCollection colecaoDeOcorrenciasDaExpressaoRegular = expressaoRegular.Matches(sentenca);
            foreach (Match ocorrenciaDeVariavelLong in colecaoDeOcorrenciasDaExpressaoRegular)
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel == ocorrenciaDeVariavelLong.Value) == 0)
                {
                    VariavelCalrion variavelClarion = new VariavelCalrion
                    {
                        NomeVariavel = ocorrenciaDeVariavelLong.Value,
                        Tipo = TipoVariaveis.Long
                    };
                    if (variavelClarion.NomeVariavel.ToLower().Contains("data"))
                        variavelClarion.Tipo = TipoVariaveis.Data;
                    else if (variavelClarion.NomeVariavel.ToLower().Contains("hora"))
                        variavelClarion.Tipo = TipoVariaveis.Hora;

                    colecaoDeVariaveisCalrion.Add(variavelClarion);
                }
            }

            // BUSCA STRINGS
            expressaoRegular = new Regex(@"(?<=(('''\s{0,}&\s{0,}clip\()))[A-Z-\:\\_\d]+(?=(\)\s{0,}&\s?'''))", RegexOptions.IgnoreCase);
            foreach (Match ocorrenciaDeVariavelString in expressaoRegular.Matches(sentenca))
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel == ocorrenciaDeVariavelString.Value) == 0)
                {
                    VariavelCalrion variavelCalrion = new VariavelCalrion
                    {
                        NomeVariavel = ocorrenciaDeVariavelString.Value,
                        Tipo = TipoVariaveis.String
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
                    VariavelCalrion variavelCalrion = new VariavelCalrion
                    {
                        NomeVariavel = ocorrenciaDeVAriavelReal.Value,
                        Tipo = TipoVariaveis.Real
                    };

                    colecaoDeVariaveisCalrion.Add(variavelCalrion);
                }

            }

            return colecaoDeVariaveisCalrion;
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
