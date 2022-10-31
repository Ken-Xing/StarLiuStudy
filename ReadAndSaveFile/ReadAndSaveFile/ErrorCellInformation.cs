﻿using System;
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

        /// <summary>
        /// Initialize this._errorColumn
        /// </summary>
        public int ErrorColumn
        {
            get
            {
                return this._errorColumn;
            }
            set
            {
                this._errorColumn = value;
            }
        }

        /// <summary>
        /// Initialize this._errorRo
        /// </summary>
        public int ErrorRow
        {
            get
            {
                return this._errorRow;
            }
            set
            {
                this._errorRow = value;
            }
        }

        /// <summary>
        /// Initialize this._errorMessage
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                this._errorMessage = value;
            }
        }

        /// <summary>
        /// Initialize this._errorType
        /// </summary>
        public _errorTypeEnum ErrorType
        {
            get
            {
                return this._errorType;
            }
            set
            {
                this._errorType = value;
            }
        }

        /// <summary>
        /// a no-argument constructor for ErrorCellInformation
        /// </summary>
        public ErrorCellInformation()
        {

        }
    }
}
