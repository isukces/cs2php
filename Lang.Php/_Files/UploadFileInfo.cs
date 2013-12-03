using System;

namespace Lang.Php
{

    /*
    smartClass
    option NoAdditionalFile
    
    property Name string Name of uploaded file
    	attribute ScriptName "name"
    
    property Type string Mime type of uploaded file
    	attribute ScriptName "type"
    
    property TmpName string Temporary file name
    	attribute ScriptName "tmp_name"
    
    property Error int Error
    	attribute ScriptName "size"
    
    property Size int File size
    	attribute ScriptName "size"
    smartClassEnd
    */
    
    [Skip]
    [AsArray]
    public partial class UploadFileInfo
    {
        public const int UPLOAD_ERR_OK = 0;
        public const int UPLOAD_ERR_INI_SIZE = 1;
        public const int UPLOAD_ERR_FORM_SIZE = 2;
        public const int UPLOAD_ERR_NO_TMP_DIR = 6;
        public const int UPLOAD_ERR_CANT_WRITE = 7;
        public const int UPLOAD_ERR_EXTENSION = 8;
        public const int UPLOAD_ERR_PARTIAL = 3;
    }
}


// -----:::::##### smartClass embedded code begin #####:::::----- generated 2013-06-20 12:36
// File generated automatically ver 2013-03-16 17:00
// Smartclass.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0c4d5d36fb5eb4ac
namespace Lang.Php
{
    public partial class UploadFileInfo 
    {
        /*
        /// <summary>
        /// Tworzy instancję obiektu
        /// </summary>
        public UploadFileInfo()
        {
        }

        Przykłady użycia

        implement INotifyPropertyChanged
        implement INotifyPropertyChanged_Passive
        implement ToString ##Name## ##Type## ##TmpName## ##Error## ##Size##
        implement ToString Name=##Name##, Type=##Type##, TmpName=##TmpName##, Error=##Error##, Size=##Size##
        implement equals Name, Type, TmpName, Error, Size
        implement equals *
        implement equals *, ~exclude1, ~exclude2
        */
        #region Constants
        /// <summary>
        /// Nazwa własności Name; Name of uploaded file
        /// </summary>
        public const string PROPERTYNAME_NAME = "Name";
        /// <summary>
        /// Nazwa własności Type; Mime type of uploaded file
        /// </summary>
        public const string PROPERTYNAME_TYPE = "Type";
        /// <summary>
        /// Nazwa własności TmpName; Temporary file name
        /// </summary>
        public const string PROPERTYNAME_TMPNAME = "TmpName";
        /// <summary>
        /// Nazwa własności Error; Error
        /// </summary>
        public const string PROPERTYNAME_ERROR = "Error";
        /// <summary>
        /// Nazwa własności Size; File size
        /// </summary>
        public const string PROPERTYNAME_SIZE = "Size";
        #endregion Constants

        #region Methods
        #endregion Methods

        #region Properties
        /// <summary>
        /// Name of uploaded file
        /// </summary>
        [ ScriptName("name") ]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                name = value;
            }
        }
        private string name = string.Empty;
        /// <summary>
        /// Mime type of uploaded file
        /// </summary>
        [ ScriptName("type") ]
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                type = value;
            }
        }
        private string type = string.Empty;
        /// <summary>
        /// Temporary file name
        /// </summary>
        [ ScriptName("tmp_name") ]
        public string TmpName
        {
            get
            {
                return tmpName;
            }
            set
            {
                value = (value ?? String.Empty).Trim();
                tmpName = value;
            }
        }
        private string tmpName = string.Empty;
        /// <summary>
        /// Error
        /// </summary>
        [ ScriptName("size") ]
        public int Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }
        private int error;
        /// <summary>
        /// File size
        /// </summary>
        [ ScriptName("size") ]
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
        private int size;
        #endregion Properties

    }
}
