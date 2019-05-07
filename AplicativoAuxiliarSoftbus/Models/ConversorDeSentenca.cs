using AplicativoAuxiliarSoftbus.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            Regex expressaoRegular = new Regex(RegexPattern.VARIAVEL_LONG, RegexOptions.IgnoreCase);
            MatchCollection colecaoDeOcorrenciasDaExpressaoRegular = expressaoRegular.Matches(sentenca);
            foreach (Match ocorrenciaDeVariavelLong in colecaoDeOcorrenciasDaExpressaoRegular)
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel.Trim() == ocorrenciaDeVariavelLong.Value.Trim()) == 0)
                {
                    VariavelClarion variavelClarion = new VariavelClarion
                    {
                        NomeVariavel = ocorrenciaDeVariavelLong.Value.Trim(),
                        Tipo = TipoDeVariavel.Long
                    };
                    if (variavelClarion.NomeVariavel.ToLower().Contains("data") || variavelClarion.NomeVariavel.ToLower().Contains("today()"))
                        variavelClarion.Tipo = TipoDeVariavel.Data;
                    else if (variavelClarion.NomeVariavel.ToLower().Contains("hora") || variavelClarion.NomeVariavel.ToLower().Contains("clock()"))
                        variavelClarion.Tipo = TipoDeVariavel.Hora;

                    colecaoDeVariaveisCalrion.Add(variavelClarion);
                }
            }

            // BUSCA STRINGS
            expressaoRegular = new Regex(RegexPattern.VARIAVEL_STRING, RegexOptions.IgnoreCase);
            foreach (Match ocorrenciaDeVariavelString in expressaoRegular.Matches(sentenca))
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel == ocorrenciaDeVariavelString.Value) == 0)
                {
                    VariavelClarion variavelCalrion = new VariavelClarion
                    {
                        NomeVariavel = ocorrenciaDeVariavelString.Value.Trim(),
                        Tipo = TipoDeVariavel.String
                    };

                    colecaoDeVariaveisCalrion.Add(variavelCalrion);
                }

            }

            //BUSCA NÚMEROS REAIS
            expressaoRegular = new Regex(RegexPattern.VARIAVEL_REAL, RegexOptions.IgnoreCase);
            foreach (Match ocorrenciaDeVAriavelReal in expressaoRegular.Matches(sentenca))
            {
                if (colecaoDeVariaveisCalrion.Count(p => p.NomeVariavel == ocorrenciaDeVAriavelReal.Value) == 0)
                {
                    VariavelClarion variavelCalrion = new VariavelClarion
                    {
                        NomeVariavel = ocorrenciaDeVAriavelReal.Value.Trim(),
                        Tipo = TipoDeVariavel.Real
                    };

                    colecaoDeVariaveisCalrion.Add(variavelCalrion);
                }

            }

            return colecaoDeVariaveisCalrion;
        }

        public static string ConverteSentencaClarionParaSQL(string sentencaClarion, ObservableCollection<VariavelClarion> variaveisCalrions)
        {
            sentencaClarion = RemoveFromStringRegexMatch(sentencaClarion, RegexPattern.QUEBRA_DE_LINHA);
            sentencaClarion = RemoveMetodoSQLCalrion(sentencaClarion);

            var rgx = new Regex(RegexPattern.VARIAVEIS_EM_SENTENCA, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match item in rgx.Matches(sentencaClarion))
            {
                var campoDeSentenca = variaveisCalrions.FirstOrDefault(p => item.Value.Contains(p.NomeVariavel));
                if (campoDeSentenca != null)
                    sentencaClarion = sentencaClarion.Replace(item.Value.Trim(), campoDeSentenca.Valor);
            }
            sentencaClarion = Regex.Replace(sentencaClarion, RegexPattern.FIM_DE_LINHA_CLARION, RegexPattern.QUEBRA_DE_LINHA);

            sentencaClarion = sentencaClarion.Replace("''", "]").Replace("'", "").Replace("]", "'").Replace("&", "");
            return sentencaClarion;
        }


        private static string RemoveMetodoSQLCalrion(string sentenca)
        {
            sentenca = RemoveFromStringRegexMatch(sentenca, RegexPattern.METODOS_SQL_CLARION);
            sentenca = RemoveFromStringRegexMatch(sentenca, @"^\(");
            if (Regex.Matches(sentenca, @"\(").Count < Regex.Matches(sentenca, @"\)").Count)
                sentenca = RemoveFromStringRegexMatch(sentenca, @"\)$");
            return sentenca;
        }

        private static string RemoveFromStringRegexMatch(string input, string pattern)
        {
            return Regex.Replace(input.Trim(), pattern, "", RegexOptions.IgnoreCase);
        }

        protected static class RegexPattern
        {
            private const string VARIAVEL_MATCH = @"[A-Z\:\,\\_\(\)\d\s]+";
            public const string VARIAVEL_LONG = @"(?<=((format)\())" + VARIAVEL_MATCH + @"(?=(,(\s+)?@))";
            public const string VARIAVEL_REAL = @"(?<=(&(?!(\s{0,}format\(|\s{0,}clip\())))" + VARIAVEL_MATCH;
            public const string QUEBRA_DE_LINHA = "\r\n";
            public const string METODOS_SQL_CLARION = @"(exeSQL\d?\('|send\(\w+\s*,')";
            public const string FIM_DE_LINHA_CLARION = @"'?\s?&\s?\|";
            public const string VARIAVEL_STRING = @"(?<=(('''\s{0,}&\s{0,}clip\()))" + VARIAVEL_MATCH + @"(?=(\)\s{0,}&\s?'''))";
            public const string VARIAVEIS_EM_SENTENCA = @"('&\s*|(?<=\|\s*))(((format)|(clip))\()?" + VARIAVEL_MATCH + @"(\s*@n_?\d+)?\)?\s*(&'|(?=&\s*))?";
        }

    }
}
