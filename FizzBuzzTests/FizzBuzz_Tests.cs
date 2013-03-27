using System;
using System.Collections.Generic;
using System.Linq;

using FizzBuzzLibrary;

using NUnit.Framework;

namespace FizzBuzzTests
{
    public class FizzBuzz_Tests
    {
    }

    [TestFixture]
    public class when_calling_execute_with_a_valid_starting_and_ending_number_and_default_subs
    {
        private List<string> _result;
        [SetUp]
        public void Setup()
        {
            var fizzBuzz = new FizzBuzz();
            fizzBuzz.includeDefaultSubs = true;
            this._result = fizzBuzz.Execute(5,10).ToList();
        }

        [Test]
        public void should_return_an_array_with_proper_Buzz_items()
        {
            Assert.AreEqual("buzz", _result[0]);
            Assert.AreEqual("buzz", _result[5]);
        }

        [Test]
        public void should_return_an_array_with_proper_fizz_items()
        {
            Assert.AreEqual("fizz", _result[1]);
            Assert.AreEqual("fizz", _result[4]);
        }

        [Test]
        public void should_return_an_array_with_proper_int_items()
        {
            Assert.AreEqual("7", _result[2]);
            Assert.AreEqual("8", _result[3]);
        }
    }

    [TestFixture]
    public class when_calling_execute_with_invalid_starting_and_ending_number
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void should_throw_error_if_start_and_end_int_are_equal()
        {
            try
            {
                new FizzBuzz().Execute(1, 1).ToList();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("starting number must be less then ending number and must be positive", ex.Message);
                return;
            }
            Assert.Fail("exception was not thrown");
        }

        [Test]
        public void should_throw_error_if_start_and_end_int_are_in_wrong_order()
        {
            try
            {
                new FizzBuzz().Execute(5, 1).ToList();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("starting number must be less then ending number and must be positive", ex.Message);
                return;
            }
            Assert.Fail("exception was not thrown");
        }

        [Test]
        public void should_throw_error_if_start_int_is_negative()
        {
            try
            {
                new FizzBuzz().Execute(-1, 5).ToList();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("starting number must be less then ending number and must be positive", ex.Message);
                return;
            }
            Assert.Fail("exception was not thrown");
        }

        [Test]
        public void should_throw_error_if_end_int_is_negative()
        {
            try
            {
                new FizzBuzz().Execute(1, -5).ToList();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("starting number must be less then ending number and must be positive", ex.Message);
                return;
            }
            Assert.Fail("exception was not thrown");
        }
        
    }

    [TestFixture]
    public class when_calling_execute_with_default_subs
    {
        private List<string> _result;

        [SetUp]
        public void Setup()
        {
            this._result = new FizzBuzz().Execute(3, 5).ToList();
        }

        [Test]
        public void should_not_return_fizz_or_buzz()
        {
            Assert.AreEqual("3", _result[0]);
            Assert.AreEqual("5", _result[2]);
        }
    }

    [TestFixture]
    public class when_calling_execute_with_custom_subs
    {
        private List<string> _result;
        private FizzBuzz _SUT;

        [SetUp]
        public void Setup()
        {
            _SUT = new FizzBuzz();
            _SUT.includeDefaultSubs = true;
            _SUT.substitutionRules.Add(x=>x == 4 ? "hello":"");
            _SUT.Execute(3, 5);
            this._result = this._SUT.Execute(3,5).ToList();
        }

        [Test]
        public void should_not_return_hello()
        {
            Assert.AreEqual("hello", _result[1]);
        }

        [Test]
        public void should_not_interfere_with_defaults()
        {
            Assert.AreEqual("fizz", _result[0]);
            Assert.AreEqual("buzz", _result[2]);
        }
    }

    [TestFixture]
    public class when_calling_execute_with_custom_subs_which_conflict
    {
        private List<string> _result;

        private FizzBuzz _SUT;

        [SetUp]
        public void Setup()
        {
            _SUT = new FizzBuzz();
            _SUT.includeDefaultSubs = true;
            _SUT.substitutionRules.Add(x => x == 4 ? "hello" : "");
            _SUT.substitutionRules.Add(x => x == 4 ? "goodbye" : "");
            _SUT.Execute(3, 5);
            this._result = this._SUT.Execute(3, 5).ToList();
        }

        [Test]
        public void should_have_first_in_win()
        {
            Assert.AreEqual("hello", _result[1]);
        }
    }


    [TestFixture]
    public class when_calling_execute_with_custom_subs_which_conflict_with_defaults
    {
        private List<string> _result;
        private FizzBuzz _SUT;

        [SetUp]
        public void Setup()
        {
            _SUT = new FizzBuzz();
            _SUT.includeDefaultSubs = true;
            _SUT.substitutionRules.Add(x => x == 3 ? "hello" : "");
            _SUT.substitutionRules.Add(x => x == 3 ? "goodbye" : "");
            _SUT.Execute(3, 5);
            this._result = this._SUT.Execute(3, 5).ToList();
        }

        [Test]
        public void should_have_first_custom_in_win()
        {
            Assert.AreEqual("hello", _result[0]);
        }
    }
}
