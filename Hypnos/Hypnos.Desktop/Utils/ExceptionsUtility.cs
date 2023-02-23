using System;
using Hypnos.Desktop.Forms;

namespace Hypnos.Desktop.Utils
{
    public static class ExceptionsUtility
    {
        public static bool IsTerminating { get; set; }

        private static ExceptionForm exceptionForm;

        private static ExceptionForm ExceptionForm => exceptionForm ?? (exceptionForm = new ExceptionForm());


        /// <param name="isTerminating">
        /// Pass the <c>True</c> if the <paramref name="exception"/> is not recorevable,
        /// e.g. you call this method form the AppDomain.CurrentDomain.UnhandledException handler.
        /// </param>
        public static void Handle(Exception exception, bool isTerminating = false)
        {
            var exceptionForm = new ExceptionForm();
            exceptionForm.Exception = exception;
            exceptionForm.ShowDialog(isTerminating);
        }
    }
}
