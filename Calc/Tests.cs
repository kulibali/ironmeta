﻿//////////////////////////////////////////////////////////////////////
// $Id$
//
// Copyright (c) 2009, The IronMeta Project
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without 
// modification, are permitted provided that the following conditions 
// are met:
// 
//     * Redistributions of source code must retain the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer.
//     * Redistributions in binary form must reproduce the above 
//       copyright notice, this list of conditions and the following 
//       disclaimer in the documentation and/or other materials 
//       provided with the distribution.
//     * Neither the name of the IronMeta Project nor the names of its 
//       contributors may be used to endorse or promote products 
//       derived from this software without specific prior written 
//       permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS 
// "AS IS" AND  ANY EXPRESS OR  IMPLIED WARRANTIES, INCLUDING, BUT NOT 
// LIMITED TO, THE  IMPLIED WARRANTIES OF  MERCHANTABILITY AND FITNESS 
// FOR  A  PARTICULAR  PURPOSE  ARE DISCLAIMED. IN  NO EVENT SHALL THE 
// COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT  LIMITED TO, PROCUREMENT  OF SUBSTITUTE  GOODS  OR SERVICES; 
// LOSS OF USE, DATA, OR  PROFITS; OR  BUSINESS  INTERRUPTION) HOWEVER 
// CAUSED AND ON ANY THEORY OF  LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR  TORT (INCLUDING NEGLIGENCE  OR OTHERWISE) ARISING IN 
// ANY WAY OUT  OF THE  USE OF THIS SOFTWARE, EVEN  IF ADVISED  OF THE 
// POSSIBILITY OF SUCH DAMAGE.
//
//////////////////////////////////////////////////////////////////////

using Xunit;

namespace Calc
{

    public class Tests
    {

        CalcMatcher matcher = new CalcMatcher(c => (int)c, true);

        [Fact]
        public void Test_Empty()
        {
            Assert.False(matcher.Match("", "Additive").Success);
        }

        [Fact]
        public void Test_List()
        {
            Assert.True(matcher.Match("zero", "Zero").Success);
        }

        [Fact]
        public void Test_Expression()
        {
            var s = "1 + 1";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 2);
        }

        [Fact]
        public void Test_Add()
        {
            var s = "2 + 3";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 5);
        }

        [Fact]
        public void Test_Subtract()
        {
            var s = "123 - 20";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 103);
        }

        [Fact]
        public void Test_Multiplicative()
        {
            var s = "12 / 4";
            var res = matcher.Match(s, "Multiplicative");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 3);
        }

        [Fact]
        public void Test_Multiply()
        {
            var s = "3 * 4";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 12);
        }

        [Fact]
        public void Test_Divide()
        {
            var s = "12 / 3";
            var res = matcher.Match(s, "Expression");
            Assert.True(res.Success && res.NextIndex == s.Length && res.Result == 4);
        }

        [Fact]
        public void Test_Precedence()
        {
            var res = matcher.Match("4 + 3 * 2", "Expression");
            Assert.True(res.Success && res.Result == 10);
        }

    } // class Tests

} // namespace Calc
