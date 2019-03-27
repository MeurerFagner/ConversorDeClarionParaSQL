﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicativoAuxiliarSoftbus.Models;
using NUnit.Framework;
using Newtonsoft.Json;

namespace TesteAplicativoAuxiliarSoftbus
{
    [TestFixture]
    public class ConversorDeSentencaTest
    {
        #region METODO ListaCamposDaSentencaEmClarion
        [Test(Description = "Testa se uma sentenca simples com uma busca de um LONG tem seu campo convertido corretamente")]
        public void RetornaObservableCollectionComUmaVariavelLong()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where ID = '& format(wVariavel, @n_10)";
            ObservableCollection<VariavelCalrion> variaveisDeSentenca = new ObservableCollection<VariavelCalrion>
            {
                new VariavelCalrion
                {
                    NomeVariavel = "wVariavel",
                    Tipo = VariavelCalrion.TipoDeVariavel.Long
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retorna duas variaveis do tipo Data")]
        public void RetornaObservableCollectionComDuasVariaveisData()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Emissao between '& format(wDataInicial, @n_10) &' and '& format(wDataFinal, @n_10)";
            ObservableCollection<VariavelCalrion> variaveisDeSentenca = new ObservableCollection<VariavelCalrion>
            {
                new VariavelCalrion
                {
                    NomeVariavel = "wDataInicial",
                    Tipo = VariavelCalrion.TipoDeVariavel.Data
                },
                new VariavelCalrion
                {
                    NomeVariavel = "wDataFinal",
                    Tipo = VariavelCalrion.TipoDeVariavel.Data
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retorna duas variaveis do tipo Hora")]
        public void RetornaObservableCollectionComDuasVariaveisHora()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where HoraEmissao between '& format(wHoraInicial, @n_10) &' and '& format(wHoraFinal, @n_10)";
            ObservableCollection<VariavelCalrion> variaveisDeSentenca = new ObservableCollection<VariavelCalrion>
            {
                new VariavelCalrion
                {
                    NomeVariavel = "wHoraInicial",
                    Tipo = VariavelCalrion.TipoDeVariavel.Hora
                },
                new VariavelCalrion
                {
                    NomeVariavel = "wHoraFinal",
                    Tipo = VariavelCalrion.TipoDeVariavel.Hora
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se retorna uma variavel string")]
        public void RetornaObservableCollectionComUmavariavelString()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Nome = '''& clip(wNome) &'''' ";
            ObservableCollection<VariavelCalrion> variaveisDeSentenca = new ObservableCollection<VariavelCalrion>
            {
                new VariavelCalrion
                {
                    NomeVariavel = "wNome",
                    Tipo = VariavelCalrion.TipoDeVariavel.String
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se retorna uma vaiavel é retornada apenas uma vez mesmo reptindo na sentenca")]
        public void RetornaObservableCollectionComUmavariavelMesmoQueAVariavelrepitaNaSentenca()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Nome = '''& clip(wNome) &''' or Nome2 = '''& clip(wNome) &''' '";
            ObservableCollection<VariavelCalrion> variaveisDeSentenca = new ObservableCollection<VariavelCalrion>
            {
                new VariavelCalrion
                {
                    NomeVariavel = "wNome",
                    Tipo = VariavelCalrion.TipoDeVariavel.String
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se retornara uma variavel uma variavel Real")]
        public void RetornaObservableCollectionComUmaVariavelReal()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Valor >= '& wValor ";
            ObservableCollection<VariavelCalrion> variaveisDeSentenca = new ObservableCollection<VariavelCalrion>
            {
                new VariavelCalrion
                {
                    NomeVariavel = "wValor",
                    Tipo = VariavelCalrion.TipoDeVariavel.Real
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se retornara uma variavel de cada tipo")]
        public void RetornaObservableCollectionComUmaVariavelDeCadaTipo()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                             "' where Valor >= '& wValor &' and ''Sim'' = '''& clip(wString) &''' and '&|" +
                             "'       Data = '& format(wData,@n_10) &' and hora >= '& format(wHora1, @n_10) &' and '&|" +
                             "'       Codigo = '& format(GLO:Codigo, @n_10) ";
            ObservableCollection<VariavelCalrion> variaveisDeSentenca = new ObservableCollection<VariavelCalrion>
            {
                new VariavelCalrion
                {
                    NomeVariavel = "wValor",
                    Tipo = VariavelCalrion.TipoDeVariavel.Real
                },
                new VariavelCalrion
                {
                    NomeVariavel = "wString",
                    Tipo = VariavelCalrion.TipoDeVariavel.String
                },
                new VariavelCalrion
                {
                    NomeVariavel = "wData",
                    Tipo = VariavelCalrion.TipoDeVariavel.Data
                },
                new VariavelCalrion
                {
                    NomeVariavel = "wHora1",
                    Tipo = VariavelCalrion.TipoDeVariavel.Hora
                },
                new VariavelCalrion
                {
                    NomeVariavel = "GLO:Codigo",
                    Tipo = VariavelCalrion.TipoDeVariavel.Long
                },
            };
            variaveisDeSentenca = variaveisDeSentenca.OrderBy(p => p.NomeVariavel) as ObservableCollection<VariavelCalrion>;
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca).OrderBy(p => p.NomeVariavel) as ObservableCollection<VariavelCalrion>;
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }

        #endregion

        [Test(Description = "Testa a conversão da Sentença vinda do clarion para uma com valores definidos")]
        public void RetornaSentencaSQLConvertidaComVariosCamposNaoRepetidos()
        {
            string sentenca = "'select * from Tabela '&| " +
                             "' where Valor >= '& wValor &' and ''Sim'' = '''& clip(wString) &''' and '&|" +
                             "'       Data = '& format(wData,@n_10) &' and hora >= '& format(wHora1, @n_10) &' and '&|" +
                             "'       Codigo = '& format(GLO:Codigo, @n_10))";
            ObservableCollection<CampoDeSentenca> variaveisDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wValor",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Real,
                    Valor = "100.10"
                },
                new CampoDeSentenca
                {
                    NomeVariavel = "wString",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.String,
                    Valor = "Sim"
                },
                new CampoDeSentenca
                {
                    NomeVariavel = "wData",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Data,
                    Valor = "900000"
                },
                new CampoDeSentenca
                {
                    NomeVariavel = "wHora1",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Hora,
                    Valor = "88888888"
                },
                new CampoDeSentenca
                {
                    NomeVariavel = "GLO:Codigo",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Long,
                    Valor = "1100"
                },
            };
            string resultado = "select * from Tabela \n " +
                             " where Valor >= 100.10 and 'Sim' = 'Sim' and \n" +
                             "       Data = 900000 and hora >= 88888888 and \n" +
                             "       Codigo = 1100";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variaveisDeSentenca));
        }
        [Test(Description = "Testa Conversão para SQL de Sentenca com campos Reptidos")]
        public void RetornaSentencaComCamposRepitidos()
        {
            var sentenca = "select * from Tabelas '&|" +
                         "' where Codigo = '& format(wCodigo, @n_10) &' or '& format(wCodigo,@n_10) &' = 0'";
            var variavesDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wCodigo",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Long,
                    Valor = "1100"
                }
            };
            var resultado = "select * from Tabelas \n" +
                           " where Codigo = 1100 or 1100 = 0";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
        }
    }
}
