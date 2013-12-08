using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    [ScriptName("\\mysqli_result")]
    public class MySQLiResult
    {
        #region Properties

        /// <summary>
        /// Position of the field cursor used for the last mysqli_fetch_field() call. 
        /// This value can be used as an argument to mysqli_field_seek().
        /// </summary>
        [DirectCall("-> field current_field")]
        public int CurrentField
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Number of fields from specified result set
        /// </summary>
        [DirectCall("-> field field_count")]
        public int FieldCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [DirectCall("-> field lengths")]
        public Falsable<int[]> Lengths
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Number of rows in the result set.
        /// </summary>
        [DirectCall("instance field num_rows")]
        public int NumRows
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Translated into fetch_assoc if T is class marked AsArray
        /// Translation defined in Lang.Php.Compiler.Translator.Node.MysqliTranslator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Fetch<T>(out T value)
        {
            // array fetch_assoc ( void )
            throw new NotImplementedException();
        }

        [DirectCall("->free")]
        public void Free()
        {

        }

        #endregion Properties

        /* Methods 
        bool data_seek ( int $offset )
        mixed fetch_all ([ int $resulttype = MYSQLI_NUM ] )
        mixed fetch_array ([ int $resulttype = MYSQLI_BOTH ] )
      
        object fetch_field_direct ( int $fieldnr )
        object fetch_field ( void )
        array fetch_fields ( void )
        object fetch_object ([ string $class_name [, array $params ]] )
        mixed fetch_row ( void )
        bool field_seek ( int $fieldnr )
        */
    }
}
