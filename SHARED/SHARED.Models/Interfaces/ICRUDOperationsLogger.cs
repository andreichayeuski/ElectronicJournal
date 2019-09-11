namespace SHARED.Models.Interfaces
{
    public interface ICRUDOperationsLogger
    {
        void SuccessCreate(string message);

        void SuccessUpdate(string message);

        void SuccessDelete(string message);

        void FailureCreate(string message);

        void FailureUpdate(string message);

        void FailureDelete(string message);
    }
    
}