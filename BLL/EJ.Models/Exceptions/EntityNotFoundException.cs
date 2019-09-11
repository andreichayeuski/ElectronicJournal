using System;

namespace EJ.Models.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message):base(message)
        {

        }
    }
}