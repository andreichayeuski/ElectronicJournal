using System;
using System.Collections.Generic;

namespace SHARED.Models
{
    public class OperationResult
    {
        public OperationResult()
        {
            ValidationMessages = new List<string>();
        }

        public bool Succeeded { get; set; }

        /// <summary>
        ///  Дополнительная информация - строка
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        ///     Используется для сообщений валидации
        /// </summary>
        public ICollection<string> ValidationMessages { get; set; }

        /// <summary>
        ///     Дополнительные параметры (например Id...)
        /// </summary>
        public Tuple<int,object> OtherParams{ get; set; }       
    }
}