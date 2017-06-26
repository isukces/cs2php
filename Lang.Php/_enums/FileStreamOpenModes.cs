namespace Lang.Php
{
    public enum FileStreamOpenModes
    {
        /// <summary>
        /// Open for reading only; place the file pointer at the beginning of the file. 
        /// </summary>
        [RenderValue("'r'")]
        READ_ONLY,

        /// <summary>
        /// Open for reading and writing; place the file pointer at the beginning of the file. 
        /// </summary>
        [RenderValue("'r+'")]
        READ_WRITE,

        /// <summary>
        /// Open for writing only; place the file pointer at the beginning of the file and truncate the file to zero length. If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'w'")]
        WRITE_ONLY,

        /// <summary>
        /// Open for reading and writing; place the file pointer at the beginning of the file and truncate the file to zero length. If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'w+'")]
        WRITE_READ,

        /// <summary>
        /// Open for writing only; place the file pointer at the end of the file. If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'a'")]
        APPEND,

        /// <summary>
        /// Open for reading and writing; place the file pointer at the end of the file. 
        /// If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'a+'")]
        APPEND_READ,


        /// <summary>
        /// Create and open for writing only; place the file pointer at the beginning of the file. If the file already exists, the fopen() call will fail by returning FALSE and generating an error of level E_WARNING. If the file does not exist, attempt to create it. This is equivalent to specifying O_EXCL|O_CREAT flags for the underlying open(2) system call. 
        /// </summary>
        [RenderValue("'x'")]
        X,
        /// <summary>
        /// Create and open for reading and writing; otherwise it has the same behavior as 'x'. 
        /// </summary>
        [RenderValue("'x+'")]
        XPLUS,
        /// <summary>
        /// Open the file for writing only. If the file does not exist, it is created. If it exists, it is neither truncated (as opposed to 'w'), nor the call to this function fails (as is the case with 'x'). The file pointer is positioned on the beginning of the file. This may be useful if it's desired to get an advisory lock (see flock()) before attempting to modify the file, as using 'w' could truncate the file before the lock was obtained (if truncation is desired, ftruncate() can be used after the lock is requested). 
        /// </summary>
        [RenderValue("'c'")]
        C,
        /// <summary>
        /// Open the file for reading and writing; otherwise it has the same behavior as 'c'. 
        /// </summary>
        [RenderValue("'c'")]
        CPLUS,







        /// <summary>
        /// Open for reading only; place the file pointer at the beginning of the file. 
        /// </summary>
        [RenderValue("'rb'")]
        BinaryReadOnly,

        /// <summary>
        /// Open for reading and writing; place the file pointer at the beginning of the file. 
        /// </summary>
        [RenderValue("'rb+'")]
        BINARY_READ_WRITE,

        /// <summary>
        /// Open for writing only; place the file pointer at the beginning of the file and truncate the file to zero length. If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'wb'")]
        BINARY_WRITE_ONLY,

        /// <summary>
        /// Open for reading and writing; place the file pointer at the beginning of the file and truncate the file to zero length. If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'wb+'")]
        BINARY_WRITE_READ,

        /// <summary>
        /// Open for writing only; place the file pointer at the end of the file. If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'ab'")]
        BINARY_APPEND,

        /// <summary>
        /// Open for reading and writing; place the file pointer at the end of the file. 
        /// If the file does not exist, attempt to create it. 
        /// </summary>
        [RenderValue("'ab+'")]
        BINARY_APPEND_READ,


        /// <summary>
        /// Create and open for writing only; place the file pointer at the beginning of the file. If the file already exists, the fopen() call will fail by returning FALSE and generating an error of level E_WARNING. If the file does not exist, attempt to create it. This is equivalent to specifying O_EXCL|O_CREAT flags for the underlying open(2) system call. 
        /// </summary>
        [RenderValue("'xb'")]
        BINARY_X,
        /// <summary>
        /// Create and open for reading and writing; otherwise it has the same behavior as 'x'. 
        /// </summary>
        [RenderValue("'xb+'")]
        BINARY_XPLUS,
        /// <summary>
        /// Open the file for writing only. If the file does not exist, it is created. If it exists, it is neither truncated (as opposed to 'w'), nor the call to this function fails (as is the case with 'x'). The file pointer is positioned on the beginning of the file. This may be useful if it's desired to get an advisory lock (see flock()) before attempting to modify the file, as using 'w' could truncate the file before the lock was obtained (if truncation is desired, ftruncate() can be used after the lock is requested). 
        /// </summary>
        [RenderValue("'cb'")]
        BINARY_C,
        /// <summary>
        /// Open the file for reading and writing; otherwise it has the same behavior as 'c'. 
        /// </summary>
        [RenderValue("'cb'")]
        BINARY_CPLUS,
    }
}
