#define _SPECIALCODE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lang.Php
{
    [Skip]
    [ScriptName("\\mysqli_stmt")]
    public class MySQLiStatement
    {

        [ScriptName("affected_rows")]
        public int AffectedRows { get; set; }

        [ScriptName("num_rows")]
        public int NumRows { get; set; }
        /*
         * 
         * 
 
int $errno;
array $error_list;
string $error;
int $field_count;
int $insert_id;
 
int $param_count;
string $sqlstate;
         */
        #region Methods

        [DirectCall("->fetch")]
        public bool Fetch()
        {
            throw new NotImplementedException();
        }
        [DirectCall("->store_result")]
        public bool StoreResult()
        {
            throw new NotImplementedException();
        }

        public MySQLiParam<T> BindParam<T>()
        {
            return new MySQLiParam<T>();
        }

        public bool BindParams<T1>(out MySQLiParam<T1> p1)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47, out MySQLiParam<T48> p48)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47, out MySQLiParam<T48> p48, out MySQLiParam<T49> p49)
        {
            throw new NotImplementedException();
        }

        public bool BindParams<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47, out MySQLiParam<T48> p48, out MySQLiParam<T49> p49, out MySQLiParam<T50> p50)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1>(out MySQLiParam<T1> p1)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47, out MySQLiParam<T48> p48)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47, out MySQLiParam<T48> p48, out MySQLiParam<T49> p49)
        {
            throw new NotImplementedException();
        }

        public bool BindResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24, T25, T26, T27, T28, T29, T30, T31, T32, T33, T34, T35, T36, T37, T38, T39, T40, T41, T42, T43, T44, T45, T46, T47, T48, T49, T50>(out MySQLiParam<T1> p1, out MySQLiParam<T2> p2, out MySQLiParam<T3> p3, out MySQLiParam<T4> p4, out MySQLiParam<T5> p5, out MySQLiParam<T6> p6, out MySQLiParam<T7> p7, out MySQLiParam<T8> p8, out MySQLiParam<T9> p9, out MySQLiParam<T10> p10, out MySQLiParam<T11> p11, out MySQLiParam<T12> p12, out MySQLiParam<T13> p13, out MySQLiParam<T14> p14, out MySQLiParam<T15> p15, out MySQLiParam<T16> p16, out MySQLiParam<T17> p17, out MySQLiParam<T18> p18, out MySQLiParam<T19> p19, out MySQLiParam<T20> p20, out MySQLiParam<T21> p21, out MySQLiParam<T22> p22, out MySQLiParam<T23> p23, out MySQLiParam<T24> p24, out MySQLiParam<T25> p25, out MySQLiParam<T26> p26, out MySQLiParam<T27> p27, out MySQLiParam<T28> p28, out MySQLiParam<T29> p29, out MySQLiParam<T30> p30, out MySQLiParam<T31> p31, out MySQLiParam<T32> p32, out MySQLiParam<T33> p33, out MySQLiParam<T34> p34, out MySQLiParam<T35> p35, out MySQLiParam<T36> p36, out MySQLiParam<T37> p37, out MySQLiParam<T38> p38, out MySQLiParam<T39> p39, out MySQLiParam<T40> p40, out MySQLiParam<T41> p41, out MySQLiParam<T42> p42, out MySQLiParam<T43> p43, out MySQLiParam<T44> p44, out MySQLiParam<T45> p45, out MySQLiParam<T46> p46, out MySQLiParam<T47> p47, out MySQLiParam<T48> p48, out MySQLiParam<T49> p49, out MySQLiParam<T50> p50)
        {
            throw new NotImplementedException();
        }

        [DirectCall("->execute")]
        public bool Execute()
        {
            throw new NotImplementedException();
        }

        [DirectCall("->close")]
        public bool Close()
        {
            throw new NotImplementedException();
        }

        #endregion Methods

#if SPECIALCODE
        public static string _GENERATE()
        {

            string x = "";
            for (int i = 1; i <= 50; i++)
            {
                var p1 = string.Join(", ", Enumerable.Range(1, i).Select(a => "T" + a).ToArray());
                var p2 = string.Join(", ", Enumerable.Range(1, i).Select(a => string.Format("out MySQLiParam<T{0}> p{0}", a)).ToArray());
                x += string.Format(@"public bool BindParams<{0}>({1})
        {{
            throw new NotImplementedException();
        }}
", p1, p2);
            }
            Console.Write(x);

            x = "";
            for (int i = 1; i <= 50; i++)
            {
                var p1 = string.Join(", ", Enumerable.Range(1, i).Select(a => "T" + a).ToArray());
                var p2 = string.Join(", ", Enumerable.Range(1, i).Select(a => string.Format("out MySQLiParam<T{0}> p{0}", a)).ToArray());
                x += string.Format(@"public bool BindResult<{0}>({1})
        {{
            throw new NotImplementedException();
        }}
", p1, p2);
            }
            Console.Write(x);
            return x;
        }
#endif
    }
}
