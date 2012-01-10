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
        /// Cria um número que será passado para extenso
        /// </summary>
        /// <param name="v">Valor decimal do número</param>
        public Numero(decimal v)
        {
            this._valor = v;
        }
    }
}
