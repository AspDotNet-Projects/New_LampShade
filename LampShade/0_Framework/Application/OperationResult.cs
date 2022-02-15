namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSuccedded { get; set; }
        public string Messege { get; set; }

        public OperationResult()
        {    ////By defult false
            IsSuccedded = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messege"></param> be in mani ke meghdare pishfraz gereftge
        /// age meghdar nadasht
        /// <returns></returns>
        public OperationResult Succedded(string messege = "عملیات با موققیت انجام شد.")
        {
            IsSuccedded = true;
            Messege = messege;
            ///return beshe hamin class
            return this;
        }
        public OperationResult Failed(string messege)
        {
            IsSuccedded = false;
            Messege = messege;
            ///return beshe hamin class
            return this;
        }
    }
}
