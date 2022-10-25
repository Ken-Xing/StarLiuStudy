using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReadAndSaveCSVFile.CSVFileHelper;

namespace ReadAndSaveFile
{
    public class ErrorCellInformation
    {
        #region member 
        #region Pulic member
        public enum _errorTypeEnum
        {
            emptyContent = 0,
            duplicateContent = 1,
            duplicateDbContent = 2,
            filedTypeError = 3,
            characterLengthError = 4,
            contentError = 5,
        }
        #endregion
        private int _errorColumn = 0;
        private int _errorRow = 0;
        private string _errorMessage = string.Empty;
        private _errorTypeEnum _errorType = 0;
        #endregion


        public int ErrorColumn { get => _errorColumn; set => _errorColumn = value; }
        public int ErrorRow { get => _errorRow; set => _errorRow = value; }
        public string ErrorMessage { get => _errorMessage; set => _errorMessage = value; }
        public _errorTypeEnum ErrorType { get => _errorType; set => _errorType = value; }

        public ErrorCellInformation()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(ErrorCellInformation errorCell)
        {
            ErrorCellInformation errorCellInformation = errorCell as ErrorCellInformation;
            return this.ErrorRow.CompareTo(errorCellInformation.ErrorRow);
        }
    }
}
