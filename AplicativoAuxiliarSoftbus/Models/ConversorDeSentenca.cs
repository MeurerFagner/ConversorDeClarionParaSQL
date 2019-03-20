using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicativoAuxiliarSoftbus.Models
{
    public class ConversorDeSentenca
    {
        public static ObservableCollection<CampoDeSentenca> ListaCamposDaSentencaEmClarion(string sentenca)
        {
            ObservableCollection<CampoDeSentenca> camposDeSentenca = new ObservableCollection<CampoDeSentenca>();
            string nomeVariavel = "";
            for (int x = 0; x < sentenca.Length; x++)
            {
                //if (sentenca.Substring(x,1) == "&" || sentenca.Substring(x, 1) == "|") continue;
                string subX = sentenca.Substring(x, 1);
                if (sentenca.Length == x+1) break;
                if (sentenca.Substring(x, 2) == "''")
                {
                    nomeVariavel = sentenca.Substring(sentenca.IndexOf("&", x)+1);

                    nomeVariavel = nomeVariavel.Remove(nomeVariavel.IndexOf("&")).Replace("clip","").Replace("(","").Replace(")","").Trim();
                    if (camposDeSentenca.FirstOrDefault(o => o.NomeVariavel == nomeVariavel) == null)
                    {
                        camposDeSentenca.Add(new CampoDeSentenca
                        {
                            NomeVariavel = nomeVariavel,
                            Tipo = CampoDeSentenca.TiposVariavelCalrion.String
                        });
                    }
                    x = sentenca.IndexOf("''", sentenca.IndexOf("'", sentenca.IndexOf("&", sentenca.IndexOf("&", x)))) + 2;
                }
                else if (sentenca.Substring(x, 1) == "'")
                {
                    for (int y = x+1; y < sentenca.Length; y++)
                    {
                        string subY = sentenca.Substring(y, 1);
                        if (sentenca.Substring(y, 1) == "|" || sentenca.Substring(y, 1) == "'")
                        {
                            x = y;
                            break;
                        }
                        if (sentenca.Substring(y,1) == "&")
                        {
                            for (int z = y + 1; z < sentenca.Length; z++)
                            {
                                if (sentenca.Substring(z,1) == "&" || sentenca.Length == z+1)
                                {
                                    nomeVariavel = sentenca.Substring(y + 1, z-y-1).Trim();
                                    var tipoVariavel = CampoDeSentenca.TiposVariavelCalrion.Real;
                                    if (nomeVariavel.Contains("format("))
                                    {
                                        nomeVariavel = nomeVariavel.Replace("format(", "").Replace(")", "").Replace("@n_10", "").Replace("@N_10", "").Replace(",","").Trim();
                                        tipoVariavel = CampoDeSentenca.TiposVariavelCalrion.Long;
                                        if (nomeVariavel.ToUpper().Contains("DATA"))
                                            tipoVariavel = CampoDeSentenca.TiposVariavelCalrion.Data;
                                        if (nomeVariavel.ToUpper().Contains("HORA"))
                                            tipoVariavel = CampoDeSentenca.TiposVariavelCalrion.Hora;

                                    }
                                    if (camposDeSentenca.FirstOrDefault(o => o.NomeVariavel == nomeVariavel) == null)
                                    {
                                        camposDeSentenca.Add(new CampoDeSentenca
                                        {
                                            NomeVariavel = nomeVariavel,
                                            Tipo = tipoVariavel
                                        });
                                    }
                                    y = z;
                                    x = y;
                                    break;
                                }
                            }
                        }
                    }
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
