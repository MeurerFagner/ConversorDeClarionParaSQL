using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicativoAuxiliarSoftbus.Models;
using NUnit.Framework;
using Newtonsoft.Json;
using AplicativoAuxiliarSoftbus.Enums;

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
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wVariavel",
                    Tipo = TipoDeVariavel.Long
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
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wDataInicial",
                    Tipo = TipoDeVariavel.Data
                },
                new VariavelClarion
                {
                    NomeVariavel = "wDataFinal",
                    Tipo = TipoDeVariavel.Data
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
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wHoraInicial",
                    Tipo = TipoDeVariavel.Hora
                },
                new VariavelClarion
                {
                    NomeVariavel = "wHoraFinal",
                    Tipo = TipoDeVariavel.Hora
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
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wNome",
                    Tipo = TipoDeVariavel.String
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
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wNome",
                    Tipo = TipoDeVariavel.String
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
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wValor",
                    Tipo = TipoDeVariavel.Real
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
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wValor",
                    Tipo = TipoDeVariavel.Real
                },
                new VariavelClarion
                {
                    NomeVariavel = "wString",
                    Tipo = TipoDeVariavel.String
                },
                new VariavelClarion
                {
                    NomeVariavel = "wData",
                    Tipo = TipoDeVariavel.Data
                },
                new VariavelClarion
                {
                    NomeVariavel = "wHora1",
                    Tipo = TipoDeVariavel.Hora
                },
                new VariavelClarion
                {
                    NomeVariavel = "GLO:Codigo",
                    Tipo = TipoDeVariavel.Long
                },
            };
            variaveisDeSentenca = variaveisDeSentenca.OrderBy(p => p.NomeVariavel) as ObservableCollection<VariavelClarion>;
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca).OrderBy(p => p.NomeVariavel) as ObservableCollection<VariavelClarion>;
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se retornara variaveis implicitas")]
        public void RetornaObservableCollectionComUmaVariavelDeCadaTipodeVariavelImplicita()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                             "' where Valor >= '& valor$ &' and ''Sim'' = '''& clip(string\") &''' and '&|" +
                             "'       Codigo = '& format(codigo#, @n_10) ";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "valor$",
                    Tipo = TipoDeVariavel.Real
                },
                new VariavelClarion
                {
                    NomeVariavel = "string\"",
                    Tipo = TipoDeVariavel.String
                },
                new VariavelClarion
                {
                    NomeVariavel = "codigo#",
                    Tipo = TipoDeVariavel.Long
                },
            };
            variaveisDeSentenca = variaveisDeSentenca.OrderBy(p => p.NomeVariavel) as ObservableCollection<VariavelClarion>;
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca).OrderBy(p => p.NomeVariavel) as ObservableCollection<VariavelClarion>;
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retornara um LONG sendo a variavel um metodo sem parametro")]
        public void RetornaObservableCollectionComUmaVariavelLongAPArtirDeUmMetodoSemParametro()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where ID = '& format(metodo(), @n_10)";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "metodo()",
                    Tipo = TipoDeVariavel.Long
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retornara um LONG sendo a variavel um metodo com parametro")]
        public void RetornaObservableCollectionComUmaVariavelLongAPArtirDeUmMetodoComParametro()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where ID = '& format(SoNumeros(wVariavel), @n_10)";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "SoNumeros(wVariavel)",
                    Tipo = TipoDeVariavel.Long
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retornara um LONG sendo a variavel um metodo com outro metodo como parametro")]
        public void RetornaObservableCollectionComUmaVariavelLongAPArtirDeUmMetodoDentroDeMetodo()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where ID = '& format(SoNumeros(clip(wVariavel)), @n_10)";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "SoNumeros(clip(wVariavel))",
                    Tipo = TipoDeVariavel.Long
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retornara um LONG sendo a variavel um metodo com varios parametros parametro")]
        public void RetornaObservableCollectionComUmaVariavelLongAPArtirDeUmMetodoComVariosParametros()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where ID = '& format(sub(wVariave,l,4), @n_10)";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "sub(wVariave,l,4)",
                    Tipo = TipoDeVariavel.Long
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retorna variaveis do tipo Data quando usa metodo today")]
        public void RetornaObservableCollectionComVariaveiDataQuandoEhMetodoToady()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where Emissao <= '& format(today(), @n_10)";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "today()",
                    Tipo = TipoDeVariavel.Data
                }
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }
        [Test(Description = "Testa se uma retorna variavel do tipo Hora quando for metodo clock")]
        public void RetornaObservableCollectionComVariavelHoraQandoEhMetodoClock()
        {
            string sentenca = "'select * from Tabela '&|\n" +
                              " where HoraEmissao <= '& format(clock(), @n_10)";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "clock()",
                    Tipo = TipoDeVariavel.Hora
                }                
            };
            var resultado = ConversorDeSentenca.ExtrairVariaveisCalrionDeSentenca(sentenca);
            Assert.AreEqual(JsonConvert.SerializeObject(variaveisDeSentenca), JsonConvert.SerializeObject(resultado));
        }


        #endregion

        #region METODO ConverteSentencaClarionParaSQL
        [Test(Description = "Testa a conversão da Sentença vinda do clarion para uma com valores definidos")]
        public void RetornaSentencaSQLConvertidaComVariosCamposNaoRepetidos()
        {
            string sentenca = "'select * from Tabela '&| " +
                             "' where Valor >= '& wValor &' and ''Sim'' = '''& clip(wString) &''' and '&|" +
                             "'       Data = '& format(wData,@n_10) &' and hora >= '& format(wHora1, @n_10) &' and '&|" +
                             "'       Codigo = '& format(GLO:Codigo, @n_10))";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wValor",
                    Tipo = TipoDeVariavel.Real,
                    Valor = "100.10"
                },
                new VariavelClarion
                {
                    NomeVariavel = "wString",
                    Tipo = TipoDeVariavel.String,
                    Valor = "Sim"
                },
                new VariavelClarion
                {
                    NomeVariavel = "wData",
                    Tipo = TipoDeVariavel.Data,
                    Valor = "900000"
                },
                new VariavelClarion
                {
                    NomeVariavel = "wHora1",
                    Tipo = TipoDeVariavel.Hora,
                    Valor = "88888888"
                },
                new VariavelClarion
                {
                    NomeVariavel = "GLO:Codigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "1100"
                },
            };
            string resultado = "select * from Tabela \r\n " +
                             " where Valor >= 100.10 and 'Sim' = 'Sim' and \r\n" +
                             "       Data = 900000 and hora >= 88888888 and \r\n" +
                             "       Codigo = 1100";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variaveisDeSentenca));
            sentenca = "'select * from Tabela '&  | " +
                             "' where Valor >= '& wValor &' and ''Sim'' = '''& clip(wString) &''' and '& |" +
                             "'       Data = '& format(wData,@n_10) &' and hora >= '& format(wHora1, @n_10) &' and '&|" +
                             "'       Codigo = '& format(GLO:Codigo, @n_10))";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variaveisDeSentenca),"TESTA COM ESPAÇO ENTRE & E |");
        }
        [Test(Description = "Testa Conversão para SQL de Sentenca com campos Reptidos")]
        public void RetornaSentencaComCamposRepitidos()
        {
            var sentenca = "select * from Tabelas '&|" +
                         "' where Codigo = '& format(wCodigo, @n_10) &' or '& format(wCodigo,@n_10) &' = 0'";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "1100"
                }
            };
            var resultado = "select * from Tabelas \r\n" +
                           " where Codigo = 1100 or 1100 = 0";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
        }
        [Test(Description = "Testa conversão de Senteça que possui quebra de linha(&|) logo apos a variavel")]
        public void RetornaSentecaComQuebraDeLinhaAposVariavel()
        {
            var sentenca = "'select * from Tabelas '&|" +
                         "' where Codigo = '& format(wCodigo, @n_10) &|" +
                         "' or Empresa = '& format(wEmpresa,@n_10) &|" +
                         "' order by 1'";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
                new VariavelClarion
                {
                    NomeVariavel = "wEmpresa",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "1100"
                }
            };
            var resultado = "select * from Tabelas \r\n" +
                           " where Codigo = 110\r\n" +
                           " or Empresa = 1100\r\n" +
                           " order by 1";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
        }
        [Test(Description = "Testa conversão de Senteça que possui variavel no inicio da linha")]
        public void RetornaSentecaComVariavelNoInicioDaLinha()
        {
            var sentenca = "'select * from Tabelas '&|" +
                 "' where Codigo = '& format(wCodigo, @n_10) &' or '&|" +
                  " format(wEmpresa,@n_10) &' = Empresa '&|" +
                  " clip(wOrder)";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
                new VariavelClarion
                {
                    NomeVariavel = "wEmpresa",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "1100"
                },
                new VariavelClarion
                {
                    NomeVariavel = "wOrder",
                    Tipo = TipoDeVariavel.String,
                    Valor = "order by 1"
                },
            };
            var resultado = "select * from Tabelas \r\n" +
                           " where Codigo = 110 or \r\n" +
                           " 1100 = Empresa \r\n" +
                           " order by 1";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
        }
        [Test(Description = "Testa conversão de Sentenca que possui format de tipos variados")]
        public void RetornaSentencaComTiposDeFormatsVariados()
        {
            var sentenca = "'select * from Tabelas '&|" +
                 "' where Codigo = '& format(wCodigo, @n_3) &' or '& format(wEmpresa,@n04) &' = Empresa'";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
                new VariavelClarion
                {
                    NomeVariavel = "wEmpresa",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "1100"
                }
            };
            var resultado = "select * from Tabelas \r\n" +
                           " where Codigo = 110 or 1100 = Empresa";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
        }
        [Test(Description = "Testa se converte para sentenca mesmo dentro do ExeSQL")]
        public void RetornaSentencaDentroDoExeSQL()
        {
            var sentenca = "exeSQL2('select * from Tabelas '&|" +
                           "' where Codigo = '& format(wCodigo, @n_10))";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
            };
            var resultado = "select * from Tabelas \r\n" +
                            " where Codigo = 110";

            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));

        }
        [Test(Description = "Testa se converte para sentenca mesmo dentro do SEND")]
        public void RetornaSentencaDentroDoSend()
        {
            var sentenca = "send(Tabelas,'select * from Tabelas '&|" +
                           "' where Codigo = '& format(wCodigo, @n_10))";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
            };
            var resultado = "select * from Tabelas \r\n" +
                            " where Codigo = 110";

            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
            sentenca = "send(Tabelas , 'select * from Tabelas '&|" +
                          "' where Codigo = '& format(wCodigo, @n_10))";

            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca),"TESTE COM ESPAÇO APÓS A VIRGULA");
        }
        [Test(Description = "Testa se converte para sentenca mesmo dentro do SEND com SelectCampos")]
        public void RetornaSentencaDentroDoSendComSelectCampos()
        {
            var sentenca = "send(Tabelas,SelectCampos('Tabelas') &|" +
                           "' where Codigo = '& format(wCodigo, @n_10))";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
            };
            var resultado = "select * from Tabelas \r\n" +
                            " where Codigo = 110";

            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
            sentenca = "send(Tabelas , 'select * from Tabelas '&|" +
                          "' where Codigo = '& format(wCodigo, @n_10))";

            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca), "TESTE COM ESPAÇO APÓS A VIRGULA");
        }
        [Test(Description = "Testa se converte para sentenca mesmo dentro do SEND com espaço após o nome da Tabelas")]
        public void RetornaSentencaDentroDoSendComEspacoAposNomeDaTabela()
        {
            var sentenca = "send(Tabelas  ,'select * from Tabelas '&|" +
                           "' where Codigo = '& format(wCodigo, @n_10))";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
            };
            var resultado = "select * from Tabelas \r\n" +
                            " where Codigo = 110";

            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));

        }

        [Test(Description = "Testa se converte para sentenca mantaendo a indentaçao")]
        public void RetornaSentencaMantendoIndentacao()
        {
            var sentenca = "'select * from Tabelas '&|" +
                           "' where Codigo = '& format(wCodigo, @n_10) &' and '&|" +
                           "'       Campo1 = campo1 and '&|" +
                           "      ' campo2 = Campo2";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "wCodigo",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
            };
            var resultado = "select * from Tabelas \r\n" +
                           " where Codigo = 110 and \r\n" +
                           "       Campo1 = campo1 and \r\n" +
                           "       campo2 = Campo2";

            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));

        }
        [Test(Description = "Testa a conversão da Sentença vinda do clarion com Metodos nas variaveis para uma com valores definidos")]
        public void RetornaSentencaSQLConvertidaComCamposComVariaveisVindoDeMetodos()
        {
            string sentenca = "'select * from Tabela '&| " +
                             "' where Valor >= '& metodo(parametro1, parametro2) &' and ''Sim'' = '''& clip(outrometodo(wString)) &''' and '&|" +
                             "'       Data = '& format(today(),@n_10) &' and hora >= '& format(clock(), @n_10) &' and '&|" +
                             "'       Codigo = '& format(SoNumeros(GLO:Codigo), @n_10))";
            ObservableCollection<VariavelClarion> variaveisDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "metodo(parametro1, parametro2)",
                    Tipo = TipoDeVariavel.Real,
                    Valor = "100.10"
                },
                new VariavelClarion
                {
                    NomeVariavel = "outrometodo(wString)",
                    Tipo = TipoDeVariavel.String,
                    Valor = "Sim"
                },
                new VariavelClarion
                {
                    NomeVariavel = "today()",
                    Tipo = TipoDeVariavel.Data,
                    Valor = "900000"
                },
                new VariavelClarion
                {
                    NomeVariavel = "clock()",
                    Tipo = TipoDeVariavel.Hora,
                    Valor = "88888888"
                },
                new VariavelClarion
                {
                    NomeVariavel = "SoNumeros(GLO:Codigo)",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "1100"
                },
            };
            string resultado = "select * from Tabela \r\n " +
                             " where Valor >= 100.10 and 'Sim' = 'Sim' and \r\n" +
                             "       Data = 900000 and hora >= 88888888 and \r\n" +
                             "       Codigo = 1100";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variaveisDeSentenca));
        }
        [Test(Description = "Testa conversão de Sentenca que possui variaveis implicitas de tipos variados")]
        public void RetornaSentencaComTiposDeVariaveisImplicitas()
        {
            string sentenca = "'select * from Tabela '&|" +
                             "' where Valor >= '& valor$ &' and ''Sim'' = '''& clip(string\") &''' and '&|" +
                             "'       Codigo = '& format(codigo#, @n_10) ";
            var variavesDeSentenca = new ObservableCollection<VariavelClarion>
            {
                new VariavelClarion
                {
                    NomeVariavel = "valor$",
                    Tipo = TipoDeVariavel.Real,
                    Valor = "100.1"
                },
                new VariavelClarion
                {
                    NomeVariavel = "string\"",
                    Tipo = TipoDeVariavel.String,
                    Valor = "Sim"
                },
                new VariavelClarion
                {
                    NomeVariavel = "codigo#",
                    Tipo = TipoDeVariavel.Long,
                    Valor = "110"
                },
            };
            var resultado = "select * from Tabela \r\n" +
                           " where Valor >= 100.1 and 'Sim' = 'Sim' and \r\n" +
                           "       Codigo = 110";
            Assert.AreEqual(resultado, ConversorDeSentenca.ConverteSentencaClarionParaSQL(sentenca, variavesDeSentenca));
        }
        #endregion
    }
}
