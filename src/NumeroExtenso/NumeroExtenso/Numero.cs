using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumeroExtenso
{
    /// <summary>
    /// Representa um número com seus digitos
    /// </summary>
    internal class Numero
    {
        /// <summary>
        /// Valor Decimal do Número
        /// </summary>
        private decimal _valor;

        /// <summary>
        /// Representa a parte inteira do número
        /// </summary>
        private int _valorInteiro;

        /// <summary>
        /// Representa a parte decimal do número
        /// </summary>
        private int _valorDecimal;

        /// <summary>
        /// String da parte inteira do numero
        /// </summary>
        private string _valorInteiroStr;


        /// <summary>
        /// Parte decimal do numero
        /// </summary>
        private string _valorDecimalStr;

        /// <summary>
        /// Monta a string extenso
        /// </summary>
        StringBuilder sb = new StringBuilder();

        /// <summary>
        /// Controla quando colocar um "e" entre os números
        /// </summary>
        bool conjucaoController = false;


        string[] unidades = new string[] 
            { "zero", "um", "dois", "tres", "quatro", "cinco", "seis", "sete", "oito", "nove" ,
            "dez", "onze", "doze", "treze", "quatorze", "quinze", "desesseis", 
            "dezesete", "dezoito", "dezenove" };

        string[] dezenas = new string[] 
        { "cem", "dez", "vinte", "trinta", "quarenta", 
            "cinquenta", "sessenta", "setenta", "oitenta", "noventa" };

        string[] centenas = new string[] 
        { "zero", "cento", "duzentos", "trezentos", "quatrocentos", "quinhentos", 
            "seiscentos", "setesenteso", "oitocentos", "novecentos" };

        /// <summary>
        /// Cria um número que será passado para extenso
        /// </summary>
        /// <param name="v">Valor decimal do número</param>
        public Numero(decimal v)
        {
            this._valor = v;
            this._valorInteiro = Convert.ToInt32(Math.Floor(v));
            this._valorInteiroStr = this._valorInteiro.ToString();
            this._valorDecimal = Convert.ToInt32(((v - this._valorInteiro) * 100));
            if (this._valorDecimal > 0)
            {
                this._valorDecimalStr = this._valorDecimal.ToString("G2");
            }
        }

        public override string ToString()
        {
            return ToExtenso();
        }

        private string ToExtenso()
        {
            string moneyNameSingular = Resources.moneyNameSingular;
            string moneyNamePlural = Resources.moneyNamePlural;
            string centsNamePlural = Resources.centsNamePlural;
            string centsNameSingular = Resources.centsNameSingular;

            extensoBilhao();

            extensoMilhao();

            extensoMilhar();

            extensoCentenas();

            string moneyName = this._valorInteiro > 1 ? moneyNamePlural : moneyNameSingular;

            sb.AppendFormat(" {0}", moneyName);

            if (!string.IsNullOrWhiteSpace(this._valorDecimalStr))
            {
                sb.AppendFormat(" e");
                extensoCentavos();

                string centavosName = this._valorDecimal > 1 ? centsNamePlural : centsNameSingular;

                sb.AppendFormat(" {0}", centavosName);
            }



            return sb.ToString().Trim();
        }

        private void extensoCentavos()
        {
            if (!string.IsNullOrWhiteSpace(this._valorDecimalStr))
            {
                if (DezenaCentavos > 1)
                {
                    adicionaParteExtenso(dezenas[DezenaCentavos]);
                    conjucaoController = true;
                }

                if (UnidadeCentavos > 0 || DezenaCentavos == 1)
                {
                    adicionaParteExtenso(unidades[DezenaCentavos == 1 ? UnidadeCentavos + 10 : UnidadeCentavos]);
                }
            }
        }

        private void extensoCentenas()
        {
            if (Centena > 0)
            {
                if (Unidade == 0 && Dezena == 0)
                {
                    adicionaParteExtenso(dezenas[0]);
                }
                else
                {
                    adicionaParteExtenso(centenas[Centena]);
                }
                conjucaoController = true;
            }

            if (Dezena > 1)
            {
                adicionaParteExtenso(dezenas[Dezena]);
                conjucaoController = true;
            }

            if (Unidade > 0 || Dezena == 1)
            {
                adicionaParteExtenso(unidades[Dezena == 1 ? Unidade + 10 : Unidade]);
            }
        }

        private void extensoMilhar()
        {
            if (CentenaMilhar > 0)
            {
                if (UnidadeMilhar == 0 && DezenaMilhar == 0)
                {
                    adicionaParteExtenso(dezenas[0]);
                }
                else
                {
                    adicionaParteExtenso(centenas[CentenaMilhar]);
                }

                conjucaoController = true;
            }

            if (DezenaMilhar > 1)
            {
                adicionaParteExtenso(dezenas[DezenaMilhar]);
                conjucaoController = true;
            }

            if (UnidadeMilhar > 0 || DezenaMilhar == 1)
            {
                adicionaParteExtenso(unidades[DezenaMilhar == 1 ? UnidadeMilhar + 10 : UnidadeMilhar]);
                conjucaoController = true;
            }

            if (conjucaoController)
            {
                sb.AppendFormat(" mil");
            }
        }

        private void extensoMilhao()
        {
            if (CentenaMilhao > 0)
            {
                if (UnidadeMilhao == 0 && DezenaMilhao == 0)
                {
                    adicionaParteExtenso(dezenas[0]);
                }
                else
                {
                    adicionaParteExtenso(centenas[CentenaMilhao]);
                }

                conjucaoController = true;
            }

            if (DezenaMilhao > 1)
            {
                adicionaParteExtenso(dezenas[DezenaMilhao]);
                conjucaoController = true;
            }

            if (UnidadeMilhao > 0 || DezenaMilhao == 1)
            {
                adicionaParteExtenso(unidades[DezenaMilhao == 1 ? UnidadeMilhao + 10 : UnidadeMilhao]);
                conjucaoController = true;
            }

            if (conjucaoController)
            {
                sb.AppendFormat((UnidadeMilhao == 1 && DezenaMilhao == 0 && CentenaMilhao == 0) ? " milhão" : " milhões");
                conjucaoController = false;
            }

            if (Unidade == 0 && Dezena == 0 && Centena == 0 &&
                UnidadeMilhar == 0 && DezenaMilhar == 0 && CentenaMilhar == 0)
            {
                sb.AppendFormat(" de");
                conjucaoController = false;
            }
        }

        private void extensoBilhao()
        {
            if (CentenaBilhao > 0)
            {
                if (UnidadeBilhao == 0 && DezenaBilhao == 0)
                {
                    adicionaParteExtenso(dezenas[0]);
                }
                else
                {
                    adicionaParteExtenso(centenas[CentenaBilhao]);
                }

                conjucaoController = true;
            }

            if (DezenaBilhao > 1)
            {
                adicionaParteExtenso(dezenas[DezenaBilhao]);
                conjucaoController = true;
            }

            if (UnidadeBilhao > 0 || DezenaBilhao == 1)
            {
                adicionaParteExtenso(unidades[DezenaBilhao == 1 ? UnidadeBilhao + 10 : UnidadeBilhao]);
                conjucaoController = true;
            }

            if (conjucaoController)
            {
                sb.AppendFormat((UnidadeBilhao == 1 && DezenaBilhao == 0 && CentenaBilhao == 0) ? " bilhão" : " bilhões");
                conjucaoController = false;
            }

            if (Unidade == 0 && Dezena == 0 && Centena == 0 &&
                UnidadeMilhar == 0 && DezenaMilhar == 0 && CentenaMilhar == 0 &&
                UnidadeMilhao == 0 && DezenaMilhao == 0 && CentenaMilhao == 0)
            {
                sb.AppendFormat(" de");
                conjucaoController = false;
            }
        }

        private void adicionaParteExtenso(string str)
        {
            const string conjucao = "e";

            sb.AppendFormat(" {0} {1}", conjucaoController ? conjucao : string.Empty, str);

            conjucaoController = false;
        }

        public int Unidade
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 1, 1));
                }
                return result;
            }
        }

        public int Dezena
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 2)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 2, 1));
                    }
                }
                return result;
            }
        }

        public int Centena
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 3)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 3, 1));
                    }
                }
                return result;
            }
        }

        public int UnidadeMilhar
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 4)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 4, 1));
                    }
                }
                return result;
            }
        }

        public int DezenaMilhar
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 5)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 5, 1));
                    }
                }
                return result;
            }
        }

        public int CentenaMilhar
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 6)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 6, 1));
                    }
                }
                return result;
            }
        }

        public int UnidadeMilhao
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 7)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 7, 1));
                    }
                }
                return result;
            }
        }

        public int DezenaMilhao
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 8)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 8, 1));
                    }
                }
                return result;
            }
        }

        public int CentenaMilhao
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 9)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 9, 1));
                    }
                }
                return result;
            }
        }

        public int UnidadeBilhao
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 10)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 10, 1));
                    }
                }
                return result;
            }
        }

        public int DezenaBilhao
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 11)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 11, 1));
                    }
                }
                return result;
            }
        }

        public int CentenaBilhao
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorInteiroStr))
                {
                    if (_valorInteiroStr.Length >= 11)
                    {
                        result = Convert.ToInt32(_valorInteiroStr.Substring(_valorInteiroStr.Length - 11, 1));
                    }
                }
                return result;
            }
        }

        public int DezenaCentavos
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorDecimalStr))
                {
                    if (_valorDecimalStr.Length >= 2)
                    {
                        result = Convert.ToInt32(_valorDecimalStr.Substring(_valorDecimalStr.Length - 2, 1));
                    }
                }
                return result;
            }
        }

        public int UnidadeCentavos
        {
            get
            {
                int result = 0;
                if (!string.IsNullOrWhiteSpace(_valorDecimalStr))
                {
                    if (_valorDecimalStr.Length >= 1)
                    {
                        result = Convert.ToInt32(_valorDecimalStr.Substring(_valorDecimalStr.Length - 1, 1));
                    }
                }
                return result;
            }
        }
    }
}
