using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicativoAuxiliarSoftbus.Models;
using NUnit.Framework;

namespace TesteAplicativoAuxiliarSoftbus
{
    [TestFixture]
    public class ConversorDeSentencaTest
    {
        [Test(Description = "Testa se uma sentenca simples com uma busca de um LONG tem seu campo convertido corretamente")]
        public void RetornaObservableCollectionComUmaVariavelLong()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where ID = '& format(wVariavel, @n_10)";
            ObservableCollection<CampoDeSentenca> variaveisDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wVariavel",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Long
                }
            };
            Assert.AreEqual(variaveisDeSentenca, ConversorDeSentenca.ListaCamposDaSentencaEmClarion(sentenca));
        }
        [Test(Description ="Testa se uma retorna duas variaveis do tipo Data")]
        public void RetornaObservableCollectionComDuasVariaveisData()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Emissao between '& format(wDataInicial, @n_10) &' and '& format(wDataFinal, @n_10)";
            ObservableCollection<CampoDeSentenca> variaveisDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wDataInicial",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Data
                },
                new CampoDeSentenca
                {
                    NomeVariavel = "wDataFinal",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Data
                }
            };
            Assert.AreEqual(variaveisDeSentenca, ConversorDeSentenca.ListaCamposDaSentencaEmClarion(sentenca));
        }
        [Test(Description = "Testa se uma retorna duas variaveis do tipo Hora")]
        public void RetornaObservableCollectionComDuasVariaveisHora()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where HoraEmissao between '& format(wHoraInicial, @n_10) &' and '& format(wHoraInicial, @n_10)";
            ObservableCollection<CampoDeSentenca> variaveisDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wDataInicial",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Data
                },
                new CampoDeSentenca
                {
                    NomeVariavel = "wDataFinal",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Data
                }
            };
            Assert.AreEqual(variaveisDeSentenca, ConversorDeSentenca.ListaCamposDaSentencaEmClarion(sentenca));
        }
        [Test(Description = "Testa se retorna uma variavel string")]
        public void RetornaObservableCollectionComUmavariavelString()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Nome = '''& clip(wNome) &'''' ";
            ObservableCollection<CampoDeSentenca> variaveisDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wNome",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.String
                }
            };
            Assert.AreEqual(variaveisDeSentenca, ConversorDeSentenca.ListaCamposDaSentencaEmClarion(sentenca));
        }
        [Test(Description = "Testa se retorna uma vaiavel é retornada apenas uma vez mesmo reptindo na sentenca")]
        public void RetornaObservableCollectionComUmavariavelMesmoQueAVariavelrepitaNaSentenca()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Nome = '''& clip(wNome) &''' or Nome2 = '''& clip(wNome) &''' '";
            ObservableCollection<CampoDeSentenca> variaveisDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wNome",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.String
                }
            };
            Assert.AreEqual(variaveisDeSentenca, ConversorDeSentenca.ListaCamposDaSentencaEmClarion(sentenca));
        }
        [Test(Description = "Testa se retornara uma variavel uma variavel Real")]
        public void RetornaObservableCollectionComUmaVariavelReal()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Valor >= '& wValor ";
            ObservableCollection<CampoDeSentenca> variaveisDeSentenca = new ObservableCollection<CampoDeSentenca>
            {
                new CampoDeSentenca
                {
                    NomeVariavel = "wValor",
                    Tipo = CampoDeSentenca.TiposVariavelCalrion.Real
                }
            };
            Assert.AreEqual(variaveisDeSentenca, ConversorDeSentenca.ListaCamposDaSentencaEmClarion(sentenca));
        }
    }
}
