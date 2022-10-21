using System;

namespace ReadAndSaveCSVFile
{
    public class ReadAndSaveFileException : Exception
    {
        #region member
        private string _exceptionMessage = string.Empty;
        #endregion

        /// <summary>
        ///Get _exceptionMessage
        /// </summary>
        public string ExceptionMessage
        {
            get
            {
                return this._exceptionMessage;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadAndSaveFileException"/> class.
        /// </summary>
        /// <param name="exceptionMessage">Exception message thrown</param>
        public ReadAndSaveFileException(string exceptionMessage)
        {
            this._exceptionMessage = exceptionMessage;
        }
    }
}
