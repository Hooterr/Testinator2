using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testinator.Server.TestSystem.Implementation;
using Xunit;

namespace Testinator.TestSystem.Implementation.Test.ErrorListener
{
    public class ErrorListenerTests
    {
        internal static ErrorListenerTestImplementation CreateNewListener => new ErrorListenerTestImplementation();

        [Fact]
        public void HandleErrorForGoodProperty()
        {
            var actionRan = false;
            var el = CreateNewListener;
            el.OnErrorFor(x => x.Prop1, (msg) => actionRan = true);
            el.HandleErrorFor(x => x.Prop1, "error msg");
            Assert.True(actionRan);
        }

        [Fact]
        public void OnErrorForWrongProperty()
        {
            var el = CreateNewListener;
            Assert.Throws<NotSupportedException>(() => 
                el.OnErrorFor(x => x.GetType(), (msg) => { }));
        }

        [Fact]
        public void OnErrorForPropertyPropertyWithoutAttribute()
        {
            var el = CreateNewListener;
            Assert.Throws<ArgumentException>(() =>
                el.OnErrorFor(x => x.PropNotEditorProp1, (msg) => { }));
        }

        [Fact]
        public void HandleErrorForPropertyActionOnErrorNotRegistered()
        {
            var el = CreateNewListener;
            el.HandleErrorFor(x => x.Prop1, "error");
            var unhandledErrors = el.GetUnhandledErrors();
            Assert.Equal("error", unhandledErrors[0]);
        }

        [Fact]
        public void HandleErrorAllGood()
        {
            var el = CreateNewListener;
            el.HandleError("error");
            var unhandledErrors = el.GetUnhandledErrors();
            Assert.Equal("error", unhandledErrors[0]);
        }

        [Fact]
        public void ClearAllErrorsAllGood()
        {
            var el = CreateNewListener;
            el.HandleError("error");
            el.ClearAllErrors();
            var unhandledErrors = el.GetUnhandledErrors();
            Assert.Empty(unhandledErrors);
        }

        [Fact]
        public void HandleErrorForWrongProperty()
        {
            var el = CreateNewListener;
            Assert.Throws<NotSupportedException>(() => el.HandleErrorFor(x => x.GetHashCode(), "d"));
        }
    }
}
